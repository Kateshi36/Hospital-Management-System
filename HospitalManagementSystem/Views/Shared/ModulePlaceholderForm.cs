using HospitalManagementSystem.Helpers;
namespace HospitalManagementSystem.Views.Shared
{
    /// <summary>
    /// "Coming soon" page shown inside a tab for a module that is not built yet.
    /// All visuals are in ModulePlaceholderForm.Designer.cs.
    /// </summary>
    public partial class ModulePlaceholderForm : Form
    {
        public ModulePlaceholderForm() : this("Module", "MP00", 0, string.Empty)
        {
        }

        public ModulePlaceholderForm(string title, string moduleCode, int buildStep, string description)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            Text = title;
            lblModuleTitle.Text = title;
            lblModuleCode.Text = buildStep > 0
                ? $"{moduleCode}  |  Coming in build Step {buildStep}"
                : moduleCode;
            lblDescription.Text = description;
        }
    }
}