-- =====================================================================
--  HOSPITAL MANAGEMENT SYSTEM  -  schema.sql
--  Target : XAMPP MySQL / MariaDB (MariaDB 10.4+ or MySQL 8.0.16+)
--  Design : 3NF where it matters, InnoDB, utf8mb4
--  Covers : MP01 - MP12 + Login/Security + shared helpers
--
--  !!! WARNING !!!  This script DROPS and RE-CREATES the database
--  `hospital_management_db`. Running it again wipes all data.
--
--  HOW TO RUN (phpMyAdmin): Import tab -> choose this file -> Go.
--  Default login after import:
--      admin        / Admin@123     (Administrator)
--      all others   / Staff@123     (reception, drvillanueva, pharmacist,
--                                    labtech, cashier)
--  Password storage: PBKDF2-HMAC-SHA256, 210000 iterations, 16-byte salt,
--  32-byte hash, both Base64. (Matches Rfc2898DeriveBytes.Pbkdf2 in .NET.)
--
--  CONVENTIONS
--   * Simple pick-lists (gender, specialization, statuses, categories ...)
--     are stored as TEXT on the row itself - there are no lookup tables.
--       - Fixed sets the app's logic depends on use ENUM
--         (statuses, gender, blood type, role, payment method ...).
--       - Open-ended lists use VARCHAR so new values can be typed in
--         (specialization, department, medicine category, supplier ...).
--         Fill those drop-downs with SELECT DISTINCT <column> FROM <table>.
--   * Business codes ARE the primary key. patient_id holds
--     'PAT-YYYYMMDD-XXX', doctor_id holds 'DOC-XXX', and so on - there is
--     no separate numeric id + code pair. Child/detail rows that have no
--     business code (schedule lines, bill items, stock movements ...) keep
--     an INT AUTO_INCREMENT id.
--   * day_of_week uses the .NET DayOfWeek numbering: 0=Sun,1=Mon ... 6=Sat
--   * Derived values (age, quantity, totals, balances, stock on hand,
--     payment status) are NOT stored; they come from generated columns
--     or the v_* views near the bottom of this file.
--   * Codes are produced by the app via the `code_sequences` table
--     (GeneratorHelper) BEFORE the INSERT, since the code is the key.
--     Non-dated codes (DOC-XXX, MED-XXX, TST-XXX) use seq_date = 1970-01-01.
-- =====================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

DROP DATABASE IF EXISTS hospital_management_db;
CREATE DATABASE hospital_management_db
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;
USE hospital_management_db;

SET FOREIGN_KEY_CHECKS = 1;

-- =====================================================================
-- SECTION 1 : SECURITY, SETTINGS, CODE SEQUENCES
-- =====================================================================

CREATE TABLE users (
    user_id               INT          NOT NULL AUTO_INCREMENT,
    username              VARCHAR(50)  NOT NULL,
    password_hash         VARCHAR(256) NOT NULL,   -- Base64 PBKDF2 hash
    password_salt         VARCHAR(128) NOT NULL,   -- Base64 random salt
    hash_iterations       INT          NOT NULL DEFAULT 210000,
    full_name             VARCHAR(120) NOT NULL,
    email                 VARCHAR(120) NULL,
    role                  ENUM('Administrator','Receptionist','Doctor',
                               'Pharmacist','Lab Technician','Cashier') NOT NULL,
    is_active             TINYINT(1)   NOT NULL DEFAULT 1,
    failed_login_attempts INT          NOT NULL DEFAULT 0,
    locked_until          DATETIME     NULL,
    last_login_at         DATETIME     NULL,
    created_at            DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id),
    UNIQUE KEY uq_users_username (username)
) ENGINE=InnoDB;

CREATE TABLE login_history (
    login_id           INT         NOT NULL AUTO_INCREMENT,
    user_id            INT         NULL,            -- NULL when username unknown
    username_attempted VARCHAR(50) NOT NULL,
    was_successful     TINYINT(1)  NOT NULL,
    attempted_at       DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (login_id),
    KEY idx_login_user (user_id),
    CONSTRAINT fk_login_user FOREIGN KEY (user_id) REFERENCES users (user_id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE app_settings (
    setting_key   VARCHAR(60)  NOT NULL,
    setting_value VARCHAR(200) NOT NULL,
    description   VARCHAR(250) NULL,
    PRIMARY KEY (setting_key)
) ENGINE=InnoDB;

-- One row per (prefix, day). Non-dated codes use seq_date = 1970-01-01.
CREATE TABLE code_sequences (
    prefix      VARCHAR(10) NOT NULL,
    seq_date    DATE        NOT NULL,
    last_number INT         NOT NULL DEFAULT 0,
    PRIMARY KEY (prefix, seq_date)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 2 : MP01 / MP02  -  PATIENTS
-- =====================================================================

-- Emergency contact is one-per-patient, so it lives on the patient row.
CREATE TABLE patients (
    patient_id                     VARCHAR(20)  NOT NULL,   -- PAT-YYYYMMDD-XXX
    first_name                     VARCHAR(60)  NOT NULL,
    middle_name                    VARCHAR(60)  NULL,
    last_name                      VARCHAR(60)  NOT NULL,
    date_of_birth                  DATE         NOT NULL,   -- Age is computed, never stored
    gender                         ENUM('Male','Female','Other') NOT NULL,
    blood_type                     ENUM('A+','A-','B+','B-','AB+','AB-','O+','O-') NULL,
    phone_number                   VARCHAR(25)  NOT NULL,
    email                          VARCHAR(120) NULL,
    street_address                 VARCHAR(200) NULL,
    city                           VARCHAR(80)  NULL,
    province                       VARCHAR(80)  NULL,
    postal_code                    VARCHAR(10)  NULL,
    allergies                      TEXT         NULL,
    medical_history                TEXT         NULL,
    emergency_contact_name         VARCHAR(120) NULL,
    emergency_contact_phone        VARCHAR(25)  NULL,
    emergency_contact_relationship ENUM('Spouse','Parent','Child','Sibling','Relative',
                                        'Friend','Guardian','Other') NULL,
    registered_at                  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    registered_by_user_id          INT          NULL,
    last_updated_at                DATETIME     NULL,
    PRIMARY KEY (patient_id),
    KEY idx_patients_name (last_name, first_name),
    KEY idx_patients_phone (phone_number),
    KEY idx_patients_dob (date_of_birth),
    CONSTRAINT fk_patients_user FOREIGN KEY (registered_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

CREATE TABLE patient_audit_log (
    audit_id           INT          NOT NULL AUTO_INCREMENT,
    patient_id         VARCHAR(20)  NOT NULL,
    field_changed      VARCHAR(60)  NOT NULL,
    old_value          TEXT         NULL,
    new_value          TEXT         NULL,
    changed_by_user_id INT          NOT NULL,
    changed_at         DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (audit_id),
    KEY idx_audit_patient (patient_id, changed_at),
    CONSTRAINT fk_audit_patient FOREIGN KEY (patient_id)         REFERENCES patients (patient_id),
    CONSTRAINT fk_audit_user    FOREIGN KEY (changed_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 3 : MP03  -  DOCTORS, SCHEDULES, LEAVES
-- =====================================================================

CREATE TABLE doctors (
    doctor_id        VARCHAR(10)   NOT NULL,             -- DOC-XXX
    first_name       VARCHAR(60)   NOT NULL,
    last_name        VARCHAR(60)   NOT NULL,
    license_number   VARCHAR(30)   NOT NULL,
    specialization   VARCHAR(60)   NOT NULL,
    department       VARCHAR(60)   NOT NULL,
    phone            VARCHAR(25)   NOT NULL,
    email            VARCHAR(120)  NULL,
    consultation_fee DECIMAL(10,2) NOT NULL,
    employment_type  ENUM('Full-time','Part-time','Visiting') NOT NULL,
    created_at       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (doctor_id),
    UNIQUE KEY uq_doctors_license (license_number),
    KEY idx_doctors_specialization (specialization),
    CONSTRAINT chk_doctor_fee CHECK (consultation_fee >= 0)
) ENGINE=InnoDB;

-- Deleting a doctor removes schedules and leaves (MP03 rule).
CREATE TABLE doctor_schedules (
    schedule_id          INT          NOT NULL AUTO_INCREMENT,
    doctor_id            VARCHAR(10)  NOT NULL,
    day_of_week          TINYINT      NOT NULL,              -- 0=Sun ... 6=Sat
    start_time           TIME         NOT NULL,
    end_time             TIME         NOT NULL,
    consultation_minutes INT          NOT NULL DEFAULT 15,
    notes                VARCHAR(200) NULL,
    PRIMARY KEY (schedule_id),
    KEY idx_schedule_doctor_day (doctor_id, day_of_week),
    CONSTRAINT chk_sched_day     CHECK (day_of_week BETWEEN 0 AND 6),
    CONSTRAINT chk_sched_times   CHECK (end_time > start_time),
    CONSTRAINT chk_sched_minutes CHECK (consultation_minutes >= 10),
    CONSTRAINT fk_sched_doctor FOREIGN KEY (doctor_id) REFERENCES doctors (doctor_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE doctor_leaves (
    leave_id   INT          NOT NULL AUTO_INCREMENT,
    doctor_id  VARCHAR(10)  NOT NULL,
    leave_date DATE         NOT NULL,
    reason     VARCHAR(200) NOT NULL,
    PRIMARY KEY (leave_id),
    UNIQUE KEY uq_leave_doctor_date (doctor_id, leave_date),
    CONSTRAINT fk_leave_doctor FOREIGN KEY (doctor_id) REFERENCES doctors (doctor_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 4 : MP04  -  APPOINTMENTS
-- =====================================================================

-- active_slot_flag is NULL for Cancelled rows, so a cancelled/rescheduled slot
-- is free again while the unique keys still stop real double-bookings.
CREATE TABLE appointments (
    appointment_id     VARCHAR(20)  NOT NULL,              -- APT-YYYYMMDD-XXX
    token_number       INT          NOT NULL,              -- per doctor / per date
    patient_id         VARCHAR(20)  NOT NULL,
    doctor_id          VARCHAR(10)  NOT NULL,
    appointment_date   DATE         NOT NULL,
    time_slot          TIME         NOT NULL,
    appointment_type   ENUM('New Consultation','Follow-up') NOT NULL,
    reason_for_visit   VARCHAR(250) NULL,
    status             ENUM('Booked','Completed','Cancelled','No-show') NOT NULL DEFAULT 'Booked',
    status_remarks     VARCHAR(250) NULL,                  -- cancel reason, etc.
    status_updated_at  DATETIME     NULL,
    created_at         DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by_user_id INT          NULL,
    active_slot_flag   TINYINT GENERATED ALWAYS AS (CASE WHEN status = 'Cancelled' THEN NULL ELSE 1 END) STORED,
    PRIMARY KEY (appointment_id),
    UNIQUE KEY uq_appt_token (doctor_id, appointment_date, token_number),
    UNIQUE KEY uq_appt_doctor_slot (doctor_id, appointment_date, time_slot, active_slot_flag),
    UNIQUE KEY uq_appt_patient_doctor_day (patient_id, doctor_id, appointment_date, active_slot_flag),
    KEY idx_appt_date (appointment_date),
    CONSTRAINT fk_appt_patient FOREIGN KEY (patient_id)         REFERENCES patients (patient_id),
    CONSTRAINT fk_appt_doctor  FOREIGN KEY (doctor_id)          REFERENCES doctors (doctor_id),
    CONSTRAINT fk_appt_user    FOREIGN KEY (created_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 5 : MP05  -  QUEUE BOARD
-- =====================================================================

-- appointment_id is NULL for walk-in / emergency patients.
-- Actual consultation minutes = completed_at - started_at (not stored).
CREATE TABLE queue_entries (
    queue_id         INT         NOT NULL AUTO_INCREMENT,
    queue_date       DATE        NOT NULL,
    token_number     INT         NOT NULL,
    patient_id       VARCHAR(20) NOT NULL,
    doctor_id        VARCHAR(10) NOT NULL,
    appointment_id   VARCHAR(20) NULL,
    queue_status     ENUM('Waiting','In Progress','Completed','Skipped') NOT NULL DEFAULT 'Waiting',
    is_emergency     TINYINT(1)  NOT NULL DEFAULT 0,
    checked_in_at    DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    started_at       DATETIME    NULL,
    completed_at     DATETIME    NULL,
    in_progress_flag TINYINT GENERATED ALWAYS AS (CASE WHEN queue_status = 'In Progress' THEN 1 ELSE NULL END) STORED,
    PRIMARY KEY (queue_id),
    UNIQUE KEY uq_queue_token (doctor_id, queue_date, token_number),
    UNIQUE KEY uq_queue_appointment (appointment_id),
    UNIQUE KEY uq_queue_one_in_progress (doctor_id, queue_date, in_progress_flag),  -- only one In Progress per doctor
    KEY idx_queue_date_status (queue_date, queue_status),
    CONSTRAINT fk_queue_patient FOREIGN KEY (patient_id)     REFERENCES patients (patient_id),
    CONSTRAINT fk_queue_doctor  FOREIGN KEY (doctor_id)      REFERENCES doctors (doctor_id),
    CONSTRAINT fk_queue_appt    FOREIGN KEY (appointment_id) REFERENCES appointments (appointment_id)
) ENGINE=InnoDB;

-- Each skip is logged with its reason. Skipped patients return to Waiting and
-- are ordered after non-skipped ones (number of skips = COUNT of this table).
CREATE TABLE queue_skip_log (
    skip_id            INT          NOT NULL AUTO_INCREMENT,
    queue_id           INT          NOT NULL,
    reason             VARCHAR(250) NOT NULL,
    skipped_at         DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    skipped_by_user_id INT          NULL,
    PRIMARY KEY (skip_id),
    KEY idx_skip_queue (queue_id),
    CONSTRAINT fk_skip_queue FOREIGN KEY (queue_id)           REFERENCES queue_entries (queue_id) ON DELETE CASCADE,
    CONSTRAINT fk_skip_user  FOREIGN KEY (skipped_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 6 : MP06  -  CONSULTATIONS & VITAL SIGNS
-- =====================================================================

CREATE TABLE consultations (
    consultation_id      VARCHAR(20)   NOT NULL,            -- CON-YYYYMMDD-XXX
    appointment_id       VARCHAR(20)   NOT NULL,            -- UNIQUE: one consultation per appointment
    systolic_bp          INT           NOT NULL,
    diastolic_bp         INT           NOT NULL,
    temperature_c        DECIMAL(4,1)  NOT NULL,
    pulse_rate_bpm       INT           NOT NULL,
    weight_kg            DECIMAL(5,1)  NOT NULL,
    height_cm            DECIMAL(5,1)  NULL,
    chief_complaint      TEXT          NOT NULL,
    examination_findings TEXT          NULL,
    diagnosis            TEXT          NOT NULL,
    treatment_plan       TEXT          NULL,
    special_instructions TEXT          NULL,
    next_follow_up_date  DATE          NULL,
    recorded_at          DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    recorded_by_user_id  INT           NULL,
    PRIMARY KEY (consultation_id),
    UNIQUE KEY uq_consult_appointment (appointment_id),
    CONSTRAINT chk_vital_sys   CHECK (systolic_bp BETWEEN 50 AND 250),
    CONSTRAINT chk_vital_dia   CHECK (diastolic_bp BETWEEN 30 AND 150),
    CONSTRAINT chk_vital_temp  CHECK (temperature_c BETWEEN 30 AND 45),
    CONSTRAINT chk_vital_pulse CHECK (pulse_rate_bpm BETWEEN 20 AND 250),
    CONSTRAINT chk_vital_wt    CHECK (weight_kg > 0),
    CONSTRAINT fk_consult_appt FOREIGN KEY (appointment_id)      REFERENCES appointments (appointment_id),
    CONSTRAINT fk_consult_user FOREIGN KEY (recorded_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 7 : MP08  -  MEDICINE CATALOG & STOCK
-- =====================================================================

CREATE TABLE medicines (
    medicine_id          VARCHAR(10)   NOT NULL,            -- MED-XXX
    medicine_name        VARCHAR(120)  NOT NULL,
    generic_name         VARCHAR(120)  NULL,
    category             VARCHAR(60)   NOT NULL,            -- Analgesic, Antibiotic ...
    manufacturer         VARCHAR(100)  NULL,
    unit_type            VARCHAR(30)   NOT NULL,            -- Tablet, Capsule, Syrup ...
    unit_price           DECIMAL(10,2) NOT NULL,
    reorder_level        INT           NOT NULL DEFAULT 0,
    storage_instructions VARCHAR(250)  NULL,
    PRIMARY KEY (medicine_id),
    UNIQUE KEY uq_medicine_name (medicine_name),
    CONSTRAINT chk_med_price   CHECK (unit_price > 0),
    CONSTRAINT chk_med_reorder CHECK (reorder_level >= 0)
) ENGINE=InnoDB;

-- Stock on hand is NEVER stored. Remaining = quantity_received + SUM(stock_movements).
CREATE TABLE stock_batches (
    batch_id            INT           NOT NULL AUTO_INCREMENT,
    medicine_id         VARCHAR(10)   NOT NULL,
    batch_number        VARCHAR(50)   NOT NULL,
    quantity_received   INT           NOT NULL,
    expiry_date         DATE          NOT NULL,
    supplier            VARCHAR(100)  NOT NULL,
    purchase_price      DECIMAL(10,2) NOT NULL,
    date_received       DATE          NOT NULL,
    received_by_user_id INT           NULL,
    PRIMARY KEY (batch_id),
    UNIQUE KEY uq_batch_medicine_number (medicine_id, batch_number),
    KEY idx_batch_expiry (expiry_date),
    CONSTRAINT chk_batch_qty   CHECK (quantity_received > 0),
    CONSTRAINT chk_batch_price CHECK (purchase_price > 0),
    CONSTRAINT fk_batch_medicine FOREIGN KEY (medicine_id)         REFERENCES medicines (medicine_id),
    CONSTRAINT fk_batch_user     FOREIGN KEY (received_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 8 : MP07  -  PRESCRIPTIONS
-- =====================================================================

CREATE TABLE prescriptions (
    prescription_id      VARCHAR(20)  NOT NULL,             -- PRE-YYYYMMDD-XXX
    consultation_id      VARCHAR(20)  NOT NULL,
    general_instructions TEXT         NULL,
    issued_date          DATE         NOT NULL,
    valid_until          DATE         NOT NULL,             -- default issued + 30 days (app_settings)
    created_by_user_id   INT          NULL,
    created_at           DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (prescription_id),
    KEY idx_prescription_consult (consultation_id),
    CONSTRAINT chk_rx_validity CHECK (valid_until >= issued_date),
    CONSTRAINT fk_rx_consult FOREIGN KEY (consultation_id)    REFERENCES consultations (consultation_id),
    CONSTRAINT fk_rx_user    FOREIGN KEY (created_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

CREATE TABLE prescription_items (
    prescription_item_id INT          NOT NULL AUTO_INCREMENT,
    prescription_id      VARCHAR(20)  NOT NULL,
    medicine_id          VARCHAR(10)  NOT NULL,
    dosage               VARCHAR(50)  NOT NULL,             -- e.g. 500mg
    frequency_per_day    INT          NOT NULL,
    duration_days        INT          NOT NULL,
    instructions         VARCHAR(250) NULL,
    quantity             INT GENERATED ALWAYS AS (frequency_per_day * duration_days) STORED,
    PRIMARY KEY (prescription_item_id),
    KEY idx_rxitem_prescription (prescription_id),
    CONSTRAINT chk_rxitem_freq CHECK (frequency_per_day > 0),
    CONSTRAINT chk_rxitem_dur  CHECK (duration_days > 0),
    CONSTRAINT fk_rxitem_rx  FOREIGN KEY (prescription_id) REFERENCES prescriptions (prescription_id) ON DELETE CASCADE,
    CONSTRAINT fk_rxitem_med FOREIGN KEY (medicine_id)     REFERENCES medicines (medicine_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 9 : MP09  -  DISPENSING
-- =====================================================================

-- Patient is derived: prescription -> consultation -> appointment -> patient.
-- Total amount is derived: SUM(dispensing_items.line_total).
-- Partial dispensing = several dispensing_records for one prescription.
CREATE TABLE dispensing_records (
    dispensing_id        VARCHAR(20) NOT NULL,              -- MS-YYYYMMDD-XXX
    prescription_id      VARCHAR(20) NOT NULL,
    payment_method       ENUM('Cash','Debit','Credit','Online','Cheque') NOT NULL,
    dispensed_by_user_id INT         NULL,
    dispensed_at         DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (dispensing_id),
    KEY idx_dispensing_rx (prescription_id),
    CONSTRAINT fk_disp_rx   FOREIGN KEY (prescription_id)      REFERENCES prescriptions (prescription_id),
    CONSTRAINT fk_disp_user FOREIGN KEY (dispensed_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- unit_price is a price SNAPSHOT at the time of sale (history, not a derived value).
CREATE TABLE dispensing_items (
    dispensing_item_id   INT           NOT NULL AUTO_INCREMENT,
    dispensing_id        VARCHAR(20)   NOT NULL,
    prescription_item_id INT           NOT NULL,
    quantity_dispensed   INT           NOT NULL,
    unit_price           DECIMAL(10,2) NOT NULL,
    line_total           DECIMAL(12,2) GENERATED ALWAYS AS (quantity_dispensed * unit_price) STORED,
    PRIMARY KEY (dispensing_item_id),
    KEY idx_dispitem_rxitem (prescription_item_id),
    CONSTRAINT chk_dispitem_qty   CHECK (quantity_dispensed > 0),
    CONSTRAINT chk_dispitem_price CHECK (unit_price >= 0),
    CONSTRAINT fk_dispitem_disp   FOREIGN KEY (dispensing_id)        REFERENCES dispensing_records (dispensing_id) ON DELETE CASCADE,
    CONSTRAINT fk_dispitem_rxitem FOREIGN KEY (prescription_item_id) REFERENCES prescription_items (prescription_item_id)
) ENGINE=InnoDB;

-- Ledger of every change to a batch after it was received.
-- ADJUSTMENT (+/-, reason required), DISPENSE (negative, linked to a dispensing item).
-- FIFO: dispensing consumes the batch with the oldest expiry_date first (one movement per batch used).
CREATE TABLE stock_movements (
    movement_id        INT          NOT NULL AUTO_INCREMENT,
    batch_id           INT          NOT NULL,
    movement_type      ENUM('ADJUSTMENT','DISPENSE') NOT NULL,
    quantity_change    INT          NOT NULL,
    reason             VARCHAR(250) NULL,
    dispensing_item_id INT          NULL,
    moved_at           DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    moved_by_user_id   INT          NULL,
    PRIMARY KEY (movement_id),
    KEY idx_move_batch (batch_id),
    CONSTRAINT chk_move_nonzero      CHECK (quantity_change <> 0),
    CONSTRAINT chk_move_reason       CHECK (movement_type <> 'ADJUSTMENT' OR reason IS NOT NULL),
    CONSTRAINT chk_move_dispense_neg CHECK (movement_type <> 'DISPENSE' OR quantity_change < 0),
    CONSTRAINT fk_move_batch    FOREIGN KEY (batch_id)           REFERENCES stock_batches (batch_id),
    CONSTRAINT fk_move_dispitem FOREIGN KEY (dispensing_item_id) REFERENCES dispensing_items (dispensing_item_id),
    CONSTRAINT fk_move_user     FOREIGN KEY (moved_by_user_id)   REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 10 : MP10  -  LABORATORY
-- =====================================================================

-- Normal range columns are strings so descriptive ranges (e.g. 'Negative') work too.
CREATE TABLE lab_tests (
    test_id               VARCHAR(10)   NOT NULL,           -- TST-XXX
    test_name             VARCHAR(120)  NOT NULL,
    category              VARCHAR(60)   NOT NULL,           -- Hematology, Serology ...
    sample_type           VARCHAR(60)   NOT NULL,           -- Serum, Urine ...
    price                 DECIMAL(10,2) NOT NULL,
    normal_range_low      VARCHAR(40)   NULL,
    normal_range_high     VARCHAR(40)   NULL,
    unit                  VARCHAR(30)   NULL,
    turnaround_hours      INT           NOT NULL DEFAULT 24,
    requires_verification TINYINT(1)    NOT NULL DEFAULT 0,
    PRIMARY KEY (test_id),
    UNIQUE KEY uq_test_name (test_name),
    CONSTRAINT chk_test_price CHECK (price > 0),
    CONSTRAINT chk_test_tat   CHECK (turnaround_hours > 0)
) ENGINE=InnoDB;

CREATE TABLE lab_orders (
    order_id             VARCHAR(20) NOT NULL,              -- LO-YYYYMMDD-XXX
    patient_id           VARCHAR(20) NOT NULL,
    test_id              VARCHAR(10) NOT NULL,
    consultation_id      VARCHAR(20) NULL,
    ordered_by_doctor_id VARCHAR(10) NULL,
    urgency              ENUM('Routine','Urgent','STAT') NOT NULL DEFAULT 'Routine',
    status               ENUM('Pending','Completed','Cancelled') NOT NULL DEFAULT 'Pending',
    ordered_at           DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (order_id),
    KEY idx_lab_order_status (status),
    CONSTRAINT fk_laborder_patient FOREIGN KEY (patient_id)           REFERENCES patients (patient_id),
    CONSTRAINT fk_laborder_test    FOREIGN KEY (test_id)              REFERENCES lab_tests (test_id),
    CONSTRAINT fk_laborder_consult FOREIGN KEY (consultation_id)      REFERENCES consultations (consultation_id),
    CONSTRAINT fk_laborder_doctor  FOREIGN KEY (ordered_by_doctor_id) REFERENCES doctors (doctor_id)
) ENGINE=InnoDB;

-- is_abnormal is a SNAPSHOT of the comparison at entry time (catalog ranges may change later).
CREATE TABLE lab_results (
    result_id           VARCHAR(20)  NOT NULL,              -- LR-YYYYMMDD-XXX
    order_id            VARCHAR(20)  NOT NULL,              -- one result per order
    result_value        VARCHAR(100) NOT NULL,
    is_abnormal         TINYINT(1)   NOT NULL DEFAULT 0,
    findings            TEXT         NULL,
    tested_by_user_id   INT          NOT NULL,
    verified_by_user_id INT          NULL,
    test_datetime       DATETIME     NOT NULL,
    created_at          DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (result_id),
    UNIQUE KEY uq_result_order (order_id),
    CONSTRAINT fk_result_order    FOREIGN KEY (order_id)            REFERENCES lab_orders (order_id),
    CONSTRAINT fk_result_tested   FOREIGN KEY (tested_by_user_id)   REFERENCES users (user_id),
    CONSTRAINT fk_result_verified FOREIGN KEY (verified_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 11 : MP11 / MP12  -  BILLING & PAYMENTS
-- =====================================================================

-- Room types stay a table because each one carries its own daily rate.
CREATE TABLE room_types (
    room_type  VARCHAR(50)   NOT NULL,
    daily_rate DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (room_type),
    CONSTRAINT chk_room_rate CHECK (daily_rate > 0)
) ENGINE=InnoDB;

-- Price list for Consultation / Procedure / Miscellaneous charges.
-- Medicine and Lab prices come from `medicines` and `lab_tests`; Room rates from `room_types`.
CREATE TABLE service_catalog (
    service_name  VARCHAR(120)  NOT NULL,
    bill_category ENUM('Consultation','Medicine','Lab Test','Room','Procedure','Miscellaneous') NOT NULL,
    unit_price    DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (service_name),
    CONSTRAINT chk_service_price CHECK (unit_price > 0)
) ENGINE=InnoDB;

-- bill_id is assigned when the bill is created (Draft bills have one too).
-- discount_value is a flat amount ('Flat') or a percentage ('Percent').
CREATE TABLE bills (
    bill_id            VARCHAR(20)   NOT NULL,              -- BILL-YYYYMMDD-XXX
    patient_id         VARCHAR(20)   NOT NULL,
    status             ENUM('Draft','Finalized') NOT NULL DEFAULT 'Draft',
    discount_type      ENUM('Flat','Percent')    NOT NULL DEFAULT 'Flat',
    discount_value     DECIMAL(10,2) NOT NULL DEFAULT 0,
    tax_rate           DECIMAL(5,4)  NOT NULL DEFAULT 0.1200,
    authorized_by      VARCHAR(100)  NULL,                  -- required above discount threshold
    created_at         DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by_user_id INT           NULL,
    finalized_at       DATETIME      NULL,
    PRIMARY KEY (bill_id),
    KEY idx_bill_patient (patient_id),
    CONSTRAINT chk_bill_discount CHECK (discount_value >= 0),
    CONSTRAINT chk_bill_tax      CHECK (tax_rate >= 0),
    CONSTRAINT fk_bill_patient FOREIGN KEY (patient_id)         REFERENCES patients (patient_id),
    CONSTRAINT fk_bill_user    FOREIGN KEY (created_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- Room charge = quantity (days) x unit_price (rate). Other categories use quantity 1 or units.
CREATE TABLE bill_items (
    bill_item_id  INT           NOT NULL AUTO_INCREMENT,
    bill_id       VARCHAR(20)   NOT NULL,
    bill_category ENUM('Consultation','Medicine','Lab Test','Room','Procedure','Miscellaneous') NOT NULL,
    description   VARCHAR(200)  NOT NULL,
    quantity      INT           NOT NULL DEFAULT 1,
    unit_price    DECIMAL(10,2) NOT NULL,
    amount        DECIMAL(12,2) GENERATED ALWAYS AS (ROUND(quantity * unit_price, 2)) STORED,
    PRIMARY KEY (bill_item_id),
    KEY idx_billitem_bill (bill_id),
    CONSTRAINT chk_billitem_qty   CHECK (quantity > 0),
    CONSTRAINT chk_billitem_price CHECK (unit_price > 0),
    CONSTRAINT fk_billitem_bill FOREIGN KEY (bill_id) REFERENCES bills (bill_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- Payment status (Outstanding / Partially Paid / Fully Paid) is derived: see v_bill_payment_summary.
CREATE TABLE payments (
    payment_id          VARCHAR(20)   NOT NULL,             -- PAY-YYYYMMDD-XXX
    bill_id             VARCHAR(20)   NOT NULL,
    amount              DECIMAL(12,2) NOT NULL,
    payment_method      ENUM('Cash','Debit','Credit','Online','Cheque') NOT NULL,
    reference_number    VARCHAR(60)   NULL,                 -- required for Debit/Credit/Online
    tendered_amount     DECIMAL(12,2) NULL,                 -- cash only; change = tendered - amount
    received_by_user_id INT           NOT NULL,
    payment_datetime    DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (payment_id),
    KEY idx_payment_bill (bill_id),
    CONSTRAINT chk_payment_amount CHECK (amount > 0),
    CONSTRAINT chk_payment_ref    CHECK (payment_method NOT IN ('Debit','Credit','Online')
                                         OR reference_number IS NOT NULL),
    CONSTRAINT fk_payment_bill FOREIGN KEY (bill_id)             REFERENCES bills (bill_id),
    CONSTRAINT fk_payment_user FOREIGN KEY (received_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 12 : VIEWS (derived data - nothing here is stored twice)
-- =====================================================================

-- Remaining quantity per batch
CREATE VIEW v_batch_stock AS
SELECT  b.batch_id, b.medicine_id, b.batch_number, b.expiry_date,
        b.supplier, b.date_received, b.quantity_received,
        b.quantity_received + COALESCE(SUM(m.quantity_change), 0) AS quantity_remaining
FROM    stock_batches b
LEFT JOIN stock_movements m ON m.batch_id = b.batch_id
GROUP BY b.batch_id, b.medicine_id, b.batch_number, b.expiry_date,
         b.supplier, b.date_received, b.quantity_received;

-- Stock summary + alert flags per medicine (threshold from app_settings)
CREATE VIEW v_medicine_stock AS
SELECT  md.medicine_id, md.medicine_name, md.reorder_level,
        COALESCE(SUM(bs.quantity_remaining), 0) AS total_stock,
        COALESCE(SUM(CASE WHEN bs.expiry_date >= CURDATE() THEN bs.quantity_remaining ELSE 0 END), 0) AS usable_stock,
        MIN(CASE WHEN bs.quantity_remaining > 0 THEN bs.expiry_date END) AS nearest_expiry,
        CASE WHEN COALESCE(SUM(bs.quantity_remaining), 0) <= md.reorder_level THEN 1 ELSE 0 END AS is_low_stock,
        MAX(CASE WHEN bs.quantity_remaining > 0 AND bs.expiry_date < CURDATE() THEN 1 ELSE 0 END) AS has_expired,
        MAX(CASE WHEN bs.quantity_remaining > 0 AND bs.expiry_date >= CURDATE()
                  AND DATEDIFF(bs.expiry_date, CURDATE()) <=
                      (SELECT CAST(setting_value AS SIGNED) FROM app_settings WHERE setting_key = 'expiring_soon_days')
                 THEN 1 ELSE 0 END) AS has_expiring_soon
FROM    medicines md
LEFT JOIN v_batch_stock bs ON bs.medicine_id = md.medicine_id
GROUP BY md.medicine_id, md.medicine_name, md.reorder_level;

-- Single display status (priority: Expired > Expiring Soon > Low Stock > Normal)
CREATE VIEW v_medicine_stock_status AS
SELECT  s.*,
        CASE WHEN s.has_expired = 1       THEN 'Expired'
             WHEN s.has_expiring_soon = 1 THEN 'Expiring Soon'
             WHEN s.is_low_stock = 1      THEN 'Low Stock'
             ELSE 'Normal' END AS stock_status
FROM v_medicine_stock s;

-- Prescribed vs dispensed vs remaining per prescription line
CREATE VIEW v_prescription_item_balance AS
SELECT  pi.prescription_item_id, pi.prescription_id, pi.medicine_id,
        pi.quantity AS quantity_prescribed,
        COALESCE(SUM(di.quantity_dispensed), 0) AS quantity_dispensed,
        pi.quantity - COALESCE(SUM(di.quantity_dispensed), 0) AS quantity_remaining
FROM    prescription_items pi
LEFT JOIN dispensing_items di ON di.prescription_item_id = pi.prescription_item_id
GROUP BY pi.prescription_item_id, pi.prescription_id, pi.medicine_id, pi.quantity;

-- Dispensing slip total
CREATE VIEW v_dispensing_totals AS
SELECT  dr.dispensing_id,
        COALESCE(SUM(di.line_total), 0) AS total_amount
FROM    dispensing_records dr
LEFT JOIN dispensing_items di ON di.dispensing_id = dr.dispensing_id
GROUP BY dr.dispensing_id;

CREATE VIEW v_bill_subtotals AS
SELECT  b.bill_id, COALESCE(SUM(bi.amount), 0) AS subtotal
FROM    bills b
LEFT JOIN bill_items bi ON bi.bill_id = b.bill_id
GROUP BY b.bill_id;

-- Order of calculation: Subtotal -> Discount -> Tax on post-discount -> Grand Total
CREATE VIEW v_bill_totals AS
SELECT  x.bill_id, x.patient_id, x.status, x.created_at,
        x.subtotal, x.discount_amount,
        (x.subtotal - x.discount_amount)                         AS taxable_amount,
        ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2)  AS tax_amount,
        (x.subtotal - x.discount_amount)
          + ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2) AS grand_total
FROM (
    SELECT  b.bill_id, b.patient_id, b.status, b.created_at, b.tax_rate,
            s.subtotal,
            ROUND(CASE WHEN b.discount_type = 'Percent'
                       THEN s.subtotal * b.discount_value / 100
                       ELSE b.discount_value END, 2) AS discount_amount
    FROM    bills b
    JOIN    v_bill_subtotals s ON s.bill_id = b.bill_id
) x;

-- Paid / balance / status per bill
CREATE VIEW v_bill_payment_summary AS
SELECT  t.bill_id, t.patient_id, t.status, t.grand_total,
        COALESCE(SUM(p.amount), 0) AS amount_paid,
        t.grand_total - COALESCE(SUM(p.amount), 0) AS balance,
        CASE WHEN t.status = 'Draft'                         THEN 'Draft'
             WHEN COALESCE(SUM(p.amount), 0) = 0              THEN 'Outstanding'
             WHEN COALESCE(SUM(p.amount), 0) >= t.grand_total THEN 'Fully Paid'
             ELSE 'Partially Paid' END AS payment_status
FROM    v_bill_totals t
LEFT JOIN payments p ON p.bill_id = t.bill_id
GROUP BY t.bill_id, t.patient_id, t.status, t.grand_total;

-- =====================================================================
-- SECTION 13 : SEED DATA  -  users, settings, price lists
-- =====================================================================

SET @d   = DATE_FORMAT(CURDATE(), '%Y%m%d');
SET @d40 = DATE_FORMAT(DATE_SUB(CURDATE(), INTERVAL 40 DAY), '%Y%m%d');

-- admin / Admin@123 ; every other user / Staff@123
INSERT INTO users (user_id, username, password_hash, password_salt, hash_iterations, full_name, email, role) VALUES
 (1, 'admin',        'rxTPG8MMJ0fMnvWp/anKrolWOIKxABgl6aPhON+Ggik=', 'NQAcdnvN5seAKh/1K7WCiQ==', 210000, 'System Administrator', 'admin@hospital.local',       'Administrator'),
 (2, 'reception',    'kRjNnh2WX3LncVVB51RhB5EvqEN4gpkDWWK6G0aumSs=', 'm0HYUcL86xS8mJIq8JuiHw==', 210000, 'Rosa Dela Pena',       'reception@hospital.local',   'Receptionist'),
 (3, 'drvillanueva', 'Mh69TFZQTMlKs27n7C1U+VglS2wpra6LnXpU0N/MG0E=', '7AqjpMr5fQv14231hKUdHQ==', 210000, 'Dr. Ramon Villanueva', 'rvillanueva@hospital.local', 'Doctor'),
 (4, 'pharmacist',   'GmyU1+qPoBFKB6vEyPj71nzgqIwmLT6a3m4A3UY8Km8=', '7U2ptyzzN2SWxp7jpsznUw==', 210000, 'Paolo Fernandez',      'pharmacy@hospital.local',    'Pharmacist'),
 (5, 'labtech',      'qyUB6LpORtab9rG+lgulcb4kmIXKySg6rI0WtJRZff4=', 'lBk0O0Ee1xtl92gDoyodzA==', 210000, 'Lea Navarro',          'lab@hospital.local',         'Lab Technician'),
 (6, 'cashier',      'ROZUR4Nn0WEPSAtU/RTaNUb897ANpukbFoJSAMgon7A=', 'KsjjZaQUz9kHDk1ZtpYHXg==', 210000, 'Mario Bautista',       'cashier@hospital.local',     'Cashier');

INSERT INTO app_settings (setting_key, setting_value, description) VALUES
 ('hospital_name',               'Hospital Management System', 'Shown on login, dashboard and printouts'),
 ('currency_symbol',             '₱',    'Currency prefix for amounts'),
 ('cancellation_window_hours',   '2',    'MP04: cannot cancel an appointment starting within N hours'),
 ('default_consultation_minutes','15',   'MP05: default average consultation time before data exists'),
 ('expiring_soon_days',          '60',   'MP08: batch counts as Expiring Soon within N days of expiry'),
 ('prescription_validity_days',  '30',   'MP07: default Valid Until = issued date + N days'),
 ('min_consultation_minutes',    '10',   'MP03: minimum schedule slot length'),
 ('max_daily_working_hours',     '12',   'MP03: max total scheduled hours per doctor per day'),
 ('default_tax_rate',            '0.12', 'MP11: default tax rate'),
 ('discount_threshold_percent',  '20',   'MP11: percent discount above this needs Authorized By'),
 ('discount_threshold_amount',   '5000', 'MP11: flat discount above this needs Authorized By'),
 ('max_login_attempts',          '5',    'Login: failed attempts before lockout'),
 ('lockout_minutes',             '15',   'Login: lockout duration');

INSERT INTO room_types (room_type, daily_rate) VALUES
 ('Ward',1200.00),('Semi-Private',2500.00),('Private',4500.00),('ICU',9000.00);

INSERT INTO service_catalog (service_name, bill_category, unit_price) VALUES
 ('General Consultation',        'Consultation',  500.00),
 ('Specialist Consultation',     'Consultation',  1000.00),
 ('Follow-up Consultation',      'Consultation',  300.00),
 ('Emergency Room Consultation', 'Consultation',  1500.00),
 ('Wound Suturing',              'Procedure',     1800.00),
 ('ECG (12-lead)',               'Procedure',     600.00),
 ('Nebulization',                'Procedure',     350.00),
 ('Minor Excision',              'Procedure',     2500.00),
 ('Chest X-Ray (PA)',            'Procedure',     700.00),
 ('Medical Certificate',         'Miscellaneous', 150.00),
 ('Administrative Fee',          'Miscellaneous', 100.00),
 ('Medical Supplies Kit',        'Miscellaneous', 250.00);

-- =====================================================================
-- SECTION 14 : SEED DATA  -  patients (MP01 / MP02)
-- =====================================================================

SET @p1  = CONCAT('PAT-',@d,'-001');  SET @p2  = CONCAT('PAT-',@d,'-002');
SET @p3  = CONCAT('PAT-',@d,'-003');  SET @p4  = CONCAT('PAT-',@d,'-004');
SET @p5  = CONCAT('PAT-',@d,'-005');  SET @p6  = CONCAT('PAT-',@d,'-006');
SET @p7  = CONCAT('PAT-',@d,'-007');  SET @p8  = CONCAT('PAT-',@d,'-008');
SET @p9  = CONCAT('PAT-',@d,'-009');  SET @p10 = CONCAT('PAT-',@d,'-010');

INSERT INTO patients
 (patient_id, first_name, middle_name, last_name, date_of_birth, gender, blood_type,
  phone_number, email, street_address, city, province, postal_code, allergies, medical_history,
  emergency_contact_name, emergency_contact_phone, emergency_contact_relationship, registered_by_user_id) VALUES
 (@p1,  'Maria',    'Clara',    'Santos',    '1990-03-14', 'Female', 'A+',  '0917-123-4567', 'maria.santos@example.com',  '12 Rizal St., Brgy. Central', 'Taguig',      'Metro Manila',  '1630', 'Penicillin',  'Asthma (childhood)', 'Roberto Santos',    '0917-111-0001', 'Spouse',  2),
 (@p2,  'Juan',     NULL,       'Dela Cruz', '1985-07-22', 'Male',   'O+',  '0918-234-5678', 'juan.delacruz@example.com', '45 Mabini Ave.',              'Quezon City', 'Metro Manila',  '1100', NULL,          'Hypertension',       'Elena Dela Cruz',   '0918-111-0002', 'Spouse',  2),
 (@p3,  'Marco',    'Antonio',  'Reyes',     '1978-11-02', 'Male',   'B+',  '0919-345-6789', 'marco.reyes@example.com',   '8 Ayala Ave.',                'Makati',      'Metro Manila',  '1226', 'Shellfish',   'High cholesterol',   'Carmen Reyes',      '0919-111-0003', 'Spouse',  2),
 (@p4,  'Marianne', NULL,       'Lopez',     '1995-01-30', 'Female', 'AB+', '0920-456-7890', NULL,                        '23 Ortigas Ext.',             'Pasig',       'Metro Manila',  '1605', NULL,          NULL,                 'Jose Lopez',        '0920-111-0004', 'Parent',  2),
 (@p5,  'Pedro',    NULL,       'Garcia',    '1962-05-18', 'Male',   'O-',  '0921-567-8901', 'pedro.garcia@example.com',  '77 Colon St.',                'Cebu City',   'Cebu',          '6000', 'Sulfa drugs', 'Type 2 diabetes',    'Lourdes Garcia',    '0921-111-0005', 'Spouse',  2),
 (@p6,  'Ana Liza', 'Cruz',     'Mendoza',   '2001-09-09', 'Female', 'A-',  '0922-678-9012', 'analiza.m@example.com',     '5 Katipunan Ave.',            'Quezon City', 'Metro Manila',  '1108', NULL,          NULL,                 'Marites Mendoza',   '0922-111-0006', 'Parent',  2),
 (@p7,  'Carlo',    NULL,       'Bautista',  '2015-04-12', 'Male',   'B-',  '0923-789-0123', NULL,                        '31 Sampaguita St.',           'Pasay',       'Metro Manila',  '1300', 'Peanuts',     NULL,                 'Ricardo Bautista',  '0923-111-0007', 'Parent',  2),
 (@p8,  'Teresita', 'Lim',      'Ramos',     '1950-12-25', 'Female', 'AB-', '0924-890-1234', NULL,                        '9 Taft Ave.',                 'Manila',      'Metro Manila',  '1000', NULL,          'Osteoarthritis',     'Daniel Ramos',      '0924-111-0008', 'Child',   2),
 (@p9,  'Luis',     'Fernando', 'Aquino',    '1999-06-06', 'Male',   'O+',  '0925-901-2345', 'luis.aquino@example.com',   '60 Emerald Dr.',              'Davao City',  'Davao del Sur', '8000', NULL,          NULL,                 'Patricia Aquino',   '0925-111-0009', 'Sibling', 2),
 (@p10, 'Sofia',    NULL,       'Castillo',  '2010-08-21', 'Female', 'A+',  '0926-012-3456', NULL,                        '14 Heritage Rd.',             'Calamba',     'Laguna',        '4027', 'Dust mites',  'Allergic rhinitis',  'Angelica Castillo', '0926-111-0010', 'Parent',  2);

INSERT INTO patient_audit_log (patient_id, field_changed, old_value, new_value, changed_by_user_id, changed_at) VALUES
 (@p2, 'phone_number',   '0918-234-0000',  '0918-234-5678',  2, DATE_SUB(NOW(), INTERVAL 3 HOUR)),
 (@p2, 'street_address', '40 Mabini Ave.', '45 Mabini Ave.', 2, DATE_SUB(NOW(), INTERVAL 3 HOUR));

-- =====================================================================
-- SECTION 15 : SEED DATA  -  doctors, schedules, leaves (MP03)
-- =====================================================================

INSERT INTO doctors
 (doctor_id, first_name, last_name, license_number, specialization, department, phone, email, consultation_fee, employment_type) VALUES
 ('DOC-001', 'Ramon',  'Villanueva', 'PRC-0012345', 'General Medicine', 'Internal Medicine', '0917-555-0001', 'rvillanueva@hospital.local', 500.00,  'Full-time'),
 ('DOC-002', 'Maria',  'Santos',     'PRC-0023456', 'Pediatrics',       'Pediatrics',        '0917-555-0002', 'msantos@hospital.local',     600.00,  'Full-time'),
 ('DOC-003', 'Jose',   'Reyes',      'PRC-0034567', 'Cardiology',       'Cardiology',        '0917-555-0003', 'jreyes@hospital.local',      1200.00, 'Part-time'),
 ('DOC-004', 'Angela', 'Cruz',       'PRC-0045678', 'Dermatology',      'Dermatology',       '0917-555-0004', 'acruz@hospital.local',       900.00,  'Visiting'),
 ('DOC-005', 'Paolo',  'Mendoza',    'PRC-0056789', 'Orthopedics',      'Orthopedics',       '0917-555-0005', 'pmendoza@hospital.local',    1000.00, 'Full-time');

INSERT INTO doctor_schedules (doctor_id, day_of_week, start_time, end_time, consultation_minutes, notes) VALUES
 -- Dr. Villanueva: Mon-Fri, morning + afternoon, 15-minute slots
 ('DOC-001',1,'08:00','12:00',15,'Morning clinic'), ('DOC-001',1,'13:00','17:00',15,'Afternoon clinic'),
 ('DOC-001',2,'08:00','12:00',15,'Morning clinic'), ('DOC-001',2,'13:00','17:00',15,'Afternoon clinic'),
 ('DOC-001',3,'08:00','12:00',15,'Morning clinic'), ('DOC-001',3,'13:00','17:00',15,'Afternoon clinic'),
 ('DOC-001',4,'08:00','12:00',15,'Morning clinic'), ('DOC-001',4,'13:00','17:00',15,'Afternoon clinic'),
 ('DOC-001',5,'08:00','12:00',15,'Morning clinic'), ('DOC-001',5,'13:00','17:00',15,'Afternoon clinic'),
 -- Dr. Santos: Mon / Wed / Fri, 20-minute slots
 ('DOC-002',1,'09:00','12:00',20,NULL), ('DOC-002',1,'14:00','17:00',20,NULL),
 ('DOC-002',3,'09:00','12:00',20,NULL), ('DOC-002',3,'14:00','17:00',20,NULL),
 ('DOC-002',5,'09:00','12:00',20,NULL), ('DOC-002',5,'14:00','17:00',20,NULL),
 -- Dr. Reyes: Tue / Thu, 30-minute slots
 ('DOC-003',2,'10:00','16:00',30,'Cardiology clinic'),
 ('DOC-003',4,'10:00','16:00',30,'Cardiology clinic'),
 -- Dr. Cruz (visiting): Wed afternoon + Sat
 ('DOC-004',3,'13:00','17:00',20,'Visiting hours'),
 ('DOC-004',6,'09:00','15:00',20,'Saturday clinic'),
 -- Dr. Mendoza: Mon, Tue, Thu, Fri, 30-minute slots
 ('DOC-005',1,'08:00','14:00',30,NULL), ('DOC-005',2,'08:00','14:00',30,NULL),
 ('DOC-005',4,'08:00','14:00',30,NULL), ('DOC-005',5,'08:00','14:00',30,NULL);

INSERT INTO doctor_leaves (doctor_id, leave_date, reason) VALUES
 ('DOC-001', DATE_ADD(CURDATE(), INTERVAL 7 DAY),  'Medical conference'),
 ('DOC-003', DATE_ADD(CURDATE(), INTERVAL 14 DAY), 'Annual leave');

-- =====================================================================
-- SECTION 16 : SEED DATA  -  appointments (MP04)
-- =====================================================================
-- Appointments 1-8 are for TODAY so the dashboard, queue and consultation
-- screens have data on any day you import. Appointment 9 is 40 days ago.
-- (Seed rows are inserted directly, so they are not checked against the
--  doctor's weekday schedule.)

SET @a1 = CONCAT('APT-',@d,'-001');  SET @a2 = CONCAT('APT-',@d,'-002');
SET @a3 = CONCAT('APT-',@d,'-003');  SET @a4 = CONCAT('APT-',@d,'-004');
SET @a5 = CONCAT('APT-',@d,'-005');  SET @a6 = CONCAT('APT-',@d,'-006');
SET @a7 = CONCAT('APT-',@d,'-007');  SET @a8 = CONCAT('APT-',@d,'-008');
SET @a9 = CONCAT('APT-',@d40,'-001');

INSERT INTO appointments
 (appointment_id, token_number, patient_id, doctor_id, appointment_date, time_slot,
  appointment_type, reason_for_visit, status, status_remarks, status_updated_at, created_by_user_id) VALUES
 (@a1, 1, @p1, 'DOC-001', CURDATE(), '08:00', 'New Consultation', 'Fever and cough',       'Completed', NULL, NOW(), 2),
 (@a2, 2, @p2, 'DOC-001', CURDATE(), '08:15', 'New Consultation', 'Hypertension check',    'Completed', NULL, NOW(), 2),
 (@a3, 3, @p3, 'DOC-001', CURDATE(), '08:30', 'New Consultation', 'Chest discomfort',      'Booked',    NULL, NULL,  2),
 (@a4, 4, @p4, 'DOC-001', CURDATE(), '08:45', 'Follow-up',        'Review of lab results', 'Booked',    NULL, NULL,  2),
 (@a5, 1, @p5, 'DOC-003', CURDATE(), '10:00', 'New Consultation', 'Palpitations',          'Booked',    NULL, NULL,  2),
 (@a6, 2, @p6, 'DOC-003', CURDATE(), '10:30', 'Follow-up',        'Follow-up ECG',         'Booked',    NULL, NULL,  2),
 (@a7, 1, @p7, 'DOC-005', CURDATE(), '08:00', 'New Consultation', 'Ankle sprain',          'Booked',    NULL, NULL,  2),
 (@a8, 2, @p8, 'DOC-005', CURDATE(), '08:30', 'New Consultation', 'Knee pain',             'Cancelled', 'Patient requested cancellation', NOW(), 2),
 (@a9, 1, @p5, 'DOC-001', DATE_SUB(CURDATE(), INTERVAL 40 DAY), '09:00', 'New Consultation', 'Diabetes monitoring', 'Completed', NULL, DATE_SUB(NOW(), INTERVAL 40 DAY), 2);

-- =====================================================================
-- SECTION 17 : SEED DATA  -  queue board (MP05)
-- =====================================================================
-- DOC-001: tokens 1-2 done, token 3 in progress, token 4 waiting, token 5 = emergency walk-in
-- DOC-003: tokens 1-2 waiting (token 2 was skipped once)    DOC-005: token 1 waiting

INSERT INTO queue_entries
 (queue_id, queue_date, token_number, patient_id, doctor_id, appointment_id, queue_status, is_emergency,
  checked_in_at, started_at, completed_at) VALUES
 (1, CURDATE(), 1, @p1, 'DOC-001', @a1,  'Completed',   0, DATE_SUB(NOW(), INTERVAL 70 MINUTE), DATE_SUB(NOW(), INTERVAL 60 MINUTE), DATE_SUB(NOW(), INTERVAL 45 MINUTE)),
 (2, CURDATE(), 2, @p2, 'DOC-001', @a2,  'Completed',   0, DATE_SUB(NOW(), INTERVAL 65 MINUTE), DATE_SUB(NOW(), INTERVAL 45 MINUTE), DATE_SUB(NOW(), INTERVAL 28 MINUTE)),
 (3, CURDATE(), 3, @p3, 'DOC-001', @a3,  'In Progress', 0, DATE_SUB(NOW(), INTERVAL 40 MINUTE), DATE_SUB(NOW(), INTERVAL 10 MINUTE), NULL),
 (4, CURDATE(), 4, @p4, 'DOC-001', @a4,  'Waiting',     0, DATE_SUB(NOW(), INTERVAL 30 MINUTE), NULL, NULL),
 (5, CURDATE(), 5, @p9, 'DOC-001', NULL, 'Waiting',     1, DATE_SUB(NOW(), INTERVAL 5 MINUTE),  NULL, NULL),
 (6, CURDATE(), 1, @p5, 'DOC-003', @a5,  'Waiting',     0, DATE_SUB(NOW(), INTERVAL 25 MINUTE), NULL, NULL),
 (7, CURDATE(), 2, @p6, 'DOC-003', @a6,  'Waiting',     0, DATE_SUB(NOW(), INTERVAL 20 MINUTE), NULL, NULL),
 (8, CURDATE(), 1, @p7, 'DOC-005', @a7,  'Waiting',     0, DATE_SUB(NOW(), INTERVAL 15 MINUTE), NULL, NULL);

INSERT INTO queue_skip_log (queue_id, reason, skipped_at, skipped_by_user_id) VALUES
 (7, 'Patient stepped out of the waiting area', DATE_SUB(NOW(), INTERVAL 12 MINUTE), 2);

-- =====================================================================
-- SECTION 18 : SEED DATA  -  consultations (MP06)
-- =====================================================================

SET @c1 = CONCAT('CON-',@d,'-001');  SET @c2 = CONCAT('CON-',@d,'-002');
SET @c3 = CONCAT('CON-',@d40,'-001');

INSERT INTO consultations
 (consultation_id, appointment_id, systolic_bp, diastolic_bp, temperature_c, pulse_rate_bpm,
  weight_kg, height_cm, chief_complaint, examination_findings, diagnosis, treatment_plan, special_instructions,
  next_follow_up_date, recorded_at, recorded_by_user_id) VALUES
 (@c1, @a1, 118, 76, 38.2, 92, 58.5, 160.0,
  'Fever and productive cough for 3 days', 'Throat erythema, clear lungs', 'Acute upper respiratory tract infection',
  'Symptomatic treatment, increase fluids', 'Rest and hydrate; return if fever persists beyond 3 days',
  DATE_ADD(CURDATE(), INTERVAL 7 DAY), DATE_SUB(NOW(), INTERVAL 50 MINUTE), 3),
 (@c2, @a2, 150, 95, 36.8, 78, 82.0, 172.0,
  'Routine hypertension monitoring', 'No edema, regular heart rhythm', 'Essential hypertension, uncontrolled',
  'Start antihypertensive regimen, low-sodium diet', 'Monitor BP daily and keep a log',
  DATE_ADD(CURDATE(), INTERVAL 14 DAY), DATE_SUB(NOW(), INTERVAL 30 MINUTE), 3),
 (@c3, @a9, 138, 88, 36.6, 80, 79.0, 168.0,
  'Diabetes monitoring', 'Foot exam normal', 'Type 2 diabetes mellitus',
  'Continue metformin, diet counselling', 'Fasting blood sugar monthly',
  NULL, DATE_SUB(NOW(), INTERVAL 40 DAY), 3);

-- =====================================================================
-- SECTION 19 : SEED DATA  -  medicines & stock (MP08)
-- =====================================================================

INSERT INTO medicines
 (medicine_id, medicine_name, generic_name, category, manufacturer, unit_type, unit_price, reorder_level, storage_instructions) VALUES
 ('MED-001', 'Paracetamol 500mg',        'Paracetamol',        'Analgesic',              'United Laboratories',  'Tablet',    3.50,   200, 'Store below 30C, dry place'),
 ('MED-002', 'Amoxicillin 500mg',        'Amoxicillin',        'Antibiotic',             'Ritemed Philippines',  'Capsule',   12.00,  150, 'Store below 25C'),
 ('MED-003', 'Cetirizine 10mg',          'Cetirizine',         'Antihistamine',          'United Laboratories',  'Tablet',    8.00,   100, 'Store below 30C'),
 ('MED-004', 'Amlodipine 5mg',           'Amlodipine',         'Antihypertensive',       'Pfizer Philippines',   'Tablet',    10.00,  100, 'Store below 30C, away from light'),
 ('MED-005', 'Losartan 50mg',            'Losartan Potassium', 'Antihypertensive',       'Pascual Laboratories', 'Tablet',    15.00,  100, 'Store below 30C'),
 ('MED-006', 'Metformin 500mg',          'Metformin HCl',      'Antidiabetic',           'Ritemed Philippines',  'Tablet',    6.00,   150, 'Store below 30C'),
 ('MED-007', 'Omeprazole 20mg',          'Omeprazole',         'Gastrointestinal',       'GlaxoSmithKline PH',   'Capsule',   11.00,  80,  'Store below 25C, keep dry'),
 ('MED-008', 'Salbutamol Syrup 2mg/5mL', 'Salbutamol',         'Respiratory',            'GlaxoSmithKline PH',   'Syrup',     95.00,  20,  'Store below 25C'),
 ('MED-009', 'Ibuprofen 400mg',          'Ibuprofen',          'Analgesic',              'United Laboratories',  'Tablet',    7.50,   120, 'Store below 30C'),
 ('MED-010', 'Ceftriaxone 1g Vial',      'Ceftriaxone',        'Antibiotic',             'Pfizer Philippines',   'Injection', 180.00, 30,  'Store below 25C, protect from light'),
 ('MED-011', 'Oral Rehydration Salts',   'ORS',                'Rehydration',            'Pascual Laboratories', 'Sachet',    12.00,  50,  'Store in a cool, dry place'),
 ('MED-012', 'Vitamin C 500mg',          'Ascorbic Acid',      'Vitamins & Supplements', 'United Laboratories',  'Tablet',    4.00,   200, 'Store below 30C');

-- Alert mix: Normal (1,2,3,5,11), Low (4,7,12), Expiring Soon (6,10), Expired (8,9)
INSERT INTO stock_batches
 (batch_id, medicine_id, batch_number, quantity_received, expiry_date, supplier, purchase_price, date_received, received_by_user_id) VALUES
 (1,  'MED-001', 'PCM-2601', 500, DATE_ADD(CURDATE(), INTERVAL 365 DAY), 'MediSupply Corp',          2.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (2,  'MED-002', 'AMX-2601', 300, DATE_ADD(CURDATE(), INTERVAL 300 DAY), 'PharmaDirect Inc',         7.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (3,  'MED-003', 'CTZ-2601', 250, DATE_ADD(CURDATE(), INTERVAL 400 DAY), 'MediSupply Corp',          4.50,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (4,  'MED-004', 'AML-2601',  40, DATE_ADD(CURDATE(), INTERVAL 200 DAY), 'HealthFirst Distributors', 5.50,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (5,  'MED-005', 'LSR-2601', 220, DATE_ADD(CURDATE(), INTERVAL 250 DAY), 'HealthFirst Distributors', 8.50,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (6,  'MED-006', 'MET-2509', 180, DATE_ADD(CURDATE(), INTERVAL 45 DAY),  'PharmaDirect Inc',         3.00,   DATE_SUB(CURDATE(), INTERVAL 150 DAY), 4),
 (7,  'MED-007', 'OMP-2601',  60, DATE_ADD(CURDATE(), INTERVAL 180 DAY), 'PharmaDirect Inc',         6.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (8,  'MED-008', 'SAL-2506',  15, DATE_SUB(CURDATE(), INTERVAL 15 DAY),  'MediSupply Corp',          55.00,  DATE_SUB(CURDATE(), INTERVAL 220 DAY), 4),
 (9,  'MED-008', 'SAL-2601',  30, DATE_ADD(CURDATE(), INTERVAL 300 DAY), 'MediSupply Corp',          56.00,  DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (10, 'MED-009', 'IBU-2506', 100, DATE_SUB(CURDATE(), INTERVAL 10 DAY),  'HealthFirst Distributors', 4.00,   DATE_SUB(CURDATE(), INTERVAL 230 DAY), 4),
 (11, 'MED-010', 'CFT-2603',  25, DATE_ADD(CURDATE(), INTERVAL 50 DAY),  'PharmaDirect Inc',         110.00, DATE_SUB(CURDATE(), INTERVAL 120 DAY), 4),
 (12, 'MED-010', 'CFT-2605',  80, DATE_ADD(CURDATE(), INTERVAL 400 DAY), 'PharmaDirect Inc',         112.00, DATE_SUB(CURDATE(), INTERVAL 20 DAY),  4),
 (13, 'MED-011', 'ORS-2601', 200, DATE_ADD(CURDATE(), INTERVAL 500 DAY), 'MediSupply Corp',          7.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (14, 'MED-012', 'VTC-2601',  90, DATE_ADD(CURDATE(), INTERVAL 200 DAY), 'MediSupply Corp',          2.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4);

INSERT INTO stock_movements (batch_id, movement_type, quantity_change, reason, moved_at, moved_by_user_id) VALUES
 (1,  'ADJUSTMENT', -10, 'Damaged packaging',           DATE_SUB(NOW(), INTERVAL 5 DAY), 4),
 (13, 'ADJUSTMENT', -4,  'Lost during inventory count', DATE_SUB(NOW(), INTERVAL 2 DAY), 4);

-- =====================================================================
-- SECTION 20 : SEED DATA  -  prescriptions (MP07)
-- =====================================================================
-- Rx 1: fully fillable        Rx 2: Amlodipine needs 60, only 40 in stock (partial)
-- Rx 3: EXPIRED (valid_until 10 days ago) - must be blocked at dispensing

SET @rx1 = CONCAT('PRE-',@d,'-001');  SET @rx2 = CONCAT('PRE-',@d,'-002');
SET @rx3 = CONCAT('PRE-',@d40,'-001');

INSERT INTO prescriptions
 (prescription_id, consultation_id, general_instructions, issued_date, valid_until, created_by_user_id) VALUES
 (@rx1, @c1, 'Take after meals. Drink plenty of fluids.',          CURDATE(), DATE_ADD(CURDATE(), INTERVAL 30 DAY), 3),
 (@rx2, @c2, 'Take in the morning. Monitor blood pressure daily.', CURDATE(), DATE_ADD(CURDATE(), INTERVAL 30 DAY), 3),
 (@rx3, @c3, 'Take with meals.', DATE_SUB(CURDATE(), INTERVAL 40 DAY), DATE_SUB(CURDATE(), INTERVAL 10 DAY), 3);

INSERT INTO prescription_items
 (prescription_item_id, prescription_id, medicine_id, dosage, frequency_per_day, duration_days, instructions) VALUES
 (1, @rx1, 'MED-001', '500mg', 3, 5,  'Every 8 hours as needed for fever'),
 (2, @rx1, 'MED-003', '10mg',  1, 5,  'At bedtime'),
 (3, @rx1, 'MED-012', '500mg', 1, 7,  'After breakfast'),
 (4, @rx2, 'MED-004', '5mg',   1, 60, 'Once daily, morning'),
 (5, @rx2, 'MED-005', '50mg',  1, 30, 'Once daily, evening'),
 (6, @rx3, 'MED-006', '500mg', 2, 30, 'After meals'),
 (7, @rx3, 'MED-007', '20mg',  1, 14, 'Before breakfast');

-- =====================================================================
-- SECTION 21 : SEED DATA  -  laboratory (MP10)
-- =====================================================================

INSERT INTO lab_tests
 (test_id, test_name, category, sample_type, price, normal_range_low, normal_range_high, unit, turnaround_hours, requires_verification) VALUES
 ('TST-001', 'Hemoglobin',                  'Hematology',         'Whole Blood (EDTA)', 150.00, '12.0',         '17.5',         'g/dL',     4, 0),
 ('TST-002', 'Fasting Blood Sugar',         'Clinical Chemistry', 'Serum',              120.00, '70',           '100',          'mg/dL',    4, 0),
 ('TST-003', 'Total Cholesterol',           'Clinical Chemistry', 'Serum',              250.00, '0',            '200',          'mg/dL',    6, 0),
 ('TST-004', 'Creatinine',                  'Clinical Chemistry', 'Serum',              200.00, '0.6',          '1.3',          'mg/dL',    6, 0),
 ('TST-005', 'White Blood Cell Count',      'Hematology',         'Whole Blood (EDTA)', 150.00, '4.5',          '11.0',         'x10^3/uL', 4, 0),
 ('TST-006', 'Urinalysis',                  'Urinalysis',         'Urine',              100.00, 'Negative',     'Negative',     NULL,       2, 0),
 ('TST-007', 'Dengue NS1 Antigen',          'Serology',           'Serum',              800.00, 'Negative',     'Negative',     NULL,       2, 1),
 ('TST-008', 'Hepatitis B Surface Antigen', 'Serology',           'Serum',              450.00, 'Non-reactive', 'Non-reactive', NULL,       3, 1),
 ('TST-009', 'Potassium',                   'Clinical Chemistry', 'Serum',              180.00, '3.5',          '5.0',          'mmol/L',   6, 0),
 ('TST-010', 'Troponin I',                  'Cardiac Markers',    'Serum',              900.00, '0',            '0.04',         'ng/mL',    2, 1);

-- Order 1 = completed (abnormal result), 2-7 = pending
SET @lo1 = CONCAT('LO-',@d,'-001');

INSERT INTO lab_orders
 (order_id, patient_id, test_id, consultation_id, ordered_by_doctor_id, urgency, status, ordered_at) VALUES
 (@lo1,                        @p1, 'TST-005', @c1,  'DOC-001', 'Urgent',  'Completed', DATE_SUB(NOW(), INTERVAL 45 MINUTE)),
 (CONCAT('LO-',@d,'-002'),     @p2, 'TST-003', @c2,  'DOC-001', 'Routine', 'Pending',   DATE_SUB(NOW(), INTERVAL 25 MINUTE)),
 (CONCAT('LO-',@d,'-003'),     @p2, 'TST-002', @c2,  'DOC-001', 'Routine', 'Pending',   DATE_SUB(NOW(), INTERVAL 25 MINUTE)),
 (CONCAT('LO-',@d,'-004'),     @p3, 'TST-010', NULL, 'DOC-001', 'STAT',    'Pending',   DATE_SUB(NOW(), INTERVAL 8 MINUTE)),
 (CONCAT('LO-',@d,'-005'),     @p4, 'TST-001', NULL, 'DOC-001', 'Routine', 'Pending',   DATE_SUB(NOW(), INTERVAL 20 MINUTE)),
 (CONCAT('LO-',@d,'-006'),     @p5, 'TST-007', NULL, 'DOC-003', 'Urgent',  'Pending',   DATE_SUB(NOW(), INTERVAL 15 MINUTE)),
 (CONCAT('LO-',@d,'-007'),     @p6, 'TST-006', NULL, 'DOC-003', 'Routine', 'Pending',   DATE_SUB(NOW(), INTERVAL 10 MINUTE));

INSERT INTO lab_results
 (result_id, order_id, result_value, is_abnormal, findings, tested_by_user_id, verified_by_user_id, test_datetime) VALUES
 (CONCAT('LR-',@d,'-001'), @lo1, '13.2', 1, 'Mild leukocytosis, consistent with infection', 5, NULL, DATE_SUB(NOW(), INTERVAL 30 MINUTE));

-- =====================================================================
-- SECTION 22 : SEED DATA  -  bills & payments (MP11 / MP12)
-- =====================================================================
-- Expected totals (12% tax on post-discount amount):
--  Bill 1  702.50 -> 786.80   paid in full             => Fully Paid
--  Bill 2 1870.00 -10% -> 1884.96  paid 1000            => Partially Paid
--  Bill 3 2500.00 -> 2800.00  no payments               => Outstanding
--  Bill 4 10280.00 -25% (authorized) -> 8635.20 paid 5000 => Partially Paid
--  Bill 5  450.00 -> 504.00   paid in full (cash, change) => Fully Paid
--  Bill 6  700.00 -50 flat -> 728.00 paid 300           => Partially Paid
--  Bill 7  Draft (no payments)

SET @b1 = CONCAT('BILL-',@d,'-001');  SET @b2 = CONCAT('BILL-',@d,'-002');
SET @b3 = CONCAT('BILL-',@d,'-003');  SET @b4 = CONCAT('BILL-',@d,'-004');
SET @b5 = CONCAT('BILL-',@d,'-005');  SET @b6 = CONCAT('BILL-',@d,'-006');
SET @b7 = CONCAT('BILL-',@d,'-007');

INSERT INTO bills
 (bill_id, patient_id, status, discount_type, discount_value, tax_rate, authorized_by,
  created_at, created_by_user_id, finalized_at) VALUES
 (@b1, @p1, 'Finalized', 'Flat',    0.00,  0.1200, NULL,                   DATE_SUB(NOW(), INTERVAL 40 MINUTE), 6, DATE_SUB(NOW(), INTERVAL 35 MINUTE)),
 (@b2, @p2, 'Finalized', 'Percent', 10.00, 0.1200, NULL,                   DATE_SUB(NOW(), INTERVAL 30 MINUTE), 6, DATE_SUB(NOW(), INTERVAL 25 MINUTE)),
 (@b3, @p3, 'Finalized', 'Flat',    0.00,  0.1200, NULL,                   DATE_SUB(NOW(), INTERVAL 20 MINUTE), 6, DATE_SUB(NOW(), INTERVAL 15 MINUTE)),
 (@b4, @p4, 'Finalized', 'Percent', 25.00, 0.1200, 'Dr. Ramon Villanueva', DATE_SUB(NOW(), INTERVAL 3 HOUR),    6, DATE_SUB(NOW(), INTERVAL 170 MINUTE)),
 (@b5, @p5, 'Finalized', 'Flat',    0.00,  0.1200, NULL,                   DATE_SUB(NOW(), INTERVAL 2 HOUR),    6, DATE_SUB(NOW(), INTERVAL 110 MINUTE)),
 (@b6, @p6, 'Finalized', 'Flat',    50.00, 0.1200, NULL,                   DATE_SUB(NOW(), INTERVAL 90 MINUTE), 6, DATE_SUB(NOW(), INTERVAL 85 MINUTE)),
 (@b7, @p7, 'Draft',     'Flat',    0.00,  0.1200, NULL,                   DATE_SUB(NOW(), INTERVAL 5 MINUTE),  6, NULL);

INSERT INTO bill_items (bill_id, bill_category, description, quantity, unit_price) VALUES
 -- Bill 1
 (@b1, 'Consultation',  'General Consultation',       1,  500.00),
 (@b1, 'Medicine',      'Paracetamol 500mg x15',      15, 3.50),
 (@b1, 'Lab Test',      'White Blood Cell Count',     1,  150.00),
 -- Bill 2
 (@b2, 'Consultation',  'General Consultation',       1,  500.00),
 (@b2, 'Procedure',     'ECG (12-lead)',              1,  600.00),
 (@b2, 'Lab Test',      'Total Cholesterol',          1,  250.00),
 (@b2, 'Lab Test',      'Fasting Blood Sugar',        1,  120.00),
 (@b2, 'Medicine',      'Amlodipine 5mg x40',         40, 10.00),
 -- Bill 3
 (@b3, 'Consultation',  'Specialist Consultation',    1,  1000.00),
 (@b3, 'Lab Test',      'Troponin I',                 1,  900.00),
 (@b3, 'Procedure',     'ECG (12-lead)',              1,  600.00),
 -- Bill 4
 (@b4, 'Room',          'Semi-Private Room (3 days)', 3,  2500.00),
 (@b4, 'Procedure',     'Nebulization',               2,  350.00),
 (@b4, 'Medicine',      'Ceftriaxone 1g Vial x6',     6,  180.00),
 (@b4, 'Consultation',  'Specialist Consultation',    1,  1000.00),
 -- Bill 5
 (@b5, 'Consultation',  'Follow-up Consultation',     1,  300.00),
 (@b5, 'Miscellaneous', 'Medical Certificate',        1,  150.00),
 -- Bill 6
 (@b6, 'Consultation',  'General Consultation',       1,  500.00),
 (@b6, 'Lab Test',      'Urinalysis',                 1,  100.00),
 (@b6, 'Miscellaneous', 'Administrative Fee',         1,  100.00),
 -- Bill 7 (draft)
 (@b7, 'Consultation',  'General Consultation',       1,  500.00);

INSERT INTO payments
 (payment_id, bill_id, amount, payment_method, reference_number, tendered_amount, received_by_user_id, payment_datetime) VALUES
 (CONCAT('PAY-',@d,'-001'), @b1, 786.80,  'Cash',   NULL,           800.00,  6, DATE_SUB(NOW(), INTERVAL 30 MINUTE)),
 (CONCAT('PAY-',@d,'-002'), @b2, 1000.00, 'Online', 'GCASH-884201', NULL,    6, DATE_SUB(NOW(), INTERVAL 20 MINUTE)),
 (CONCAT('PAY-',@d,'-003'), @b4, 3000.00, 'Credit', 'CC-APP-55821', NULL,    6, DATE_SUB(NOW(), INTERVAL 160 MINUTE)),
 (CONCAT('PAY-',@d,'-004'), @b4, 2000.00, 'Cash',   NULL,           2000.00, 6, DATE_SUB(NOW(), INTERVAL 100 MINUTE)),
 (CONCAT('PAY-',@d,'-005'), @b5, 504.00,  'Cash',   NULL,           600.00,  6, DATE_SUB(NOW(), INTERVAL 100 MINUTE)),
 (CONCAT('PAY-',@d,'-006'), @b6, 300.00,  'Debit',  'DB-771203',    NULL,    6, DATE_SUB(NOW(), INTERVAL 80 MINUTE));

-- =====================================================================
-- SECTION 23 : SEED DATA  -  code sequences (must match the rows above)
-- =====================================================================

INSERT INTO code_sequences (prefix, seq_date, last_number) VALUES
 ('PAT',  CURDATE(), 10),
 ('APT',  CURDATE(), 8),
 ('APT',  DATE_SUB(CURDATE(), INTERVAL 40 DAY), 1),
 ('CON',  CURDATE(), 2),
 ('CON',  DATE_SUB(CURDATE(), INTERVAL 40 DAY), 1),
 ('PRE',  CURDATE(), 2),
 ('PRE',  DATE_SUB(CURDATE(), INTERVAL 40 DAY), 1),
 ('MS',   CURDATE(), 0),
 ('LO',   CURDATE(), 7),
 ('LR',   CURDATE(), 1),
 ('BILL', CURDATE(), 7),
 ('PAY',  CURDATE(), 6),
 ('DOC',  '1970-01-01', 5),
 ('MED',  '1970-01-01', 12),
 ('TST',  '1970-01-01', 10);

-- =====================================================================
-- END OF schema.sql
-- Quick sanity checks (run manually after import):
--   SELECT * FROM v_medicine_stock_status;      -- Normal / Low / Expiring / Expired all appear
--   SELECT * FROM v_bill_payment_summary;       -- Fully Paid / Partially Paid / Outstanding / Draft
--   SELECT username, role FROM users;           -- 6 users
--   SELECT DISTINCT specialization FROM doctors; -- feeds the Specialization drop-down
-- =====================================================================
