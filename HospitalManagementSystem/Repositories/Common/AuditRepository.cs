using System.Data;
using System.Globalization;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.Common
{
    /// <summary>
    /// Reads the audit trail for the Audit Trail screen (Administrator only): every row of audit_log (creates, finalizations,
    /// voids, payments, refunds, dispensing, stock changes and who OPENED which patient's record), plus the field-by-field
    /// patient edits of patient_audit_log, newest first. Nothing here changes data.
    /// </summary>
    public class AuditRepository
    {
        public static readonly string[] Actions =
        {
            AuditHelper.View, AuditHelper.Create, AuditHelper.Update, AuditHelper.Delete, AuditHelper.Finalize, AuditHelper.Void,
            AuditHelper.Payment, AuditHelper.Refund, AuditHelper.Dispense, AuditHelper.StockIn, AuditHelper.Adjust
        };

        public static readonly string[] EntityTypes =
        {
            AuditHelper.Patient, AuditHelper.Consultation, AuditHelper.Prescription, AuditHelper.Dispensing, AuditHelper.MedicineStock,
            AuditHelper.LabResult, AuditHelper.Bill, AuditHelper.PaymentRecord, AuditHelper.Doctor
        };

        // both sources shaped the same way; patient field changes become "Update Patient" rows
        private const string InnerSql =
            "SELECT a.audit_id AS seq, a.logged_at, u.full_name AS user_name, u.role, a.action, a.entity_type, a.entity_id, a.patient_id, a.details " +
            "FROM audit_log a LEFT JOIN users u ON u.user_id = a.user_id " +
            "UNION ALL " +
            "SELECT pa.audit_id, pa.changed_at, u.full_name, u.role, 'Update', 'Patient', pa.patient_id, pa.patient_id, " +
            "       CONCAT(pa.field_changed, ': ', COALESCE(pa.old_value, '(empty)'), ' -> ', COALESCE(pa.new_value, '(empty)')) " +
            "FROM patient_audit_log pa LEFT JOIN users u ON u.user_id = pa.changed_by_user_id";

        public List<AuditTrailRow> Search(AuditTrailFilter filter)
        {
            var sql = new StringBuilder(
                "SELECT x.logged_at, x.user_name, x.role, x.action, x.entity_type, x.entity_id, x.patient_id, x.details, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name " +
                "FROM (" + InnerSql + ") x LEFT JOIN patients p ON p.patient_id = x.patient_id WHERE 1 = 1");
            var parameters = new List<MySqlParameter>();

            if (!string.IsNullOrEmpty(filter.Action))
            {
                sql.Append(" AND x.action = @action");
                parameters.Add(DatabaseHelper.Param("@action", filter.Action));
            }
            if (!string.IsNullOrEmpty(filter.EntityType))
            {
                sql.Append(" AND x.entity_type = @type");
                parameters.Add(DatabaseHelper.Param("@type", filter.EntityType));
            }
            if (filter.From.HasValue)
            {
                sql.Append(" AND x.logged_at >= @from");
                parameters.Add(DatabaseHelper.Param("@from", filter.From.Value.Date));
            }
            if (filter.To.HasValue)
            {
                sql.Append(" AND x.logged_at < @to");
                parameters.Add(DatabaseHelper.Param("@to", filter.To.Value.Date.AddDays(1)));
            }

            string[] words = (filter.Text ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                string name = "@w" + i.ToString(CultureInfo.InvariantCulture);
                sql.Append($" AND (x.entity_id LIKE {name} OR x.patient_id LIKE {name} OR x.user_name LIKE {name} " +
                           $"OR x.details LIKE {name} OR p.first_name LIKE {name} OR p.last_name LIKE {name})");
                string escaped = words[i].Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
                parameters.Add(DatabaseHelper.Param(name, "%" + escaped + "%"));
            }

            int limit = Math.Clamp(filter.Limit, 1, 5000);
            sql.Append(" ORDER BY x.logged_at DESC, x.seq DESC LIMIT ").Append(limit.ToString(CultureInfo.InvariantCulture));

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(r => new AuditTrailRow
            {
                LoggedAt = DatabaseHelper.GetNullableDate(r, "logged_at") ?? DateTime.MinValue,
                UserName = DatabaseHelper.GetString(r, "user_name"),
                Role = DatabaseHelper.GetString(r, "role"),
                Action = DatabaseHelper.GetString(r, "action"),
                EntityType = DatabaseHelper.GetString(r, "entity_type"),
                EntityId = DatabaseHelper.GetString(r, "entity_id"),
                PatientId = DatabaseHelper.GetString(r, "patient_id"),
                PatientName = DatabaseHelper.GetString(r, "patient_name"),
                Details = DatabaseHelper.GetString(r, "details")
            }).ToList();
        }
    }
}
