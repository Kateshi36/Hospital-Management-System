using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;

namespace HospitalManagementSystem.Repositories.Common
{
    /// <summary>
    /// Read-only queries for the dashboard. Nothing here changes data.
    /// Column names returned match the DataPropertyName values of the dashboard grids.
    /// </summary>
    public class DashboardRepository
    {
        /// <summary>
        /// The dashboard's search-as-you-type: patients whose ID, first / last name or phone number contain every typed word,
        /// best matches first (ID or last name starting with the text). Returns at most <paramref name="limit"/> rows.
        /// </summary>
        public List<LookupItem> FindPatients(string text, int limit = 8)
        {
            string[] words = (text ?? string.Empty).Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0) return new List<LookupItem>();

            var sql = new System.Text.StringBuilder(
                "SELECT patient_id, first_name, last_name, phone_number, " +
                "       TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) AS age " +
                "FROM patients WHERE 1 = 1");
            var parameters = new List<MySqlConnector.MySqlParameter>();
            for (int i = 0; i < words.Length; i++)
            {
                string name = "@w" + i.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string digits = new string(words[i].Where(char.IsDigit).ToArray());
                sql.Append($" AND (patient_id LIKE {name} OR first_name LIKE {name} OR last_name LIKE {name} OR middle_name LIKE {name}");
                if (digits.Length >= 3)
                {
                    sql.Append($" OR REPLACE(REPLACE(REPLACE(phone_number, '-', ''), ' ', ''), '+', '') LIKE {name}d");
                    parameters.Add(DatabaseHelper.Param(name + "d", "%" + digits + "%"));
                }
                sql.Append(')');
                string escaped = words[i].Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
                parameters.Add(DatabaseHelper.Param(name, "%" + escaped + "%"));
            }
            parameters.Add(DatabaseHelper.Param("@start", words[0] + "%"));
            sql.Append(" ORDER BY (patient_id LIKE @start OR last_name LIKE @start) DESC, last_name, first_name LIMIT ")
               .Append(Math.Clamp(limit, 1, 50).ToString(System.Globalization.CultureInfo.InvariantCulture));

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(r => new LookupItem
            {
                Id = DatabaseHelper.GetString(r, "patient_id"),
                Name = $"{DatabaseHelper.GetString(r, "last_name")}, {DatabaseHelper.GetString(r, "first_name")}",
                Extra = $"{DatabaseHelper.GetString(r, "patient_id")}  ·  {DatabaseHelper.GetInt(r, "age")} y  ·  {PhoneHelper.Format(DatabaseHelper.GetString(r, "phone_number"))}"
            }).ToList();
        }

        /// <summary>The five card numbers, fetched in a single round trip.</summary>
        public DashboardStats GetStats()
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT " +
                "  (SELECT COUNT(*) FROM patients) AS total_patients, " +
                "  (SELECT COUNT(*) FROM patients WHERE DATE(registered_at) = CURDATE()) AS registrations_today, " +
                "  (SELECT COUNT(*) FROM appointments WHERE appointment_date = CURDATE()) AS appointments_today, " +
                "  (SELECT COUNT(*) FROM appointments WHERE appointment_date = CURDATE() AND status = 'Completed') AS completed_today, " +
                "  (SELECT COUNT(*) FROM appointments WHERE appointment_date = CURDATE() AND status = 'Booked') AS booked_today, " +
                "  (SELECT COUNT(*) FROM appointments WHERE appointment_date = CURDATE() AND status = 'Cancelled') AS cancelled_today, " +
                "  (SELECT COUNT(*) FROM appointments WHERE appointment_date = CURDATE() AND status = 'No-show') AS noshow_today, " +
                "  (SELECT COUNT(*) FROM queue_entries WHERE queue_date = CURDATE() AND queue_status = 'Waiting' AND is_emergency = 1) AS emergencies_waiting, " +
                "  (SELECT COUNT(*) FROM payments WHERE DATE(payment_datetime) = CURDATE() AND payment_type = 'Payment') AS payments_today, " +
                "  (SELECT COUNT(*) FROM queue_entries WHERE queue_date = CURDATE() AND queue_status IN ('Waiting', 'In Progress')) AS active_queue, " +
                // day_of_week uses 0=Sunday..6=Saturday; MySQL DAYOFWEEK is 1=Sunday..7=Saturday
                "  (SELECT COUNT(DISTINCT ds.doctor_id) FROM doctor_schedules ds " +
                "     WHERE ds.day_of_week = (DAYOFWEEK(CURDATE()) - 1) " +
                "       AND NOT EXISTS (SELECT 1 FROM doctor_leaves dl WHERE dl.doctor_id = ds.doctor_id AND dl.leave_date = CURDATE())) AS doctors_on_duty, " +
                "  (SELECT COUNT(*) FROM lab_orders WHERE status = 'Pending') AS labs_pending, " +
                "  (SELECT COALESCE(SUM(CASE WHEN payment_type = 'Refund' THEN -amount ELSE amount END), 0) FROM payments " +
                "   WHERE DATE(payment_datetime) = CURDATE()) AS revenue_today");

            DataRow row = table.Rows[0];
            return new DashboardStats
            {
                TotalPatients = DatabaseHelper.GetInt(row, "total_patients"),
                RegistrationsToday = DatabaseHelper.GetInt(row, "registrations_today"),
                AppointmentsToday = DatabaseHelper.GetInt(row, "appointments_today"),
                CompletedToday = DatabaseHelper.GetInt(row, "completed_today"),
                BookedToday = DatabaseHelper.GetInt(row, "booked_today"),
                CancelledToday = DatabaseHelper.GetInt(row, "cancelled_today"),
                NoShowToday = DatabaseHelper.GetInt(row, "noshow_today"),
                EmergenciesWaiting = DatabaseHelper.GetInt(row, "emergencies_waiting"),
                PaymentsToday = DatabaseHelper.GetInt(row, "payments_today"),
                ActiveQueue = DatabaseHelper.GetInt(row, "active_queue"),
                DoctorsOnDuty = DatabaseHelper.GetInt(row, "doctors_on_duty"),
                LabTestsPending = DatabaseHelper.GetInt(row, "labs_pending"),
                RevenueToday = DatabaseHelper.GetDecimal(row, "revenue_today")
            };
        }

        /// <summary>The 50 most recently registered patients.</summary>
        public DataTable GetRecentPatients()
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT p.patient_id, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
                "       TIMESTAMPDIFF(YEAR, p.date_of_birth, CURDATE()) AS age, " +
                "       p.gender, " +
                "       DATE_FORMAT(p.registered_at, '%b %e, %Y %l:%i %p') AS registered, " +
                "       p.phone_number AS phone " +
                "FROM patients p " +
                "ORDER BY p.registered_at DESC, p.patient_id DESC LIMIT 50");
        }

        /// <summary>Today's queue across all doctors (in progress first, then waiting).</summary>
        public DataTable GetTodayQueue()
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT q.token_number, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
                "       CONCAT('Dr. ', d.last_name) AS doctor_name, " +
                "       q.queue_status, " +
                "       CASE WHEN q.is_emergency = 1 THEN 'Emergency' ELSE 'Normal' END AS priority " +
                "FROM queue_entries q " +
                "JOIN patients p ON p.patient_id = q.patient_id " +
                "JOIN doctors d ON d.doctor_id = q.doctor_id " +
                "WHERE q.queue_date = CURDATE() " +
                "ORDER BY d.doctor_id, FIELD(q.queue_status, 'In Progress', 'Waiting', 'Skipped', 'Completed'), q.is_emergency DESC, q.token_number");
        }

        /// <summary>Today's appointments by time slot.</summary>
        public DataTable GetTodayAppointments()
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT TIME_FORMAT(a.time_slot, '%h:%i %p') AS appt_time, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
                "       CONCAT('Dr. ', d.first_name, ' ', d.last_name) AS doctor_name, " +
                "       a.status AS status_name " +
                "FROM appointments a " +
                "JOIN patients p ON p.patient_id = a.patient_id " +
                "JOIN doctors d ON d.doctor_id = a.doctor_id " +
                "WHERE a.appointment_date = CURDATE() " +
                "ORDER BY a.time_slot, a.doctor_id");
        }

        /// <summary>Medicines that are Expired, Expiring Soon or Low Stock (worst first).</summary>
        /// <summary>
        /// "Queue right now" on the dashboard: patients being seen first, then the next in line (emergencies first).
        /// minutes_in = minutes since the consultation started (In Progress only).
        /// </summary>
        public DataTable GetQueueHighlights(int limit = 4)
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT q.token_number, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
                "       CONCAT('Dr. ', d.last_name) AS doctor_name, " +
                "       q.queue_status, q.is_emergency, " +
                "       TIMESTAMPDIFF(MINUTE, q.started_at, NOW()) AS minutes_in " +
                "FROM queue_entries q " +
                "JOIN patients p ON p.patient_id = q.patient_id " +
                "JOIN doctors d ON d.doctor_id = q.doctor_id " +
                "WHERE q.queue_date = CURDATE() AND q.queue_status IN ('In Progress', 'Waiting') " +
                "ORDER BY FIELD(q.queue_status, 'In Progress', 'Waiting'), q.is_emergency DESC, q.token_number " +
                "LIMIT @limit",
                DatabaseHelper.Param("@limit", limit));
        }

        public DataTable GetStockAlerts()
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT medicine_name, total_stock, reorder_level, stock_status " +
                "FROM v_medicine_stock_status " +
                "WHERE stock_status <> 'Normal' " +
                "ORDER BY FIELD(stock_status, 'Expired', 'Expiring Soon', 'Low Stock'), medicine_name");
        }

        /// <summary>Consultations recorded today, newest first.</summary>
        public DataTable GetTodayConsultations()
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT c.consultation_id, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
                "       CONCAT('Dr. ', d.last_name) AS doctor_name, " +
                "       c.diagnosis " +
                "FROM consultations c " +
                "JOIN appointments a ON a.appointment_id = c.appointment_id " +
                "JOIN patients p ON p.patient_id = a.patient_id " +
                "JOIN doctors d ON d.doctor_id = a.doctor_id " +
                "WHERE DATE(c.recorded_at) = CURDATE() " +
                "ORDER BY c.recorded_at DESC");
        }
    }
}