namespace EPREAGLE.MainFrm.Frm
{
    partial class frmBanksManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBanksManagement));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcBank = new DevExpress.XtraGrid.GridControl();
            this.gvBank = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BankNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BankAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnMove = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportToPdf = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportToExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcBank);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1198, 620);
            this.groupControl1.TabIndex = 5;
            // 
            // gcBank
            // 
            this.gcBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBank.Location = new System.Drawing.Point(2, 23);
            this.gcBank.MainView = this.gvBank;
            this.gcBank.Name = "gcBank";
            this.gcBank.Size = new System.Drawing.Size(1194, 595);
            this.gcBank.TabIndex = 1;
            this.gcBank.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBank});
            // 
            // gvBank
            // 
            this.gvBank.ColumnPanelRowHeight = 35;
            this.gvBank.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.BankName,
            this.BankNo,
            this.Amount,
            this.BankAccount});
            this.gvBank.GridControl = this.gcBank;
            this.gvBank.Name = "gvBank";
            this.gvBank.RowHeight = 35;
            // 
            // ID
            // 
            this.ID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.ID.AppearanceCell.Options.UseFont = true;
            this.ID.AppearanceCell.Options.UseTextOptions = true;
            this.ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.ID.AppearanceHeader.Options.UseFont = true;
            this.ID.AppearanceHeader.Options.UseTextOptions = true;
            this.ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.ID.Caption = "الكود";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            this.ID.Width = 106;
            // 
            // BankName
            // 
            this.BankName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.BankName.AppearanceCell.Options.UseFont = true;
            this.BankName.AppearanceCell.Options.UseTextOptions = true;
            this.BankName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BankName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BankName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.BankName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.BankName.AppearanceHeader.Options.UseFont = true;
            this.BankName.AppearanceHeader.Options.UseTextOptions = true;
            this.BankName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BankName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BankName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.BankName.Caption = "اسم البنك";
            this.BankName.Name = "BankName";
            this.BankName.OptionsColumn.AllowEdit = false;
            this.BankName.Visible = true;
            this.BankName.VisibleIndex = 1;
            this.BankName.Width = 349;
            // 
            // BankNo
            // 
            this.BankNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.BankNo.AppearanceCell.Options.UseFont = true;
            this.BankNo.AppearanceCell.Options.UseTextOptions = true;
            this.BankNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BankNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BankNo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.BankNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.BankNo.AppearanceHeader.Options.UseFont = true;
            this.BankNo.AppearanceHeader.Options.UseTextOptions = true;
            this.BankNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BankNo.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BankNo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.BankNo.Caption = "رقم الحساب";
            this.BankNo.Name = "BankNo";
            this.BankNo.OptionsColumn.AllowEdit = false;
            this.BankNo.Visible = true;
            this.BankNo.VisibleIndex = 2;
            this.BankNo.Width = 419;
            // 
            // Amount
            // 
            this.Amount.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Amount.AppearanceCell.Options.UseFont = true;
            this.Amount.AppearanceCell.Options.UseTextOptions = true;
            this.Amount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Amount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Amount.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Amount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Amount.AppearanceHeader.Options.UseFont = true;
            this.Amount.AppearanceHeader.Options.UseTextOptions = true;
            this.Amount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Amount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Amount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.Amount.Caption = "الرصيد";
            this.Amount.DisplayFormat.FormatString = "D2";
            this.Amount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Amount.Name = "Amount";
            this.Amount.OptionsColumn.AllowEdit = false;
            this.Amount.Visible = true;
            this.Amount.VisibleIndex = 3;
            this.Amount.Width = 176;
            // 
            // BankAccount
            // 
            this.BankAccount.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.BankAccount.AppearanceCell.Options.UseFont = true;
            this.BankAccount.AppearanceCell.Options.UseTextOptions = true;
            this.BankAccount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BankAccount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BankAccount.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.BankAccount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.BankAccount.AppearanceHeader.Options.UseFont = true;
            this.BankAccount.AppearanceHeader.Options.UseTextOptions = true;
            this.BankAccount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BankAccount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BankAccount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.BankAccount.Caption = "الكود المحاسبي";
            this.BankAccount.Name = "BankAccount";
            this.BankAccount.Visible = true;
            this.BankAccount.VisibleIndex = 4;
            this.BankAccount.Width = 150;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnMove);
            this.panelControl2.Controls.Add(this.btnExportToPdf);
            this.panelControl2.Controls.Add(this.btnExportToExcel);
            this.panelControl2.Controls.Add(this.btnDelete);
            this.panelControl2.Controls.Add(this.btnEdit);
            this.panelControl2.Controls.Add(this.btnNew);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 620);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1198, 48);
            this.panelControl2.TabIndex = 6;
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
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
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
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNew.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNew.ImageOptions.SvgImage")));
            this.btnNew.ImageOptions.SvgImageSize = new System.Drawing.Size(48, 48);
            this.btnNew.Location = new System.Drawing.Point(1141, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNew.Size = new System.Drawing.Size(55, 44);
            this.btnNew.TabIndex = 0;
            this.btnNew.ToolTip = "إضافة جديد";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmBanksManagement
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 668);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmBanksManagement";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة بيانات الحسابات البنكية";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExportToPdf;
        private DevExpress.XtraEditors.SimpleButton btnExportToExcel;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnMove;
        private DevExpress.XtraGrid.GridControl gcBank;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBank;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn BankName;
        private DevExpress.XtraGrid.Columns.GridColumn BankNo;
        private DevExpress.XtraGrid.Columns.GridColumn Amount;
        private DevExpress.XtraGrid.Columns.GridColumn BankAccount;
    }
}