-- =====================================================================
--  HOSPITAL MANAGEMENT SYSTEM  -  upgrade_v2.sql
--  Upgrades an EXISTING hospital_management_db (made with the first schema.sql)
--  to version 2 WITHOUT deleting any data. Target: XAMPP MariaDB 10.4+.
--
--  HOW TO RUN (phpMyAdmin): select hospital_management_db -> Import -> this file -> Go.
--  Safe to run more than once.
--
--  A NEW installation does not need this file: just import schema.sql (it already
--  contains everything below).
--
--  What changes:
--   * doctors.consultation_fee: maximum 20,000.00
--   * audit_log: system-wide audit trail (who did / viewed what)
--   * dispensing: new payment method 'Charge to Bill' (the medicines go on the patient's bill)
--   * bills: Senior Citizen / PWD discount (20 %, VAT-exempt, ID number), Voided status
--            with reason / who / when, corrects_bill_id for the replacement bill
--   * bill_items: source_type / source_id link a charge to its consultation, dispensed medicine or lab order
--   * payments: refunds as reversing entries (payment_type 'Refund', refund_of_payment_id, remarks)
--   * views: v_bill_totals, v_bill_payment_summary (net of refunds, Voided), v_unbilled_charges (new)
--
--  NOTE: consultations, lab orders and 'Charge to Bill' medicines that already existed before this upgrade
--  are not linked to any bill yet, so they appear in Itemized Billing under "Unbilled charges".
--  Leave the ones that were already billed by hand unticked.
-- =====================================================================

USE hospital_management_db;
SET NAMES utf8mb4;

-- ---------------------------------------------------------------- doctors
UPDATE doctors SET consultation_fee = 20000 WHERE consultation_fee > 20000;
ALTER TABLE doctors DROP CONSTRAINT IF EXISTS chk_doctor_fee;
ALTER TABLE doctors ADD CONSTRAINT chk_doctor_fee CHECK (consultation_fee >= 0 AND consultation_fee <= 20000);

-- ---------------------------------------------------------------- audit trail
CREATE TABLE IF NOT EXISTS audit_log (
    audit_id    BIGINT       NOT NULL AUTO_INCREMENT,
    logged_at   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    user_id     INT          NULL,
    action      VARCHAR(30)  NOT NULL,
    entity_type VARCHAR(40)  NOT NULL,
    entity_id   VARCHAR(40)  NOT NULL,
    patient_id  VARCHAR(20)  NULL,
    details     VARCHAR(500) NULL,
    PRIMARY KEY (audit_id),
    KEY idx_auditlog_time (logged_at),
    KEY idx_auditlog_entity (entity_type, entity_id),
    KEY idx_auditlog_patient (patient_id, logged_at),
    CONSTRAINT fk_auditlog_user    FOREIGN KEY (user_id)    REFERENCES users (user_id),
    CONSTRAINT fk_auditlog_patient FOREIGN KEY (patient_id) REFERENCES patients (patient_id)
) ENGINE=InnoDB;

-- ---------------------------------------------------------------- dispensing
ALTER TABLE dispensing_records
    MODIFY payment_method ENUM('Cash','Debit','Credit','Online','Cheque','Charge to Bill') NOT NULL;

-- ---------------------------------------------------------------- bills
ALTER TABLE bills
    MODIFY status        ENUM('Draft','Finalized','Voided') NOT NULL DEFAULT 'Draft',
    MODIFY discount_type ENUM('Flat','Percent','Senior Citizen','PWD') NOT NULL DEFAULT 'Flat',
    ADD COLUMN IF NOT EXISTS discount_id_number VARCHAR(30)  NULL AFTER discount_value,
    ADD COLUMN IF NOT EXISTS voided_at          DATETIME     NULL AFTER finalized_at,
    ADD COLUMN IF NOT EXISTS voided_by_user_id  INT          NULL AFTER voided_at,
    ADD COLUMN IF NOT EXISTS void_reason        VARCHAR(250) NULL AFTER voided_by_user_id,
    ADD COLUMN IF NOT EXISTS corrects_bill_id   VARCHAR(20)  NULL AFTER void_reason;

ALTER TABLE bills DROP CONSTRAINT IF EXISTS chk_bill_senior;
ALTER TABLE bills ADD CONSTRAINT chk_bill_senior CHECK (discount_type NOT IN ('Senior Citizen','PWD')
    OR (discount_value = 20 AND tax_rate = 0 AND discount_id_number IS NOT NULL));
ALTER TABLE bills DROP CONSTRAINT IF EXISTS chk_bill_void;
ALTER TABLE bills ADD CONSTRAINT chk_bill_void CHECK (status <> 'Voided' OR (void_reason IS NOT NULL AND voided_at IS NOT NULL));
ALTER TABLE bills DROP FOREIGN KEY IF EXISTS fk_bill_voidedby;
ALTER TABLE bills ADD CONSTRAINT fk_bill_voidedby FOREIGN KEY (voided_by_user_id) REFERENCES users (user_id);
ALTER TABLE bills DROP FOREIGN KEY IF EXISTS fk_bill_corrects;
ALTER TABLE bills ADD CONSTRAINT fk_bill_corrects FOREIGN KEY (corrects_bill_id) REFERENCES bills (bill_id);

-- ---------------------------------------------------------------- bill items
ALTER TABLE bill_items
    ADD COLUMN IF NOT EXISTS source_type ENUM('Consultation','Dispensing','Lab Order') NULL AFTER amount,
    ADD COLUMN IF NOT EXISTS source_id   VARCHAR(20) NULL AFTER source_type,
    ADD KEY IF NOT EXISTS idx_billitem_source (source_type, source_id);
ALTER TABLE bill_items DROP CONSTRAINT IF EXISTS chk_billitem_source;
ALTER TABLE bill_items ADD CONSTRAINT chk_billitem_source CHECK ((source_type IS NULL) = (source_id IS NULL));

-- ---------------------------------------------------------------- payments
ALTER TABLE payments
    ADD COLUMN IF NOT EXISTS payment_type         ENUM('Payment','Refund') NOT NULL DEFAULT 'Payment' AFTER bill_id,
    ADD COLUMN IF NOT EXISTS refund_of_payment_id VARCHAR(20)  NULL AFTER tendered_amount,
    ADD COLUMN IF NOT EXISTS remarks              VARCHAR(250) NULL AFTER refund_of_payment_id,
    ADD KEY IF NOT EXISTS idx_payment_refund_of (refund_of_payment_id);
ALTER TABLE payments DROP CONSTRAINT IF EXISTS chk_payment_ref;
ALTER TABLE payments ADD CONSTRAINT chk_payment_ref CHECK (payment_type = 'Refund'
    OR payment_method NOT IN ('Debit','Credit','Online') OR reference_number IS NOT NULL);
ALTER TABLE payments DROP CONSTRAINT IF EXISTS chk_payment_refund;
ALTER TABLE payments ADD CONSTRAINT chk_payment_refund CHECK (payment_type = 'Payment'
    OR (refund_of_payment_id IS NOT NULL AND remarks IS NOT NULL AND tendered_amount IS NULL));
ALTER TABLE payments DROP FOREIGN KEY IF EXISTS fk_payment_refund_of;
ALTER TABLE payments ADD CONSTRAINT fk_payment_refund_of FOREIGN KEY (refund_of_payment_id) REFERENCES payments (payment_id);

-- ---------------------------------------------------------------- settings
INSERT IGNORE INTO app_settings (setting_key, setting_value, description) VALUES
 ('senior_pwd_discount_percent', '20',    'MP11: Senior Citizen / PWD discount (VAT-exempt)'),
 ('max_consultation_fee',        '20000', 'MP03: highest consultation fee a doctor can have');

-- ---------------------------------------------------------------- views
CREATE OR REPLACE VIEW v_bill_totals AS
SELECT  x.bill_id, x.patient_id, x.status, x.created_at,
        x.subtotal, x.discount_amount,
        (x.subtotal - x.discount_amount)                         AS taxable_amount,
        ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2)  AS tax_amount,
        (x.subtotal - x.discount_amount)
          + ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2) AS grand_total
FROM (
    SELECT  b.bill_id, b.patient_id, b.status, b.created_at, b.tax_rate,
            s.subtotal,
            ROUND(CASE WHEN b.discount_type IN ('Percent','Senior Citizen','PWD')
                       THEN s.subtotal * b.discount_value / 100
                       ELSE b.discount_value END, 2) AS discount_amount
    FROM    bills b
    JOIN    v_bill_subtotals s ON s.bill_id = b.bill_id
) x;

CREATE OR REPLACE VIEW v_bill_payment_summary AS
SELECT  x.bill_id, x.patient_id, x.status, x.grand_total, x.amount_paid, x.amount_refunded,
        CASE WHEN x.status = 'Voided' THEN 0 ELSE x.grand_total - x.amount_paid END AS balance,
        CASE WHEN x.status = 'Draft'                 THEN 'Draft'
             WHEN x.status = 'Voided'                THEN 'Voided'
             WHEN x.amount_paid <= 0                 THEN 'Outstanding'
             WHEN x.amount_paid >= x.grand_total     THEN 'Fully Paid'
             ELSE 'Partially Paid' END AS payment_status
FROM (
    SELECT  t.bill_id, t.patient_id, t.status, t.grand_total,
            COALESCE(SUM(CASE WHEN p.payment_type = 'Refund' THEN -p.amount ELSE p.amount END), 0) AS amount_paid,
            COALESCE(SUM(CASE WHEN p.payment_type = 'Refund' THEN p.amount ELSE 0 END), 0)         AS amount_refunded
    FROM    v_bill_totals t
    LEFT JOIN payments p ON p.bill_id = t.bill_id
    GROUP BY t.bill_id, t.patient_id, t.status, t.grand_total
) x;

CREATE OR REPLACE VIEW v_unbilled_charges AS
SELECT  'Consultation' AS source_type, c.consultation_id AS source_id, a.patient_id,
        'Consultation' AS bill_category,
        CONCAT('Consultation - Dr. ', d.first_name, ' ', d.last_name, ' (', d.specialization, ')') AS description,
        1 AS quantity, d.consultation_fee AS unit_price, c.recorded_at AS charge_date
FROM    consultations c
JOIN    appointments a ON a.appointment_id = c.appointment_id
JOIN    doctors d      ON d.doctor_id = a.doctor_id
WHERE   d.consultation_fee > 0
  AND   NOT EXISTS (SELECT 1 FROM bill_items bi JOIN bills b ON b.bill_id = bi.bill_id
                    WHERE b.status <> 'Voided' AND bi.source_type = 'Consultation' AND bi.source_id = c.consultation_id)
UNION ALL
SELECT  'Dispensing', CAST(di.dispensing_item_id AS CHAR CHARACTER SET utf8mb4) COLLATE utf8mb4_unicode_ci, a.patient_id,
        'Medicine',
        CONCAT(m.medicine_name, ' x', di.quantity_dispensed, ' (', dr.dispensing_id, ')'),
        di.quantity_dispensed, di.unit_price, dr.dispensed_at
FROM    dispensing_items di
JOIN    dispensing_records dr ON dr.dispensing_id = di.dispensing_id
JOIN    prescription_items pi ON pi.prescription_item_id = di.prescription_item_id
JOIN    medicines m           ON m.medicine_id = pi.medicine_id
JOIN    prescriptions r       ON r.prescription_id = dr.prescription_id
JOIN    consultations c       ON c.consultation_id = r.consultation_id
JOIN    appointments a        ON a.appointment_id = c.appointment_id
WHERE   dr.payment_method = 'Charge to Bill'
  AND   di.unit_price > 0
  AND   NOT EXISTS (SELECT 1 FROM bill_items bi JOIN bills b ON b.bill_id = bi.bill_id
                    WHERE b.status <> 'Voided' AND bi.source_type = 'Dispensing'
                      AND bi.source_id = CAST(di.dispensing_item_id AS CHAR CHARACTER SET utf8mb4) COLLATE utf8mb4_unicode_ci)
UNION ALL
SELECT  'Lab Order', o.order_id, o.patient_id,
        'Lab Test',
        CONCAT(t.test_name, ' (', o.order_id, ')'),
        1, t.price, o.ordered_at
FROM    lab_orders o
JOIN    lab_tests t ON t.test_id = o.test_id
WHERE   o.status <> 'Cancelled'
  AND   NOT EXISTS (SELECT 1 FROM bill_items bi JOIN bills b ON b.bill_id = bi.bill_id
                    WHERE b.status <> 'Voided' AND bi.source_type = 'Lab Order' AND bi.source_id = o.order_id);

-- =====================================================================
-- END OF upgrade_v2.sql
-- =====================================================================
