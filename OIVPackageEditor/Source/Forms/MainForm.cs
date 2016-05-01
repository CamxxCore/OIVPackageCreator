using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Drawing;
using System.Linq;
using System;

namespace OIVPackageEditor
{
    public partial class MainForm : DevComponents.DotNetBar.Office2007Form
    {
        OIVPackageInfo info = new OIVPackageInfo();
        bool wizardActive = false;

        public MainForm()
        {
            InitializeComponent();
            propertyGrid1.SetParent(this);
            Properties.Settings.Default.Upgrade();
            superTabControl1.SelectedTabChanged += SelectedTabChanged;
            colorPickerButton1.SelectedColorChanged += ColorPickerButton1_SelectedColorChanged;
            colorPickerButton2.SelectedColorChanged += ColorPickerButton2_SelectedColorChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            SetupTabSize();
            propertyGrid1.SelectedObject = info;
            base.OnLoad(e);
        }

        private void ColorPickerButton1_SelectedColorChanged(object sender, EventArgs e)
        {
            if (info != null)
            {
                info.IconBackground = colorPickerButton1.SelectedColor;
            }
        }

        private void ColorPickerButton2_SelectedColorChanged(object sender, EventArgs e)
        {
            if (info != null)
            {
                info.HeaderBackground = colorPickerButton2.SelectedColor;
            }
        }

        private void SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            SetupTabSize();
            if (superTabControl1.SelectedTabIndex == 0)
                propertyGrid1.Focus();
        }

        private void SetupTabSize()
        {
            switch (superTabControl1.SelectedTabIndex)
            {
                case 0: MinimumSize = MaximumSize = Size = new Size(544, 582); break;
                case 1: MinimumSize = MaximumSize = Size = new Size(544, 389); break;
                case 2: MinimumSize = MaximumSize = Size = new Size(382, 290); break;
            }
        }

        private void GenericContentWz_Finished(object sender, OIVGenericFile[] files)
        {
            foreach (var file in files)
            {
                listBox1.Items.Add(string.Format("{0} -> {1}",
                    file.Source.Substring(file.Source.LastIndexOf('\\') + 1),
                    file.Destination));
            }

            info = info ?? new OIVPackageInfo();
            info.GenericFiles.AddRange(files);
            wizardActive = false;
        }

        private void ArchiveContentWz_Finished(object sender, OIVArchive archive)
        {
            foreach (var ar in archive.GetNodes())
            {
                if (ar.SourceFiles == null) continue;

                foreach (var f in ar.SourceFiles)
                    listBox1.Items.Add(string.Format("{0} -> {1}",
                        f.Source.Substring(f.Source.LastIndexOf('\\') + 1),
                        f.FullPath).Replace("\\\\", "\\"));
            }

            info = info ?? new OIVPackageInfo();
            info.Archives.Add(archive);

            wizardActive = false;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (!wizardActive)
            {
                var splash = new WizardSplashPage();
                splash.FormClosed += OnWizardExit;
                splash.GenericFileWizard.FormClosed += OnWizardExit;
                splash.ArchiveFileWizard.FormClosed += OnWizardExit;
                splash.GenericFileWizard.Finished += GenericContentWz_Finished;
                splash.ArchiveFileWizard.Finished += ArchiveContentWz_Finished;
                splash.Show();

                wizardActive = true;
            }

            else MessageBox.Show("Another wizard is already running. Please complete it before starting a new one.");
        }

        private void OnWizardExit(object sender, EventArgs e)
        {
            wizardActive = false;
        }

        private async void buttonX3_Click(object sender, EventArgs e)
        {
            if (info == null) return;

            using (var sfd = new SaveFileDialog { Filter = "OpenIV Archive Files (*.oiv)|*.oiv|All files (*.*)|*.*" })
            {
                sfd.FileName = info.Name ?? "package.oiv";

                var result = sfd.ShowDialog();

                switch (result)
                {
                    case DialogResult.Cancel: break;
                    case DialogResult.OK:
                        Enabled = false;
                        using (var manager = new OIVPackageManager(sfd.FileName))
                        {
                            await manager.ExportPackage(info);
                            manager.Dispose();
                        }
                        Enabled = true;
                        MessageBox.Show(string.Format("Successfully exported to {0}.", sfd.FileName));
                        break;
                }

                sfd.Dispose();
            }
        }

        private async void buttonX2_Click(object sender, EventArgs e)
        {
            if (info == null) return;

            string filename = "";

            using (var ofd = new OpenFileDialog { Filter = "OpenIV Archive Files (*.oiv)|*.oiv|All files (*.*)|*.*" })
            {
                var result = ofd.ShowDialog();

                switch (result)
                {
                    case DialogResult.Cancel: break;
                    case DialogResult.OK:
                        filename = ofd.FileName;
                        break;
                }

                ofd.Dispose();
            }

            if (filename.Length <= 0) return;

            var oivpkg = new OIVPackageManager(filename);

            Enabled = false;

            info = await oivpkg.ReadPackage();

            propertyGrid1.SelectedObject = info;

            propertyGrid1.Refresh();

            listBox1.Items.Clear();

            foreach (var f in info.GenericFiles)
            {
                listBox1.Items.Add(string.Format("{0} -> {1}",
                    f.Source.Substring(f.Source.LastIndexOf('\\') + 1),
                    f.Destination));
            }

            foreach (var a in info.Archives)
            {
                foreach (var f in a.GetNestedFiles())
                    listBox1.Items.Add(string.Format("{0} -> {1}",
                        f.Source.Substring(f.Source.LastIndexOf('\\') + 1),
                        f.FullPath).Replace("\\\\", "\\"));
            }

            colorPickerButton1.SelectedColor = info.IconBackground;

            colorPickerButton2.SelectedColor = info.HeaderBackground;

            checkBoxX1.Checked = info.BlackTextEnabled;

            if (info.IconPath?.Length > 0)
            {
                var image = Image.FromFile(info.IconPath);

                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }

            Enabled = true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            var str = listBox1.SelectedItem as string;

            bool foundIt = false;

            foreach (var a in info.Archives)
            {
                var files = a.GetNestedFiles().ToArray();

                foreach (var f in files)
                    if (str.Contains(f.FullPath.Replace("\\\\", "\\")))
                    {
                        foundIt = true;
                        f.Parent.SourceFiles.Remove(f);
                    }
            }

            for (int i = info.GenericFiles.Count - 1; i >= 0; i--)
            {
                if (str.Contains(info.GenericFiles[i].Destination))
                {
                    foundIt = true;
                    info.GenericFiles.RemoveAt(i);
                }
            }

            if (foundIt)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            else MessageBox.Show("Error removing the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (info == null) return;

            using (var ofd = new OpenFileDialog() { Filter = "PNG Image Files (*.png)|*.png" })
            {
                var result = ofd.ShowDialog();

                switch (result)
                {
                    case DialogResult.Cancel: break;
                    case DialogResult.OK:
                        var image = Image.FromFile(ofd.FileName);
                        info.IconPath = ofd.FileName;
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                        pictureBox1.Refresh();
                        break;
                }

                ofd.Dispose();
            }    
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (info == null) return;
            info.BlackTextEnabled = checkBoxX1.Checked;
        }
    }
}