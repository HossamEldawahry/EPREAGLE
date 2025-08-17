using DataBaseOperations;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using EPREAGLE.Dataset;
using EPREAGLE.Helper;
using EPREAGLE.Reports;
using EPREAGLE.Services.Classes;
using EPREAGLE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPREAGLE.MainFrm.Frm
{
    public partial class frmSafesMove : DevExpress.XtraEditors.XtraForm
    {
        DateTime fromDate;
        DateTime toDate;
        DatabaseConnection con;
        IUserMove userMove = new UserMove();
        IFillComboBox fillComboBox = new FillComboBox();
        int safeId;
        rptSafeMove rpt;
        public frmSafesMove(DateTime _fromDate, DateTime _toDate, int _Id = -1)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            fillComboBox.FillComboBoxes(cmbBranch, "Main.Branch_tbl", "BranchId", "BranchName");
            cmbBranch.SelectedIndex = -1;
            fillComboBox.FillComboBoxes(cmbSafe, "Main.Safe_tbl", "SafeId", "SafeName");
            fromDate = _fromDate;
            toDate = _toDate;
            deFromDate.EditValue = _fromDate;
            deToDate.EditValue = _toDate;
            safeId = _Id;
            LaodMove(safeId);
        }
        private void LaodMove(int Id)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                string mainQuery = @"SELECT BranchId, SafeId, SafeAccount
                            FROM Main.Safe_tbl
                            WHERE SafeId = @SafeId";
                string query = @"SELECT m.MoveDate AS MoveDate,
                            m.MoveDescription AS MoveDescription,
                            m.MoveDebit AS MoveDebit,
                            m.MoveCredit AS MoveCredit,
                            u.FullName As FullName
                            FROM FIANCE.SafeMove_tbl AS m
                            INNER JOIN Main.User_tbl AS u 
                            ON u.UserId = m.UserId
                            WHERE m.SafeId = @SafeId AND m.MoveDate BETWEEN @FromDate AND @ToDate";
                using (var command = new SqlCommand(mainQuery, con.Connection))
                {
                    command.Parameters.AddWithValue("@SafeId", Id);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count < 1)
                        {
                            //Alarm.Warning("لا توجد بيانات لهذا الحساب البنكي!");
                            cmbBranch.SelectedIndex = -1;
                            cmbSafe.SelectedIndex = -1;
                            lblSafeAccount.Text = string.Empty;
                            gcSafeMove.DataSource = null;
                            return;
                        }
                        cmbBranch.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["BranchId"]);
                        lblSafeAccount.Text = dataTable.Rows[0]["SafeAccount"].ToString();
                        cmbSafe.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["SafeId"]);
                    }
                }
                using (var command = new SqlCommand(query, con.Connection))
                {
                    command.Parameters.AddWithValue("@SafeId", safeId);
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count < 1)
                        {
                            Alarm.Warning("لا توجد حركات لهذة الخزينة في الفترة المحددة!");
                            gcSafeMove.DataSource = null;
                            return;
                        }
                        gcSafeMove.DataSource = dataTable;
                        Date.FieldName = "MoveDate";
                        Time.FieldName = "MoveDate";
                        Description.FieldName = "MoveDescription";
                        Debit.FieldName = "MoveDebit";
                        Credit.FieldName = "MoveCredit";
                        User.FieldName = "FullName";
                    }
                }
            }
            catch (Exception ex)
            {
                //Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally
            {
                con.CloseConnection();
            }
            
        }
        private void DateChange(object sender, EventArgs e)
        {
            fromDate = Convert.ToDateTime(deFromDate.EditValue);
            toDate = Convert.ToDateTime(deToDate.EditValue);
            if (fromDate > toDate)
            {
                //Alarm.Warning("تاريخ البداية يجب أن يكون قبل تاريخ النهاية!");
                return;
            }
            LaodMove(safeId);
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void cmbSafe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                safeId = Convert.ToInt32(cmbSafe.SelectedValue);
                LaodMove(safeId);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
        }
        private void GetPrintData(rptSafeMove rptSafeMove)
        {
            try
            {
                if (gcSafeMove.DataSource == null)
                {
                    Alarm.Warning("لا توجد بيانات للطباعة!");
                    return;
                }
                // إعداد البيانات للطباعة
                dsMove data = new dsMove();
                for (int i = 0; i <= gvSafeMove.RowCount - 1; i++)
                {
                    data.DataTable1.Rows.Add();
                    data.DataTable1.Rows[i]["Date"] = Convert.ToDateTime(gvSafeMove.GetRowCellValue(i, "MoveDate")).ToShortDateString();
                    data.DataTable1.Rows[i]["Time"] = Convert.ToDateTime(gvSafeMove.GetRowCellValue(i, "MoveDate")).ToLongTimeString();
                    data.DataTable1.Rows[i]["Description"] = gvSafeMove.GetRowCellValue(i, "MoveDescription");
                    data.DataTable1.Rows[i]["Debit"] = gvSafeMove.GetRowCellValue(i, "MoveDebit");
                    data.DataTable1.Rows[i]["Credit"] = gvSafeMove.GetRowCellValue(i, "MoveCredit");
                    data.DataTable1.Rows[i]["User"] = gvSafeMove.GetRowCellValue(i, "FullName");
                }
                rptSafeMove.DataSource = data;
                rptSafeMove.lblFromDate.Text = Convert.ToDateTime(deFromDate.EditValue).ToShortDateString();
                rptSafeMove.lblToDate.Text = Convert.ToDateTime(deToDate.EditValue).ToShortDateString();
                rptSafeMove.lblSafeName.Text = cmbSafe.Text;
                rptSafeMove.lblBranchName.Text = cmbBranch.Text;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                Alarm.Error($"حدث خطأ أثناء الطباعة: {ex.Message}");
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            rpt = new rptSafeMove();
            GetPrintData(rpt);
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                con.BeginTransaction();
                userMove.LogUserMove($"قام المستخدم {Data.User.FullName} بطباعة بيانات حركات الخزينة {cmbSafe.Text} الخاص بفرع {cmbBranch.Text}");
                con.CommitTransaction();
                rpt.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                Alarm.Error($"حدث خطأ أثناء عرض التقرير: {ex.Message}");
            }
        }
        private async void btnExportToPdf_Click(object sender, EventArgs e)
        {
            rpt = new rptSafeMove();
            GetPrintData(rpt);
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Export to PDF",
                FileName = $"SafeMove_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {Data.User.FullName} بتصدير بيانات حركات الخزينة {cmbSafe.Text} الخاصة بفرع {cmbBranch.Text} إلي PDF على المسار التالي * {saveFileDialog.FileName} *");
                    con.CommitTransaction();
                    await rpt.ExportToPdfAsync(saveFileDialog.FileName).ConfigureAwait(true);
                    Alarm.Success("تم تصدير التقرير بنجاح!");
                }
                catch (Exception ex)
                {
                    Log.LogError(ex);
                    Alarm.Error($"حدث خطأ أثناء تصدير التقرير: {ex.Message}");
                }
            }
        }

        private async void btnExportToExcel_Click(object sender, EventArgs e)
        {
            rpt = new rptSafeMove();
            GetPrintData(rpt);
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Export to Excel",
                FileName = $"SafeMove_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {Data.User.FullName} بتصدير بيانات حركات الخزينة {cmbSafe.Text} الخاصة بفرع  {cmbBranch.Text} إلي Excel على المسار التالي * {saveFile.FileName} *");
                    con.CommitTransaction();
                    await rpt.ExportToXlsxAsync(saveFile.FileName).ConfigureAwait(true);
                    Alarm.Success("تم تصدير التقرير بنجاح!");
                }
                catch (Exception ex)
                {
                    Log.LogError(ex);
                    Alarm.Error($"حدث خطأ أثناء تصدير التقرير: {ex.Message}");
                }
            }
        }
    }
}