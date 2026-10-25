# Hospital Management System: Project Creation Guide

**Stack:** C# · Windows Forms · .NET 10.0 · MySQL/MariaDB (XAMPP) · 3NF schema
**Rule of the build:** one step at a time. Test in Visual Studio, then reply with "proceed" or a bug report.

---

## Part A: Prerequisites

| Item | Requirement |
|---|---|
| IDE | **Visual Studio 2026** (or the latest VS that lists .NET 10). To my knowledge, Visual Studio 2022 cannot build `net10.0` projects, so check **Help → About** and the Visual Studio Installer. |
| Workload | ".NET desktop development" (Windows Forms). |
| SDK | .NET 10 SDK. Verify with `dotnet --list-sdks`. |
| Database | XAMPP with the **MySQL** module (usually MariaDB 10.4+). The schema also works on MySQL 8.0.16+. |
| Browser | Any, for phpMyAdmin at `http://localhost/phpmyadmin`. |

---

## Part B: Create the database first

1. Open the **XAMPP Control Panel** and click **Start** on **Apache** and **MySQL**. Both should turn green.
2. Open `http://localhost/phpmyadmin`.
3. Click the **Import** tab → **Choose File** → select `schema.sql` → leave the character set as `utf-8` → click **Import**.
4. Check the result:
   - A database named `hospital_management_db` appears in the left tree.
   - It contains **28 tables** plus **8 views** (the final schema has no lookup tables: pick-lists are ENUM/VARCHAR columns and business codes are the primary keys).
   - In the SQL tab, run `SELECT username FROM users;`. You should see 6 users.
5. Default logins (used from the Login step onward):

| Username | Password | Role |
|---|---|---|
| `admin` | `Admin@123` | Administrator |
| `reception` / `drvillanueva` / `pharmacist` / `labtech` / `cashier` | `Staff@123` | Receptionist / Doctor / Pharmacist / Lab Technician / Cashier |

> ⚠️ Re-running `schema.sql` **drops and recreates** the whole database. Use it to reset to clean test data.
> ℹ️ The seed data is built around today's date (`CURDATE()`), so appointments, queue entries and bills always show as "today". Import it on the day you test, or re-import to refresh it.

---

## Part C: Create the Visual Studio project

1. **File → New → Project**.
2. Search **"Windows Forms App"**. Pick the **C#** template named exactly *Windows Forms App*. Do **not** pick the ".NET Framework" one.
3. Click **Next** and enter:
   - **Project name:** `HospitalManagementSystem`
   - **Location:** a short path, e.g. `C:\Projects\`
   - **Solution name:** `HospitalManagementSystem`
   - Leave **"Place solution and project in the same directory"** *unchecked*.
4. Click **Next**, set **Framework = `.NET 10.0`**, then click **Create**.
5. In Solution Explorer, **delete `Form1.cs`**. Right-click it → Delete. We replace it with our own forms.
6. Double-click the project (or right-click → **Edit Project File**) and confirm it contains:
   - `<TargetFramework>net10.0-windows</TargetFramework>`
   - `<UseWindowsForms>true</UseWindowsForms>`
   - `<Nullable>enable</Nullable>`
   - `<ImplicitUsings>enable</ImplicitUsings>`
7. Build once (**Ctrl+Shift+B**). The project has no forms yet, so it should compile with 0 errors.

---

## Part D: NuGet packages

Right-click the project → **Manage NuGet Packages → Browse**, then install these three:

| Package | Purpose |
|---|---|
| `MySqlConnector` | MySQL/MariaDB driver. Works well with XAMPP's MariaDB and .NET 10. |
| `Microsoft.Extensions.Configuration` | Configuration base. |
| `Microsoft.Extensions.Configuration.Json` | Reads `appsettings.json`. |

---

## Part E: Create the exact folder structure

Right-click the **project** → **Add → New Folder** for each folder below.

**Shortcut:** open *Tools → Command Line → Developer PowerShell*, `cd` into the **project** folder (the one containing the `.csproj`), and run:

```powershell
$folders = @(
 "Config","Database","Helpers",
 "Models\Common","Repositories\Common",
 "Views\Auth","Views\Main","Views\Shared",
 "Views\MP01_PatientRegistration","Views\MP02_PatientSearchEditor","Views\MP03_DoctorScheduling",
 "Views\MP04_AppointmentBooking","Views\MP05_QueueBoard","Views\MP06_ConsultationVitals",
 "Views\MP07_PrescriptionBuilder","Views\MP08_PharmacyInventory","Views\MP09_MedicineDispensing",
 "Views\MP10_LaboratoryTests","Views\MP11_ItemizedBilling","Views\MP12_PaymentProcessing",
 "Models\MP01_PatientRegistration","Models\MP02_PatientSearchEditor","Models\MP03_DoctorScheduling",
 "Models\MP04_AppointmentBooking","Models\MP05_QueueBoard","Models\MP06_ConsultationVitals",
 "Models\MP07_PrescriptionBuilder","Models\MP08_PharmacyInventory","Models\MP09_MedicineDispensing",
 "Models\MP10_LaboratoryTests","Models\MP11_ItemizedBilling","Models\MP12_PaymentProcessing",
 "Repositories\MP01_PatientRegistration","Repositories\MP02_PatientSearchEditor","Repositories\MP03_DoctorScheduling",
 "Repositories\MP04_AppointmentBooking","Repositories\MP05_QueueBoard","Repositories\MP06_ConsultationVitals",
 "Repositories\MP07_PrescriptionBuilder","Repositories\MP08_PharmacyInventory","Repositories\MP09_MedicineDispensing",
 "Repositories\MP10_LaboratoryTests","Repositories\MP11_ItemizedBilling","Repositories\MP12_PaymentProcessing"
)
$folders | ForEach-Object { New-Item -ItemType Directory -Force -Path $_ | Out-Null }
```

If the new folders don't appear in Solution Explorer, click **Show All Files** (top toolbar of Solution Explorer).

Copy `schema.sql` into the `Database\` folder, so the script travels with the project.

### Target structure

```
HospitalManagementSystem\                      (solution)
└── HospitalManagementSystem\                  (project)
    ├── appsettings.json                       connection string (Copy if newer)
    ├── Program.cs                             entry point: Login -> Main
    │
    ├── Config\
    │   └── AppConfig.cs                       reads appsettings.json once
    ├── Database\
    │   └── schema.sql
    ├── Helpers\                               shared, D.R.Y. utilities
    │   ├── DatabaseHelper.cs                  the ONLY place a connection is opened
    │   ├── ValidationHelper.cs                email/phone/required/range checks + ErrorProvider
    │   ├── GeneratorHelper.cs                 PAT-/APT-/... codes via code_sequences
    │   ├── PasswordHelper.cs                  PBKDF2 hash + verify
    │   ├── SessionManager.cs                  current logged-in user
    │   ├── SettingsHelper.cs                  cached app_settings constants (added in Step 1)
    │   ├── ThemeHelper.cs                     dark-teal colours, button/grid styling
    │   ├── GridHelper.cs                      forces read-only DataGridView rules
    │   └── MessageHelper.cs                   consistent info/warn/error/confirm dialogs
    │
    ├── Models\                                plain classes (one folder per MP)
    │   ├── Common\                            User + AuthResult + DashboardStats, Role, lookup items
    │   ├── MP01_PatientRegistration\
    │   ├── MP02_PatientSearchEditor\          PatientSearchCriteria, AuditEntry (Step 5)
    │   ├── MP03_DoctorScheduling\
    │   ├── MP04_AppointmentBooking\
    │   ├── MP05_QueueBoard\
    │   ├── MP06_ConsultationVitals\
    │   ├── MP07_PrescriptionBuilder\
    │   ├── MP08_PharmacyInventory\
    │   ├── MP09_MedicineDispensing\
    │   ├── MP10_LaboratoryTests\
    │   ├── MP11_ItemizedBilling\
    │   └── MP12_PaymentProcessing\
    │
    ├── Repositories\                          all SQL lives here (mirrors Models)
    │   ├── Common\                            UserRepository (Step 2), DashboardRepository (Step 3), LookupRepository
    │   └── MP01_ ... MP12_ (same names)   MP02: PatientSearchRepository (5A), PatientProfileRepository (5B)
    │
    └── Views\                                 Forms (.cs + .Designer.cs), one folder per MP
        ├── Auth\                              LoginForm (+ LoginForm.Designer.cs)  (Step 2)
        ├── Main\                              MainForm (sidebar + tabs), DashboardForm (Step 3)
        ├── Shared\                            ModulePlaceholderForm (Step 3); reusable dialogs later
        ├── MP01_PatientRegistration\          PatientRegistrationForm, PatientProfileCardForm
        ├── MP02_PatientSearchEditor\          PatientSearchForm, PatientEditForm, AuditHistoryForm
        ├── MP03_DoctorScheduling\             DoctorManagementForm, ScheduleBuilderForm
        ├── MP04_AppointmentBooking\           AppointmentBookingForm, AppointmentCancelDialog, AppointmentRescheduleDialog
        ├── MP05_QueueBoard\                   QueueBoardForm
        ├── MP06_ConsultationVitals\           ConsultationForm
        ├── MP07_PrescriptionBuilder\          PrescriptionBuilderForm, PrescriptionPreviewForm
        ├── MP08_PharmacyInventory\            MedicineCatalogForm, StockManagerForm
        ├── MP09_MedicineDispensing\           DispensingForm, DispensingSlipForm
        ├── MP10_LaboratoryTests\              LabCatalogForm, LabResultEntryForm
        ├── MP11_ItemizedBilling\              BillingForm
        └── MP12_PaymentProcessing\            PaymentForm, BillingRecordsForm, ReceiptPreviewForm
```

(The file names above are the plan, so nothing is created yet. Each step adds its files to the right folder.)

---

## Part F: Naming conventions

| Thing | Convention | Example |
|---|---|---|
| Namespace | Mirrors the folder path | `HospitalManagementSystem.Views.MP01_PatientRegistration` |
| Forms | `PascalCase` + `Form` | `PatientRegistrationForm` |
| Controls | 3-letter prefix + PascalCase | `txtFirstName`, `cboGender`, `dtpDateOfBirth`, `dgvPatients`, `btnSave`, `grpPersonal`, `lblAge`, `pnlHeader`, `tabMain` |
| Models | Singular noun | `Patient`, `Appointment` |
| Repositories | `<Entity>Repository` | `PatientRepository` |
| Helpers | `<Purpose>Helper` | `ValidationHelper` |
| Private fields | `_camelCase` | `_patientRepo` |
| DB tables / columns | `snake_case`, plural tables | `patients`, `patient_code` |
| SQL | Always **parameterised** (`@param`). No string concatenation. | |

---

## Part G: UI and window rules (applied in every form)

1. **Maximized windows.** `LoginForm`, `MainForm` and every pop-up dialog set `WindowState = FormWindowState.Maximized`.
2. **Module screens** (MP01–MP12) are Forms hosted inside `MainForm`'s tab area (`TopLevel = false`, `Dock = Fill`, no border), matching your reference image. They fill whatever size the main window has.
3. **Responsive layout.** Use `Dock` for the big regions and `Anchor` for controls inside them. Use `TableLayoutPanel` / `SplitContainer` where proportions matter. No fixed pixel positions for grids or main panels.
4. **Every `DataGridView`** is configured through `GridHelper`: `ReadOnly = true`, `AllowUserToAddRows = false`, `AllowUserToDeleteRows = false`, `SelectionMode = FullRowSelect`, `MultiSelect = false`, `AutoSizeColumnsMode = Fill`, `DefaultCellStyle.WrapMode = True`, `AutoSizeRowsMode = AllCells`, and a per-column `MinimumWidth` equal to the content width (capped at 320 px; longer text wraps). All text is always visible. Editing happens only in the form controls, never inside the grid. Grid columns are created in the form's `.cs` (`BuildGridColumns`), not in the Designer.
6. **DPI and drop-downs.** The app is DPI-aware **PerMonitorV2** (`ApplicationHighDpiMode` in the `.csproj`), every form uses `AutoScaleMode = Dpi`, and every form calls `FormHelper.Prepare(this)` right after `InitializeComponent()`, which sets each ComboBox's `DropDownWidth` to the box width or the longest item. Do not type a fixed `DropDownWidth` in the Designer.
5. **Proposed dark-teal palette** (taken from your mockup, to be applied by `ThemeHelper`):

| Role | Hex |
|---|---|
| Window background | `#14202E` |
| Panel / card | `#1C2E40` |
| Sidebar group header | `#1B6F7A` |
| Accent / primary button / active tab | `#1FA2B8` |
| Success / Completed | `#2E9E5B` |
| Warning / Waiting | `#F0A020` |
| Danger / Expired | `#C62828` |
| Primary text | `#E8F1F5` |
| Muted text | `#9FB3C2` |

---

## Part H: What each schema area is used by

| Modules | Main tables |
|---|---|
| Login | `users` (role is an ENUM column), `login_history`, `app_settings` |
| MP01 / MP02 | `patients` (gender, blood type, city, province and the emergency contact are columns on the row), `patient_audit_log` |
| MP03 | `doctors` (specialization, department, employment type are columns), `doctor_schedules`, `doctor_leaves` |
| MP04 | `appointments` (`appointment_type`, `status` ENUM columns) |
| MP05 | `queue_entries` (`queue_status` ENUM), `queue_skip_log` |
| MP06 | `consultations` |
| MP07 | `prescriptions`, `prescription_items` |
| MP08 | `medicines`, `stock_batches`, `stock_movements` + views `v_batch_stock`, `v_medicine_stock`, `v_medicine_stock_status` |
| MP09 | `dispensing_records`, `dispensing_items` + views `v_prescription_item_balance`, `v_dispensing_totals` |
| MP10 | `lab_tests`, `lab_orders`, `lab_results` |
| MP11 | `bills`, `bill_items`, `service_catalog`, `room_types` + views `v_bill_subtotals`, `v_bill_totals` |
| MP12 | `payments` (`payment_method` ENUM) + view `v_bill_payment_summary` |
| All | `code_sequences` (ID generator), `app_settings` (configurable constants) |

Business codes are the primary keys (`patient_id` = `PAT-YYYYMMDD-XXX`, `doctor_id` = `DOC-XXX`, ...). Drop-downs of open lists (specialization, department, city, supplier ...) are filled with `SELECT DISTINCT` (`LookupRepository.Distinct`); fixed lists mirror the ENUMs (`LookupRepository.Genders`, ...).

---

## Part I: Build roadmap

| Step | Deliverable | Status |
|---|---|---|
| 1 | Foundation: `appsettings.json`, `AppConfig`, all Helpers, `Program.cs` | Done |
| 2 | Secure Login (`LoginForm`, user repository, lockout) | Done |
| 3 | `MainForm` shell (sidebar + closable tabs) and the Dashboard | Done |
| 4 | MP01 Patient Registration | Done |
| 5A | MP02 Patient Search (multi-criteria, results grid) | Done |
| 6 | Global input-validation rule (`PhoneHelper`, `InputHelper`, `FormValidator` additions) | Done |
| 5B | MP02 Profile editor, audit trail, change history | Done (built and tested) |
| 6.1 | `MainForm` sidebar fixes (single-click groups, text clipping, DPI-aware heights, `&` in tab titles) | Done (tested) |
| 6.2 | `MainForm` sidebar alignment polish and tab-strip overflow fix | Done (tested) |
| 6.3 | `MainForm` tab-title vertical alignment and dark sidebar scroll bar | Done |
| 7A | MP03 Doctor Directory (add / edit / search / delete doctors, DOC-XXX code, unique license) | Done |
| 7B | MP03 Weekly Schedule Builder (blocks, overlap check, 7-column grid, leave dates) | Done |
| 8A | MP04 Appointment Booking: patient picker, doctor and slot selection, booking with APT code and token, appointments list with filters | Done |
| 8B | MP04 Appointment management: Cancel, Reschedule, Mark Completed, Mark No-show | Done |
| 9 | MP05 Patient Queue Board | Done |
| 10 | MP06 Consultation and Vital Signs | Done |
| 11 | MP07 Prescription Builder | Done |
| 12 | MP08 Medicine Catalog and Stock | Done |
| 13 | MP09 Dispensing Simulator | Done |
| 14 | MP10 Lab Tests and Result Entry | Done |
| 15 | MP11 Itemized Billing | Done |
| 16 | MP12 Payment Processing | Done |
| 17 | Final-project fixes: new database alignment check, drop-down alignment (PerMonitorV2), grid auto-size, rebuilt grid columns, logo (Part Y) | Done |

(`ModuleRegistry.cs` is re-delivered with every module step. From Step 7A the "coming soon" pages show the corrected step numbers: MP04 = Step 8 ... MP12 = Step 16.)

---

## Part J: Checklist before replying "proceed"

- [ ] XAMPP Apache + MySQL running
- [ ] `schema.sql` imported with no errors (28 tables, 8 views)
- [ ] Project created as **Windows Forms App · .NET 10.0**, `Form1.cs` deleted
- [ ] 3 NuGet packages installed
- [ ] All folders from Part E visible in Solution Explorer
- [ ] Project builds with 0 errors

Reply **"proceed"** to start Step 1, or tell me about any problem (for example, the exact import error from phpMyAdmin, or the build error text).

---

## Part K: Where the Step 1 files go

All paths are relative to the **project folder** (the one containing the `.csproj`).
In Solution Explorer: right-click the target folder → **Add → Existing Item**.

| File | Location |
|---|---|
| `appsettings.json` | project root; Properties → **Copy to Output Directory = Copy if newer** |
| `Program.cs` | project root; **replaces** the template's `Program.cs` |
| `AppConfig.cs` | `Config\` |
| `DatabaseHelper.cs` | `Helpers\` |
| `SettingsHelper.cs` | `Helpers\` |
| `GeneratorHelper.cs` | `Helpers\` |
| `PasswordHelper.cs` | `Helpers\` |
| `SessionManager.cs` | `Helpers\` |
| `ThemeHelper.cs` | `Helpers\` |
| `GridHelper.cs` | `Helpers\` |
| `MessageHelper.cs` | `Helpers\` |
| `ValidationHelper.cs` | `Helpers\` |
| `User.cs` | `Models\Common\` |
| `FoundationTestForm.cs` | `Views\Shared\` (temporary; deleted in Step 3) |

Rule for every later step: each response will list the exact folder for every file it delivers.

---

## Part L: Where the Step 2 (Login) files go

| File | Location |
|---|---|
| `AuthResult.cs` | `Models\Common\` |
| `UserRepository.cs` | `Repositories\Common\` |
| `LoginForm.cs` | `Views\Auth\` |
| `LoginForm.Designer.cs` | `Views\Auth\` (must sit next to `LoginForm.cs`; Visual Studio nests it automatically) |
| `WelcomeForm.cs` | `Views\Main\` (temporary; deleted in Step 3) |
| `Program.cs` | project root; **replaces** the Step 1 `Program.cs` |

---

## Part M: Where the Step 3 (Main shell + Dashboard) files go

**First delete** these two temporary files from the project (right-click → Delete):
`Views\Main\WelcomeForm.cs` and `Views\Shared\FoundationTestForm.cs`.

| File | Location |
|---|---|
| `GridHelper.cs` | `Helpers\` (**replaces** the existing file) |
| `DashboardStats.cs` | `Models\Common\` |
| `DashboardRepository.cs` | `Repositories\Common\` |
| `ModuleRegistry.cs` | `Views\Main\` (every later module step replaces this file) |
| `MainForm.cs` + `MainForm.Designer.cs` | `Views\Main\` (**replaced in Step 6.1**, see the sidebar fix note at the end of Part Q) |
| `DashboardForm.cs` + `DashboardForm.Designer.cs` | `Views\Main\` |
| `ModulePlaceholderForm.cs` + `ModulePlaceholderForm.Designer.cs` | `Views\Shared\` |
| `Program.cs` | project root; **replaces** the Step 2 `Program.cs` |

Role visibility used by the sidebar (Administrator always sees everything):
Receptionist → MP01-MP05; Doctor → MP02, MP05, MP06, MP07, MP10; Pharmacist → MP08, MP09; Lab Technician → MP10; Cashier → MP11, MP12.

---

## Part N: Where the Step 4 (MP01 Patient Registration) files go

| File | Location |
|---|---|
| `LookupItem.cs` | `Models\Common\` |
| `LookupRepository.cs` | `Repositories\Common\` |
| `Patient.cs` | `Models\MP01_PatientRegistration\` |
| `PatientRepository.cs` | `Repositories\MP01_PatientRegistration\` |
| `PatientRegistrationForm.cs` + `PatientRegistrationForm.Designer.cs` | `Views\MP01_PatientRegistration\` |
| `ModuleRegistry.cs` | `Views\Main\` (**replaces** the Step 3 file) |

Open it from the sidebar: **Patients & Registration → Patient Registration** (log in as `admin` or `reception`).

---

## Part O: Where the Step 5, Part 5A (MP02 search) files go

| File | Location |
|---|---|
| `PatientSearchCriteria.cs` | `Models\MP02_PatientSearchEditor\` |
| `PatientSearchRepository.cs` | `Repositories\MP02_PatientSearchEditor\` |
| `PatientSearchForm.cs` + `PatientSearchForm.Designer.cs` | `Views\MP02_PatientSearchEditor\` |
| `ModuleRegistry.cs` | `Views\Main\` (**replaces** the existing file) |

Open it from the sidebar: **Patients & Registration -> Patient Search & Profile**. Part 5B adds the editor, audit trail and history.

---

## Part P: Global input-validation rule (applies to EVERY form and module)

Every input field must accept only what its data type allows, at the keyboard **and** when pasting
(`InputHelper.Attach(...)`), and must be re-checked on Save (`FormValidator`). Every new module follows this table.

| Kind of field | `InputKind` | Allowed characters | Save-time check |
|---|---|---|---|
| Person names (first, middle, last, contact name) | `PersonName` | letters, space, `-` `'` `.` (must start with a letter; **no digits**) | `PersonName(...)` |
| Name search box | `PersonNameSearch` | same as names plus `,` | - |
| Mobile / contact number | `PhoneSubscriber` | digits only, **max 10, first digit 9**, after a fixed `+63` box | `Phone(...)` |
| Phone search box | `PhoneSearch` | digits only, max 12 | - |
| Postal code | `Digits` (max 4) | digits only | `PostalCode(...)` |
| Street address | `Address` | letters, digits, space and `, . - # / ' ( )` | - |
| Email | `Email` | letters, digits, `. _ % + - @` | `Email(...)` |
| User name | `Username` | letters, digits, `. _ -` | `Required(...)` |
| Record codes | `Code` | capital letters, digits, `-` | - |
| Whole numbers (quantities, days, counts) | `Digits` | digits only | `PositiveInteger(...)` / `InRange(...)` |
| Money / measurements | `Decimal2` | digits and one `.`, max 2 decimals | `Positive(...)` / `InRange(...)` |
| Dates | date pickers | cannot be typed | `NotFutureDate(...)` etc. |
| Choices (gender, blood type, ...) | drop-down list (`DropDownList`) | cannot be typed | `Required(...)` |
| Free text (allergies, notes) | none | any text, limited by `MaxLength` | length only |
| Passwords | none | any character | `PasswordHelper.ValidateStrength` when set |

**Phone number format (one rule for all modules):** Philippine mobile = `+63` + 10 digits starting with 9.
Typed as `917 123 4567` after the fixed `+63` box, **stored** as `+639171234567` (13 characters, fits the existing
`VARCHAR(25)` columns - no schema change), **shown** as `+63 917 123 4567`. Pasted `09171234567`,
`0917-123-4567` or `+63 917 123 4567` are converted automatically. All logic lives in `PhoneHelper`.

### Where the Step 6 (validation rule) files go

| File | Location |
|---|---|
| `PhoneHelper.cs` | `Helpers\` (new) |
| `InputHelper.cs` | `Helpers\` (new) |
| `ValidationHelper.cs` | `Helpers\` (**replaces** the existing file) |
| `PatientRegistrationForm.cs` + `.Designer.cs` | `Views\MP01_PatientRegistration\` (**replace**) |
| `PatientSearchForm.cs` | `Views\MP02_PatientSearchEditor\` (**replaces**; its Designer is unchanged) |
| `PatientSearchRepository.cs` | `Repositories\MP02_PatientSearchEditor\` (**replaces**) |
| `LoginForm.cs` | `Views\Auth\` (**replaces**; its Designer is unchanged) |
| `migration_001_phone_format.sql` | `Database\` (optional - run once in phpMyAdmin) |

---

## Part Q: Where the Step 5, Part 5B (MP02 editor, audit trail, change history) files go

All paths are relative to the **project folder**. Copy each `.cs` and its `.Designer.cs` into the same folder
(Visual Studio nests the Designer file under its form automatically).

| File | Location |
|---|---|
| `AuditEntry.cs` | `Models\MP02_PatientSearchEditor\` (new) |
| `PatientProfileRepository.cs` | `Repositories\MP02_PatientSearchEditor\` (new) |
| `PatientEditForm.cs` + `PatientEditForm.Designer.cs` | `Views\MP02_PatientSearchEditor\` (new) |
| `AuditHistoryForm.cs` + `AuditHistoryForm.Designer.cs` | `Views\MP02_PatientSearchEditor\` (new) |
| `PatientSearchForm.cs` | `Views\MP02_PatientSearchEditor\` (**replaces** the 5A file; its Designer is unchanged) |

No schema change and no change to `ModuleRegistry.cs` (MP02 is already registered).

### Editor rules (what the screen enforces)

| Field | Behaviour |
|---|---|
| Patient Code, Registration Date, Full Name, Gender, Age | Always shown, **never editable** (gray, read-only). Names and gender are not in the editable list of the MP02 spec. |
| Mobile Number, Email, Street Address, City (Province follows), Postal Code | Editable. Mobile = fixed `+63` box + 10 digits; postal code = 4 digits; email pattern; input filtered by `InputHelper`. |
| Emergency Contact (name, relationship, mobile) | Editable. All three together, or clear all three to remove the contact. Name = letters only. |
| Allergies, Medical History | Editable free text (max 2000). |
| Blood Type | Editable, **critical**: saving a change shows a confirmation with old -> new. |
| Date of Birth | **Locked.** Tick *Administrator override*, type an administrator's username and password (checked with `PasswordHelper`, 3 attempts). Then it unlocks; saving a changed date shows the critical confirmation. The approving administrator is written into the log (`[approved by admin]`). |

Audit trail: one `patient_audit_log` row **per changed field only** (unchanged fields and the legacy-vs-new phone format are not logged),
written in the same transaction as the update. `Changed By` is the logged-in user. Blank values show as `(blank)`.
Concurrency: if someone else saves the same patient while the editor is open, the save is refused with a plain message
(use *Undo Changes* to reload).

### How to test 5B

1. Log in as `reception` / `Staff@123`, open **Patient Search & Profile**, search `mar`, double-click a row (or **Open / Edit Profile**). The editor opens full screen.
2. Change the email, then watch the footer: `1 field changed: Email`. **Save Changes**, then **View Change History** - one row (Email, old, new, your name, now).
3. Change Blood Type -> confirm dialog appears; change several fields at once -> one history row each.
4. Tick **Administrator override**: wrong password shows an inline message (3 tries); `admin` / `Admin@123` unlocks Date of Birth. Change it, confirm, and check the history row ends with `[approved by admin]`.
5. Clear emergency name, phone and set relationship to *Not specified* -> contact is removed (3 history rows). Type only a name -> inline validation errors.
6. Close with unsaved edits -> a confirmation appears. After a save, the search results refresh and keep the patient selected.

### Fix note: CS0103 `txtSearchPhone_TextChanged` does not exist (PatientSearchForm.Designer.cs)

Cause: double-clicking a control in the Designer makes Visual Studio add an event hook (`... += xxx_TextChanged;`) plus an empty handler in the form's `.cs`.
Replacing the `.cs` file removed that handler but the hook stayed. All events in this project are wired in the form's `.cs` (`WireEvents()`), never through the Designer.
Fix: replace `Views\MP02_PatientSearchEditor\PatientSearchForm.Designer.cs` with the delivered copy (it has no event hooks).
Rule going forward: do not double-click controls in [Design] view; if Visual Studio adds a hook by accident, replace the Designer from the delivered file.

### Fix note (Step 6.1): `MainForm` sidebar - double-click, clipped text, uneven alignment, rigid sub-items

| File | Location |
|---|---|
| `MainForm.cs` + `MainForm.Designer.cs` | `Views\Main\` (**replace** both existing files) |

No other file changes (`ModuleRegistry.cs` is untouched until Step 7).

Root causes and fixes:

| Symptom | Cause | Fix |
|---|---|---|
| Group header needed a double-click the first time | `SetGroupExpanded` counted children with `Control.Visible`, which is **always false until the form is shown**. Every group was therefore collapsed in height while its text still said open (arrow down), so click 1 "collapsed" it (no visible change) and click 2 expanded it. | Open/closed state now lives in `_groupExpanded` (a dictionary), and the allowed children in `_itemAllowed`. Nothing reads `Visible` or the button text any more. |
| `PATIENTS & REGISTRATION` cut to `PATIENTS &` | Bold 10 pt text plus arrow plus padding was wider than the 270 px sidebar, so the second word wrapped out of the 46 px button. | Sidebar widened to 290 px, arrow removed from the text, `AutoEllipsis = true` on every sidebar button (a `...` appears instead of a hard cut on very long titles). |
| `&` | Sidebar buttons already use `UseMnemonic = false`, so a **single** `&` is shown literally (adding `&&` would show two). The swallowed `&` was only in the **tab titles** (`Patient Search & Profile` showed as `Patient Search  Prof...`). | Tab title labels created in `CreateTab` now set `UseMnemonic = false`. (`DashboardForm` revenue title and `ModulePlaceholderForm` title still have the same cosmetic issue; not changed yet.) |
| Messy alignment | Dashboard and headers used 12 px padding, children 30 px, and the two arrow glyphs had different widths so header text shifted when toggled. | Three fixed levels: arrow zone at x = 18 (drawn by a `Paint` handler, so it never moves the text), **Dashboard and group header text at 36 px**, **child items at 56 px** (raised to 60 px in Step 6.2). |
| Sub-items not filling / fitting | Group heights used constants 46 and 44, ignoring Windows display scaling (125 % / 150 %). | `ApplyGroupHeight` sums the real `Height` of the header and each allowed child; it runs after role filtering, on `Load` and on `DpiChanged`. Children are `Dock = Top`, so they fill the full sidebar width. |

How to test 6.1:

1. Run and log in as `admin`. Every group starts **expanded**; **one** click on a header collapses it, a second click expands it. The arrow changes (down = open, right = closed) and the header text does not move.
2. `PATIENTS & REGISTRATION` is fully visible. `Dashboard Overview` and all header texts start at the same x position; all sub-items are indented further.
3. Log in as `cashier`: only BILLING & PAYMENTS shows, with exactly two children and no gap.
4. Open `Patient Search & Profile`: the tab title now shows the `&`.
5. Optional: set Windows display scaling to 125 % or 150 % and repeat step 1.

### Fix note (Step 6.2): `MainForm` sidebar alignment polish and tab-strip overflow

| File | Location |
|---|---|
| `MainForm.cs` + `MainForm.Designer.cs` | `Views\Main\` (**replace** both existing files) |

No other file changes.

**Sidebar alignment.** Measured from your screenshots, `Dashboard Overview` and the header texts already started at the same x; what looked uneven was (a) the Dashboard row having nothing in the arrow column while every header has an arrow, (b) a keyboard focus rectangle on the last-clicked child, and (c) a shallow child indent.

| Change | Detail |
|---|---|
| Dashboard icon | A small 2x2 grid icon is painted in the arrow column (same x as the header arrows), so Dashboard and headers read as one column. |
| Alignment values | Arrow / icon column centred at x = 18; **Dashboard and header text `Padding.Left = 36`**; **all 12 child buttons `Padding.Left = 60`**. `TextAlign = MiddleLeft` on every sidebar button. |
| Focus rectangle | `TabStop = false` on all sidebar buttons, so the white focus border no longer appears. |

**Tab strip overflow.** Cause: `flowTabs` had `AutoScroll = true` and a height of only 42 px. With several tabs open, Windows added a **light horizontal scroll bar** inside that strip (the white bleed), which squeezed the tabs vertically so they clipped and overlapped.

| Change | Detail |
|---|---|
| Structure | New `pnlTabBar` (dark, docked top) holds `flowTabs` (Fill, **`AutoScroll = false`**, `WrapContents = false`) and `pnlTabNav` (docked right, hidden unless needed) with the `<` `>` buttons `btnTabPrev` / `btnTabNext`. No scroll bar can appear any more. |
| Fitting | `LayoutTabs` shrinks all tabs evenly from 230 px down to 120 px (design pixels, scaled to the display DPI) so they always fit one row at the strip height. |
| Still too many tabs | The `<` `>` buttons appear and move a window of tabs; the active tab is always kept in view. Runs on open, close, activate, resize/maximize and DPI change. |
| Readability | Truncated titles show an ellipsis and the full title in a tooltip (`tipTabs`). Close button and padding are DPI-scaled. |

How to test 6.2:

1. Log in as `admin` and open every module (13 tabs including the dashboard). No white bar, no clipping; tabs shrink, and on a narrow window `<` `>` appear.
2. Maximise/restore the window: tabs re-fit. Close tabs from the middle: remaining tabs re-spread and the previous tab becomes active.
3. Hover a shortened tab: the full title appears. Click `>` / `<` when they are shown (ends are disabled).
4. Sidebar: `Dashboard Overview` has an icon in the arrow column and its text lines up exactly with the header texts; all sub-items share one deeper indent and show no white focus border after clicking.

### Fix note (Step 6.3): `MainForm` tab titles shifted upwards, white sidebar scroll bar

| File | Location |
|---|---|
| `MainForm.cs` + `MainForm.Designer.cs` | `Views\Main\` (**replace** both existing files) |

No other file changes.

**Tab titles.** Cause: `Label.AutoEllipsis = true` loses its vertical centring (`MiddleLeft`) the moment the text is truncated, so only the shortened tabs drew their text higher than the others. The header panels themselves were always level.

| Change | Detail |
|---|---|
| Manual ellipsis | `AutoEllipsis` removed from every tab title label (including `lblTabDashboardTitle` in the Designer). `FitTabText` measures the title with `TextRenderer`, shortens it with `...` itself and stores the full title in `TabEntry.FullTitle`; the tooltip still shows the full title. |
| Fixed height | `LayoutTabs` gives every header the same height, with `MinimumSize.Height = MaximumSize.Height = height` (width 0 = unlimited) and the same `Margin`, so no tab can drift. |

**Sidebar scroll bar.** Cause: `pnlMenu.AutoScroll = true` made Windows draw its default light scroll bar. Windows does not let a WinForms `Panel` recolour it, so it is replaced.

| Change | Detail |
|---|---|
| Structure | `pnlMenu` is now a clipping viewport (`AutoScroll = false`). `pnlMenuContent` holds the Dashboard button and the six groups and is moved up/down by code. A slim dark `pnlScrollTrack` + `pnlScrollThumb` sits at the sidebar's right edge. |
| Behaviour | The bar appears **only** when the menu is taller than the window (for example admin at 125-150 % scaling). Thumb: drag; click the track to page; hover turns it teal. Mouse wheel over the sidebar scrolls it (message filter, works even when another control has focus; only while the main window is active). |
| Recalculation | `UpdateMenuScroll` runs after role filtering, group toggle, sidebar resize and DPI change; collapsing a group clamps the scroll offset. |

How to test 6.3:

1. Open 6-8 modules, several with long titles (`Medicine Catalog & Stock`, `Doctor Directory & Schedule`): all tab titles are vertically centred at the same level, truncated ones end in `...`, and hovering shows the full title.
2. Admin with all groups expanded on a small window or 125-150 % scaling: no white bar; a slim dark thumb appears at the right of the sidebar. Drag it, click the track, use the mouse wheel over the sidebar.
3. Collapse groups until everything fits: the thumb disappears and the menu snaps back to the top. Log in as `cashier`: no scroll bar at all.

---

## Part R: Step 7, Part 7A (MP03 Doctor Directory) - files and rules

All paths are relative to the **project folder** (the one containing the `.csproj`).
Copy each `.cs` and its `.Designer.cs` into the same folder (Visual Studio nests the Designer file under its form).
No schema change and no migration: the `doctors`, `specializations`, `departments`, `employment_types`, `doctor_schedules` and `doctor_leaves` tables already exist.

| File | Location |
|---|---|
| `Doctor.cs` (classes `Doctor` and `DoctorDeleteImpact`) | `Models\MP03_DoctorScheduling\` (new) |
| `DoctorRepository.cs` | `Repositories\MP03_DoctorScheduling\` (new) |
| `LookupRepository.cs` | `Repositories\Common\` (**replaces** the existing file; adds `GetSpecializations`, `GetDepartments`, `GetEmploymentTypes`) |
| `DoctorManagementForm.cs` + `DoctorManagementForm.Designer.cs` | `Views\MP03_DoctorScheduling\` (new) |
| `ModuleRegistry.cs` | `Views\Main\` (**replaces** the existing file: MP03 now opens `DoctorManagementForm`; placeholder step numbers corrected to Step 8-16) |

Open it from the sidebar: **Doctors & Schedules -> Doctor Schedule** (log in as `admin` or `reception`).

### Screen layout

Left: **Doctor Directory** grid with a live search box (name, code, license number or specialization; the grid filters 0.3 s after you stop typing; **Clear** empties the box).
Right: **Doctor Details** form with a banner showing the mode (`New doctor` or `Editing DOC-001 - Dr. ...`), then the buttons **Save Doctor / Save Changes**, **New Doctor**, **Delete Doctor**, **Weekly Schedule...**.
Clicking a grid row loads that doctor into the form (edit mode). **New Doctor** clears the form and returns to add mode.

### Field rules (global validation rule applied)

| Field | Control / `InputKind` | Rule on save |
|---|---|---|
| Doctor Code | read-only box | `DOC-XXX`, generated by `GeneratorHelper` **inside the save transaction**; never editable |
| First Name, Last Name | `PersonName`, max 60 | required, letters / space / `-` `'` `.` only |
| License Number | `Code` (capitals, digits, `-`), max 30 | required, 4-30 characters, **unique** (checked while validating, again inside the save transaction, and by the database key) |
| Specialization, Department, Employment Type | drop-down lists (no typing) from the lookup tables | required |
| Mobile Number | fixed `+63` box + `PhoneSubscriber` | required, 10 digits starting with 9, stored `+639XXXXXXXXX`, shown `+63 917 555 0001`. An old seed number such as `0917-555-0001` is converted automatically when the doctor is loaded |
| Email | `Email`, max 120 | optional, basic email pattern |
| Consultation Fee | `Decimal2`, max 11 characters | required, 0.01 to 99,999,999.99 (2 decimals) |

### Delete rule

1. **Delete Doctor** first counts what depends on the doctor.
2. If any **appointments, queue entries or lab orders** refer to the doctor, deletion is **refused** with a plain message listing the counts (they are patient history).
3. Otherwise a confirmation states how many **weekly schedule blocks and leave dates will be removed with the doctor**; on **Yes** the doctor is deleted and the database cascade (`ON DELETE CASCADE`) removes those rows. The same checks run again inside the delete transaction.

The **Weekly Schedule...** button is enabled when a doctor is selected; from Part 7B it opens the schedule builder (Part S).

### How to test 7A

1. Log in as `reception` / `Staff@123`, open **Doctor Schedule**. The grid shows the 5 seeded doctors (DOC-001 to DOC-005) with `+63` phone numbers and fees.
2. Type `card` in the search box: only Dr. Reyes (Cardiology) remains. Type a license such as `0034`: the same doctor. **Clear** restores the list.
3. Click DOC-001: the form fills, the banner says `Editing DOC-001 - Dr. Ramon Villanueva`, the mobile number box shows `917 555 0001` after `+63`. Change the fee and click **Save Changes**; the grid refreshes.
4. **New Doctor**, then **Save Doctor** with everything empty: one message lists every problem and the bad fields turn red. Try digits in a name (rejected while typing), 11 digits or a first digit other than 9 in the mobile box, and a fee with 3 decimals (the third digit is not accepted).
5. Add a doctor with license `PRC-0012345`: refused as already used by DOC-001. Use a new license such as `PRC-0067890` and fill everything: the message shows the new code `DOC-006`.
6. Select DOC-006 and **Delete Doctor**: the confirmation says 0 schedule blocks and 0 leave dates; confirm and it disappears.
7. Select DOC-001 (Dr. Villanueva) and **Delete Doctor**: refused, because seeded appointments, queue entries and lab orders refer to this doctor (DOC-003 and DOC-005 are refused too).
8. Select DOC-004 (Dr. Cruz: weekly schedule blocks, no leave dates, no patient history) and **Delete Doctor**: the confirmation lists her schedule block(s) and 0 leave dates. Confirm and she disappears. DOC-002 (no schedule, no history) deletes the same way. Re-import `schema.sql` afterwards to restore the seed data.

Known limits (by design, for later steps): doctors have no audit table in the schema, so edits are not logged; the Weekly Schedule button is wired in Part 7B.

---

## Part S: Step 7, Part 7B (MP03 Weekly Schedule Builder) - files and rules

### Where the files go

Copy the `MP03_7B` folder contents into the project, keeping the sub-folders (Visual Studio: right-click the folder > Add > Existing Item, or copy in File Explorer and use *Show All Files*).

| File | Destination folder | Status |
|---|---|---|
| `ScheduleBlock.cs` (classes `ScheduleBlock`, `LeaveDate`, `ScheduleProblem`, enum `ScheduleField`) | `Models\MP03_DoctorScheduling\` | New |
| `DoctorScheduleRepository.cs` | `Repositories\MP03_DoctorScheduling\` | New |
| `ScheduleBuilderForm.cs` and `ScheduleBuilderForm.Designer.cs` | `Views\MP03_DoctorScheduling\` | New |
| `DoctorManagementForm.cs` | `Views\MP03_DoctorScheduling\` | **Replaces** the 7A file (its Designer is unchanged) |

`ModuleRegistry.cs` needs no change in 7B (MP03 is already registered). If Visual Studio shows a stray `ScheduleBuilderForm.resx` requirement, it is created automatically on first open of the form in the designer; the form runs without it.

### What the screen does

Open **Doctor Schedule**, select a doctor, click **Weekly Schedule...**. A maximized dialog opens for that one doctor:

* **Weekly Availability grid (top left):** 7 columns Mon-Sun; each block is one coloured cell (time range and minutes per visit). Column headings show the day's total hours or `(off)`. Clicking a block cell loads it into the editor.
* **Availability Blocks list (bottom left):** every block with Day, Start, End, Hours, Min / Visit, Visits and Notes. Click a header to sort.
* **Availability Block editor (top right):** Day (drop-down), Start/End time pickers, Minutes per Visit (digits only, max 3), Notes. A live hint shows the block length, how many visits fit and the day's total against the daily limit. Buttons: **Add Block / Save Changes**, **New Block**, **Delete Block**.
* **Leave Dates (bottom right):** list of the doctor's unavailable dates with a Past / Today / Upcoming status, a date picker (today or later), a required reason, **Add Leave** and **Remove Selected**.

### Rules (checked in the form for early feedback and again inside the save transaction)

| Rule | Detail |
|---|---|
| Day | required (drop-down, Monday first) |
| End after Start | End Time must be later than Start Time |
| Minimum consultation | Minutes per Visit >= `min_consultation_minutes` (10, from `app_settings`) |
| Visit fits the block | Minutes per Visit cannot be longer than the block itself |
| No overlap | for the same doctor and day: new.Start < old.End **and** new.End > old.Start is an overlap; blocks that only touch (12:00 end, 12:00 start) are allowed; the message names the conflicting block; editing a block ignores itself |
| Daily limit | all blocks of the doctor on that day plus this one must not exceed `max_daily_working_hours` (12) |
| Leave date | required reason (max 200); date today or later; one entry per doctor and date (also enforced by the `uq_leave_doctor_date` key) |
| Protect bookings | a block cannot be deleted, moved or shortened while **Booked** appointments dated today or later fall inside the part that would be removed; the message tells staff to cancel or reschedule first |
| Leave with bookings | marking a date as leave when the doctor already has Booked appointments that day asks for confirmation (they are not cancelled automatically) |
| Concurrency | the doctor row is locked (`SELECT ... FOR UPDATE`) inside every block / leave save, so two users cannot save conflicting blocks at the same moment |

Notes and Reason accept letters, digits and `, . - # / ' ( )` only.

### Overlap algorithm (for the README of the MP)

1. Load the doctor's other blocks for the chosen day (`schedule_id <> this block`).
2. For each one, it overlaps when `newStart < otherEnd AND newEnd > otherStart`.
3. Add up the minutes of the other blocks plus this block; if the total exceeds `max_daily_working_hours * 60`, reject.

### How to test 7B

1. Log in as `reception` / `Staff@123`, open **Doctor Schedule**, select **DOC-001 Dr. Villanueva**, click **Weekly Schedule...**. The grid shows Mon-Fri with two cells each (8:00 AM - 12:00 PM and 1:00 PM - 5:00 PM, 15 min visits), headings like `Mon  (8 h)`, `Sat  (off)`, `Sun  (off)`. The Leave list shows one upcoming leave (Medical conference, 7 days ahead).
2. Click the Tuesday morning cell: the editor fills, the banner says `Editing Tuesday 8:00 AM - 12:00 PM`, the cell turns bright, **Delete Block** is enabled.
3. **New Block**, choose Monday, 11:00 AM - 2:00 PM, 15 minutes, **Add Block**: refused, the message names both overlapping Monday blocks and the Start Time field shows the red icon.
4. Monday 12:00 PM - 1:00 PM is accepted (touching ends are fine). Then try Monday 5:00 PM - 11:00 PM: refused, total would be 13 hours (limit 12).
5. Minutes per Visit `5`: refused (minimum 10). Minutes `300` on a 1-hour block: refused (longer than the block). End earlier than Start: refused. Day not chosen: the list shows every problem at once.
6. Select **DOC-004 Dr. Cruz** (Wed afternoon + Sat): add Sunday 9:00 AM - 11:00 AM, 20 minutes: the `Sun` column fills and the heading changes to `Sun  (2 h)`. Edit it to 10 AM - 12 PM and **Save Changes**; then **Delete Block** and confirm.
7. Leave dates: add a date 3 days ahead with reason `Annual leave`; add the same date again: refused as already marked. Leave the reason empty: refused. Select a row and **Remove Selected**.
8. Delete the seeded Tuesday block of DOC-001 only if no booked appointment is in that time; otherwise the refusal message is the expected result (seed appointments for today fall in DOC-001's blocks if today is a weekday).
9. Close the dialog: the doctor list refreshes. Re-import `schema.sql` afterwards to restore the seed data.

Known limits (by design): schedule changes are not audit-logged (the schema has no audit table for doctors); changing Minutes per Visit does not move existing appointments, it only affects slots offered from then on.

---

## Part T: Step 8, Part 8A (MP04 Appointment Booking) - files and rules

### Where the files go

Copy the `MP04_8A` folder contents into the project, keeping the sub-folders.

| File | Destination folder | Status |
|---|---|---|
| `AppointmentModels.cs` (`AppointmentStatus`, `DoctorOption`, `SlotInfo`, `DayAvailability`, `BookingRequest`, `BookingResult`, `AppointmentFilter`) | `Models\MP04_AppointmentBooking\` | New |
| `AppointmentRepository.cs` | `Repositories\MP04_AppointmentBooking\` | New |
| `AppointmentBookingForm.cs` and `AppointmentBookingForm.Designer.cs` | `Views\MP04_AppointmentBooking\` | New |
| `ModuleRegistry.cs` | `Views\Main\` | **Replaces** the existing file (MP04 now opens `AppointmentBookingForm`) |

The folders `Models\MP04_AppointmentBooking`, `Repositories\MP04_AppointmentBooking` and `Views\MP04_AppointmentBooking` already exist from Part E.

### What the screen does

Open **Appointment Booking** (Receptionist and Administrator). The left side is the booking form, the right side is the appointments list.

* **1. Find the patient:** type part of a name, patient code or phone number and press **Search** (or Enter). Pick a patient in the result grid; one single match is selected automatically. The blue line under the grid shows who is selected.
* **2. Choose doctor, date and time:** Specialization filter (narrows the doctor list), Doctor, Date (today up to 365 days ahead, no past dates), **Time slot** (only open slots are listed), Visit type (New Consultation / Follow-up), optional Reason (letters, digits and `, . - # / ' ( )`, max 250). A hint under the form shows the working hours, how many slots are open or taken, which times are taken, leave, or a warning when the patient is already booked with that doctor that day.
* **Book Appointment** saves and shows the generated Appointment Code and Token Number. The patient and reason are cleared; doctor and date stay for the next booking. **Clear** resets the whole form.
* **Appointments list (right):** Date (default today, untick to see all dates), Doctor, Patient (name or code) and Status filters, **Show All** button, colour-coded Status column, and a footer with counts per status. A newly booked appointment is selected in the list.

### How open slots are computed

1. Take the doctor's weekly blocks whose `day_of_week` matches the chosen date (0 = Sunday ... 6 = Saturday).
2. Cut each block into slots of its `consultation_minutes`; a slot must finish inside the block (08:00-12:00 at 15 minutes gives 08:00 ... 11:45).
3. A doctor leave on that date removes every slot.
4. A slot is taken when a **Booked, Completed or No-show** appointment already uses that time; **Cancelled** appointments free their slot (same rule as the unique key `uq_appt_doctor_slot`).
5. For today, slots whose start time has already passed are not offered.

### Booking rules (checked in the form, then again inside the save transaction with the doctor row locked)

| Rule | Detail |
|---|---|
| Patient, doctor, slot, visit type | all required; the list of problems shows at once with red marks |
| No past dates | the date picker starts today; the repository rejects past dates as well |
| Slot must be offered | the time must be one of the doctor's slots for that weekday, not taken, not already passed today, not on a leave date |
| One appointment per patient, doctor and day | a patient cannot have a second non-cancelled appointment with the same doctor on the same date |
| Appointment Code | `APT-YYYYMMDD-XXX` from `GeneratorHelper`, generated inside the save transaction; the date is the **booking date** (so a later reschedule keeps the same code) |
| Token Number | highest token of that doctor on that date plus 1, computed inside the transaction; cancelled appointments keep their token, so tokens are never reused |
| Concurrency | if another user takes the slot first, the save is refused with a clear message and the slot list refreshes |

### How to test 8A

1. Log in as `reception` / `Staff@123`, open **Appointment Booking**. The list shows today's seeded appointments (Booked blue, Completed green, Cancelled red) and the footer counts. (If today is a Saturday or Sunday, Dr. Villanueva has no schedule; use another weekday for steps 3-6 by changing the date.)
2. Search `lu`: patient Luis Aquino (PAT-...-009) is found and selected automatically. Search `a`: several patients are listed; click one. Search `zzz`: the label says no patient was found. Press Search with an empty box: the red icon appears on the search box.
3. Choose Dr. Villanueva and today (a weekday): the hint shows `8:00 AM - 12:00 PM, 1:00 PM - 5:00 PM (15-minute visits)` and `Taken: 8:00 AM, 8:15 AM, 8:30 AM, 8:45 AM` (times before now are counted as already passed). The Time slot list does not contain taken or past times.
4. Pick Cardiology in Specialization: only Dr. Reyes is listed. Dr. Reyes works Tue and Thu: choose a Monday and the hint says he does not see patients on Mondays, with his working days.
5. Choose Dr. Villanueva, the date 7 days ahead: the hint says he is on leave (Medical conference) and the slot list is empty. Pressing Book shows the message for the missing slot.
6. Select Luis Aquino, Dr. Villanueva, a free weekday, first free slot, **Book Appointment**: success message with the code and token (the token is the next number of that doctor on that date). The list jumps to that date and selects the new row. Book Luis again with the same doctor and date: refused (already has an appointment that day).
7. Book Sofia Castillo with the same doctor, date and slot: the slot is no longer in the list. Open a second copy of the app, book the slot there first, then press Book in the first copy: the refusal message appears and the slot list refreshes.
8. Filters: untick the Date box and press **Show All** to see every appointment (including the one from 40 days ago); filter Doctor, Status = Cancelled, and Patient `santos`.
9. Re-import `schema.sql` afterwards to restore the seed data.

Known limits (by design): the list shows at most 500 rows. Cancel, Reschedule, Mark Completed and Mark No-show were added in Part 8B (Part U below).

---

## Part U: Step 8, Part 8B (MP04 Appointment Management) - files and rules

### Where the files go

Copy the `MP04_8B` folder contents into the project, keeping the sub-folders. `ModuleRegistry.cs` does **not** change in 8B (MP04 is already registered).

| File | Destination folder | Status |
|---|---|---|
| `AppointmentModels.cs` (adds `AppointmentAction`, `AppointmentRecord`, `RescheduleResult`) | `Models\MP04_AppointmentBooking\` | **Replaces** the 8A file |
| `AppointmentRepository.cs` (adds `GetAppointment`, `GetBlockReason`, `Cancel`, `Reschedule`, `MarkCompleted`, `MarkNoShow`) | `Repositories\MP04_AppointmentBooking\` | **Replaces** the 8A file |
| `AppointmentBookingForm.cs` and `AppointmentBookingForm.Designer.cs` | `Views\MP04_AppointmentBooking\` | **Replace** the 8A files |
| `AppointmentCancelDialog.cs` and `AppointmentCancelDialog.Designer.cs` | `Views\MP04_AppointmentBooking\` | New |
| `AppointmentRescheduleDialog.cs` and `AppointmentRescheduleDialog.Designer.cs` | `Views\MP04_AppointmentBooking\` | New |

### What the screen does

Select a row in the **Appointments** list on the right. Four buttons sit under the list:

* **Reschedule:** opens a dialog with a new date and the open slots of the **same doctor**. The appointment keeps its code. The old slot is free again.
* **Cancel Appointment:** opens a dialog that requires a reason (max 250 characters, stored in the appointment's status remarks). The slot is free again at once.
* **Mark Completed** and **Mark No-show:** ask for confirmation, then change the status.

The buttons are enabled whenever a row is selected, even a Completed or Cancelled one. Pressing a button on such a row shows **why** it is not allowed. This is intended, so the rules can be seen working.
The two small dialogs are normal fixed-size dialogs, not maximized (they only ask one thing each).

### Rules (one function, `CheckAction`, used by the form for early feedback and again inside every save transaction)

| Rule | Detail |
|---|---|
| Booked only | only a **Booked** appointment can be cancelled, rescheduled, completed or marked No-show |
| Completed is final | a Completed appointment can no longer be cancelled, rescheduled or marked No-show |
| Cancelled / No-show are final | they cannot be changed again either |
| Cancellation window | cannot cancel when the visit starts within `cancellation_window_hours` (setting, default **2**) or has already started; the message shows the minutes left |
| Reason | required for Cancel, max 250 characters, same allowed characters as every free-text box |
| Reschedule slot | same open-slot computation as booking (weekly blocks of that weekday, minus leave, minus taken slots, minus times already passed today); the appointment's own current slot is never offered |
| Reschedule limits | today up to 365 days ahead; same doctor; not the current date and time |
| One per patient, doctor and day | moving to another date is refused if the patient already has another non-cancelled appointment with that doctor on that date |
| Token | when the **date** changes the appointment gets the next token of that doctor on the new date (highest + 1, never reused); when only the **time** changes the token stays |
| Appointment Code | never changes |
| Freed slots | Cancel sets status 3, so the database column `active_slot_flag` becomes NULL and the slot is free for other patients; Reschedule frees the old slot because the appointment leaves it |
| Mark Completed | allowed on or after the appointment date (not for a future date) |
| Mark No-show | allowed only after the appointment time has passed (not for a future date, not earlier today); the slot stays used |
| Queue board | Cancel, Reschedule and No-show are refused once the patient has checked in (a row exists in the queue board); finish the visit from the queue board instead. Mark Completed is still allowed |
| Concurrency | the appointment row is locked (`FOR UPDATE`) in every action, the doctor row is locked first in Reschedule (same order as booking); if someone else changed the appointment first, nothing is written and the list refreshes |

Reschedule is **not** subject to the cancellation window (the specification limits only cancelling).

### How to test 8B

1. Log in as `reception` / `Staff@123`, open **Appointment Booking**. Today's list shows the seeded appointments.
2. **Final statuses.** Select the Completed row (Fever and cough) and press Cancel Appointment: a message says it is already Completed. Try Reschedule and Mark No-show on it: same kind of message. Select the Cancelled row (Knee pain) and press Mark Completed: already Cancelled.
3. **Queue rule.** Select a Booked row for today (for example Chest discomfort) and press Cancel Appointment: refused because the patient has already checked in on the queue board. (All seeded Booked rows for today are in the queue.)
4. **Future appointments for the next tests.** Book Luis Aquino with Dr. Villanueva 3 or more days ahead on a weekday (first free slot). Select the new row in the list.
5. **Reschedule, same date.** Press Reschedule, keep the date, choose a different time, **Move Appointment**: success message, token unchanged ("unchanged"), same code. The hint in the dialog says the current slot is not offered. Choose nothing and press Move Appointment: the time slot box shows the red icon.
6. **Reschedule, other date.** Reschedule it to another weekday: the message shows the new token number, the list jumps to the new date and selects the row. Pick a Sunday: the hint says the doctor does not see patients on Sundays and the slot list is empty. Pick the leave date of Dr. Villanueva (7 days ahead): the hint says he is on leave.
7. **Double-day rule.** Book Luis Aquino again with the same doctor on another date, then reschedule the first one onto that date: refused (already has another appointment that day).
8. **Cancel.** Select a future appointment, press Cancel Appointment, press the red button with an empty reason: red icon, nothing happens. Type a reason and confirm: success message, the row turns red (Cancelled). In the booking form pick that doctor and date: the freed slot is listed again. Book another patient into it: it works.
9. **Cancellation window.** On a weekday, book an appointment for today with a slot that starts within the next 2 hours, select it and press Cancel Appointment: refused, the message says it starts in N minutes. (If no such slot exists, run `UPDATE app_settings SET setting_value = '24' WHERE setting_key = 'cancellation_window_hours';` in phpMyAdmin, restart the app and try a slot for tomorrow; set the value back to `2` afterwards.) A slot more than 2 hours away can be cancelled.
10. **Mark Completed.** Select the seeded Booked row *Review of lab results* (Dr. Villanueva, 8:45 AM) and press Mark Completed, confirm: the row turns green. Press Cancel Appointment, Reschedule and Mark No-show on it: all refused. Select a future appointment and press Mark Completed: refused (future date).
11. **Mark No-show (success case).** All seeded rows are in the queue, so add a test row in phpMyAdmin (SQL tab):
    `INSERT INTO appointments (appointment_code, token_number, patient_id, doctor_id, appointment_date, time_slot, appointment_type_id, reason_for_visit, status_id, created_by_user_id) VALUES (CONCAT('APT-', DATE_FORMAT(CURDATE(), '%Y%m%d'), '-090'), 90, 3, 3, CURDATE(), '00:30', 1, 'No-show test', 1, 2);`
    Click **Show All** (or filter today) and select it: Cancel Appointment is refused (already started or passed). Press Mark No-show, confirm: the row turns red (No-show). A future appointment or one later today cannot be marked No-show.
12. Footer: the last action ("Cancelled APT-...", "Rescheduled ... to ...") appears in the summary line.
13. Re-import `schema.sql` afterwards to restore the seed data.

Known limits (by design): the doctor cannot be changed when rescheduling (cancel and book again for another doctor); no audit table exists for appointments, so only the last remark is kept in the appointment (cancel reason, or "Rescheduled from ..."); Mark Completed here only changes the appointment status (the consultation record comes with MP06).

---

## Part V: Step 9 (MP05 Patient Queue Board) - files and rules

### Where the Step 9 files go

| File | Destination (inside the project folder that contains the `.csproj`) |
|---|---|
| `QueueModels.cs` | `Models\MP05_QueueBoard\` |
| `QueueRepository.cs` | `Repositories\MP05_QueueBoard\` |
| `QueueBoardForm.cs` and `QueueBoardForm.Designer.cs` | `Views\MP05_QueueBoard\` |
| `QueueSkipDialog.cs` and `QueueSkipDialog.Designer.cs` | `Views\MP05_QueueBoard\` |
| `QueueWalkInDialog.cs` and `QueueWalkInDialog.Designer.cs` | `Views\MP05_QueueBoard\` |
| `ModuleRegistry.cs` (replaces the old one) | `Views\Main\` |
| `HMS_Project_Creation_Guide.md` | anywhere you keep the guide |

No new NuGet package and no schema change. The MP05 tab is visible to Receptionist, Doctor and Administrator.

### What the screen does

One board shows **one doctor, today**. The doctor drop-down starts on the doctor with the busiest queue.

| Area | Content |
|---|---|
| **Now Serving** | large token, patient name, patient code, appointment code, start time and the running consultation time (updates every second); the token turns red for an emergency |
| **Waiting list** | patients in call order with Pos, Token, Patient, Code, Type (Appointment / Walk-in / Emergency), Checked in, Skips and Est. wait; emergency rows are tinted red |
| **Buttons** | Call Next Patient, Skip Patient, Mark Complete; each is enabled only when it can act |
| **Statistics** | completed today, patients in the queue, emergencies waiting, running average consultation time |
| **Check-in** | today's Booked appointments of that doctor that have not arrived yet; tick *Emergency* and press **Check In Selected** (or double-click a row); **Add Walk-in / Emergency...** adds a patient without an appointment |

The board refreshes itself every 15 seconds (check box *Auto-refresh*, **Refresh** button for a manual refresh).

### Rules (all in `QueueRepository`, checked again inside every save transaction)

| Rule | Detail |
|---|---|
| One patient at a time | only one token can be **In Progress** per doctor; Call Next is refused (and disabled) while a consultation is running |
| Call order | 1. emergency patients, 2. patients not skipped before the skipped ones, 3. the patient skipped longest ago first, 4. lowest token (FIFO), then check-in order |
| Call Next | takes the first patient of the call order, stores `started_at` (database clock) |
| Skip (chosen behaviour) | only the patient **In Progress** can be skipped; a reason is **required** (max 250 characters) and saved in `queue_skip_log`; the patient goes **back to Waiting behind everybody who has not been skipped**; nobody is removed from the queue; `started_at` is cleared |
| Skipped emergency | stays ahead of normal patients (emergency always wins) but behind emergency patients who were not skipped |
| Mark Complete | only the patient In Progress; stores `completed_at`; the actual time taken is `completed_at - started_at`; the linked appointment becomes **Completed** (same as Mark Completed in Appointment Booking) |
| Average consultation time | average of today's completed visits of that doctor; the setting `default_consultation_minutes` (15) is used until the first visit is completed (shown as "(default)") |
| Estimated wait | **position in the waiting list x the doctor's average consultation time**, rounded to whole minutes (at least 1) |
| Check-in of an appointment | only today's **Booked** appointments; the queue token is the appointment's token when it is still free for that doctor today, otherwise the next free number; an appointment can check in only once |
| Walk-in / emergency | no appointment; the token is the next free number (above every queue token and appointment token of that doctor today); refused when the patient has a Booked appointment today with that doctor that has not checked in (check that appointment in instead) |
| No duplicates | a patient who is already Waiting or In Progress for that doctor today cannot be added again |
| Concurrency | the doctor row is locked (`FOR UPDATE`) first in every change; if someone else changed the queue first, a warning appears and the board refreshes |
| Interaction with MP04 | after check-in, Cancel / Reschedule / No-show of the appointment are refused (Part U); finish the visit here |

### How to test Step 9 (seed data of the day you imported `schema.sql`)

Seed: **Dr. Villanueva** has tokens 1-2 Completed (15 and 17 minutes), token 3 In Progress, token 4 Waiting and token 5 an **emergency walk-in**. Doctor 3 has tokens 1 and 2 Waiting (token 2 was skipped once). Doctor 5 has token 1 Waiting.

1. Log in as `reception` / `Staff@123` and open **Patient Queue Board**. The board opens on Dr. Villanueva.
2. **Board state.** Now Serving shows token 003. The waiting list shows the **emergency token 005 first** (red row), then token 004. Statistics: 2 completed, 2 waiting, 1 emergency, average **16 min**. Estimated waits: **16 min** (position 1) and **32 min** (position 2).
3. **One at a time.** Call Next Patient is disabled while token 003 is being seen.
4. **Mark Complete.** Press Mark Complete, confirm: the status line shows the minutes taken and the new average; Completed becomes 3; Now Serving becomes empty and says who is next in line (token 005, EMERGENCY). In **Appointment Booking** the matching appointment is now Completed.
5. **Emergency first.** Press Call Next Patient: token **005** (emergency) is called although token 004 has the lower number. The token is red.
6. **Mark Complete** again, then **Call Next** for token 004 and **Mark Complete** once more. The waiting list is empty, Now Serving says "Nobody is waiting" and Call Next Patient is disabled.
7. **Skip.** Switch the doctor to the one with tokens 1 and 2 (Dr. 3). Estimated waits start at **15 min** and **30 min** (default average). Press Call Next (token 001), then Skip Patient: press **Skip Patient** with an empty reason: red icon, nothing happens. Type a reason and confirm: token 001 is Waiting again, its Skips column shows *Skipped x1*, and it sits **after token 002** (skipped longer ago).
8. **Check-in of an appointment.** In **Appointment Booking** book a patient who has no appointment today with Dr. 5 (or any doctor) for a slot later today. On the queue board press **Refresh**: the appointment is listed under *Check-in*. Select it, tick **Emergency** and press **Check In Selected**: the patient appears **first** in the waiting list as an emergency. Check in another appointment without the tick: it joins the end.
9. **Walk-in.** Press **Add Walk-in / Emergency...**, search a patient, select, press **Add to Queue**: the token is the next free number and the type is *Walk-in*. Try the same patient again: warning that the patient is already on the queue. Try a patient who has a Booked appointment today with that doctor: warning to check in the appointment instead.
10. **Empty search.** In the walk-in dialog press Search with an empty box: red icon, nothing happens. Press Add to Queue without selecting a patient: a message asks for a patient.
11. **MP04 interaction.** In Appointment Booking select a checked-in Booked row and press Cancel Appointment: refused (already on the queue board).
12. **Auto-refresh.** Open the board in two windows (two logins/instances) and change the queue in one; the other updates within 15 seconds or on Refresh. Switch off *Auto-refresh* and confirm it no longer changes by itself.
13. Re-import `schema.sql` afterwards to restore the seed data.

Known limits (by design): the board is per doctor (there is no "all doctors" view); the status *Skipped* of the database is not used because a skipped patient returns to Waiting (documented choice); a patient who left cannot be removed from the queue; only today's queue is shown.

---

## Part W: Step 10 (MP06 Consultation & Vital Signs) - files and rules

### Where the Step 10 files go

| File | Destination (inside the project folder that contains the `.csproj`) |
|---|---|
| `ConsultationModels.cs` | `Models\MP06_ConsultationVitals\` |
| `ConsultationRepository.cs` | `Repositories\MP06_ConsultationVitals\` |
| `ConsultationForm.cs` and `ConsultationForm.Designer.cs` | `Views\MP06_ConsultationVitals\` |
| `ModuleRegistry.cs` (replaces the old one; MP05 and MP06 are both registered) | `Views\Main\` |
| `HMS_Project_Creation_Guide.md` | anywhere you keep the guide |

No new NuGet package and no schema change. The MP06 tab is visible to Doctor and Administrator.

### What the screen does

Two views, switched with the buttons at the top.

**New Consultation**

| Area | Content |
|---|---|
| 1. Appointment picker | today's appointments that are **Booked or Completed** and have **no consultation yet**; filter by doctor; the column *Queue* shows Waiting / In Progress / Completed / Not arrived; patients being seen are listed first; the line under the grid shows patient, age, code, doctor, reason and allergies |
| 2. Vital signs | Systolic BP, Diastolic BP, Temperature, Pulse, Weight (all required), Height (optional) |
| 3. Clinical notes | Chief complaint (required), Examination findings, Diagnosis (required), Treatment plan, Special instructions, optional Next follow-up date |

**History & Chart Notes:** every consultation recorded so far (newest first), a patient name/code filter, and a read-only formatted chart note (patient, visit, vitals with BMI when the height is known, notes, follow-up).

### Rules

| Rule | Detail |
|---|---|
| Appointment | only today's Booked or Completed appointments without a consultation; Cancelled, No-show, other dates and appointments that already have a consultation are never listed |
| Mandatory | Systolic BP, Diastolic BP, Temperature, Pulse, Weight, Chief complaint and **at least one Diagnosis**; Height is optional |
| Numeric ranges | Systolic BP **50-250** mmHg, Diastolic BP **30-150** mmHg, Temperature **30-45** °C, Pulse **20-250** bpm, Weight **greater than 0** (the database repeats these in CHECK constraints) |
| Extra guards (typing mistakes) | Diastolic BP must be lower than systolic BP; Weight at most 500 kg; Height 30-250 cm when entered; Temperature, Weight and Height take at most 1 decimal place (the database stores one decimal) |
| Input | BP and pulse accept digits only; temperature, weight and height accept digits and one decimal point; the text boxes accept the same characters as every free-text box (letters, digits, space and `, . - # / ' ( )`) |
| Follow-up | optional; when ticked it must be after today (tomorrow to two years ahead) |
| Code and time stamp | `CON-YYYYMMDD-XXX` from the code generator inside the save transaction; `recorded_at` from the database clock; `recorded_by` is the logged-in user |
| One consultation per appointment | the picker hides appointments with a consultation; Save checks again while the appointment row is locked (`FOR UPDATE`); the database also has a unique key on `appointment_id`; if someone else saved first, a warning appears and the list refreshes |
| Appointment status | saving a consultation does **not** change the appointment status (the queue board completes it, or Mark Completed in Appointment Booking) |
| Clear Form | resets all fields; the selected appointment stays selected |

### How to test Step 10 (seed data of the day you imported `schema.sql`; re-import it first if you tested Step 9)

Seed: consultations 001 and 002 of today (Maria Santos, Juan Dela Cruz) and one from 40 days ago (Pedro Garcia) already exist. Appointments of today **without** a consultation: Marco Reyes (Chest discomfort, Dr. Villanueva, In Progress), Carlo Bautista (Ankle sprain), Marianne Lopez (Review of lab results), Pedro Garcia (Palpitations) and Ana Liza Mendoza (Follow-up ECG).

1. Log in as `drvillanueva` / `Staff@123` and open **Consultations & Vitals**. The picker lists the 5 appointments above, **Marco Reyes first** (Queue = In Progress). Maria Santos, Juan Dela Cruz and the cancelled *Knee pain* appointment are not listed. Choose a doctor in the filter: the list shrinks to that doctor.
2. **Save is disabled** until an appointment is selected. Select Marco Reyes: the line under the grid shows his name, age, code, doctor and reason.
3. **Everything empty.** Press **Save Consultation**: red icons appear on every required field (five vitals, chief complaint, diagnosis) and a message lists the problems.
4. **Out-of-range vitals.** Enter Systolic `300` (message: between 50 and 250 mmHg), Diastolic `20`, Temperature `46`, Pulse `10`, Weight `0`: each is rejected next to its field. Enter Systolic `120` and Diastolic `130`: diastolic must be lower than systolic. Enter Temperature `38.55`: at most 1 decimal place. Letters cannot be typed into the numeric boxes.
5. **Diagnosis.** Fill every vital correctly and the chief complaint, leave Diagnosis empty and Save: "Enter at least one diagnosis before saving."
6. **Successful save.** Systolic `130`, Diastolic `85`, Temperature `36.8`, Pulse `88`, Weight `70.5`, Height `170`, Chief complaint `Chest discomfort for two days`, Diagnosis `Costochondritis`, tick *Schedule a next follow-up date* (keep the default date) and Save. A message shows the new code (today's next number, normally `-003`), the time and your name. The form clears and **Marco Reyes disappears from the picker**.
7. **Optional height.** Select another appointment, leave Height empty, fill the rest and save: it works.
8. **One per appointment.** Start a second copy of the application, log in, open this screen and select the same appointment in both copies. Save in the first, then Save in the second: a warning says the appointment already has a consultation and the list refreshes.
9. **History.** Press *History and Chart Notes*: newest first, your new consultations on top, then Juan Dela Cruz, Maria Santos and Pedro Garcia (40 days ago). Select a row: the chart note appears (the entry for Marco shows BMI 24.4). Type `garcia` and Search: only Pedro Garcia's note. **Show All** restores the list. Search a name with no consultation: the count line says none was found and the note panel asks you to select a consultation.
10. Return to *New Consultation* and press **Clear Form** after typing some values: every field empties and the selected appointment stays selected.
11. Re-import `schema.sql` afterwards to restore the seed data.

Known limits (by design): only today's appointments can be consulted; a saved consultation cannot be edited (no update rule in the specification); the prescription of MP07 will be linked to these consultations.

---

## Part X: Step 11 (MP07 Prescription Builder) - files and rules

### Where the Step 11 files go

| File | Destination (inside the project folder that contains the `.csproj`) |
|---|---|
| `PrescriptionModels.cs` | `Models\MP07_Prescription\` |
| `PrescriptionRepository.cs` | `Repositories\MP07_Prescription\` |
| `PrescriptionForm.cs` and `PrescriptionForm.Designer.cs` | `Views\MP07_Prescription\` |
| `PrescriptionPreviewDialog.cs` and `PrescriptionPreviewDialog.Designer.cs` | `Views\MP07_Prescription\` |
| `ModuleRegistry.cs` (replaces the old one; MP05, MP06 and MP07 are registered) | `Views\Main\` |
| `ConsultationForm.cs` and `ConsultationForm.Designer.cs` (MP06 re-delivery, see below) | `Views\MP06_ConsultationVitals\` |
| `HMS_Project_Creation_Guide.md` | anywhere you keep the guide |

No new NuGet package and no schema change. The MP07 tab is visible to Doctor and Administrator.

**About the MP06 re-delivery.** Visual Studio's [Design] view deletes grid columns that a designer file creates (this is why the MP06 grids showed every property). From now on the grid **columns are created in the form's `.cs`** (method `BuildGridColumns`) and the Designer only holds layout and colours. The MP06 buttons are also no longer docked, so you can resize them in [Design]. Use the two MP06 files of this step if you have not fixed MP06 yourself.

### What the screen does

Two views, switched with the buttons at the top.

**New Prescription**

| Area | Content |
|---|---|
| 1. Consultation | consultations of the last 60 days, newest first, filter by patient; the *Rx* column shows how many prescriptions the consultation already has |
| 2. Details | General instructions (optional) and **Valid until** (default: today + the setting `prescription_validity_days`, 30; editable, from today to one year) |
| 3. Add a medicine line | Medicine (drop-down of the catalog with the usable stock), Dosage, Times per day, Duration (days), **Quantity (calculated, read-only)**, Instructions, and a stock hint |
| Lines grid | Edit Line and Remove Line (double-click also edits), a *Stock check* column, and a total line |

After **Save Prescription** a white, print-style preview opens (hospital, patient, allergies, doctor, diagnosis, dates, numbered medicine lines with dosage / frequency / quantity, general instructions, signature line).

**Issued Prescriptions:** every prescription issued so far (filter by patient) and the same preview (button or double-click).

### Rules

| Rule | Detail |
|---|---|
| At least one line | saving without a medicine line is refused with a message next to the lines grid |
| Medicine from the catalog | a drop-down, no free typing (so it can be matched to the pharmacy stock); a medicine may appear only once per prescription (edit its line instead) |
| Quantity | always **times per day x days**; calculated while typing, and calculated again by the database when saving |
| Positive whole numbers | Times per day 1-24, Duration 1-365 days (digits only; the upper limits catch typing mistakes) |
| Dosage | required, at most 50 characters (for example `500mg`) |
| Valid until | not before today and at most 365 days ahead (the database also requires valid until >= issue date) |
| **Stock warning (never blocks)** | usable stock = remaining quantity of the batches that have **not expired** (view `v_medicine_stock`); when a line needs more, the hint turns orange and a confirmation asks whether to add the line anyway; the grid shows *Short by N*. Prescriptions that are issued but not dispensed yet do not reserve stock; the dispensing screen (MP09) is the real check |
| Code and date | `PRE-YYYYMMDD-XXX` from the code generator inside the save transaction; the issue date is the database date; header and lines are saved together or not at all |
| Several per consultation | a consultation may have more than one prescription (the schema allows it, for example a follow-up prescription) |

### How to test Step 11 (seed data of the day you imported `schema.sql`)

Seed: prescriptions 1 and 2 (today) and 3 (40 days ago) exist. Usable stock: Paracetamol 490, Amlodipine **40**, Salbutamol Syrup **30** (15 expired are not counted), Ibuprofen **0** (its only batch has expired), Ceftriaxone 105.

1. Log in as `drvillanueva` / `Staff@123` and open **Prescription Builder**. The consultation list shows the three consultations (the *Rx* column is `1` for each). **Save Prescription** is disabled until you select one.
2. Select the consultation of Juan Dela Cruz: the line under the grid shows patient, age, code, doctor, diagnosis and allergies.
3. **Empty save.** Select a consultation and press Save: the message says to add at least one medicine line, and a red icon appears at the lines grid.
4. **Empty line.** Press **Add Line** with nothing entered: red icons on Medicine, Dosage, Times per day and Duration.
5. **Quantity.** Choose *Paracetamol 500mg*, Dosage `500mg`, Times per day `3`, Duration `5`: **Quantity shows 15** and the hint says the stock is enough. Letters cannot be typed in the number boxes; `0`, `25` (times per day) and `400` (days) are rejected with a range message.
6. **Add and total.** Press Add Line: the line appears, the form empties, and the total reads 1 medicine, 15 units. Add *Cetirizine 10mg*, `10mg`, 1 x 5 (instructions `At bedtime`).
7. **Duplicate.** Try to add Paracetamol again: "already in this prescription. Edit that line instead."
8. **Stock warning.** Choose *Amlodipine 5mg*, `5mg`, 1 per day, 60 days: the hint turns orange (needs 60, only 40 available, short by 20). Press Add Line: a confirmation appears; answer **Yes**: the line is added and its Stock check column shows *Short by 20*. Now choose *Ibuprofen 400mg*: the hint says 0 available (status Expired).
9. **Edit and remove.** Select the Cetirizine line and press **Edit Line** (the group changes to *Edit the medicine line* and the button to *Update Line*): change the duration to 7 and Update: quantity becomes 7. Press Edit then **Cancel Edit**: the form empties. Select the Amlodipine line and **Remove Line**: confirm, and it disappears.
10. **Valid until.** The date defaults to today + 30 days; you cannot choose a date before today.
11. **Save.** Add General instructions `Take after meals`, press **Save Prescription**: the white preview opens with the new code (today's next number, normally `PRE-...-003`), the patient, doctor, the two lines with quantities, the instructions and the signature line. Close it: the form is empty and the *Rx* column of that consultation shows `2`.
12. **Issued Prescriptions.** Switch the view: your prescription is on top; select a row and press **Preview / Print View** (or double-click). Type `garcia` or a patient name and press Search; **Show All** restores the list.
13. Re-import `schema.sql` afterwards to restore the seed data.

Known limits (by design): consultations older than 60 days are not listed; a saved prescription cannot be edited or deleted (the specification has no such rule); nothing is really printed.

---

## Part Y: Step 17 (final-project fixes) - files and rules

Team list: (1) drop-down lists not lined up with their box, (2) grids cutting off text, (3) align the code with the new database, plus the hospital logo and a final check of MP01-MP12.

### Where the Step 17 files go

| File | Location | Status |
|---|---|---|
| `HospitalManagementSystem.csproj` | project root | **Replaces** (`ApplicationHighDpiMode = PerMonitorV2`, logo files copied to the output, `.exe` icon) |
| `Program.cs` | project root | **Replaces** (comment only) |
| `ComboBoxHelper.cs`, `FormHelper.cs`, `LogoHelper.cs` | `Helpers\` | New |
| `GridHelper.cs` | `Helpers\` | **Replaces** (auto-size + wrap, `AddTextColumn`, `FitColumns`) |
| `DoctorScheduleRepository.cs` | `Repositories\MP03_DoctorScheduling\` | **Replaces** (leave list also returns `day_name`) |
| every form `.cs` under `Views\` | same folders | **Replaces** (one added line: `FormHelper.Prepare(this);`) |
| `PatientRegistrationForm.cs`, `DoctorManagementForm.cs`, `ScheduleBuilderForm.cs`, `AppointmentBookingForm.cs`, `QueueBoardForm.cs` | their `Views\MP0x_...` folders | **Replaces** (new `BuildGridColumns()`) |
| 10 `*.Designer.cs` files with fixed `DropDownWidth` values | their folders | **Replaces** (the fixed `DropDownWidth = 460/520` lines removed) |
| `LoginForm.cs`, `MainForm.cs` | `Views\Auth\`, `Views\Main\` | **Replaces** (logo) |
| `Assets\logo.png`, `Assets\app.ico` | new `Assets\` folder in the project | New (the logo; see `Assets\README.txt`) |

### 1. Drop-down (ComboBox) alignment

| Setting | Where |
|---|---|
| DPI awareness **PerMonitorV2** | `.csproj`: `<ApplicationHighDpiMode>PerMonitorV2</ApplicationHighDpiMode>`; `ApplicationConfiguration.Initialize()` applies it |
| `AutoScaleMode = Dpi` | already on all 28 forms (`AutoScaleDimensions` = the DPI each form was designed at: 120 or 96) |
| `DropDownWidth` = box width or longest item | `ComboBoxHelper` (called by `FormHelper.Prepare(this)` in every form). Re-measured each time the list opens, so new items and DPI changes are covered. Never wider than the screen. |

Cause of the old look: the app ran *System-aware* (Windows stretches and offsets its windows when a monitor's scaling differs from the main display) and 23 combo boxes had a fixed `DropDownWidth` of 460-560 design pixels, so the list opened much wider than (and offset from) the box.

### 2. DataGridView auto-size

`GridHelper.Apply` (used by every grid): `AutoSizeColumnsMode = Fill`, `DefaultCellStyle.WrapMode = True`, `AutoSizeRowsMode = AllCells`, header text wraps and the header row grows. `FitColumns` sets each column's `MinimumWidth` to the width its content needs (header and all cells, capped at 320 px), so Fill can never squeeze text away; when the window is too narrow a horizontal scroll bar appears. Text longer than the cap (notes, findings, reasons) wraps and the row grows. Rows keep a minimum height of 30 px (scaled).

**Grids that had lost their columns.** Visual Studio's [Design] view had deleted the designer-made columns of 9 grids (MP01 registered patients; MP03 doctor directory, schedule blocks, leave dates; MP04 patient picker and appointments list; MP05 waiting list and check-in list). They were showing auto-generated columns with raw names (`specialization_name`, `QueueId`, ...), the MP04 status colours and the MP05 emergency colour of the Type cell did not work. Their columns are now created in code (`BuildGridColumns`) with `DataPropertyName` = the exact column names returned by the repositories.

### 3. New database alignment (verified, not assumed)

Checked against a real MariaDB 10.11 import of `Database\schema.sql`:

| Check | Result |
|---|---|
| Every SQL statement in `Repositories\` and `Helpers\` (191) prepared against the schema (all table and column names resolve) | 0 errors |
| Every repository read method run (77) - result column names match what the code reads | 0 errors |
| End-to-end write flow per module (register, edit + audit, doctor + blocks + leave, book / reschedule / cancel, queue complete / call next / skip / walk-in, consultation, prescription, medicine + stock + adjust, dispense, lab test + result, bill draft / finalize, payments) | all pass; every business-rule refusal fires |
| Grid `DataPropertyName` (string names) vs. the real result columns | all match |
| ComboBox `DisplayMember` / `ValueMember` | all use `nameof(...)` (compiler-checked) |
| Parameter data types | text for business codes (`VARCHAR` keys), `int` for auto-increment ids, `decimal` for money, `DateTime` for dates |
| Connection string (`appsettings.json`) | `Database=hospital_management_db` - matches `schema.sql`; no change needed |

### 4. Logo

`Assets\logo.png` is shown on the login card (instead of the cross symbol) and at the left of the main window header; `Assets\app.ico` is the window / taskbar / `.exe` icon. Without the files the app runs with the old text-only look.

### 5. MP01-MP12 check against the machine-problem specs

All required features, rules and UI elements are present. Deliberate differences (keep them in the README): data is stored in MySQL instead of in-memory lists (merged final project); MP01 phone = Philippine mobile `+63 9XX XXX XXXX` (stricter than "7+ digits"); MP05 a skipped patient returns to Waiting behind non-skipped patients (the spec lets the team pick); MP09 a stock shortfall dispenses what is available (partial) and the rest stays on the prescription.

### How to test Step 17

1. Re-import `schema.sql`, rebuild (**Build > Rebuild Solution**), run.
2. Windows **Settings > Display > Scale** at 100 %, 125 % and 150 % (and drag the window between two monitors with different scaling if you have them): every drop-down list opens directly under its box, as wide as the box (wider only when an item would be cut).
3. Open every module: no column shows `...`; long notes wrap onto several lines; shrinking the window shows a horizontal scroll bar instead of cutting text.
4. MP03 Doctor Schedule shows headers *Doctor ID, Name, License No., Specialization ...* (not `specialization_name`); MP04 status cells are coloured; MP05 emergency rows are red with a dark-red Type cell.
5. The logo appears on the login screen, in the header and in the taskbar.
