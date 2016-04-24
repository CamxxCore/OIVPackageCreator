using System;
using System.Windows.Forms;

namespace OIVPackageCreator
{
    public partial class WizardSplashPage : Form
    {
        public AddGenericFileWizard GenericFileWizard;

        public AddArchiveFileWizard ArchiveFileWizard;

        public WizardSplashPage()
        {
            InitializeComponent();
            GenericFileWizard = new AddGenericFileWizard();
            ArchiveFileWizard = new AddArchiveFileWizard();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ArchiveFileWizard.Show();
            }

            else
            {
                GenericFileWizard.Show();
            }

            Hide();
        }
    }
}
