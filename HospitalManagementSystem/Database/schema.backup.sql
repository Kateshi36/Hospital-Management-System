-- =====================================================================
--  HOSPITAL MANAGEMENT SYSTEM  -  schema.sql
--  Target : XAMPP MySQL / MariaDB (MariaDB 10.4+ or MySQL 8.0.16+)
--  Design : Third Normal Form (3NF), InnoDB, utf8mb4
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
--   * day_of_week uses the .NET DayOfWeek numbering: 0=Sun,1=Mon ... 6=Sat
--   * Derived values (age, quantity, totals, balances, stock on hand,
--     payment status) are NOT stored; they come from generated columns
--     or the v_* views near the bottom of this file.
--   * Dated codes (PAT-YYYYMMDD-XXX etc.) are produced by the app via the
--     `code_sequences` table (see GeneratorHelper, a later step).
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

CREATE TABLE roles (
    role_id      INT          NOT NULL AUTO_INCREMENT,
    role_name    VARCHAR(50)  NOT NULL,
    description  VARCHAR(200) NULL,
    PRIMARY KEY (role_id),
    UNIQUE KEY uq_roles_name (role_name)
) ENGINE=InnoDB;

CREATE TABLE users (
    user_id               INT          NOT NULL AUTO_INCREMENT,
    username              VARCHAR(50)  NOT NULL,
    password_hash         VARCHAR(256) NOT NULL,   -- Base64 PBKDF2 hash
    password_salt         VARCHAR(128) NOT NULL,   -- Base64 random salt
    hash_iterations       INT          NOT NULL DEFAULT 210000,
    full_name             VARCHAR(120) NOT NULL,
    email                 VARCHAR(120) NULL,
    role_id               INT          NOT NULL,
    is_active             TINYINT(1)   NOT NULL DEFAULT 1,
    failed_login_attempts INT          NOT NULL DEFAULT 0,
    locked_until          DATETIME     NULL,
    last_login_at         DATETIME     NULL,
    created_at            DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id),
    UNIQUE KEY uq_users_username (username),
    CONSTRAINT fk_users_role FOREIGN KEY (role_id) REFERENCES roles (role_id)
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
-- SECTION 2 : SHARED LOOKUP TABLES
-- =====================================================================

CREATE TABLE genders (
    gender_id   INT         NOT NULL AUTO_INCREMENT,
    gender_name VARCHAR(30) NOT NULL,
    PRIMARY KEY (gender_id),
    UNIQUE KEY uq_gender_name (gender_name)
) ENGINE=InnoDB;

CREATE TABLE blood_types (
    blood_type_id   INT        NOT NULL AUTO_INCREMENT,
    blood_type_name VARCHAR(5) NOT NULL,
    PRIMARY KEY (blood_type_id),
    UNIQUE KEY uq_blood_type_name (blood_type_name)
) ENGINE=InnoDB;

CREATE TABLE relationships (
    relationship_id   INT         NOT NULL AUTO_INCREMENT,
    relationship_name VARCHAR(40) NOT NULL,
    PRIMARY KEY (relationship_id),
    UNIQUE KEY uq_relationship_name (relationship_name)
) ENGINE=InnoDB;

CREATE TABLE provinces (
    province_id   INT          NOT NULL AUTO_INCREMENT,
    province_name VARCHAR(80)  NOT NULL,
    PRIMARY KEY (province_id),
    UNIQUE KEY uq_province_name (province_name)
) ENGINE=InnoDB;

CREATE TABLE cities (
    city_id     INT          NOT NULL AUTO_INCREMENT,
    province_id INT          NOT NULL,
    city_name   VARCHAR(80)  NOT NULL,
    PRIMARY KEY (city_id),
    UNIQUE KEY uq_city_province (province_id, city_name),
    CONSTRAINT fk_cities_province FOREIGN KEY (province_id) REFERENCES provinces (province_id)
) ENGINE=InnoDB;

CREATE TABLE payment_methods (
    payment_method_id  INT         NOT NULL AUTO_INCREMENT,
    method_name        VARCHAR(30) NOT NULL,
    requires_reference TINYINT(1)  NOT NULL DEFAULT 0,
    PRIMARY KEY (payment_method_id),
    UNIQUE KEY uq_payment_method_name (method_name)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 3 : MP01 / MP02  -  PATIENTS
-- =====================================================================

CREATE TABLE patients (
    patient_id            INT          NOT NULL AUTO_INCREMENT,
    patient_code          VARCHAR(20)  NOT NULL,          -- PAT-YYYYMMDD-XXX
    first_name            VARCHAR(60)  NOT NULL,
    middle_name           VARCHAR(60)  NULL,
    last_name             VARCHAR(60)  NOT NULL,
    date_of_birth         DATE         NOT NULL,          -- Age is computed, never stored
    gender_id             INT          NOT NULL,
    blood_type_id         INT          NULL,
    phone_number          VARCHAR(25)  NOT NULL,
    email                 VARCHAR(120) NULL,
    street_address        VARCHAR(200) NULL,
    city_id               INT          NULL,              -- province derived via cities
    postal_code           VARCHAR(10)  NULL,
    allergies             TEXT         NULL,
    medical_history       TEXT         NULL,
    registered_at         DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    registered_by_user_id INT          NULL,
    last_updated_at       DATETIME     NULL,
    PRIMARY KEY (patient_id),
    UNIQUE KEY uq_patients_code (patient_code),
    KEY idx_patients_name (last_name, first_name),
    KEY idx_patients_phone (phone_number),
    KEY idx_patients_dob (date_of_birth),
    CONSTRAINT fk_patients_gender FOREIGN KEY (gender_id)     REFERENCES genders (gender_id),
    CONSTRAINT fk_patients_blood  FOREIGN KEY (blood_type_id) REFERENCES blood_types (blood_type_id),
    CONSTRAINT fk_patients_city   FOREIGN KEY (city_id)       REFERENCES cities (city_id),
    CONSTRAINT fk_patients_user   FOREIGN KEY (registered_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

CREATE TABLE patient_emergency_contacts (
    contact_id      INT          NOT NULL AUTO_INCREMENT,
    patient_id      INT          NOT NULL,
    contact_name    VARCHAR(120) NOT NULL,
    contact_phone   VARCHAR(25)  NOT NULL,
    relationship_id INT          NOT NULL,
    PRIMARY KEY (contact_id),
    UNIQUE KEY uq_emergency_patient (patient_id),          -- one contact per patient
    CONSTRAINT fk_emerg_patient      FOREIGN KEY (patient_id)      REFERENCES patients (patient_id) ON DELETE CASCADE,
    CONSTRAINT fk_emerg_relationship FOREIGN KEY (relationship_id) REFERENCES relationships (relationship_id)
) ENGINE=InnoDB;

CREATE TABLE patient_audit_log (
    audit_id           INT          NOT NULL AUTO_INCREMENT,
    patient_id         INT          NOT NULL,
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
-- SECTION 4 : MP03  -  DOCTORS, SCHEDULES, LEAVES
-- =====================================================================

CREATE TABLE specializations (
    specialization_id   INT         NOT NULL AUTO_INCREMENT,
    specialization_name VARCHAR(60) NOT NULL,
    PRIMARY KEY (specialization_id),
    UNIQUE KEY uq_specialization_name (specialization_name)
) ENGINE=InnoDB;

CREATE TABLE departments (
    department_id   INT         NOT NULL AUTO_INCREMENT,
    department_name VARCHAR(60) NOT NULL,
    PRIMARY KEY (department_id),
    UNIQUE KEY uq_department_name (department_name)
) ENGINE=InnoDB;

CREATE TABLE employment_types (
    employment_type_id   INT         NOT NULL AUTO_INCREMENT,
    employment_type_name VARCHAR(30) NOT NULL,
    PRIMARY KEY (employment_type_id),
    UNIQUE KEY uq_employment_type_name (employment_type_name)
) ENGINE=InnoDB;

CREATE TABLE doctors (
    doctor_id          INT           NOT NULL AUTO_INCREMENT,
    doctor_code        VARCHAR(10)   NOT NULL,             -- DOC-XXX
    first_name         VARCHAR(60)   NOT NULL,
    last_name          VARCHAR(60)   NOT NULL,
    license_number     VARCHAR(30)   NOT NULL,
    specialization_id  INT           NOT NULL,
    department_id      INT           NOT NULL,
    phone              VARCHAR(25)   NOT NULL,
    email              VARCHAR(120)  NULL,
    consultation_fee   DECIMAL(10,2) NOT NULL,
    employment_type_id INT           NOT NULL,
    created_at         DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (doctor_id),
    UNIQUE KEY uq_doctors_code    (doctor_code),
    UNIQUE KEY uq_doctors_license (license_number),
    CONSTRAINT chk_doctor_fee CHECK (consultation_fee >= 0),
    CONSTRAINT fk_doctors_spec  FOREIGN KEY (specialization_id)  REFERENCES specializations (specialization_id),
    CONSTRAINT fk_doctors_dept  FOREIGN KEY (department_id)      REFERENCES departments (department_id),
    CONSTRAINT fk_doctors_emp   FOREIGN KEY (employment_type_id) REFERENCES employment_types (employment_type_id)
) ENGINE=InnoDB;

-- Deleting a doctor removes schedules and leaves (MP03 rule).
CREATE TABLE doctor_schedules (
    schedule_id          INT         NOT NULL AUTO_INCREMENT,
    doctor_id            INT         NOT NULL,
    day_of_week          TINYINT     NOT NULL,              -- 0=Sun ... 6=Sat
    start_time           TIME        NOT NULL,
    end_time             TIME        NOT NULL,
    consultation_minutes INT         NOT NULL DEFAULT 15,
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
    doctor_id  INT          NOT NULL,
    leave_date DATE         NOT NULL,
    reason     VARCHAR(200) NOT NULL,
    PRIMARY KEY (leave_id),
    UNIQUE KEY uq_leave_doctor_date (doctor_id, leave_date),
    CONSTRAINT fk_leave_doctor FOREIGN KEY (doctor_id) REFERENCES doctors (doctor_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 5 : MP04  -  APPOINTMENTS
-- =====================================================================

CREATE TABLE appointment_types (
    appointment_type_id   INT         NOT NULL AUTO_INCREMENT,
    appointment_type_name VARCHAR(40) NOT NULL,
    PRIMARY KEY (appointment_type_id),
    UNIQUE KEY uq_appt_type_name (appointment_type_name)
) ENGINE=InnoDB;

CREATE TABLE appointment_statuses (
    status_id   INT         NOT NULL AUTO_INCREMENT,
    status_name VARCHAR(30) NOT NULL,
    PRIMARY KEY (status_id),
    UNIQUE KEY uq_appt_status_name (status_name)
) ENGINE=InnoDB;

-- status_id: 1 Booked, 2 Completed, 3 Cancelled, 4 No-show
-- active_slot_flag is NULL for Cancelled rows, so a cancelled/rescheduled slot
-- is free again while the unique keys still stop real double-bookings.
CREATE TABLE appointments (
    appointment_id      INT          NOT NULL AUTO_INCREMENT,
    appointment_code    VARCHAR(20)  NOT NULL,              -- APT-YYYYMMDD-XXX
    token_number        INT          NOT NULL,              -- per doctor / per date
    patient_id          INT          NOT NULL,
    doctor_id           INT          NOT NULL,
    appointment_date    DATE         NOT NULL,
    time_slot           TIME         NOT NULL,
    appointment_type_id INT          NOT NULL,
    reason_for_visit    VARCHAR(250) NULL,
    status_id           INT          NOT NULL DEFAULT 1,
    status_remarks      VARCHAR(250) NULL,                  -- cancel reason, etc.
    status_updated_at   DATETIME     NULL,
    created_at          DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by_user_id  INT          NULL,
    active_slot_flag    TINYINT GENERATED ALWAYS AS (CASE WHEN status_id = 3 THEN NULL ELSE 1 END) STORED,
    PRIMARY KEY (appointment_id),
    UNIQUE KEY uq_appt_code (appointment_code),
    UNIQUE KEY uq_appt_token (doctor_id, appointment_date, token_number),
    UNIQUE KEY uq_appt_doctor_slot (doctor_id, appointment_date, time_slot, active_slot_flag),
    UNIQUE KEY uq_appt_patient_doctor_day (patient_id, doctor_id, appointment_date, active_slot_flag),
    KEY idx_appt_date (appointment_date),
    CONSTRAINT fk_appt_patient FOREIGN KEY (patient_id)          REFERENCES patients (patient_id),
    CONSTRAINT fk_appt_doctor  FOREIGN KEY (doctor_id)           REFERENCES doctors (doctor_id),
    CONSTRAINT fk_appt_type    FOREIGN KEY (appointment_type_id) REFERENCES appointment_types (appointment_type_id),
    CONSTRAINT fk_appt_status  FOREIGN KEY (status_id)           REFERENCES appointment_statuses (status_id),
    CONSTRAINT fk_appt_user    FOREIGN KEY (created_by_user_id)  REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 6 : MP05  -  QUEUE BOARD
-- =====================================================================

CREATE TABLE queue_statuses (
    queue_status_id   INT         NOT NULL AUTO_INCREMENT,
    queue_status_name VARCHAR(30) NOT NULL,
    PRIMARY KEY (queue_status_id),
    UNIQUE KEY uq_queue_status_name (queue_status_name)
) ENGINE=InnoDB;

-- queue_status_id: 1 Waiting, 2 In Progress, 3 Completed, 4 Skipped
-- appointment_id is NULL for walk-in / emergency patients.
-- Actual consultation minutes = completed_at - started_at (not stored).
CREATE TABLE queue_entries (
    queue_id         INT      NOT NULL AUTO_INCREMENT,
    queue_date       DATE     NOT NULL,
    token_number     INT      NOT NULL,
    patient_id       INT      NOT NULL,
    doctor_id        INT      NOT NULL,
    appointment_id   INT      NULL,
    queue_status_id  INT      NOT NULL DEFAULT 1,
    is_emergency     TINYINT(1) NOT NULL DEFAULT 0,
    checked_in_at    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    started_at       DATETIME NULL,
    completed_at     DATETIME NULL,
    in_progress_flag TINYINT GENERATED ALWAYS AS (CASE WHEN queue_status_id = 2 THEN 1 ELSE NULL END) STORED,
    PRIMARY KEY (queue_id),
    UNIQUE KEY uq_queue_token (doctor_id, queue_date, token_number),
    UNIQUE KEY uq_queue_appointment (appointment_id),
    UNIQUE KEY uq_queue_one_in_progress (doctor_id, queue_date, in_progress_flag),  -- only one In Progress per doctor
    KEY idx_queue_date_status (queue_date, queue_status_id),
    CONSTRAINT fk_queue_patient FOREIGN KEY (patient_id)      REFERENCES patients (patient_id),
    CONSTRAINT fk_queue_doctor  FOREIGN KEY (doctor_id)       REFERENCES doctors (doctor_id),
    CONSTRAINT fk_queue_appt    FOREIGN KEY (appointment_id)  REFERENCES appointments (appointment_id),
    CONSTRAINT fk_queue_status  FOREIGN KEY (queue_status_id) REFERENCES queue_statuses (queue_status_id)
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
-- SECTION 7 : MP06  -  CONSULTATIONS & VITAL SIGNS
-- =====================================================================

CREATE TABLE consultations (
    consultation_id      INT           NOT NULL AUTO_INCREMENT,
    consultation_code    VARCHAR(20)   NOT NULL,            -- CON-YYYYMMDD-XXX
    appointment_id       INT           NOT NULL,            -- UNIQUE: one consultation per appointment
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
    UNIQUE KEY uq_consult_code (consultation_code),
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
-- SECTION 8 : MP08  -  MEDICINE CATALOG & STOCK
-- =====================================================================

CREATE TABLE medicine_categories (
    category_id   INT         NOT NULL AUTO_INCREMENT,
    category_name VARCHAR(60) NOT NULL,
    PRIMARY KEY (category_id),
    UNIQUE KEY uq_med_category_name (category_name)
) ENGINE=InnoDB;

CREATE TABLE manufacturers (
    manufacturer_id   INT          NOT NULL AUTO_INCREMENT,
    manufacturer_name VARCHAR(100) NOT NULL,
    PRIMARY KEY (manufacturer_id),
    UNIQUE KEY uq_manufacturer_name (manufacturer_name)
) ENGINE=InnoDB;

CREATE TABLE unit_types (
    unit_type_id   INT         NOT NULL AUTO_INCREMENT,
    unit_type_name VARCHAR(30) NOT NULL,
    PRIMARY KEY (unit_type_id),
    UNIQUE KEY uq_unit_type_name (unit_type_name)
) ENGINE=InnoDB;

CREATE TABLE suppliers (
    supplier_id   INT          NOT NULL AUTO_INCREMENT,
    supplier_name VARCHAR(100) NOT NULL,
    PRIMARY KEY (supplier_id),
    UNIQUE KEY uq_supplier_name (supplier_name)
) ENGINE=InnoDB;

CREATE TABLE medicines (
    medicine_id          INT           NOT NULL AUTO_INCREMENT,
    medicine_code        VARCHAR(10)   NOT NULL,            -- MED-XXX
    medicine_name        VARCHAR(120)  NOT NULL,
    generic_name         VARCHAR(120)  NULL,
    category_id          INT           NOT NULL,
    manufacturer_id      INT           NULL,
    unit_type_id         INT           NOT NULL,
    unit_price           DECIMAL(10,2) NOT NULL,
    reorder_level        INT           NOT NULL DEFAULT 0,
    storage_instructions VARCHAR(250)  NULL,
    PRIMARY KEY (medicine_id),
    UNIQUE KEY uq_medicine_code (medicine_code),
    UNIQUE KEY uq_medicine_name (medicine_name),
    CONSTRAINT chk_med_price   CHECK (unit_price > 0),
    CONSTRAINT chk_med_reorder CHECK (reorder_level >= 0),
    CONSTRAINT fk_med_category     FOREIGN KEY (category_id)     REFERENCES medicine_categories (category_id),
    CONSTRAINT fk_med_manufacturer FOREIGN KEY (manufacturer_id) REFERENCES manufacturers (manufacturer_id),
    CONSTRAINT fk_med_unit         FOREIGN KEY (unit_type_id)    REFERENCES unit_types (unit_type_id)
) ENGINE=InnoDB;

-- Stock on hand is NEVER stored. Remaining = quantity_received + SUM(stock_movements).
CREATE TABLE stock_batches (
    batch_id           INT           NOT NULL AUTO_INCREMENT,
    medicine_id        INT           NOT NULL,
    batch_number       VARCHAR(50)   NOT NULL,
    quantity_received  INT           NOT NULL,
    expiry_date        DATE          NOT NULL,
    supplier_id        INT           NOT NULL,
    purchase_price     DECIMAL(10,2) NOT NULL,
    date_received      DATE          NOT NULL,
    received_by_user_id INT          NULL,
    PRIMARY KEY (batch_id),
    UNIQUE KEY uq_batch_medicine_number (medicine_id, batch_number),
    KEY idx_batch_expiry (expiry_date),
    CONSTRAINT chk_batch_qty   CHECK (quantity_received > 0),
    CONSTRAINT chk_batch_price CHECK (purchase_price > 0),
    CONSTRAINT fk_batch_medicine FOREIGN KEY (medicine_id)        REFERENCES medicines (medicine_id),
    CONSTRAINT fk_batch_supplier FOREIGN KEY (supplier_id)        REFERENCES suppliers (supplier_id),
    CONSTRAINT fk_batch_user     FOREIGN KEY (received_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

CREATE TABLE stock_movement_types (
    movement_type_id   INT         NOT NULL AUTO_INCREMENT,
    movement_type_name VARCHAR(30) NOT NULL,
    PRIMARY KEY (movement_type_id),
    UNIQUE KEY uq_movement_type_name (movement_type_name)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 9 : MP07  -  PRESCRIPTIONS
-- =====================================================================

CREATE TABLE prescriptions (
    prescription_id      INT          NOT NULL AUTO_INCREMENT,
    prescription_code    VARCHAR(20)  NOT NULL,             -- PRE-YYYYMMDD-XXX
    consultation_id      INT          NOT NULL,
    general_instructions TEXT         NULL,
    issued_date          DATE         NOT NULL,
    valid_until          DATE         NOT NULL,             -- default issued + 30 days (app_settings)
    created_by_user_id   INT          NULL,
    created_at           DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (prescription_id),
    UNIQUE KEY uq_prescription_code (prescription_code),
    KEY idx_prescription_consult (consultation_id),
    CONSTRAINT chk_rx_validity CHECK (valid_until >= issued_date),
    CONSTRAINT fk_rx_consult FOREIGN KEY (consultation_id)    REFERENCES consultations (consultation_id),
    CONSTRAINT fk_rx_user    FOREIGN KEY (created_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

CREATE TABLE prescription_items (
    prescription_item_id INT          NOT NULL AUTO_INCREMENT,
    prescription_id      INT          NOT NULL,
    medicine_id          INT          NOT NULL,
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
-- SECTION 10 : MP09  -  DISPENSING
-- =====================================================================

-- Patient is derived: prescription -> consultation -> appointment -> patient.
-- Total amount is derived: SUM(dispensing_items.line_total).
-- Partial dispensing = several dispensing_records for one prescription.
CREATE TABLE dispensing_records (
    dispensing_id         INT         NOT NULL AUTO_INCREMENT,
    dispensing_code       VARCHAR(20) NOT NULL,             -- MS-YYYYMMDD-XXX
    prescription_id       INT         NOT NULL,
    payment_method_id     INT         NOT NULL,
    dispensed_by_user_id  INT         NULL,
    dispensed_at          DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (dispensing_id),
    UNIQUE KEY uq_dispensing_code (dispensing_code),
    KEY idx_dispensing_rx (prescription_id),
    CONSTRAINT fk_disp_rx     FOREIGN KEY (prescription_id)      REFERENCES prescriptions (prescription_id),
    CONSTRAINT fk_disp_method FOREIGN KEY (payment_method_id)    REFERENCES payment_methods (payment_method_id),
    CONSTRAINT fk_disp_user   FOREIGN KEY (dispensed_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- unit_price is a price SNAPSHOT at the time of sale (history, not a derived value).
CREATE TABLE dispensing_items (
    dispensing_item_id   INT           NOT NULL AUTO_INCREMENT,
    dispensing_id        INT           NOT NULL,
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
-- movement_type_id: 1 ADJUSTMENT (+/-, reason required), 2 DISPENSE (negative, linked to a dispensing item).
-- FIFO: dispensing consumes the batch with the oldest expiry_date first (one movement per batch used).
CREATE TABLE stock_movements (
    movement_id          INT          NOT NULL AUTO_INCREMENT,
    batch_id             INT          NOT NULL,
    movement_type_id     INT          NOT NULL,
    quantity_change      INT          NOT NULL,
    reason               VARCHAR(250) NULL,
    dispensing_item_id   INT          NULL,
    moved_at             DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    moved_by_user_id     INT          NULL,
    PRIMARY KEY (movement_id),
    KEY idx_move_batch (batch_id),
    CONSTRAINT chk_move_nonzero CHECK (quantity_change <> 0),
    CONSTRAINT chk_move_reason  CHECK (movement_type_id <> 1 OR reason IS NOT NULL),
    CONSTRAINT chk_move_dispense_neg CHECK (movement_type_id <> 2 OR quantity_change < 0),
    CONSTRAINT fk_move_batch    FOREIGN KEY (batch_id)           REFERENCES stock_batches (batch_id),
    CONSTRAINT fk_move_type     FOREIGN KEY (movement_type_id)   REFERENCES stock_movement_types (movement_type_id),
    CONSTRAINT fk_move_dispitem FOREIGN KEY (dispensing_item_id) REFERENCES dispensing_items (dispensing_item_id),
    CONSTRAINT fk_move_user     FOREIGN KEY (moved_by_user_id)   REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 11 : MP10  -  LABORATORY
-- =====================================================================

CREATE TABLE lab_categories (
    lab_category_id   INT         NOT NULL AUTO_INCREMENT,
    lab_category_name VARCHAR(60) NOT NULL,
    PRIMARY KEY (lab_category_id),
    UNIQUE KEY uq_lab_category_name (lab_category_name)
) ENGINE=InnoDB;

CREATE TABLE sample_types (
    sample_type_id   INT         NOT NULL AUTO_INCREMENT,
    sample_type_name VARCHAR(60) NOT NULL,
    PRIMARY KEY (sample_type_id),
    UNIQUE KEY uq_sample_type_name (sample_type_name)
) ENGINE=InnoDB;

CREATE TABLE urgency_levels (
    urgency_id   INT         NOT NULL AUTO_INCREMENT,
    urgency_name VARCHAR(20) NOT NULL,
    PRIMARY KEY (urgency_id),
    UNIQUE KEY uq_urgency_name (urgency_name)
) ENGINE=InnoDB;

CREATE TABLE lab_order_statuses (
    lab_status_id   INT         NOT NULL AUTO_INCREMENT,
    lab_status_name VARCHAR(30) NOT NULL,
    PRIMARY KEY (lab_status_id),
    UNIQUE KEY uq_lab_status_name (lab_status_name)
) ENGINE=InnoDB;

-- Normal range columns are strings so descriptive ranges (e.g. 'Negative') work too.
CREATE TABLE lab_tests (
    test_id               INT           NOT NULL AUTO_INCREMENT,
    test_code             VARCHAR(10)   NOT NULL,           -- TST-XXX
    test_name             VARCHAR(120)  NOT NULL,
    lab_category_id       INT           NOT NULL,
    sample_type_id        INT           NOT NULL,
    price                 DECIMAL(10,2) NOT NULL,
    normal_range_low      VARCHAR(40)   NULL,
    normal_range_high     VARCHAR(40)   NULL,
    unit                  VARCHAR(30)   NULL,
    turnaround_hours      INT           NOT NULL DEFAULT 24,
    requires_verification TINYINT(1)    NOT NULL DEFAULT 0,
    PRIMARY KEY (test_id),
    UNIQUE KEY uq_test_code (test_code),
    UNIQUE KEY uq_test_name (test_name),
    CONSTRAINT chk_test_price CHECK (price > 0),
    CONSTRAINT chk_test_tat   CHECK (turnaround_hours > 0),
    CONSTRAINT fk_test_category FOREIGN KEY (lab_category_id) REFERENCES lab_categories (lab_category_id),
    CONSTRAINT fk_test_sample   FOREIGN KEY (sample_type_id)  REFERENCES sample_types (sample_type_id)
) ENGINE=InnoDB;

CREATE TABLE lab_orders (
    order_id             INT      NOT NULL AUTO_INCREMENT,
    order_code           VARCHAR(20) NOT NULL,              -- LO-YYYYMMDD-XXX
    patient_id           INT      NOT NULL,
    test_id              INT      NOT NULL,
    consultation_id      INT      NULL,
    ordered_by_doctor_id INT      NULL,
    urgency_id           INT      NOT NULL,
    lab_status_id        INT      NOT NULL DEFAULT 1,       -- 1 Pending, 2 Completed, 3 Cancelled
    ordered_at           DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (order_id),
    UNIQUE KEY uq_lab_order_code (order_code),
    KEY idx_lab_order_status (lab_status_id),
    CONSTRAINT fk_laborder_patient  FOREIGN KEY (patient_id)           REFERENCES patients (patient_id),
    CONSTRAINT fk_laborder_test     FOREIGN KEY (test_id)              REFERENCES lab_tests (test_id),
    CONSTRAINT fk_laborder_consult  FOREIGN KEY (consultation_id)      REFERENCES consultations (consultation_id),
    CONSTRAINT fk_laborder_doctor   FOREIGN KEY (ordered_by_doctor_id) REFERENCES doctors (doctor_id),
    CONSTRAINT fk_laborder_urgency  FOREIGN KEY (urgency_id)           REFERENCES urgency_levels (urgency_id),
    CONSTRAINT fk_laborder_status   FOREIGN KEY (lab_status_id)        REFERENCES lab_order_statuses (lab_status_id)
) ENGINE=InnoDB;

-- is_abnormal is a SNAPSHOT of the comparison at entry time (catalog ranges may change later).
CREATE TABLE lab_results (
    result_id           INT          NOT NULL AUTO_INCREMENT,
    result_code         VARCHAR(20)  NOT NULL,              -- LR-YYYYMMDD-XXX
    order_id            INT          NOT NULL,              -- one result per order
    result_value        VARCHAR(100) NOT NULL,
    is_abnormal         TINYINT(1)   NOT NULL DEFAULT 0,
    findings            TEXT         NULL,
    tested_by_user_id   INT          NOT NULL,
    verified_by_user_id INT          NULL,
    test_datetime       DATETIME     NOT NULL,
    created_at          DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (result_id),
    UNIQUE KEY uq_result_code (result_code),
    UNIQUE KEY uq_result_order (order_id),
    CONSTRAINT fk_result_order    FOREIGN KEY (order_id)            REFERENCES lab_orders (order_id),
    CONSTRAINT fk_result_tested   FOREIGN KEY (tested_by_user_id)   REFERENCES users (user_id),
    CONSTRAINT fk_result_verified FOREIGN KEY (verified_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 12 : MP11 / MP12  -  BILLING & PAYMENTS
-- =====================================================================

CREATE TABLE bill_categories (
    bill_category_id   INT         NOT NULL AUTO_INCREMENT,
    bill_category_name VARCHAR(40) NOT NULL,
    PRIMARY KEY (bill_category_id),
    UNIQUE KEY uq_bill_category_name (bill_category_name)
) ENGINE=InnoDB;

CREATE TABLE bill_statuses (
    bill_status_id   INT         NOT NULL AUTO_INCREMENT,
    bill_status_name VARCHAR(30) NOT NULL,
    PRIMARY KEY (bill_status_id),
    UNIQUE KEY uq_bill_status_name (bill_status_name)
) ENGINE=InnoDB;

CREATE TABLE discount_types (
    discount_type_id   INT         NOT NULL AUTO_INCREMENT,
    discount_type_name VARCHAR(20) NOT NULL,
    PRIMARY KEY (discount_type_id),
    UNIQUE KEY uq_discount_type_name (discount_type_name)
) ENGINE=InnoDB;

CREATE TABLE room_types (
    room_type_id INT           NOT NULL AUTO_INCREMENT,
    room_type_name VARCHAR(50) NOT NULL,
    daily_rate   DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (room_type_id),
    UNIQUE KEY uq_room_type_name (room_type_name),
    CONSTRAINT chk_room_rate CHECK (daily_rate > 0)
) ENGINE=InnoDB;

-- Price list for Consultation / Procedure / Miscellaneous charges.
-- Medicine and Lab prices come from `medicines` and `lab_tests`; Room rates from `room_types`.
CREATE TABLE service_catalog (
    service_id       INT           NOT NULL AUTO_INCREMENT,
    bill_category_id INT           NOT NULL,
    service_name     VARCHAR(120)  NOT NULL,
    unit_price       DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (service_id),
    UNIQUE KEY uq_service_name (service_name),
    CONSTRAINT chk_service_price CHECK (unit_price > 0),
    CONSTRAINT fk_service_category FOREIGN KEY (bill_category_id) REFERENCES bill_categories (bill_category_id)
) ENGINE=InnoDB;

-- bill_code is assigned only when the bill is Finalized (NULL while Draft).
-- discount_value is a flat amount (type 1) or a percentage (type 2).
CREATE TABLE bills (
    bill_id            INT           NOT NULL AUTO_INCREMENT,
    bill_code          VARCHAR(20)   NULL,                  -- BILL-YYYYMMDD-XXX
    patient_id         INT           NOT NULL,
    bill_status_id     INT           NOT NULL DEFAULT 1,    -- 1 Draft, 2 Finalized
    discount_type_id   INT           NOT NULL DEFAULT 1,
    discount_value     DECIMAL(10,2) NOT NULL DEFAULT 0,
    tax_rate           DECIMAL(5,4)  NOT NULL DEFAULT 0.1200,
    authorized_by      VARCHAR(100)  NULL,                  -- required above discount threshold
    created_at         DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by_user_id INT           NULL,
    finalized_at       DATETIME      NULL,
    PRIMARY KEY (bill_id),
    UNIQUE KEY uq_bill_code (bill_code),
    KEY idx_bill_patient (patient_id),
    CONSTRAINT chk_bill_discount CHECK (discount_value >= 0),
    CONSTRAINT chk_bill_tax      CHECK (tax_rate >= 0),
    CONSTRAINT fk_bill_patient  FOREIGN KEY (patient_id)         REFERENCES patients (patient_id),
    CONSTRAINT fk_bill_status   FOREIGN KEY (bill_status_id)     REFERENCES bill_statuses (bill_status_id),
    CONSTRAINT fk_bill_discount FOREIGN KEY (discount_type_id)   REFERENCES discount_types (discount_type_id),
    CONSTRAINT fk_bill_user     FOREIGN KEY (created_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- Room charge = quantity (days) x unit_price (rate). Other categories use quantity 1 or units.
CREATE TABLE bill_items (
    bill_item_id     INT           NOT NULL AUTO_INCREMENT,
    bill_id          INT           NOT NULL,
    bill_category_id INT           NOT NULL,
    description      VARCHAR(200)  NOT NULL,
    quantity         INT           NOT NULL DEFAULT 1,
    unit_price       DECIMAL(10,2) NOT NULL,
    amount           DECIMAL(12,2) GENERATED ALWAYS AS (ROUND(quantity * unit_price, 2)) STORED,
    PRIMARY KEY (bill_item_id),
    KEY idx_billitem_bill (bill_id),
    CONSTRAINT chk_billitem_qty   CHECK (quantity > 0),
    CONSTRAINT chk_billitem_price CHECK (unit_price > 0),
    CONSTRAINT fk_billitem_bill     FOREIGN KEY (bill_id)          REFERENCES bills (bill_id) ON DELETE CASCADE,
    CONSTRAINT fk_billitem_category FOREIGN KEY (bill_category_id) REFERENCES bill_categories (bill_category_id)
) ENGINE=InnoDB;

-- Payment status (Outstanding / Partially Paid / Fully Paid) is derived: see v_bill_payment_summary.
CREATE TABLE payments (
    payment_id         INT           NOT NULL AUTO_INCREMENT,
    payment_code       VARCHAR(20)   NOT NULL,              -- PAY-YYYYMMDD-XXX
    bill_id            INT           NOT NULL,
    amount             DECIMAL(12,2) NOT NULL,
    payment_method_id  INT           NOT NULL,
    reference_number   VARCHAR(60)   NULL,                  -- required for Debit/Credit/Online (app rule)
    tendered_amount    DECIMAL(12,2) NULL,                  -- cash only; change = tendered - amount
    received_by_user_id INT          NOT NULL,
    payment_datetime   DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (payment_id),
    UNIQUE KEY uq_payment_code (payment_code),
    KEY idx_payment_bill (bill_id),
    CONSTRAINT chk_payment_amount CHECK (amount > 0),
    CONSTRAINT fk_payment_bill     FOREIGN KEY (bill_id)             REFERENCES bills (bill_id),
    CONSTRAINT fk_payment_method   FOREIGN KEY (payment_method_id)   REFERENCES payment_methods (payment_method_id),
    CONSTRAINT fk_payment_user     FOREIGN KEY (received_by_user_id) REFERENCES users (user_id)
) ENGINE=InnoDB;

-- =====================================================================
-- SECTION 13 : VIEWS (derived data - nothing here is stored twice)
-- =====================================================================

-- Remaining quantity per batch
CREATE VIEW v_batch_stock AS
SELECT  b.batch_id, b.medicine_id, b.batch_number, b.expiry_date,
        b.supplier_id, b.date_received, b.quantity_received,
        b.quantity_received + COALESCE(SUM(m.quantity_change), 0) AS quantity_remaining
FROM    stock_batches b
LEFT JOIN stock_movements m ON m.batch_id = b.batch_id
GROUP BY b.batch_id, b.medicine_id, b.batch_number, b.expiry_date,
         b.supplier_id, b.date_received, b.quantity_received;

-- Stock summary + alert flags per medicine (threshold from app_settings)
CREATE VIEW v_medicine_stock AS
SELECT  md.medicine_id, md.medicine_code, md.medicine_name, md.reorder_level,
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
GROUP BY md.medicine_id, md.medicine_code, md.medicine_name, md.reorder_level;

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
SELECT  dr.dispensing_id, dr.dispensing_code,
        COALESCE(SUM(di.line_total), 0) AS total_amount
FROM    dispensing_records dr
LEFT JOIN dispensing_items di ON di.dispensing_id = dr.dispensing_id
GROUP BY dr.dispensing_id, dr.dispensing_code;

CREATE VIEW v_bill_subtotals AS
SELECT  b.bill_id, COALESCE(SUM(bi.amount), 0) AS subtotal
FROM    bills b
LEFT JOIN bill_items bi ON bi.bill_id = b.bill_id
GROUP BY b.bill_id;

-- Order of calculation: Subtotal -> Discount -> Tax on post-discount -> Grand Total
CREATE VIEW v_bill_totals AS
SELECT  x.bill_id, x.bill_code, x.patient_id, x.bill_status_id, x.created_at,
        x.subtotal, x.discount_amount,
        (x.subtotal - x.discount_amount)                         AS taxable_amount,
        ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2)  AS tax_amount,
        (x.subtotal - x.discount_amount)
          + ROUND((x.subtotal - x.discount_amount) * x.tax_rate, 2) AS grand_total
FROM (
    SELECT  b.bill_id, b.bill_code, b.patient_id, b.bill_status_id, b.created_at, b.tax_rate,
            s.subtotal,
            ROUND(CASE WHEN b.discount_type_id = 2
                       THEN s.subtotal * b.discount_value / 100
                       ELSE b.discount_value END, 2) AS discount_amount
    FROM    bills b
    JOIN    v_bill_subtotals s ON s.bill_id = b.bill_id
) x;

-- Paid / balance / status per bill
CREATE VIEW v_bill_payment_summary AS
SELECT  t.bill_id, t.bill_code, t.patient_id, t.bill_status_id, t.grand_total,
        COALESCE(SUM(p.amount), 0) AS amount_paid,
        t.grand_total - COALESCE(SUM(p.amount), 0) AS balance,
        CASE WHEN t.bill_status_id = 1                   THEN 'Draft'
             WHEN COALESCE(SUM(p.amount), 0) = 0          THEN 'Outstanding'
             WHEN COALESCE(SUM(p.amount), 0) >= t.grand_total THEN 'Fully Paid'
             ELSE 'Partially Paid' END AS payment_status
FROM    v_bill_totals t
LEFT JOIN payments p ON p.bill_id = t.bill_id
GROUP BY t.bill_id, t.bill_code, t.patient_id, t.bill_status_id, t.grand_total;

-- =====================================================================
-- SECTION 14 : SEED DATA  -  reference / lookup tables
-- =====================================================================

SET @d   = DATE_FORMAT(CURDATE(), '%Y%m%d');
SET @d40 = DATE_FORMAT(DATE_SUB(CURDATE(), INTERVAL 40 DAY), '%Y%m%d');

INSERT INTO roles (role_id, role_name, description) VALUES
 (1, 'Administrator',  'Full access to every module'),
 (2, 'Receptionist',   'Patients, appointments, queue'),
 (3, 'Doctor',         'Consultations, prescriptions, lab orders'),
 (4, 'Pharmacist',     'Medicine catalog, stock, dispensing'),
 (5, 'Lab Technician', 'Laboratory catalog and result entry'),
 (6, 'Cashier',        'Billing and payments');

-- admin / Admin@123 ; every other user / Staff@123
INSERT INTO users (user_id, username, password_hash, password_salt, hash_iterations, full_name, email, role_id) VALUES
 (1, 'admin',        'rxTPG8MMJ0fMnvWp/anKrolWOIKxABgl6aPhON+Ggik=', 'NQAcdnvN5seAKh/1K7WCiQ==', 210000, 'System Administrator', 'admin@hospital.local',        1),
 (2, 'reception',    'kRjNnh2WX3LncVVB51RhB5EvqEN4gpkDWWK6G0aumSs=', 'm0HYUcL86xS8mJIq8JuiHw==', 210000, 'Rosa Dela Pena',        'reception@hospital.local',    2),
 (3, 'drvillanueva', 'Mh69TFZQTMlKs27n7C1U+VglS2wpra6LnXpU0N/MG0E=', '7AqjpMr5fQv14231hKUdHQ==', 210000, 'Dr. Ramon Villanueva',  'rvillanueva@hospital.local',  3),
 (4, 'pharmacist',   'GmyU1+qPoBFKB6vEyPj71nzgqIwmLT6a3m4A3UY8Km8=', '7U2ptyzzN2SWxp7jpsznUw==', 210000, 'Paolo Fernandez',       'pharmacy@hospital.local',     4),
 (5, 'labtech',      'qyUB6LpORtab9rG+lgulcb4kmIXKySg6rI0WtJRZff4=', 'lBk0O0Ee1xtl92gDoyodzA==', 210000, 'Lea Navarro',           'lab@hospital.local',          5),
 (6, 'cashier',      'ROZUR4Nn0WEPSAtU/RTaNUb897ANpukbFoJSAMgon7A=', 'KsjjZaQUz9kHDk1ZtpYHXg==', 210000, 'Mario Bautista',        'cashier@hospital.local',      6);

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

INSERT INTO genders (gender_id, gender_name) VALUES (1,'Male'),(2,'Female'),(3,'Other');

INSERT INTO blood_types (blood_type_id, blood_type_name) VALUES
 (1,'A+'),(2,'A-'),(3,'B+'),(4,'B-'),(5,'AB+'),(6,'AB-'),(7,'O+'),(8,'O-');

INSERT INTO relationships (relationship_id, relationship_name) VALUES
 (1,'Spouse'),(2,'Parent'),(3,'Child'),(4,'Sibling'),(5,'Relative'),(6,'Friend'),(7,'Guardian'),(8,'Other');

INSERT INTO provinces (province_id, province_name) VALUES
 (1,'Metro Manila'),(2,'Cebu'),(3,'Davao del Sur'),(4,'Cavite'),(5,'Laguna'),(6,'Bulacan');

INSERT INTO cities (city_id, province_id, city_name) VALUES
 (1,1,'Manila'),(2,1,'Quezon City'),(3,1,'Makati'),(4,1,'Taguig'),(5,1,'Pasig'),(6,1,'Mandaluyong'),(7,1,'Pasay'),(8,1,'Caloocan'),
 (9,2,'Cebu City'),(10,2,'Mandaue'),(11,2,'Lapu-Lapu'),
 (12,3,'Davao City'),
 (13,4,'Bacoor'),(14,4,'Imus'),(15,4,'Dasmarinas'),
 (16,5,'Calamba'),(17,5,'Santa Rosa'),
 (18,6,'Malolos');

INSERT INTO payment_methods (payment_method_id, method_name, requires_reference) VALUES
 (1,'Cash',0),(2,'Debit',1),(3,'Credit',1),(4,'Online',1),(5,'Cheque',0);

INSERT INTO specializations (specialization_id, specialization_name) VALUES
 (1,'General Medicine'),(2,'Pediatrics'),(3,'Cardiology'),(4,'Orthopedics'),(5,'Dermatology'),
 (6,'Obstetrics & Gynecology'),(7,'Ear, Nose & Throat'),(8,'Neurology'),(9,'Ophthalmology'),(10,'Psychiatry');

INSERT INTO departments (department_id, department_name) VALUES
 (1,'Outpatient'),(2,'Internal Medicine'),(3,'Pediatrics'),(4,'Cardiology'),(5,'Orthopedics'),
 (6,'Dermatology'),(7,'OB-GYN'),(8,'ENT'),(9,'Emergency');

INSERT INTO employment_types (employment_type_id, employment_type_name) VALUES
 (1,'Full-time'),(2,'Part-time'),(3,'Visiting');

INSERT INTO appointment_types (appointment_type_id, appointment_type_name) VALUES
 (1,'New Consultation'),(2,'Follow-up');

INSERT INTO appointment_statuses (status_id, status_name) VALUES
 (1,'Booked'),(2,'Completed'),(3,'Cancelled'),(4,'No-show');

INSERT INTO queue_statuses (queue_status_id, queue_status_name) VALUES
 (1,'Waiting'),(2,'In Progress'),(3,'Completed'),(4,'Skipped');

INSERT INTO medicine_categories (category_id, category_name) VALUES
 (1,'Analgesic'),(2,'Antibiotic'),(3,'Antihistamine'),(4,'Antihypertensive'),(5,'Antidiabetic'),
 (6,'Gastrointestinal'),(7,'Respiratory'),(8,'Vitamins & Supplements'),(9,'Rehydration');

INSERT INTO manufacturers (manufacturer_id, manufacturer_name) VALUES
 (1,'United Laboratories'),(2,'Pascual Laboratories'),(3,'Pfizer Philippines'),(4,'GlaxoSmithKline PH'),(5,'Ritemed Philippines');

INSERT INTO unit_types (unit_type_id, unit_type_name) VALUES
 (1,'Tablet'),(2,'Capsule'),(3,'Syrup'),(4,'Injection'),(5,'Sachet'),(6,'Cream'),(7,'Drops');

INSERT INTO suppliers (supplier_id, supplier_name) VALUES
 (1,'MediSupply Corp'),(2,'PharmaDirect Inc'),(3,'HealthFirst Distributors');

INSERT INTO stock_movement_types (movement_type_id, movement_type_name) VALUES
 (1,'ADJUSTMENT'),(2,'DISPENSE');

INSERT INTO lab_categories (lab_category_id, lab_category_name) VALUES
 (1,'Hematology'),(2,'Clinical Chemistry'),(3,'Urinalysis'),(4,'Serology'),(5,'Cardiac Markers');

INSERT INTO sample_types (sample_type_id, sample_type_name) VALUES
 (1,'Whole Blood (EDTA)'),(2,'Serum'),(3,'Urine'),(4,'Stool'),(5,'Swab');

INSERT INTO urgency_levels (urgency_id, urgency_name) VALUES (1,'Routine'),(2,'Urgent'),(3,'STAT');

INSERT INTO lab_order_statuses (lab_status_id, lab_status_name) VALUES (1,'Pending'),(2,'Completed'),(3,'Cancelled');

INSERT INTO bill_categories (bill_category_id, bill_category_name) VALUES
 (1,'Consultation'),(2,'Medicine'),(3,'Lab Test'),(4,'Room'),(5,'Procedure'),(6,'Miscellaneous');

INSERT INTO bill_statuses (bill_status_id, bill_status_name) VALUES (1,'Draft'),(2,'Finalized');

INSERT INTO discount_types (discount_type_id, discount_type_name) VALUES (1,'Flat'),(2,'Percent');

INSERT INTO room_types (room_type_id, room_type_name, daily_rate) VALUES
 (1,'Ward',1200.00),(2,'Semi-Private',2500.00),(3,'Private',4500.00),(4,'ICU',9000.00);

INSERT INTO service_catalog (service_id, bill_category_id, service_name, unit_price) VALUES
 (1,1,'General Consultation',500.00),
 (2,1,'Specialist Consultation',1000.00),
 (3,1,'Follow-up Consultation',300.00),
 (4,1,'Emergency Room Consultation',1500.00),
 (5,5,'Wound Suturing',1800.00),
 (6,5,'ECG (12-lead)',600.00),
 (7,5,'Nebulization',350.00),
 (8,5,'Minor Excision',2500.00),
 (9,5,'Chest X-Ray (PA)',700.00),
 (10,6,'Medical Certificate',150.00),
 (11,6,'Administrative Fee',100.00),
 (12,6,'Medical Supplies Kit',250.00);

-- =====================================================================
-- SECTION 15 : SEED DATA  -  patients (MP01 / MP02)
-- =====================================================================

INSERT INTO patients
 (patient_id, patient_code, first_name, middle_name, last_name, date_of_birth, gender_id, blood_type_id,
  phone_number, email, street_address, city_id, postal_code, allergies, medical_history, registered_by_user_id) VALUES
 (1,  CONCAT('PAT-',@d,'-001'), 'Maria',    'Clara',   'Santos',    '1990-03-14', 2, 1, '0917-123-4567', 'maria.santos@example.com',  '12 Rizal St., Brgy. Central',  4,  '1630', 'Penicillin',          'Asthma (childhood)', 2),
 (2,  CONCAT('PAT-',@d,'-002'), 'Juan',     NULL,      'Dela Cruz', '1985-07-22', 1, 7, '0918-234-5678', 'juan.delacruz@example.com', '45 Mabini Ave.',            2,  '1100', NULL,                  'Hypertension',       2),
 (3,  CONCAT('PAT-',@d,'-003'), 'Marco',    'Antonio', 'Reyes',     '1978-11-02', 1, 3, '0919-345-6789', 'marco.reyes@example.com',   '8 Ayala Ave.',              3,  '1226', 'Shellfish',           'High cholesterol',   2),
 (4,  CONCAT('PAT-',@d,'-004'), 'Marianne', NULL,      'Lopez',     '1995-01-30', 2, 5, '0920-456-7890', NULL,                        '23 Ortigas Ext.',           5,  '1605', NULL,                  NULL,                 2),
 (5,  CONCAT('PAT-',@d,'-005'), 'Pedro',    NULL,      'Garcia',    '1962-05-18', 1, 8, '0921-567-8901', 'pedro.garcia@example.com',  '77 Colon St.',              9,  '6000', 'Sulfa drugs',         'Type 2 diabetes',    2),
 (6,  CONCAT('PAT-',@d,'-006'), 'Ana Liza','Cruz',     'Mendoza',   '2001-09-09', 2, 2, '0922-678-9012', 'analiza.m@example.com',     '5 Katipunan Ave.',          2,  '1108', NULL,                  NULL,                 2),
 (7,  CONCAT('PAT-',@d,'-007'), 'Carlo',    NULL,      'Bautista',  '2015-04-12', 1, 4, '0923-789-0123', NULL,                        '31 Sampaguita St.',         7,  '1300', 'Peanuts',             NULL,                 2),
 (8,  CONCAT('PAT-',@d,'-008'), 'Teresita', 'Lim',     'Ramos',     '1950-12-25', 2, 6, '0924-890-1234', NULL,                        '9 Taft Ave.',               1,  '1000', NULL,                  'Osteoarthritis',     2),
 (9,  CONCAT('PAT-',@d,'-009'), 'Luis',     'Fernando','Aquino',    '1999-06-06', 1, 7, '0925-901-2345', 'luis.aquino@example.com',   '60 Emerald Dr.',            12, '8000', NULL,                  NULL,                 2),
 (10, CONCAT('PAT-',@d,'-010'), 'Sofia',    NULL,      'Castillo',  '2010-08-21', 2, 1, '0926-012-3456', NULL,                        '14 Heritage Rd.',           16, '4027', 'Dust mites',          'Allergic rhinitis',  2);

INSERT INTO patient_emergency_contacts (patient_id, contact_name, contact_phone, relationship_id) VALUES
 (1,  'Roberto Santos',     '0917-111-0001', 1),
 (2,  'Elena Dela Cruz',    '0918-111-0002', 1),
 (3,  'Carmen Reyes',       '0919-111-0003', 1),
 (4,  'Jose Lopez',         '0920-111-0004', 2),
 (5,  'Lourdes Garcia',     '0921-111-0005', 1),
 (6,  'Marites Mendoza',    '0922-111-0006', 2),
 (7,  'Ricardo Bautista',   '0923-111-0007', 2),
 (8,  'Daniel Ramos',       '0924-111-0008', 3),
 (9,  'Patricia Aquino',    '0925-111-0009', 4),
 (10, 'Angelica Castillo',  '0926-111-0010', 2);

INSERT INTO patient_audit_log (patient_id, field_changed, old_value, new_value, changed_by_user_id, changed_at) VALUES
 (2, 'phone_number',   '0918-234-0000', '0918-234-5678', 2, DATE_SUB(NOW(), INTERVAL 3 HOUR)),
 (2, 'street_address', '40 Mabini Ave.', '45 Mabini Ave.', 2, DATE_SUB(NOW(), INTERVAL 3 HOUR));

-- =====================================================================
-- SECTION 16 : SEED DATA  -  doctors, schedules, leaves (MP03)
-- =====================================================================

INSERT INTO doctors
 (doctor_id, doctor_code, first_name, last_name, license_number, specialization_id, department_id, phone, email, consultation_fee, employment_type_id) VALUES
 (1, 'DOC-001', 'Ramon',  'Villanueva', 'PRC-0012345', 1, 2, '0917-555-0001', 'rvillanueva@hospital.local', 500.00,  1),
 (2, 'DOC-002', 'Maria',  'Santos',     'PRC-0023456', 2, 3, '0917-555-0002', 'msantos@hospital.local',     600.00,  1),
 (3, 'DOC-003', 'Jose',   'Reyes',      'PRC-0034567', 3, 4, '0917-555-0003', 'jreyes@hospital.local',      1200.00, 2),
 (4, 'DOC-004', 'Angela', 'Cruz',       'PRC-0045678', 5, 6, '0917-555-0004', 'acruz@hospital.local',       900.00,  3),
 (5, 'DOC-005', 'Paolo',  'Mendoza',    'PRC-0056789', 4, 5, '0917-555-0005', 'pmendoza@hospital.local',    1000.00, 1);

INSERT INTO doctor_schedules (doctor_id, day_of_week, start_time, end_time, consultation_minutes, notes) VALUES
 -- Dr. Villanueva: Mon-Fri, morning + afternoon, 15-minute slots
 (1,1,'08:00','12:00',15,'Morning clinic'),   (1,1,'13:00','17:00',15,'Afternoon clinic'),
 (1,2,'08:00','12:00',15,'Morning clinic'),   (1,2,'13:00','17:00',15,'Afternoon clinic'),
 (1,3,'08:00','12:00',15,'Morning clinic'),   (1,3,'13:00','17:00',15,'Afternoon clinic'),
 (1,4,'08:00','12:00',15,'Morning clinic'),   (1,4,'13:00','17:00',15,'Afternoon clinic'),
 (1,5,'08:00','12:00',15,'Morning clinic'),   (1,5,'13:00','17:00',15,'Afternoon clinic'),
 -- Dr. Santos: Mon / Wed / Fri, 20-minute slots
 (2,1,'09:00','12:00',20,NULL), (2,1,'14:00','17:00',20,NULL),
 (2,3,'09:00','12:00',20,NULL), (2,3,'14:00','17:00',20,NULL),
 (2,5,'09:00','12:00',20,NULL), (2,5,'14:00','17:00',20,NULL),
 -- Dr. Reyes: Tue / Thu, 30-minute slots
 (3,2,'10:00','16:00',30,'Cardiology clinic'),
 (3,4,'10:00','16:00',30,'Cardiology clinic'),
 -- Dr. Cruz (visiting): Wed afternoon + Sat
 (4,3,'13:00','17:00',20,'Visiting hours'),
 (4,6,'09:00','15:00',20,'Saturday clinic'),
 -- Dr. Mendoza: Mon, Tue, Thu, Fri, 30-minute slots
 (5,1,'08:00','14:00',30,NULL), (5,2,'08:00','14:00',30,NULL),
 (5,4,'08:00','14:00',30,NULL), (5,5,'08:00','14:00',30,NULL);

INSERT INTO doctor_leaves (doctor_id, leave_date, reason) VALUES
 (1, DATE_ADD(CURDATE(), INTERVAL 7 DAY),  'Medical conference'),
 (3, DATE_ADD(CURDATE(), INTERVAL 14 DAY), 'Annual leave');

-- =====================================================================
-- SECTION 17 : SEED DATA  -  appointments (MP04)
-- =====================================================================
-- Appointments 1-8 are for TODAY so the dashboard, queue and consultation
-- screens have data on any day you import. Appointment 9 is 40 days ago.
-- (Seed rows are inserted directly, so they are not checked against the
--  doctor's weekday schedule.)

INSERT INTO appointments
 (appointment_id, appointment_code, token_number, patient_id, doctor_id, appointment_date, time_slot,
  appointment_type_id, reason_for_visit, status_id, status_remarks, status_updated_at, created_by_user_id) VALUES
 (1, CONCAT('APT-',@d,'-001'), 1, 1, 1, CURDATE(), '08:00', 1, 'Fever and cough',          2, NULL, NOW(), 2),
 (2, CONCAT('APT-',@d,'-002'), 2, 2, 1, CURDATE(), '08:15', 1, 'Hypertension check',       2, NULL, NOW(), 2),
 (3, CONCAT('APT-',@d,'-003'), 3, 3, 1, CURDATE(), '08:30', 1, 'Chest discomfort',         1, NULL, NULL,  2),
 (4, CONCAT('APT-',@d,'-004'), 4, 4, 1, CURDATE(), '08:45', 2, 'Review of lab results',    1, NULL, NULL,  2),
 (5, CONCAT('APT-',@d,'-005'), 1, 5, 3, CURDATE(), '10:00', 1, 'Palpitations',             1, NULL, NULL,  2),
 (6, CONCAT('APT-',@d,'-006'), 2, 6, 3, CURDATE(), '10:30', 2, 'Follow-up ECG',            1, NULL, NULL,  2),
 (7, CONCAT('APT-',@d,'-007'), 1, 7, 5, CURDATE(), '08:00', 1, 'Ankle sprain',             1, NULL, NULL,  2),
 (8, CONCAT('APT-',@d,'-008'), 2, 8, 5, CURDATE(), '08:30', 1, 'Knee pain',                3, 'Patient requested cancellation', NOW(), 2),
 (9, CONCAT('APT-',@d40,'-001'), 1, 5, 1, DATE_SUB(CURDATE(), INTERVAL 40 DAY), '09:00', 1, 'Diabetes monitoring', 2, NULL, DATE_SUB(NOW(), INTERVAL 40 DAY), 2);

-- =====================================================================
-- SECTION 18 : SEED DATA  -  queue board (MP05)
-- =====================================================================
-- Doctor 1: tokens 1-2 done, token 3 in progress, token 4 waiting, token 5 = emergency walk-in
-- Doctor 3: tokens 1-2 waiting (token 2 was skipped once)    Doctor 5: token 1 waiting

INSERT INTO queue_entries
 (queue_id, queue_date, token_number, patient_id, doctor_id, appointment_id, queue_status_id, is_emergency,
  checked_in_at, started_at, completed_at) VALUES
 (1, CURDATE(), 1, 1, 1, 1,    3, 0, DATE_SUB(NOW(), INTERVAL 70 MINUTE), DATE_SUB(NOW(), INTERVAL 60 MINUTE), DATE_SUB(NOW(), INTERVAL 45 MINUTE)),
 (2, CURDATE(), 2, 2, 1, 2,    3, 0, DATE_SUB(NOW(), INTERVAL 65 MINUTE), DATE_SUB(NOW(), INTERVAL 45 MINUTE), DATE_SUB(NOW(), INTERVAL 28 MINUTE)),
 (3, CURDATE(), 3, 3, 1, 3,    2, 0, DATE_SUB(NOW(), INTERVAL 40 MINUTE), DATE_SUB(NOW(), INTERVAL 10 MINUTE), NULL),
 (4, CURDATE(), 4, 4, 1, 4,    1, 0, DATE_SUB(NOW(), INTERVAL 30 MINUTE), NULL, NULL),
 (5, CURDATE(), 5, 9, 1, NULL, 1, 1, DATE_SUB(NOW(), INTERVAL 5 MINUTE),  NULL, NULL),
 (6, CURDATE(), 1, 5, 3, 5,    1, 0, DATE_SUB(NOW(), INTERVAL 25 MINUTE), NULL, NULL),
 (7, CURDATE(), 2, 6, 3, 6,    1, 0, DATE_SUB(NOW(), INTERVAL 20 MINUTE), NULL, NULL),
 (8, CURDATE(), 1, 7, 5, 7,    1, 0, DATE_SUB(NOW(), INTERVAL 15 MINUTE), NULL, NULL);

INSERT INTO queue_skip_log (queue_id, reason, skipped_at, skipped_by_user_id) VALUES
 (7, 'Patient stepped out of the waiting area', DATE_SUB(NOW(), INTERVAL 12 MINUTE), 2);

-- =====================================================================
-- SECTION 19 : SEED DATA  -  consultations (MP06)
-- =====================================================================

INSERT INTO consultations
 (consultation_id, consultation_code, appointment_id, systolic_bp, diastolic_bp, temperature_c, pulse_rate_bpm,
  weight_kg, height_cm, chief_complaint, examination_findings, diagnosis, treatment_plan, special_instructions,
  next_follow_up_date, recorded_at, recorded_by_user_id) VALUES
 (1, CONCAT('CON-',@d,'-001'), 1, 118, 76, 38.2, 92, 58.5, 160.0,
  'Fever and productive cough for 3 days', 'Throat erythema, clear lungs', 'Acute upper respiratory tract infection',
  'Symptomatic treatment, increase fluids', 'Rest and hydrate; return if fever persists beyond 3 days',
  DATE_ADD(CURDATE(), INTERVAL 7 DAY), DATE_SUB(NOW(), INTERVAL 50 MINUTE), 3),
 (2, CONCAT('CON-',@d,'-002'), 2, 150, 95, 36.8, 78, 82.0, 172.0,
  'Routine hypertension monitoring', 'No edema, regular heart rhythm', 'Essential hypertension, uncontrolled',
  'Start antihypertensive regimen, low-sodium diet', 'Monitor BP daily and keep a log',
  DATE_ADD(CURDATE(), INTERVAL 14 DAY), DATE_SUB(NOW(), INTERVAL 30 MINUTE), 3),
 (3, CONCAT('CON-',@d40,'-001'), 9, 138, 88, 36.6, 80, 79.0, 168.0,
  'Diabetes monitoring', 'Foot exam normal', 'Type 2 diabetes mellitus',
  'Continue metformin, diet counselling', 'Fasting blood sugar monthly',
  NULL, DATE_SUB(NOW(), INTERVAL 40 DAY), 3);

-- =====================================================================
-- SECTION 20 : SEED DATA  -  medicines & stock (MP08)
-- =====================================================================

INSERT INTO medicines
 (medicine_id, medicine_code, medicine_name, generic_name, category_id, manufacturer_id, unit_type_id, unit_price, reorder_level, storage_instructions) VALUES
 (1,  'MED-001', 'Paracetamol 500mg',       'Paracetamol',        1, 1, 1, 3.50,   200, 'Store below 30C, dry place'),
 (2,  'MED-002', 'Amoxicillin 500mg',       'Amoxicillin',        2, 5, 2, 12.00,  150, 'Store below 25C'),
 (3,  'MED-003', 'Cetirizine 10mg',         'Cetirizine',         3, 1, 1, 8.00,   100, 'Store below 30C'),
 (4,  'MED-004', 'Amlodipine 5mg',          'Amlodipine',         4, 3, 1, 10.00,  100, 'Store below 30C, away from light'),
 (5,  'MED-005', 'Losartan 50mg',           'Losartan Potassium', 4, 2, 1, 15.00,  100, 'Store below 30C'),
 (6,  'MED-006', 'Metformin 500mg',         'Metformin HCl',      5, 5, 1, 6.00,   150, 'Store below 30C'),
 (7,  'MED-007', 'Omeprazole 20mg',         'Omeprazole',         6, 4, 2, 11.00,  80,  'Store below 25C, keep dry'),
 (8,  'MED-008', 'Salbutamol Syrup 2mg/5mL','Salbutamol',         7, 4, 3, 95.00,  20,  'Store below 25C'),
 (9,  'MED-009', 'Ibuprofen 400mg',         'Ibuprofen',          1, 1, 1, 7.50,   120, 'Store below 30C'),
 (10, 'MED-010', 'Ceftriaxone 1g Vial',     'Ceftriaxone',        2, 3, 4, 180.00, 30,  'Store below 25C, protect from light'),
 (11, 'MED-011', 'Oral Rehydration Salts',  'ORS',                9, 2, 5, 12.00,  50,  'Store in a cool, dry place'),
 (12, 'MED-012', 'Vitamin C 500mg',         'Ascorbic Acid',      8, 1, 1, 4.00,   200, 'Store below 30C');

-- Alert mix: Normal (1,2,3,5,11), Low (4,7,12), Expiring Soon (6,10), Expired (8,9)
INSERT INTO stock_batches
 (batch_id, medicine_id, batch_number, quantity_received, expiry_date, supplier_id, purchase_price, date_received, received_by_user_id) VALUES
 (1,  1,  'PCM-2601', 500, DATE_ADD(CURDATE(), INTERVAL 365 DAY), 1, 2.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (2,  2,  'AMX-2601', 300, DATE_ADD(CURDATE(), INTERVAL 300 DAY), 2, 7.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (3,  3,  'CTZ-2601', 250, DATE_ADD(CURDATE(), INTERVAL 400 DAY), 1, 4.50,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (4,  4,  'AML-2601',  40, DATE_ADD(CURDATE(), INTERVAL 200 DAY), 3, 5.50,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (5,  5,  'LSR-2601', 220, DATE_ADD(CURDATE(), INTERVAL 250 DAY), 3, 8.50,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (6,  6,  'MET-2509', 180, DATE_ADD(CURDATE(), INTERVAL 45 DAY),  2, 3.00,   DATE_SUB(CURDATE(), INTERVAL 150 DAY), 4),
 (7,  7,  'OMP-2601',  60, DATE_ADD(CURDATE(), INTERVAL 180 DAY), 2, 6.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (8,  8,  'SAL-2506',  15, DATE_SUB(CURDATE(), INTERVAL 15 DAY),  1, 55.00,  DATE_SUB(CURDATE(), INTERVAL 220 DAY), 4),
 (9,  8,  'SAL-2601',  30, DATE_ADD(CURDATE(), INTERVAL 300 DAY), 1, 56.00,  DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (10, 9,  'IBU-2506', 100, DATE_SUB(CURDATE(), INTERVAL 10 DAY),  3, 4.00,   DATE_SUB(CURDATE(), INTERVAL 230 DAY), 4),
 (11, 10, 'CFT-2603',  25, DATE_ADD(CURDATE(), INTERVAL 50 DAY),  2, 110.00, DATE_SUB(CURDATE(), INTERVAL 120 DAY), 4),
 (12, 10, 'CFT-2605',  80, DATE_ADD(CURDATE(), INTERVAL 400 DAY), 2, 112.00, DATE_SUB(CURDATE(), INTERVAL 20 DAY),  4),
 (13, 11, 'ORS-2601', 200, DATE_ADD(CURDATE(), INTERVAL 500 DAY), 1, 7.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4),
 (14, 12, 'VTC-2601',  90, DATE_ADD(CURDATE(), INTERVAL 200 DAY), 1, 2.00,   DATE_SUB(CURDATE(), INTERVAL 30 DAY),  4);

INSERT INTO stock_movements (batch_id, movement_type_id, quantity_change, reason, moved_at, moved_by_user_id) VALUES
 (1, 1, -10, 'Damaged packaging', DATE_SUB(NOW(), INTERVAL 5 DAY), 4),
 (13, 1, -4, 'Lost during inventory count', DATE_SUB(NOW(), INTERVAL 2 DAY), 4);

-- =====================================================================
-- SECTION 21 : SEED DATA  -  prescriptions (MP07)
-- =====================================================================
-- Rx 1: fully fillable        Rx 2: Amlodipine needs 60, only 40 in stock (partial)
-- Rx 3: EXPIRED (valid_until 10 days ago) - must be blocked at dispensing

INSERT INTO prescriptions
 (prescription_id, prescription_code, consultation_id, general_instructions, issued_date, valid_until, created_by_user_id) VALUES
 (1, CONCAT('PRE-',@d,'-001'),   1, 'Take after meals. Drink plenty of fluids.',  CURDATE(), DATE_ADD(CURDATE(), INTERVAL 30 DAY), 3),
 (2, CONCAT('PRE-',@d,'-002'),   2, 'Take in the morning. Monitor blood pressure daily.', CURDATE(), DATE_ADD(CURDATE(), INTERVAL 30 DAY), 3),
 (3, CONCAT('PRE-',@d40,'-001'), 3, 'Take with meals.', DATE_SUB(CURDATE(), INTERVAL 40 DAY), DATE_SUB(CURDATE(), INTERVAL 10 DAY), 3);

INSERT INTO prescription_items
 (prescription_item_id, prescription_id, medicine_id, dosage, frequency_per_day, duration_days, instructions) VALUES
 (1, 1, 1,  '500mg', 3, 5,  'Every 8 hours as needed for fever'),
 (2, 1, 3,  '10mg',  1, 5,  'At bedtime'),
 (3, 1, 12, '500mg', 1, 7,  'After breakfast'),
 (4, 2, 4,  '5mg',   1, 60, 'Once daily, morning'),
 (5, 2, 5,  '50mg',  1, 30, 'Once daily, evening'),
 (6, 3, 6,  '500mg', 2, 30, 'After meals'),
 (7, 3, 7,  '20mg',  1, 14, 'Before breakfast');

-- =====================================================================
-- SECTION 22 : SEED DATA  -  laboratory (MP10)
-- =====================================================================

INSERT INTO lab_tests
 (test_id, test_code, test_name, lab_category_id, sample_type_id, price, normal_range_low, normal_range_high, unit, turnaround_hours, requires_verification) VALUES
 (1,  'TST-001', 'Hemoglobin',                    1, 1, 150.00, '12.0',     '17.5',     'g/dL',     4, 0),
 (2,  'TST-002', 'Fasting Blood Sugar',           2, 2, 120.00, '70',       '100',      'mg/dL',    4, 0),
 (3,  'TST-003', 'Total Cholesterol',             2, 2, 250.00, '0',        '200',      'mg/dL',    6, 0),
 (4,  'TST-004', 'Creatinine',                    2, 2, 200.00, '0.6',      '1.3',      'mg/dL',    6, 0),
 (5,  'TST-005', 'White Blood Cell Count',        1, 1, 150.00, '4.5',      '11.0',     'x10^3/uL', 4, 0),
 (6,  'TST-006', 'Urinalysis',                    3, 3, 100.00, 'Negative', 'Negative', NULL,       2, 0),
 (7,  'TST-007', 'Dengue NS1 Antigen',            4, 2, 800.00, 'Negative', 'Negative', NULL,       2, 1),
 (8,  'TST-008', 'Hepatitis B Surface Antigen',   4, 2, 450.00, 'Non-reactive', 'Non-reactive', NULL, 3, 1),
 (9,  'TST-009', 'Potassium',                     2, 2, 180.00, '3.5',      '5.0',      'mmol/L',   6, 0),
 (10, 'TST-010', 'Troponin I',                    5, 2, 900.00, '0',        '0.04',     'ng/mL',    2, 1);

-- Orders 1 = completed (abnormal result), 2-7 = pending
INSERT INTO lab_orders
 (order_id, order_code, patient_id, test_id, consultation_id, ordered_by_doctor_id, urgency_id, lab_status_id, ordered_at) VALUES
 (1, CONCAT('LO-',@d,'-001'), 1, 5,  1,    1, 2, 2, DATE_SUB(NOW(), INTERVAL 45 MINUTE)),
 (2, CONCAT('LO-',@d,'-002'), 2, 3,  2,    1, 1, 1, DATE_SUB(NOW(), INTERVAL 25 MINUTE)),
 (3, CONCAT('LO-',@d,'-003'), 2, 2,  2,    1, 1, 1, DATE_SUB(NOW(), INTERVAL 25 MINUTE)),
 (4, CONCAT('LO-',@d,'-004'), 3, 10, NULL, 1, 3, 1, DATE_SUB(NOW(), INTERVAL 8 MINUTE)),
 (5, CONCAT('LO-',@d,'-005'), 4, 1,  NULL, 1, 1, 1, DATE_SUB(NOW(), INTERVAL 20 MINUTE)),
 (6, CONCAT('LO-',@d,'-006'), 5, 7,  NULL, 3, 2, 1, DATE_SUB(NOW(), INTERVAL 15 MINUTE)),
 (7, CONCAT('LO-',@d,'-007'), 6, 6,  NULL, 3, 1, 1, DATE_SUB(NOW(), INTERVAL 10 MINUTE));

INSERT INTO lab_results
 (result_id, result_code, order_id, result_value, is_abnormal, findings, tested_by_user_id, verified_by_user_id, test_datetime) VALUES
 (1, CONCAT('LR-',@d,'-001'), 1, '13.2', 1, 'Mild leukocytosis, consistent with infection', 5, NULL, DATE_SUB(NOW(), INTERVAL 30 MINUTE));

-- =====================================================================
-- SECTION 23 : SEED DATA  -  bills & payments (MP11 / MP12)
-- =====================================================================
-- Expected totals (12% tax on post-discount amount):
--  Bill 1  702.50 -> 786.80   paid in full             => Fully Paid
--  Bill 2 1870.00 -10% -> 1884.96  paid 1000            => Partially Paid
--  Bill 3 2500.00 -> 2800.00  no payments               => Outstanding
--  Bill 4 10280.00 -25% (authorized) -> 8635.20 paid 5000 => Partially Paid
--  Bill 5  450.00 -> 504.00   paid in full (cash, change) => Fully Paid
--  Bill 6  700.00 -50 flat -> 728.00 paid 300           => Partially Paid
--  Bill 7  Draft (no bill code, no payments)

INSERT INTO bills
 (bill_id, bill_code, patient_id, bill_status_id, discount_type_id, discount_value, tax_rate, authorized_by,
  created_at, created_by_user_id, finalized_at) VALUES
 (1, CONCAT('BILL-',@d,'-001'), 1, 2, 1, 0.00,  0.1200, NULL, DATE_SUB(NOW(), INTERVAL 40 MINUTE), 6, DATE_SUB(NOW(), INTERVAL 35 MINUTE)),
 (2, CONCAT('BILL-',@d,'-002'), 2, 2, 2, 10.00, 0.1200, NULL, DATE_SUB(NOW(), INTERVAL 30 MINUTE), 6, DATE_SUB(NOW(), INTERVAL 25 MINUTE)),
 (3, CONCAT('BILL-',@d,'-003'), 3, 2, 1, 0.00,  0.1200, NULL, DATE_SUB(NOW(), INTERVAL 20 MINUTE), 6, DATE_SUB(NOW(), INTERVAL 15 MINUTE)),
 (4, CONCAT('BILL-',@d,'-004'), 4, 2, 2, 25.00, 0.1200, 'Dr. Ramon Villanueva', DATE_SUB(NOW(), INTERVAL 3 HOUR), 6, DATE_SUB(NOW(), INTERVAL 170 MINUTE)),
 (5, CONCAT('BILL-',@d,'-005'), 5, 2, 1, 0.00,  0.1200, NULL, DATE_SUB(NOW(), INTERVAL 2 HOUR), 6, DATE_SUB(NOW(), INTERVAL 110 MINUTE)),
 (6, CONCAT('BILL-',@d,'-006'), 6, 2, 1, 50.00, 0.1200, NULL, DATE_SUB(NOW(), INTERVAL 90 MINUTE), 6, DATE_SUB(NOW(), INTERVAL 85 MINUTE)),
 (7, NULL,                      7, 1, 1, 0.00,  0.1200, NULL, DATE_SUB(NOW(), INTERVAL 5 MINUTE), 6, NULL);

INSERT INTO bill_items (bill_id, bill_category_id, description, quantity, unit_price) VALUES
 -- Bill 1
 (1, 1, 'General Consultation',                1, 500.00),
 (1, 2, 'Paracetamol 500mg x15',               15, 3.50),
 (1, 3, 'White Blood Cell Count',              1, 150.00),
 -- Bill 2
 (2, 1, 'General Consultation',                1, 500.00),
 (2, 5, 'ECG (12-lead)',                       1, 600.00),
 (2, 3, 'Total Cholesterol',                   1, 250.00),
 (2, 3, 'Fasting Blood Sugar',                 1, 120.00),
 (2, 2, 'Amlodipine 5mg x40',                  40, 10.00),
 -- Bill 3
 (3, 1, 'Specialist Consultation',             1, 1000.00),
 (3, 3, 'Troponin I',                          1, 900.00),
 (3, 5, 'ECG (12-lead)',                       1, 600.00),
 -- Bill 4
 (4, 4, 'Semi-Private Room (3 days)',          3, 2500.00),
 (4, 5, 'Nebulization',                        2, 350.00),
 (4, 2, 'Ceftriaxone 1g Vial x6',              6, 180.00),
 (4, 1, 'Specialist Consultation',             1, 1000.00),
 -- Bill 5
 (5, 1, 'Follow-up Consultation',              1, 300.00),
 (5, 6, 'Medical Certificate',                 1, 150.00),
 -- Bill 6
 (6, 1, 'General Consultation',                1, 500.00),
 (6, 3, 'Urinalysis',                          1, 100.00),
 (6, 6, 'Administrative Fee',                  1, 100.00),
 -- Bill 7 (draft)
 (7, 1, 'General Consultation',                1, 500.00);

INSERT INTO payments
 (payment_id, payment_code, bill_id, amount, payment_method_id, reference_number, tendered_amount, received_by_user_id, payment_datetime) VALUES
 (1, CONCAT('PAY-',@d,'-001'), 1, 786.80,  1, NULL,           800.00, 6, DATE_SUB(NOW(), INTERVAL 30 MINUTE)),
 (2, CONCAT('PAY-',@d,'-002'), 2, 1000.00, 4, 'GCASH-884201', NULL,   6, DATE_SUB(NOW(), INTERVAL 20 MINUTE)),
 (3, CONCAT('PAY-',@d,'-003'), 4, 3000.00, 3, 'CC-APP-55821', NULL,   6, DATE_SUB(NOW(), INTERVAL 160 MINUTE)),
 (4, CONCAT('PAY-',@d,'-004'), 4, 2000.00, 1, NULL,           2000.00, 6, DATE_SUB(NOW(), INTERVAL 100 MINUTE)),
 (5, CONCAT('PAY-',@d,'-005'), 5, 504.00,  1, NULL,           600.00, 6, DATE_SUB(NOW(), INTERVAL 100 MINUTE)),
 (6, CONCAT('PAY-',@d,'-006'), 6, 300.00,  2, 'DB-771203',    NULL,   6, DATE_SUB(NOW(), INTERVAL 80 MINUTE));

-- =====================================================================
-- SECTION 24 : SEED DATA  -  code sequences (must match the rows above)
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
 ('BILL', CURDATE(), 6),
 ('PAY',  CURDATE(), 6),
 ('DOC',  '1970-01-01', 5),
 ('MED',  '1970-01-01', 12),
 ('TST',  '1970-01-01', 10);

-- =====================================================================
-- END OF schema.sql
-- Quick sanity checks (run manually after import):
--   SELECT * FROM v_medicine_stock_status;      -- Normal / Low / Expiring / Expired all appear
--   SELECT * FROM v_bill_payment_summary;       -- Fully Paid / Partially Paid / Outstanding / Draft
--   SELECT username, role_id FROM users;        -- 6 users
-- =====================================================================
