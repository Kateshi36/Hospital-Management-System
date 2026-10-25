namespace HospitalManagementSystem.Models.Common
{
    /// <summary>
    /// One choice of a drop-down list (gender, specialization, a doctor, a patient ...).
    /// Use DisplayMember = "Name" and ValueMember = "Id" when binding to a ComboBox.
    /// For plain text lists (specialization, blood type ...) Id and Name are the same text,
    /// because the text itself is what the database stores. An empty Id is a placeholder row.
    /// </summary>
    public class LookupItem
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        /// <summary>Optional extra text.</summary>
        public string Extra { get; set; } = string.Empty;

        /// <summary>False for placeholder rows such as "All doctors" or "Select ...".</summary>
        public bool HasValue => !string.IsNullOrEmpty(Id);

        /// <summary>A text value whose id is the text itself.</summary>
        public static LookupItem Of(string text) => new LookupItem { Id = text, Name = text };

        /// <summary>A placeholder row ("All doctors", "Not specified" ...).</summary>
        public static LookupItem Placeholder(string text) => new LookupItem { Id = string.Empty, Name = text };

        public override string ToString() => Name;
    }
}
