namespace OIVPackageEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.Metadata = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel3 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonX5 = new DevComponents.DotNetBar.ButtonX();
            this.superTabItem2 = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.colorPickerButton2 = new DevComponents.DotNetBar.ColorPickerButton();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.colorPickerButton1 = new DevComponents.DotNetBar.ColorPickerButton();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.propertyGrid1 = new OIVPackageEditor.TabbedPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            this.superTabControlPanel3.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerColorTint = System.Drawing.Color.Black;
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(218)))), ((int)(((byte)(237))))));
            // 
            // superTabControl1
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Controls.Add(this.superTabControlPanel1);
            this.superTabControl1.Controls.Add(this.superTabControlPanel3);
            this.superTabControl1.Controls.Add(this.toolStrip1);
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.Location = new System.Drawing.Point(0, 0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = false;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(528, 544);
            this.superTabControl1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControl1.TabIndex = 7;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.Metadata,
            this.superTabItem2,
            this.superTabItem1});
            this.superTabControl1.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
            this.superTabControl1.Text = "tabControl";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.propertyGrid1);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 23);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(528, 521);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.Metadata;
            // 
            // Metadata
            // 
            this.Metadata.AttachedControl = this.superTabControlPanel1;
            this.Metadata.GlobalItem = false;
            this.Metadata.Name = "Metadata";
            this.Metadata.Text = "Metadata";
            // 
            // superTabControlPanel3
            // 
            this.superTabControlPanel3.Controls.Add(this.buttonX1);
            this.superTabControlPanel3.Controls.Add(this.listBox1);
            this.superTabControlPanel3.Controls.Add(this.buttonX5);
            this.superTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel3.Location = new System.Drawing.Point(0, 23);
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.Size = new System.Drawing.Size(528, 521);
            this.superTabControlPanel3.TabIndex = 0;
            this.superTabControlPanel3.TabItem = this.superTabItem2;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Image = global::OIVPackageEditor.Properties.Resources.Minus_blue_svg;
            this.buttonX1.ImageFixedSize = new System.Drawing.Size(20, 23);
            this.buttonX1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.buttonX1.Location = new System.Drawing.Point(20, 71);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(36, 32);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 11;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(69, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(439, 290);
            this.listBox1.TabIndex = 10;
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX5.Image = global::OIVPackageEditor.Properties.Resources.Plus_blue_svg;
            this.buttonX5.ImageFixedSize = new System.Drawing.Size(20, 23);
            this.buttonX5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.buttonX5.Location = new System.Drawing.Point(20, 25);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(36, 32);
            this.buttonX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX5.TabIndex = 9;
            this.buttonX5.Click += new System.EventHandler(this.buttonX5_Click);
            // 
            // superTabItem2
            // 
            this.superTabItem2.AttachedControl = this.superTabControlPanel3;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "Content";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.superTabControlPanel2.Controls.Add(this.groupPanel1);
            this.superTabControlPanel2.Controls.Add(this.buttonX3);
            this.superTabControlPanel2.Controls.Add(this.buttonX2);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 23);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(528, 521);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem1;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.groupPanel1.Controls.Add(this.buttonX4);
            this.groupPanel1.Controls.Add(this.pictureBox1);
            this.groupPanel1.Controls.Add(this.colorPickerButton2);
            this.groupPanel1.Controls.Add(this.checkBoxX1);
            this.groupPanel1.Controls.Add(this.colorPickerButton1);
            this.groupPanel1.Location = new System.Drawing.Point(18, 18);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(157, 188);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 14;
            this.groupPanel1.Text = "OpenIV Installer Settings";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Location = new System.Drawing.Point(62, 19);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(75, 23);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 15;
            this.buttonX4.Text = "Browse...";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // colorPickerButton2
            // 
            this.colorPickerButton2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.colorPickerButton2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.colorPickerButton2.Image = ((System.Drawing.Image)(resources.GetObject("colorPickerButton2.Image")));
            this.colorPickerButton2.Location = new System.Drawing.Point(15, 58);
            this.colorPickerButton2.Name = "colorPickerButton2";
            this.colorPickerButton2.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.colorPickerButton2.Size = new System.Drawing.Size(122, 23);
            this.colorPickerButton2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.colorPickerButton2.TabIndex = 8;
            this.colorPickerButton2.Text = "Header Color";
            this.colorPickerButton2.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.Class = "";
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.Location = new System.Drawing.Point(13, 132);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(125, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 13;
            this.checkBoxX1.Text = "Black Text Enabled";
            this.checkBoxX1.CheckedChanged += new System.EventHandler(this.checkBoxX1_CheckedChanged);
            // 
            // colorPickerButton1
            // 
            this.colorPickerButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.colorPickerButton1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.colorPickerButton1.Image = ((System.Drawing.Image)(resources.GetObject("colorPickerButton1.Image")));
            this.colorPickerButton1.Location = new System.Drawing.Point(15, 96);
            this.colorPickerButton1.Name = "colorPickerButton1";
            this.colorPickerButton1.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.colorPickerButton1.Size = new System.Drawing.Size(122, 23);
            this.colorPickerButton1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.colorPickerButton1.TabIndex = 7;
            this.colorPickerButton1.Text = "Icon Background";
            this.colorPickerButton1.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(202, 82);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(136, 30);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 12;
            this.buttonX3.Text = "Export to OIV Package";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(202, 37);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(136, 30);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 11;
            this.buttonX2.Text = "Import Existing Package";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel2;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "Import / Export";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(528, 544);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.propertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.propertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.propertyGrid1.CommandsForeColor = System.Drawing.Color.Black;
            this.propertyGrid1.ForeColor = System.Drawing.Color.Black;
            this.propertyGrid1.HelpBackColor = System.Drawing.Color.White;
            this.propertyGrid1.HelpForeColor = System.Drawing.Color.Black;
            this.propertyGrid1.Location = new System.Drawing.Point(5, 4);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(518, 512);
            this.propertyGrid1.TabIndex = 5;
            this.propertyGrid1.ViewBackColor = System.Drawing.Color.White;
            this.propertyGrid1.ViewForeColor = System.Drawing.Color.Black;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 544);
            this.Controls.Add(this.superTabControl1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "OpenIV Package Editor";
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControl1.PerformLayout();
            this.superTabControlPanel1.ResumeLayout(false);
            this.superTabControlPanel3.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private TabbedPropertyGrid propertyGrid1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem Metadata;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel3;
        private DevComponents.DotNetBar.SuperTabItem superTabItem2;
        private DevComponents.DotNetBar.ColorPickerButton colorPickerButton2;
        private DevComponents.DotNetBar.ColorPickerButton colorPickerButton1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX5;
        private System.Windows.Forms.ListBox listBox1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

