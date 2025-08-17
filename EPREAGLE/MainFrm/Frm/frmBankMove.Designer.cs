namespace EPREAGLE.MainFrm.Frm
{
    partial class frmBankMove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBankMove));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcBankMove = new DevExpress.XtraGrid.GridControl();
            this.gvBanckMove = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Debit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Credit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnMove = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportToPdf = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportToExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.cmbBankNo = new System.Windows.Forms.ComboBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.deToDate = new DevExpress.XtraEditors.DateEdit();
            this.deFromDate = new DevExpress.XtraEditors.DateEdit();
            this.lblBankAccount = new DevExpress.XtraEditors.LabelControl();
            this.lblBankName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBankMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBanckMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcBankMove);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 202);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1198, 418);
            this.groupControl1.TabIndex = 8;
            // 
            // gcBankMove
            // 
            this.gcBankMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBankMove.Location = new System.Drawing.Point(2, 23);
            this.gcBankMove.MainView = this.gvBanckMove;
            this.gcBankMove.Name = "gcBankMove";
            this.gcBankMove.Size = new System.Drawing.Size(1194, 393);
            this.gcBankMove.TabIndex = 1;
            this.gcBankMove.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBanckMove});
            // 
            // gvBanckMove
            // 
            this.gvBanckMove.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gvBanckMove.Appearance.FooterPanel.Options.UseFont = true;
            this.gvBanckMove.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gvBanckMove.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvBanckMove.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvBanckMove.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gvBanckMove.ColumnPanelRowHeight = 35;
            this.gvBanckMove.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Date,
            this.Time,
            this.Description,
            this.Debit,
            this.Credit,
            this.User});
            this.gvBanckMove.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gvBanckMove.GridControl = this.gcBankMove;
            this.gvBanckMove.Name = "gvBanckMove";
            this.gvBanckMove.OptionsView.AutoCalcPreviewLineCount = true;
            this.gvBanckMove.OptionsView.ShowFooter = true;
            this.gvBanckMove.RowHeight = 35;
            // 
            // Date
            // 
            this.Date.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Date.AppearanceCell.Options.UseFont = true;
            this.Date.AppearanceCell.Options.UseTextOptions = true;
            this.Date.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Date.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Date.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Date.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Date.AppearanceHeader.Options.UseFont = true;
            this.Date.AppearanceHeader.Options.UseTextOptions = true;
            this.Date.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Date.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Date.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Date.Caption = "التاريخ";
            this.Date.DisplayFormat.FormatString = "d";
            this.Date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Date.Name = "Date";
            this.Date.OptionsColumn.AllowEdit = false;
            this.Date.Visible = true;
            this.Date.VisibleIndex = 0;
            this.Date.Width = 142;
            // 
            // Time
            // 
            this.Time.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Time.AppearanceCell.Options.UseFont = true;
            this.Time.AppearanceCell.Options.UseTextOptions = true;
            this.Time.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Time.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Time.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Time.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Time.AppearanceHeader.Options.UseFont = true;
            this.Time.AppearanceHeader.Options.UseTextOptions = true;
            this.Time.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Time.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Time.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Time.Caption = "الوقت";
            this.Time.DisplayFormat.FormatString = "t";
            this.Time.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Time.Name = "Time";
            this.Time.OptionsColumn.AllowEdit = false;
            this.Time.Visible = true;
            this.Time.VisibleIndex = 1;
            this.Time.Width = 142;
            // 
            // Description
            // 
            this.Description.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Description.AppearanceCell.Options.UseFont = true;
            this.Description.AppearanceCell.Options.UseTextOptions = true;
            this.Description.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Description.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Description.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Description.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Description.AppearanceHeader.Options.UseFont = true;
            this.Description.AppearanceHeader.Options.UseTextOptions = true;
            this.Description.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Description.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Description.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Description.Caption = "الوصف";
            this.Description.Name = "Description";
            this.Description.OptionsColumn.AllowEdit = false;
            this.Description.Visible = true;
            this.Description.VisibleIndex = 2;
            this.Description.Width = 420;
            // 
            // Debit
            // 
            this.Debit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Debit.AppearanceCell.Options.UseFont = true;
            this.Debit.AppearanceCell.Options.UseTextOptions = true;
            this.Debit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Debit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Debit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Debit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Debit.AppearanceHeader.Options.UseFont = true;
            this.Debit.AppearanceHeader.Options.UseTextOptions = true;
            this.Debit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Debit.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Debit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Debit.Caption = "مدين";
            this.Debit.DisplayFormat.FormatString = "C2";
            this.Debit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Debit.GroupFormat.FormatString = "C2";
            this.Debit.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Debit.Name = "Debit";
            this.Debit.OptionsColumn.AllowEdit = false;
            this.Debit.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", "{0:C2} ")});
            this.Debit.Visible = true;
            this.Debit.VisibleIndex = 3;
            this.Debit.Width = 175;
            // 
            // Credit
            // 
            this.Credit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Credit.AppearanceCell.Options.UseFont = true;
            this.Credit.AppearanceCell.Options.UseTextOptions = true;
            this.Credit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Credit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Credit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Credit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Credit.AppearanceHeader.Options.UseFont = true;
            this.Credit.AppearanceHeader.Options.UseTextOptions = true;
            this.Credit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Credit.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Credit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Credit.Caption = "دائن";
            this.Credit.DisplayFormat.FormatString = "C2";
            this.Credit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Credit.GroupFormat.FormatString = "C2";
            this.Credit.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Credit.Name = "Credit";
            this.Credit.OptionsColumn.AllowEdit = false;
            this.Credit.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", "{0:C2} ")});
            this.Credit.Visible = true;
            this.Credit.VisibleIndex = 4;
            this.Credit.Width = 148;
            // 
            // User
            // 
            this.User.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.User.AppearanceCell.Options.UseFont = true;
            this.User.AppearanceCell.Options.UseTextOptions = true;
            this.User.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.User.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.User.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.User.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.User.AppearanceHeader.Options.UseFont = true;
            this.User.AppearanceHeader.Options.UseTextOptions = true;
            this.User.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.User.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.User.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.User.Caption = "المسئول";
            this.User.Name = "User";
            this.User.OptionsColumn.AllowEdit = false;
            this.User.Visible = true;
            this.User.VisibleIndex = 5;
            this.User.Width = 200;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnMove);
            this.panelControl2.Controls.Add(this.btnExportToPdf);
            this.panelControl2.Controls.Add(this.btnExportToExcel);
            this.panelControl2.Controls.Add(this.btnDelete);
            this.panelControl2.Controls.Add(this.btnEdit);
            this.panelControl2.Controls.Add(this.btnPrint);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 620);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1198, 48);
            this.panelControl2.TabIndex = 9;
            // 
            // btnMove
            // 
            this.btnMove.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMove.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnMove.ImageOptions.SvgImage")));
            this.btnMove.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnMove.Location = new System.Drawing.Point(866, 2);
            this.btnMove.Name = "btnMove";
            this.btnMove.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnMove.Size = new System.Drawing.Size(55, 44);
            this.btnMove.TabIndex = 5;
            this.btnMove.ToolTip = "عرض حركة الحساب المحدد";
            this.btnMove.Visible = false;
            // 
            // btnExportToPdf
            // 
            this.btnExportToPdf.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExportToPdf.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExportToPdf.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnExportToPdf.ImageOptions.SvgImage")));
            this.btnExportToPdf.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnExportToPdf.Location = new System.Drawing.Point(921, 2);
            this.btnExportToPdf.Name = "btnExportToPdf";
            this.btnExportToPdf.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnExportToPdf.Size = new System.Drawing.Size(55, 44);
            this.btnExportToPdf.TabIndex = 4;
            this.btnExportToPdf.ToolTip = "تصدير القائمة PDF";
            this.btnExportToPdf.Click += new System.EventHandler(this.btnExportToPdf_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExportToExcel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExportToExcel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnExportToExcel.ImageOptions.SvgImage")));
            this.btnExportToExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnExportToExcel.Location = new System.Drawing.Point(976, 2);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnExportToExcel.Size = new System.Drawing.Size(55, 44);
            this.btnExportToExcel.TabIndex = 3;
            this.btnExportToExcel.ToolTip = "تصديق القائمة إلي أكسيل";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDelete.ImageOptions.SvgImage")));
            this.btnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnDelete.Location = new System.Drawing.Point(1031, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnDelete.Size = new System.Drawing.Size(55, 44);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.ToolTip = "حذف المحدد";
            this.btnDelete.Visible = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEdit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnEdit.ImageOptions.SvgImage")));
            this.btnEdit.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnEdit.Location = new System.Drawing.Point(1086, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnEdit.Size = new System.Drawing.Size(55, 44);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.ToolTip = "تعديل المحدد";
            this.btnEdit.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrint.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPrint.ImageOptions.SvgImage")));
            this.btnPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnPrint.Location = new System.Drawing.Point(1141, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPrint.Size = new System.Drawing.Size(55, 44);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.ToolTip = "إضافة جديد";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.cmbBankNo);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.deToDate);
            this.groupControl2.Controls.Add(this.deFromDate);
            this.groupControl2.Controls.Add(this.lblBankAccount);
            this.groupControl2.Controls.Add(this.lblBankName);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1198, 202);
            this.groupControl2.TabIndex = 10;
            // 
            // cmbBankNo
            // 
            this.cmbBankNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBankNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBankNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBankNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbBankNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbBankNo.FormattingEnabled = true;
            this.cmbBankNo.Location = new System.Drawing.Point(821, 83);
            this.cmbBankNo.Name = "cmbBankNo";
            this.cmbBankNo.Size = new System.Drawing.Size(249, 22);
            this.cmbBankNo.TabIndex = 71;
            this.cmbBankNo.SelectedIndexChanged += new System.EventHandler(this.cmbBankNo_SelectedIndexChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.AppearanceDisabled.Options.UseFont = true;
            this.labelControl5.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.AppearanceHovered.Options.UseFont = true;
            this.labelControl5.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.AppearancePressed.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(727, 86);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(53, 14);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "إلي تاريخ";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.AppearanceDisabled.Options.UseFont = true;
            this.labelControl4.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.AppearanceHovered.Options.UseFont = true;
            this.labelControl4.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.AppearancePressed.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(732, 33);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "من تاريخ";
            // 
            // deToDate
            // 
            this.deToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deToDate.EditValue = new System.DateTime(2025, 5, 12, 11, 41, 32, 0);
            this.deToDate.Location = new System.Drawing.Point(510, 83);
            this.deToDate.Name = "deToDate";
            this.deToDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deToDate.Properties.Appearance.Options.UseFont = true;
            this.deToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deToDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.deToDate.Properties.DisplayFormat.FormatString = "G";
            this.deToDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deToDate.Properties.EditFormat.FormatString = "G";
            this.deToDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deToDate.Properties.MaskSettings.Set("mask", "G");
            this.deToDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.deToDate.Size = new System.Drawing.Size(201, 20);
            this.deToDate.TabIndex = 8;
            this.deToDate.EditValueChanged += new System.EventHandler(this.DateChange);
            // 
            // deFromDate
            // 
            this.deFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deFromDate.EditValue = new System.DateTime(2025, 5, 12, 11, 41, 32, 0);
            this.deFromDate.Location = new System.Drawing.Point(510, 30);
            this.deFromDate.Name = "deFromDate";
            this.deFromDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deFromDate.Properties.Appearance.Options.UseFont = true;
            this.deFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFromDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.deFromDate.Properties.DisplayFormat.FormatString = "G";
            this.deFromDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deFromDate.Properties.EditFormat.FormatString = "G";
            this.deFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deFromDate.Properties.MaskSettings.Set("mask", "G");
            this.deFromDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.deFromDate.Size = new System.Drawing.Size(201, 20);
            this.deFromDate.TabIndex = 7;
            this.deFromDate.EditValueChanged += new System.EventHandler(this.DateChange);
            // 
            // lblBankAccount
            // 
            this.lblBankAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBankAccount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBankAccount.Appearance.Options.UseFont = true;
            this.lblBankAccount.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBankAccount.AppearanceDisabled.Options.UseFont = true;
            this.lblBankAccount.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBankAccount.AppearanceHovered.Options.UseFont = true;
            this.lblBankAccount.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBankAccount.AppearancePressed.Options.UseFont = true;
            this.lblBankAccount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblBankAccount.Location = new System.Drawing.Point(821, 137);
            this.lblBankAccount.Name = "lblBankAccount";
            this.lblBankAccount.Size = new System.Drawing.Size(249, 27);
            this.lblBankAccount.TabIndex = 6;
            this.lblBankAccount.Text = "الكود المحاسبي";
            // 
            // lblBankName
            // 
            this.lblBankName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBankName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBankName.Appearance.Options.UseFont = true;
            this.lblBankName.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBankName.AppearanceDisabled.Options.UseFont = true;
            this.lblBankName.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBankName.AppearanceHovered.Options.UseFont = true;
            this.lblBankName.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBankName.AppearancePressed.Options.UseFont = true;
            this.lblBankName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblBankName.Location = new System.Drawing.Point(821, 26);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(249, 27);
            this.lblBankName.TabIndex = 4;
            this.lblBankName.Text = "اسم الفرع";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.AppearanceDisabled.Options.UseFont = true;
            this.labelControl3.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.AppearanceHovered.Options.UseFont = true;
            this.labelControl3.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.AppearancePressed.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(1092, 144);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(94, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "الكود المحاسبي";
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
            this.labelControl2.Location = new System.Drawing.Point(1114, 86);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "رقم الحساب";
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
            this.labelControl1.Location = new System.Drawing.Point(1127, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "اسم البنك";
            // 
            // frmBankMove
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 668);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.panelControl2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmBankMove";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "كشف حركة الحساب البنكي";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBankMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBanckMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcBankMove;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBanckMove;
        private DevExpress.XtraGrid.Columns.GridColumn Date;
        private DevExpress.XtraGrid.Columns.GridColumn Time;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private DevExpress.XtraGrid.Columns.GridColumn Debit;
        private DevExpress.XtraGrid.Columns.GridColumn Credit;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnMove;
        private DevExpress.XtraEditors.SimpleButton btnExportToPdf;
        private DevExpress.XtraEditors.SimpleButton btnExportToExcel;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.Columns.GridColumn User;
        private DevExpress.XtraEditors.LabelControl lblBankAccount;
        private DevExpress.XtraEditors.LabelControl lblBankName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deFromDate;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit deToDate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        internal System.Windows.Forms.ComboBox cmbBankNo;
    }
}