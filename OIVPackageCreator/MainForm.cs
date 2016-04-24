using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System;

namespace OIVPackageCreator
{
    public partial class MainForm : DevComponents.DotNetBar.Office2007Form
    {
        OIVPackageInfo info = new OIVPackageInfo();

        public MainForm()
        {
            InitializeComponent();
            propertyGrid1.SetParent(this);
            propertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;
            superTabControl1.SelectedTabChanged += SelectedTabChanged;
            colorPickerButton1.SelectedColorChanged += ColorPickerButton1_SelectedColorChanged;
            colorPickerButton2.SelectedColorChanged += ColorPickerButton2_SelectedColorChanged;
            SetupGridViewItems();
            SetupTabSize();
        }

        private void ColorPickerButton1_SelectedColorChanged(object sender, EventArgs e)
        {
            if (info == null) return;
            info.IconBackground = colorPickerButton1.SelectedColor;
        }

        private void ColorPickerButton2_SelectedColorChanged(object sender, EventArgs e)
        {
            if (info == null) return;
            info.HeaderBackground = colorPickerButton2.SelectedColor;
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
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

            foreach (var oivFile in info.GenericFiles)
                MessageBox.Show(string.Format("src: {0} dest: {1}", oivFile.Source, oivFile.Destination));
        }

        private void ArchiveContentWz_Finished(object sender, OIVArchive archive)
        {
            var nodes = GetNodes(archive);
            MessageBox.Show(nodes.Count().ToString());
            foreach (var ar in nodes)
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

            foreach (var oivFile in info.Archives)
                MessageBox.Show(string.Format("path: {0} {1} {2}", oivFile.Path, oivFile.Version, oivFile.CreateIfNotExist));
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            //export

            if (info == null) return;

            var oivpkg = new OIVPackageManager("package.oiv");

            oivpkg.CreatePackage(info);
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
         
            //     oivpkg.CreatePackage(info);
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            var splash = new WizardSplashPage();
            splash.GenericFileWizard.Finished += GenericContentWz_Finished;
            splash.ArchiveFileWizard.Finished += ArchiveContentWz_Finished;
            splash.Show();
        }

        public static IEnumerable<OIVArchive> GetNodes(OIVArchive node)
        {
            if (node == null)
            {
                yield break;
            }
            yield return node;
            foreach (var n in node.NestedArchives)
            {
                foreach (var innerN in GetNodes(n))
                {
                    yield return innerN;
                }
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
               /* a.NestedArchives.ForEach(x => x.SourceFiles.ForEach(y => listBox1.Items.Add(string.Format("{0} -> {1}/{2}",
                        y.Source.Substring(y.Source.LastIndexOf('\\') + 1),
                        a.Path,
                        y.Name))));*/

               // foreach (var nested in a.NestedArchives)
              //  {
//
              //  }

                foreach (var f in a.SourceFiles)
                    listBox1.Items.Add(string.Format("{0} -> {1}/{2}",
                        f.Source.Substring(f.Source.LastIndexOf('\\') + 1),
                        a.Path,
                        f.Name));
            }

        }
    }
}
