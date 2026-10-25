using System.Data;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP02_PatientSearchEditor;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP02_PatientSearchEditor
{
    /// <summary>SQL for searching patients (MP02). The profile editor and audit queries are added in Part 5B.</summary>
    public class PatientSearchRepository
    {
        private const int MaxRows = 500;

        /// <summary>
        /// Finds patients matching ALL filled criteria. Matching is case-insensitive and partial:
        ///  * Code  : any part of the code ("003" or "PAT-2026")
        ///  * Name  : every word typed must appear in the first, middle or last name ("mar" finds Maria and Marco)
        ///  * Phone : digits only; a leading 0 or 63 is ignored on both sides (see PhoneHelper.ToSearchDigits)
        ///  * DOB   : exact date
        /// </summary>
        public DataTable Search(PatientSearchCriteria criteria)
        {
            var sql = new StringBuilder(
                "SELECT p.patient_id, " +
                "       CONCAT(p.last_name, ', ', p.first_name, " +
                "              IF(p.middle_name IS NULL OR p.middle_name = '', '', CONCAT(' ', LEFT(p.middle_name, 1), '.'))) AS full_name, " +
                "       p.phone_number, " +
                "       DATE_FORMAT(p.date_of_birth, '%b %e, %Y') AS birth_date, " +
                "       TIMESTAMPDIFF(YEAR, p.date_of_birth, CURDATE()) AS age, " +
                "       p.gender, " +
                "       COALESCE(p.city, '') AS city " +
                "FROM patients p " +
                "WHERE 1 = 1");

            var parameters = new List<MySqlParameter>();

            if (!string.IsNullOrWhiteSpace(criteria.Code))
            {
                sql.Append(" AND p.patient_id LIKE @code");
                parameters.Add(DatabaseHelper.Param("@code", "%" + EscapeLike(criteria.Code.Trim()) + "%"));
            }

            string[] words = criteria.Name.Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                sql.Append($" AND (p.first_name LIKE @w{i} OR p.middle_name LIKE @w{i} OR p.last_name LIKE @w{i})");
                parameters.Add(DatabaseHelper.Param($"@w{i}", "%" + EscapeLike(words[i]) + "%"));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Phone))
            {
                // Compare only the 10 digits after the country/trunk prefix, so "0917...", "+63917..."
                // and "917..." all find the same patient whichever way the number was stored.
                string digits = PhoneHelper.ToSearchDigits(criteria.Phone);
                if (digits.Length == 0)
                {
                    sql.Append(" AND 1 = 0");   // nothing usable was typed, so nothing can match
                }
                else
                {
                    sql.Append(" AND (CASE WHEN REGEXP_REPLACE(p.phone_number, '[^0-9]', '') LIKE '63%' " +
                               "            THEN SUBSTRING(REGEXP_REPLACE(p.phone_number, '[^0-9]', ''), 3) " +
                               "            WHEN REGEXP_REPLACE(p.phone_number, '[^0-9]', '') LIKE '0%' " +
                               "            THEN SUBSTRING(REGEXP_REPLACE(p.phone_number, '[^0-9]', ''), 2) " +
                               "            ELSE REGEXP_REPLACE(p.phone_number, '[^0-9]', '') END) LIKE @phone");
                    parameters.Add(DatabaseHelper.Param("@phone", "%" + digits + "%"));
                }
            }

            if (criteria.DateOfBirth.HasValue)
            {
                sql.Append(" AND p.date_of_birth = @dob");
                parameters.Add(DatabaseHelper.Param("@dob", criteria.DateOfBirth.Value.Date));
            }

            sql.Append($" ORDER BY p.last_name, p.first_name, p.patient_id LIMIT {MaxRows}");
            return DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
        }

        /// <summary>Makes % and _ typed by the user match literally instead of acting as wildcards.</summary>
        private static string EscapeLike(string text)
        {
            return text.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
        }
    }
}