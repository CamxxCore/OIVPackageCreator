﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OIVPackageEditor
{
    public delegate void ArchiveFileWizardFinishedDelegate(object sender, OIVArchive archive);

    public partial class AddArchiveFileWizard : Form
    {
        public bool Active { get; set; }

        private OIVArchive archive = new OIVArchive();

        private List<string> files = new List<string>();

        public event ArchiveFileWizardFinishedDelegate Finished;

        private RageArchiveType type = RageArchiveType.RPF7;

        public AddArchiveFileWizard()
        {
            InitializeComponent();
            wizard1.CancelButtonClick += CancelButtonClick1;
            wizard1.FinishButtonClick += FinishButtonClick;
            wizardPage3.NextButtonClick += NextButtonClick;
        }

        private void NextButtonClick(object sender, CancelEventArgs e)
        {
           var selected = panel1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (selected == radioButton1)
                type = RageArchiveType.RPF7;
            else if (selected == radioButton2)
                type = RageArchiveType.RPF4;
            else if (selected == radioButton3)
                type = RageArchiveType.RPF3;
            else if (selected == radioButton4)
                type = RageArchiveType.RPF2;
            else if (selected == radioButton5)
                type = RageArchiveType.IMG3;

            archive.Version = type;
        }

        private void FinishButtonClick(object sender, CancelEventArgs e)
        {
            string fullPath = textBox1.Text.Trim();

            if (fullPath.Length <= 0)
            {
                MessageBox.Show("Invalid archive path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int startIndex = 0;
            int endIndex = 0;
            startIndex = fullPath.IndexOf(".rpf") + 4;

            var initialPath = fullPath.Substring(0, startIndex);

            archive.Path = initialPath;

            string subStr = fullPath;

            OIVArchive currentNode = archive;

            for (int i = 1; i < Regex.Matches(fullPath, ".rpf").Count; i++)
            {
                subStr = subStr.Substring(startIndex, subStr.Length - startIndex);
                endIndex = subStr.IndexOf(".rpf") + 4;
                var subArchivePath = subStr.Substring(0, endIndex).TrimStart('/');
                startIndex = endIndex;
                var subArchive = new OIVArchive(archive.Version, new List<OIVArchiveFile>(), subArchivePath, false);
                currentNode.NestedArchives.Add(subArchive);
                currentNode = subArchive;
            }

            foreach (var file in files)
                currentNode.Add(file, file.Substring(file.LastIndexOf("\\") + 1));

            Finished?.Invoke(this, archive);
            Close();
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
                            files.Add(file);
                        break;
                }

                ofd.Dispose();
            }

            listBox1.Items.Clear();
            listBox2.Items.Clear();

            foreach (var file in files)
            {
                var filename = file.Substring(file.LastIndexOf("\\") + 1);

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

        private void CancelButtonClick1(object sender, CancelEventArgs e)
        {
            Close();
        }
    }
}