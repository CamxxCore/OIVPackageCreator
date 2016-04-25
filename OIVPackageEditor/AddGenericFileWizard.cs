using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace OIVPackageEditor
{
    public delegate void GenericFileWizardFinishedDelegate(object sender, OIVGenericFile[] files);

    public partial class AddGenericFileWizard : Form
    {
        public bool Active { get; set; }

        private List<OIVGenericFile> files = new List<OIVGenericFile>();

        public event GenericFileWizardFinishedDelegate Finished;

        public AddGenericFileWizard()
        {
            InitializeComponent();
            wizard1.CancelButtonClick += CancelButtonClick;
            wizard1.FinishButtonClick += FinishButtonClick;
            listBox2.SelectedIndexChanged += ListBox2_SelectedIndexChanged;
            textBox1.LostFocus += TextBox1_LostFocus;
        }

        private void FinishButtonClick(object sender, CancelEventArgs e)
        {
            Finished?.Invoke(this, files.ToArray());
            Close();
        }

        private void TextBox1_LostFocus(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0)
            {
                foreach (var item in files)
                    item.Destination = textBox1.Text;
            }

            else
            {
                files[listBox2.SelectedIndex].Destination = textBox1.Text;
            }
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0) return;
            textBox1.Text = files[listBox2.SelectedIndex].Destination;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { Multiselect = true })
            {
                var result = ofd.ShowDialog();

                switch (result)
                {
                    case DialogResult.Cancel: break;
                    case DialogResult.OK:
                        foreach (var file in ofd.FileNames)
                        {
                            if (new FileInfo(file).Length > 0)
                                files.Add(new OIVGenericFile(file, file.Substring(file.LastIndexOf("\\") + 1)));
                            else MessageBox.Show(string.Format("The file {0} is invalid and cannot be used.", file));
                        }
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

        protected override void OnShown(EventArgs e)
        {
            Active = true;
            base.OnShown(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            Active = false;
            base.OnClosed(e);
        }

        private void CancelButtonClick(object sender, CancelEventArgs e)
        {
            Close();
        }
    }
}
