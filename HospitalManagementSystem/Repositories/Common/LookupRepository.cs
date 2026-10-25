using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;

namespace HospitalManagementSystem.Repositories.Common
{
    /// <summary>
    /// The pick-lists used by drop-down boxes. Fixed lists mirror the ENUM columns in schema.sql;
    /// open lists (specialization, department, city ...) are the values already in use, and the
    /// combo boxes let staff type a new one.
    /// </summary>
    public class LookupRepository
    {
        public static readonly string[] Genders = { "Male", "Female", "Other" };
        public static readonly string[] BloodTypes = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
        public static readonly string[] Relationships = { "Spouse", "Parent", "Child", "Sibling", "Relative", "Friend", "Guardian", "Other" };
        public static readonly string[] EmploymentTypes = { "Full-time", "Part-time", "Visiting" };
        public static readonly string[] PaymentMethods = { "Cash", "Debit", "Credit", "Online", "Cheque" };

        public List<LookupItem> GetGenders() => Fixed(Genders);
        public List<LookupItem> GetBloodTypes() => Fixed(BloodTypes);
        public List<LookupItem> GetRelationships() => Fixed(Relationships);
        public List<LookupItem> GetEmploymentTypes() => Fixed(EmploymentTypes);

        /// <summary>Cities already used by patients; Extra holds the province last used with that city.</summary>
        public List<LookupItem> GetCities()
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT city, MAX(province) AS province FROM patients " +
                "WHERE city IS NOT NULL AND city <> '' GROUP BY city ORDER BY city");
            return table.Rows.Cast<DataRow>()
                .Select(r => new LookupItem
                {
                    Id = DatabaseHelper.GetString(r, "city"),
                    Name = DatabaseHelper.GetString(r, "city"),
                    Extra = DatabaseHelper.GetString(r, "province")
                })
                .ToList();
        }

        /// <summary>Doctor specializations already in use (MP03).</summary>
        public List<LookupItem> GetSpecializations() => Distinct("doctors", "specialization");

        /// <summary>Hospital departments already in use (MP03).</summary>
        public List<LookupItem> GetDepartments() => Distinct("doctors", "department");

        public static List<LookupItem> Fixed(IEnumerable<string> values) => values.Select(LookupItem.Of).ToList();

        /// <summary>SELECT DISTINCT of one text column. Table/column names are code constants, never user input.</summary>
        public static List<LookupItem> Distinct(string table, string column)
        {
            DataTable rows = DatabaseHelper.ExecuteQuery(
                $"SELECT DISTINCT {column} AS v FROM {table} WHERE {column} IS NOT NULL AND {column} <> '' ORDER BY {column}");
            return rows.Rows.Cast<DataRow>().Select(r => LookupItem.Of(DatabaseHelper.GetString(r, "v"))).ToList();
        }
    }
}
