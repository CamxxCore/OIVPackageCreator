using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
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
            superTabControl1.SelectedTabChanged += SelectedTabChanged;
            colorPickerButton1.SelectedColorChanged += ColorPickerButton1_SelectedColorChanged;
            colorPickerButton2.SelectedColorChanged += ColorPickerButton2_SelectedColorChanged;
            SetupGridViewItems();
            SetupTabSize();
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
                case 1: MinimumSize = MaximumSize = Size = new Size(544, 400); break;
                case 2: MinimumSize = MaximumSize = Size = new Size(544, 230); break;
            }
        }

        public void SetupGridViewItems()
        {
            propertyGrid1.SelectedObject = info;
        }

        private void GenericContentWz_Finished(object sender, OIVGenericFile[] files)
        {
            foreach (var f in files)
            {
                listBox1.Items.Add(string.Format("{0} -> {1}",
                    f.Source.Substring(f.Source.LastIndexOf('\\') + 1),
                    f.Destination));
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
                    listBox1.Items.Add(string.Format("{0} -> {1}/{2}",
                        f.Source.Substring(f.Source.LastIndexOf('\\') + 1),
                        ar.Path,
                        f.Name));
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

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (info == null) return;

            using (var sfd = new SaveFileDialog { Filter = "OpenIV Archive Files (*.oiv)|*.oiv|All files (*.*)|*.*" })
            {
                sfd.FileName = info.Name ?? "modname" + ".oiv";

                var result = sfd.ShowDialog();

                switch (result)
                {
                    case DialogResult.Cancel: break;
                    case DialogResult.OK:
                        var manager = new OIVPackageManager(sfd.FileName);
                        manager.CreatePackage(info);
                        break;
                }

                sfd.Dispose();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (info == null) return;

            string filename = "";

            using (var ofd = new OpenFileDialog())
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

            info = oivpkg.ReadPackage();

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
                        f.FullPath));
            }

            colorPickerButton1.SelectedColor = info.IconBackground;

            colorPickerButton2.SelectedColor = info.HeaderBackground;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            List<OIVArchiveFile> foundFiles = new List<OIVArchiveFile>();

            foreach (var a in info.Archives)
            {
                foreach (var f in a.GetNestedFiles())
                    if ((listBox1.SelectedItem as string).Contains(f.FullPath))
                        foundFiles.Add(f);
            }

            if (foundFiles.Count <= 0) return;

            foundFiles.ForEach(x => x.Parent.SourceFiles.Remove(x));

            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }
    }
}