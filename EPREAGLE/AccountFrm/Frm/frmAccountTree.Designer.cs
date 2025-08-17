namespace EPREAGLE.AccountFrm.Frm
{
    partial class frmAccountTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccountTree));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExportToPdf = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportToExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.tvAccount = new System.Windows.Forms.TreeView();
            this.txtAccountName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAccountCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cmbAccountFinal = new System.Windows.Forms.ComboBox();
            this.cmbAccountKind = new System.Windows.Forms.ComboBox();
            this.cmbAccountParents = new System.Windows.Forms.ComboBox();
            this.cmbAccountType = new System.Windows.Forms.ComboBox();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cmbAccountCurrency = new System.Windows.Forms.ComboBox();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnExportToPdf);
            this.panelControl2.Controls.Add(this.btnExportToExcel);
            this.panelControl2.Controls.Add(this.btnDelete);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Controls.Add(this.btnNew);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 451);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(567, 48);
            this.panelControl2.TabIndex = 4;
            // 
            // btnExportToPdf
            // 
            this.btnExportToPdf.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExportToPdf.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExportToPdf.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnExportToPdf.ImageOptions.SvgImage")));
            this.btnExportToPdf.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnExportToPdf.Location = new System.Drawing.Point(290, 2);
            this.btnExportToPdf.Name = "btnExportToPdf";
            this.btnExportToPdf.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnExportToPdf.Size = new System.Drawing.Size(55, 44);
            this.btnExportToPdf.TabIndex = 4;
            this.btnExportToPdf.Visible = false;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExportToExcel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExportToExcel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnExportToExcel.ImageOptions.SvgImage")));
            this.btnExportToExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnExportToExcel.Location = new System.Drawing.Point(345, 2);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnExportToExcel.Size = new System.Drawing.Size(55, 44);
            this.btnExportToExcel.TabIndex = 3;
            this.btnExportToExcel.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDelete.ImageOptions.SvgImage")));
            this.btnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnDelete.Location = new System.Drawing.Point(400, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnDelete.Size = new System.Drawing.Size(55, 44);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnSave.Location = new System.Drawing.Point(455, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSave.Size = new System.Drawing.Size(55, 44);
            this.btnSave.TabIndex = 1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNew.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNew.ImageOptions.SvgImage")));
            this.btnNew.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnNew.Location = new System.Drawing.Point(510, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNew.Size = new System.Drawing.Size(55, 44);
            this.btnNew.TabIndex = 0;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // tvAccount
            // 
            this.tvAccount.Dock = System.Windows.Forms.DockStyle.Right;
            this.tvAccount.Location = new System.Drawing.Point(290, 0);
            this.tvAccount.Name = "tvAccount";
            this.tvAccount.RightToLeftLayout = true;
            this.tvAccount.Size = new System.Drawing.Size(277, 451);
            this.tvAccount.TabIndex = 5;
            this.tvAccount.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvAccount_AfterSelect);
            // 
            // txtAccountName
            // 
            this.txtAccountName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccountName.Location = new System.Drawing.Point(8, 79);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountName.Properties.Appearance.Options.UseFont = true;
            this.txtAccountName.Properties.MaxLength = 150;
            this.txtAccountName.Size = new System.Drawing.Size(276, 20);
            this.txtAccountName.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.AppearanceDisabled.Options.UseFont = true;
            this.labelControl1.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.AppearanceHovered.Options.UseFont = true;
            this.labelControl1.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.AppearancePressed.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(208, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 14);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "اسم الحساب";
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccountCode.Location = new System.Drawing.Point(8, 33);
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountCode.Properties.Appearance.Options.UseFont = true;
            this.txtAccountCode.Properties.MaxLength = 150;
            this.txtAccountCode.Properties.ReadOnly = true;
            this.txtAccountCode.Size = new System.Drawing.Size(276, 20);
            this.txtAccountCode.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.AppearanceDisabled.Options.UseFont = true;
            this.labelControl2.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.AppearanceHovered.Options.UseFont = true;
            this.labelControl2.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.AppearancePressed.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(213, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "كود الحساب";
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.AppearanceDisabled.Options.UseFont = true;
            this.labelControl5.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.AppearanceHovered.Options.UseFont = true;
            this.labelControl5.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.AppearancePressed.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(187, 249);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(97, 14);
            this.labelControl5.TabIndex = 75;
            this.labelControl5.Text = "الحساب الختامي";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.AppearanceDisabled.Options.UseFont = true;
            this.labelControl4.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.AppearanceHovered.Options.UseFont = true;
            this.labelControl4.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.AppearancePressed.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(199, 201);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(85, 14);
            this.labelControl4.TabIndex = 74;
            this.labelControl4.Text = "طبيعة الحساب";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.AppearanceDisabled.Options.UseFont = true;
            this.labelControl3.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.AppearanceHovered.Options.UseFont = true;
            this.labelControl3.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.AppearancePressed.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(211, 153);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(73, 14);
            this.labelControl3.TabIndex = 73;
            this.labelControl3.Text = "الحساب الاب";
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl6.AppearanceDisabled.Options.UseFont = true;
            this.labelControl6.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl6.AppearanceHovered.Options.UseFont = true;
            this.labelControl6.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl6.AppearancePressed.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(215, 105);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(69, 14);
            this.labelControl6.TabIndex = 72;
            this.labelControl6.Text = "نوع الحساب";
            // 
            // cmbAccountFinal
            // 
            this.cmbAccountFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAccountFinal.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbAccountFinal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbAccountFinal.FormattingEnabled = true;
            this.cmbAccountFinal.Items.AddRange(new object[] {
            "قائمة الدخل",
            "قائمة المركز المالي",
            "الميزانية"});
            this.cmbAccountFinal.Location = new System.Drawing.Point(8, 269);
            this.cmbAccountFinal.Name = "cmbAccountFinal";
            this.cmbAccountFinal.Size = new System.Drawing.Size(276, 22);
            this.cmbAccountFinal.TabIndex = 71;
            // 
            // cmbAccountKind
            // 
            this.cmbAccountKind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAccountKind.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbAccountKind.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbAccountKind.FormattingEnabled = true;
            this.cmbAccountKind.Items.AddRange(new object[] {
            "مدين",
            "دائن"});
            this.cmbAccountKind.Location = new System.Drawing.Point(8, 221);
            this.cmbAccountKind.Name = "cmbAccountKind";
            this.cmbAccountKind.Size = new System.Drawing.Size(276, 22);
            this.cmbAccountKind.TabIndex = 70;
            // 
            // cmbAccountParents
            // 
            this.cmbAccountParents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAccountParents.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAccountParents.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAccountParents.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbAccountParents.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbAccountParents.FormattingEnabled = true;
            this.cmbAccountParents.Location = new System.Drawing.Point(8, 173);
            this.cmbAccountParents.Name = "cmbAccountParents";
            this.cmbAccountParents.Size = new System.Drawing.Size(276, 22);
            this.cmbAccountParents.TabIndex = 69;
            this.cmbAccountParents.SelectedIndexChanged += new System.EventHandler(this.GetAccountCode);
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAccountType.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbAccountType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbAccountType.FormattingEnabled = true;
            this.cmbAccountType.Items.AddRange(new object[] {
            "رئيسي",
            "فرعي"});
            this.cmbAccountType.Location = new System.Drawing.Point(8, 125);
            this.cmbAccountType.Name = "cmbAccountType";
            this.cmbAccountType.Size = new System.Drawing.Size(276, 22);
            this.cmbAccountType.TabIndex = 68;
            this.cmbAccountType.SelectedIndexChanged += new System.EventHandler(this.GetAccountCode);
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl7.AppearanceDisabled.Options.UseFont = true;
            this.labelControl7.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl7.AppearanceHovered.Options.UseFont = true;
            this.labelControl7.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl7.AppearancePressed.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(186, 297);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(98, 14);
            this.labelControl7.TabIndex = 77;
            this.labelControl7.Text = "العملة الافتراضية";
            // 
            // cmbAccountCurrency
            // 
            this.cmbAccountCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAccountCurrency.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbAccountCurrency.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbAccountCurrency.FormattingEnabled = true;
            this.cmbAccountCurrency.Location = new System.Drawing.Point(8, 317);
            this.cmbAccountCurrency.Name = "cmbAccountCurrency";
            this.cmbAccountCurrency.Size = new System.Drawing.Size(276, 22);
            this.cmbAccountCurrency.TabIndex = 76;
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Location = new System.Drawing.Point(8, 345);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Properties.Appearance.Options.UseFont = true;
            this.txtID.Properties.MaxLength = 5;
            this.txtID.Size = new System.Drawing.Size(144, 20);
            this.txtID.TabIndex = 78;
            this.txtID.Visible = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseTextOptions = true;
            this.labelControl8.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl8.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl8.AppearanceDisabled.Options.UseFont = true;
            this.labelControl8.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl8.AppearanceHovered.Options.UseFont = true;
            this.labelControl8.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl8.AppearancePressed.Options.UseFont = true;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.Location = new System.Drawing.Point(12, 371);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(272, 76);
            this.labelControl8.TabIndex = 79;
            this.labelControl8.Text = "في حالة الخزينة و الحسابات البنكية و العملاء و الموردين و المحافظ الالكترونية يتم" +
    " إضافة الحساب الاب فقط و يتم التكويد من التكويدات\r\n";
            // 
            // frmAccountTree
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 499);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.cmbAccountCurrency);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.cmbAccountFinal);
            this.Controls.Add(this.cmbAccountKind);
            this.Controls.Add(this.cmbAccountParents);
            this.Controls.Add(this.cmbAccountType);
            this.Controls.Add(this.txtAccountCode);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.tvAccount);
            this.Controls.Add(this.panelControl2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "frmAccountTree";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "شجرة الحسابات";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExportToPdf;
        private DevExpress.XtraEditors.SimpleButton btnExportToExcel;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private System.Windows.Forms.TreeView tvAccount;
        private DevExpress.XtraEditors.TextEdit txtAccountName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtAccountCode;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        internal System.Windows.Forms.ComboBox cmbAccountFinal;
        internal System.Windows.Forms.ComboBox cmbAccountKind;
        internal System.Windows.Forms.ComboBox cmbAccountParents;
        internal System.Windows.Forms.ComboBox cmbAccountType;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        internal System.Windows.Forms.ComboBox cmbAccountCurrency;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}