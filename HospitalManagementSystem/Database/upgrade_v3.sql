-- =====================================================================
--  HOSPITAL MANAGEMENT SYSTEM  -  upgrade_v3.sql
--  Upgrades an EXISTING hospital_management_db to version 3 WITHOUT deleting any data.
--  Run upgrade_v2.sql first if the database is older than version 2.
--  The system runs this automatically at start-up; to run it by hand (phpMyAdmin):
--  select hospital_management_db -> Import -> this file -> Go. Safe to run more than once.
--
--  What changes:
--   * patients: discount card (None / Senior Citizen / PWD + ID number) and coverage
--               (None / PhilHealth / HMO + member or card number + HMO provider)
--   * bills:    PhilHealth / HMO coverage (type, number, provider, covered amount)
--   * v_bill_totals: gross_total, coverage_amount; grand_total = what the patient pays
-- =====================================================================

USE hospital_management_db;
SET NAMES utf8mb4;

-- ---------------------------------------------------------------- patients
ALTER TABLE patients
    ADD COLUMN IF NOT EXISTS discount_card_type   ENUM('None','Senior Citizen','PWD') NOT NULL DEFAULT 'None',
    ADD COLUMN IF NOT EXISTS discount_card_number VARCHAR(30) NULL,
    ADD COLUMN IF NOT EXISTS coverage_type        ENUM('None','PhilHealth','HMO') NOT NULL DEFAULT 'None',
    ADD COLUMN IF NOT EXISTS coverage_number      VARCHAR(30) NULL,
    ADD COLUMN IF NOT EXISTS coverage_provider    VARCHAR(80) NULL;
ALTER TABLE patients DROP CONSTRAINT IF EXISTS chk_patient_card;
ALTER TABLE patients DROP CONSTRAINT IF EXISTS chk_patient_coverage;
ALTER TABLE patients DROP CONSTRAINT IF EXISTS chk_patient_hmo;
ALTER TABLE patients ADD CONSTRAINT chk_patient_card     CHECK (discount_card_type = 'None' OR discount_card_number IS NOT NULL);
ALTER TABLE patients ADD CONSTRAINT chk_patient_coverage CHECK (coverage_type = 'None' OR coverage_number IS NOT NULL);
ALTER TABLE patients ADD CONSTRAINT chk_patient_hmo      CHECK (coverage_type <> 'HMO' OR coverage_provider IS NOT NULL);

-- ---------------------------------------------------------------- bills
ALTER TABLE bills
    ADD COLUMN IF NOT EXISTS coverage_type     ENUM('None','PhilHealth','HMO') NOT NULL DEFAULT 'None' AFTER corrects_bill_id,
    ADD COLUMN IF NOT EXISTS coverage_number   VARCHAR(30)   NULL AFTER coverage_type,
    ADD COLUMN IF NOT EXISTS coverage_provider VARCHAR(80)   NULL AFTER coverage_number,
    ADD COLUMN IF NOT EXISTS coverage_amount   DECIMAL(10,2) NOT NULL DEFAULT 0 AFTER coverage_provider;
ALTER TABLE bills DROP CONSTRAINT IF EXISTS chk_bill_coverage;
ALTER TABLE bills ADD CONSTRAINT chk_bill_coverage CHECK (coverage_amount >= 0 AND
                                        (coverage_type <> 'None' OR coverage_amount = 0) AND
                                        (coverage_type = 'None' OR coverage_number IS NOT NULL) AND
                                        (coverage_type <> 'HMO' OR coverage_provider IS NOT NULL));

-- ---------------------------------------------------------------- views
CREATE OR REPLACE VIEW v_bill_totals AS
SELECT  y.bill_id, y.patient_id, y.status, y.created_at,
        y.subtotal, y.discount_amount, y.taxable_amount, y.tax_amount,
        y.gross_total, y.coverage_type, y.coverage_amount,
        (y.gross_total - y.coverage_amount)                    AS grand_total     -- what the patient pays
FROM (
    SELECT  x.bill_id, x.patient_id, x.status, x.created_at, x.coverage_type,
            LEAST(x.coverage_amount, (x.subtotal - x.discount_amount)
                  + ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2)) AS coverage_amount,
            x.subtotal, x.discount_amount,
            (x.subtotal - x.discount_amount)                         AS taxable_amount,
            ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2)  AS tax_amount,
            (x.subtotal - x.discount_amount)
              + ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2) AS gross_total
    FROM (
        SELECT  b.bill_id, b.patient_id, b.status, b.created_at, b.tax_rate,
                b.coverage_type, b.coverage_amount,
                s.subtotal,
                ROUND(CASE WHEN b.discount_type IN ('Percent','Senior Citizen','PWD')
                           THEN s.subtotal * b.discount_value / 100
                           ELSE b.discount_value END, 2) AS discount_amount
        FROM    bills b
        JOIN    v_bill_subtotals s ON s.bill_id = b.bill_id
    ) x
) y;

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

-- ---------------------------------------------------------------- settings
INSERT IGNORE INTO app_settings (setting_key, setting_value, description) VALUES
 ('schema_version', '3', 'Database structure version (set by upgrade scripts)');
UPDATE app_settings SET setting_value = '3' WHERE setting_key = 'schema_version';
