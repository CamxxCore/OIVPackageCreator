using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace OIVPackageCreator
{
    public delegate void GenericFileWizardFinishedDelegate(object sender, OIVGenericFile[] files);

    public partial class AddGenericFileWizard : Form
    {
        private List<OIVGenericFile> files = 
            new List<OIVGenericFile>();

        public event GenericFileWizardFinishedDelegate Finished;

        public AddGenericFileWizard()
        {
            InitializeComponent();
            wizard1.CancelButtonClick += Wizard1_CancelButtonClick;
            wizard1.FinishButtonClick += Wizard1_FinishButtonClick;
            listBox2.SelectedIndexChanged += ListBox2_SelectedIndexChanged;
            textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void Wizard1_FinishButtonClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Finished?.Invoke(this, files.ToArray());
            Close();
        }

        private void Wizard1_CancelButtonClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            var currentValue = files[listBox2.SelectedIndex].Destination;

            files[listBox2.SelectedIndex] =
               new OIVGenericFile(currentValue, textBox1.Text);
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = files[listBox2.SelectedIndex].Destination;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { Multiselect = true } )
            {
                var result = ofd.ShowDialog();

                switch (result)
                {
                    case DialogResult.Cancel: break;
                    case DialogResult.OK:
                        foreach (var file in ofd.FileNames)
                            files.Add(new OIVGenericFile(file, file.Substring(file.LastIndexOf("\\") + 1)));
                        break;
                }

                ofd.Dispose();
            }

            listBox1.Items.Clear();
            listBox2.Items.Clear();

            foreach (var file in files)
            {
                var filename = file.Source.Substring(file.Source.LastIndexOf("\\") + 1);

                listBox1.Items.Add(filename);
                listBox2.Items.Add(filename);
            }
        }
    }
}
