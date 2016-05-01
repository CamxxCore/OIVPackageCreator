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
            textBox1.PreviewKeyDown += TextBox1_PreviewKeyDown;
            textBox1.AutoCompleteCustomSource = Properties.Settings.Default.autoCompGenPaths;
        }

        private void TextBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
            }
        }

        private void SaveCommonPath(string path)
        {
            Properties.Settings.Default.autoCompGenPaths =
                Properties.Settings.Default.autoCompGenPaths ??
                new AutoCompleteStringCollection();

            if (!Properties.Settings.Default.autoCompGenPaths.Contains(path))
            {
                Properties.Settings.Default.autoCompGenPaths.Add(path);
                Properties.Settings.Default.Save();
            }
        }

        private void FinishButtonClick(object sender, CancelEventArgs e)
        {
            if (files.Count <= 0)
            {
                MessageBox.Show("No files were selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var file in files)
            {
                if (file.Destination?.Length <= 0)
                {
                    MessageBox.Show(string.Format("Invalid path specified for file at {0}", file.Source), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                file.Destination = file.Destination.Replace('/', '\\');
                SaveCommonPath(file.Destination);
            }

            Finished?.Invoke(this, files.ToArray());
            Close();
        }

        private void TextBox1_LostFocus(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0)
            {
                foreach (var item in files)
                    item.Destination = 
                        textBox1.Text.Replace('/', '\\').TrimEnd('\\') + "\\" + item.Name;
            }

            else
            {
                files[listBox2.SelectedIndex].Destination = 
                    textBox1.Text.Replace('/', '\\').TrimEnd('\\') + "\\"  + files[listBox2.SelectedIndex].Name;
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
