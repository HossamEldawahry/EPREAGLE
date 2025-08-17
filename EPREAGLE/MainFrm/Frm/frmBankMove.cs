using DataBaseOperations;
using DevExpress.Security;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using EPREAGLE.Data;
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
    public partial class frmBankMove : DevExpress.XtraEditors.XtraForm
    {
        DateTime fromDate;
        DateTime toDate;
        DatabaseConnection con;
        IUserMove userMove = new UserMove();
        IFillComboBox fillComboBox = new FillComboBox();
        int bankId;
        rptBankMove rpt;
        public frmBankMove(DateTime _fromDate ,DateTime _toDate,int _Id = -1)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            fillComboBox.FillComboBoxes(cmbBankNo, "Main.Bank_tbl", "BankId", "BankNo");
            cmbBankNo.SelectedIndex = -1;
            fromDate = _fromDate;
            toDate = _toDate;
            deFromDate.EditValue = _fromDate;
            deToDate.EditValue = _toDate;
            bankId = _Id;
            LaodMove();
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
            LaodMove();
        }
        private void LaodMove()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                string mainQuery = @"SELECT BankName, BankId, BankAccount
                                FROM Main.Bank_tbl
                                WHERE BankId = @BankId";
                string query = @"SELECT m.MoveDate AS MoveDate,
                                m.MoveDescription AS MoveDescription,
                                m.MoveDebit AS MoveDebit,
                                m.MoveCredit AS MoveCredit,
                                u.FullName As FullName
                                FROM FIANCE.BankMove_tbl AS m
                                INNER JOIN Main.User_tbl AS u 
                                ON u.UserId = m.UserId
                                WHERE m.BankId = @BankId AND m.MoveDate BETWEEN @FromDate AND @ToDate";
                using (var command = new SqlCommand(mainQuery, con.Connection))
                {
                    command.Parameters.AddWithValue("@BankId", bankId);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count < 1)
                        {
                            //Alarm.Warning("لا توجد بيانات لهذا الحساب البنكي!");
                            lblBankName.Text = string.Empty;
                            cmbBankNo.SelectedIndex = -1;
                            lblBankAccount.Text = string.Empty;
                            gcBankMove.DataSource = null;
                            return;
                        }
                        lblBankName.Text = dataTable.Rows[0]["BankName"].ToString();
                        cmbBankNo.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["BankId"]);
                        lblBankAccount.Text = dataTable.Rows[0]["BankAccount"].ToString();
                    }
                }
                using (var command = new SqlCommand(query, con.Connection))
                {
                    command.Parameters.AddWithValue("@BankId", bankId);
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count < 1)
                        {
                            Alarm.Warning("لا توجد حركات لهذا الحساب البنكي في الفترة المحددة!");
                            gcBankMove.DataSource = null;
                            return;
                        }
                        gcBankMove.DataSource = dataTable;
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

        private void cmbBankNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bankId = Convert.ToInt32(cmbBankNo.SelectedValue);
                LaodMove();
            }
            catch(Exception ex)
            {
                Log.LogError(ex);
            }
        }
        private void GetPrintData(rptBankMove rptBankMove)
        {
            try
            {
                if (gcBankMove.DataSource == null)
                {
                    Alarm.Warning("لا توجد بيانات للطباعة!");
                    return;
                }
                // إعداد البيانات للطباعة
                dsMove data = new dsMove();
                for (int i = 0; i <= gvBanckMove.RowCount - 1; i++)
                {
                    data.DataTable1.Rows.Add();
                    data.DataTable1.Rows[i]["Date"] = Convert.ToDateTime(gvBanckMove.GetRowCellValue(i,"MoveDate")).ToShortDateString();
                    data.DataTable1.Rows[i]["Time"] = Convert.ToDateTime(gvBanckMove.GetRowCellValue(i, "MoveDate")).ToLongTimeString();
                    data.DataTable1.Rows[i]["Description"] = gvBanckMove.GetRowCellValue(i, "MoveDescription");
                    data.DataTable1.Rows[i]["Debit"] = gvBanckMove.GetRowCellValue(i, "MoveDebit");
                    data.DataTable1.Rows[i]["Credit"] = gvBanckMove.GetRowCellValue(i, "MoveCredit");
                    data.DataTable1.Rows[i]["User"] = gvBanckMove.GetRowCellValue(i, "FullName");
                }
                rptBankMove.DataSource = data;
                rptBankMove.lblFromDate.Text = Convert.ToDateTime(deFromDate.EditValue).ToShortDateString();
                rptBankMove.lblToDate.Text = Convert.ToDateTime(deToDate.EditValue).ToShortDateString();
                rptBankMove.lblBankName.Text = lblBankName.Text;
                rptBankMove.lblAccountNo.Text = cmbBankNo.Text;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                Alarm.Error($"حدث خطأ أثناء الطباعة: {ex.Message}");
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            rpt = new rptBankMove();
            GetPrintData(rpt);
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                con.BeginTransaction();
                userMove.LogUserMove($"قام المستخدم {Data.User.FullName} بطباعة بيانات حركات الحساب البنكي {lblBankName.Text} رقم حساب {cmbBankNo.Text}");
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
            rpt = new rptBankMove();
            GetPrintData(rpt);
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Export to PDF",
                FileName = $"BankMove_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
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
                    userMove.LogUserMove($"قام المستخدم {Data.User.FullName} بتصدير بيانات حركات الحساب البنكي {lblBankName.Text} رقم حساب {cmbBankNo.Text} إلي PDF على المسار التالي * {saveFileDialog.FileName} *");
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
            rpt = new rptBankMove();
            GetPrintData(rpt);
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Export to Excel",
                FileName = $"BankMove_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
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
                    userMove.LogUserMove($"قام المستخدم {Data.User.FullName} بتصدير بيانات حركات الحساب البنكي {lblBankName.Text} رقم حساب {cmbBankNo.Text} إلي Excel على المسار التالي * {saveFile.FileName} *");
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