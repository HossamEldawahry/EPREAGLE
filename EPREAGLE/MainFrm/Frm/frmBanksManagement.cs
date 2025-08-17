using DataBaseOperations;
using DevExpress.XtraEditors;
using EPREAGLE.Data;
using EPREAGLE.Helper;
using EPREAGLE.Services.Classes;
using EPREAGLE.Services.Interfaces;
using MessageBoxes;
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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace EPREAGLE.MainFrm.Frm
{
    public partial class frmBanksManagement : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        IUserMove userMove = new UserMove();
        public frmBanksManagement()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
            FillGridControl();
        }
        private void FillGridControl()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }

                const string query = "SELECT BankId, BankName, BankNo, Amount, BankAccount FROM Main.Bank_tbl WHERE IsActive = 1";

                using (var command = new SqlCommand(query, con.Connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gcBank.DataSource = dataTable;

                    // تعيين أسماء الحقول
                    ID.FieldName = "BankId";
                    BankName.FieldName = "BankName";
                    BankNo.FieldName = "BankNo";
                    Amount.FieldName = "Amount";
                    BankAccount.FieldName = "BankAccount";
                }
            }
            catch (Exception ex)
            {
                Alarm.Error($"حدث خطأ أثناء تحميل البيانات: {ex.Message}");
                // يمكن إضافة تسجيل الخطأ هنا
                Log.LogError(ex);
            }
            finally
            {
                con.CloseConnection();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmAddEditBank frm = new frmAddEditBank();
            frm.ShowDialog();
            FillGridControl();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int BankId = Convert.ToInt32(gvBank.GetFocusedRowCellValue("BankId"));
            frmAddEditBank frm = new frmAddEditBank( true, BankId);
            frm.ShowDialog();
            FillGridControl();
        }
        private void DeleteBank()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                con.BeginTransaction();
                int BankId = Convert.ToInt32(gvBank.GetFocusedRowCellValue("BankId"));
                string BankAccount = gvBank.GetFocusedRowCellValue("BankAccount").ToString();
                string BankName = gvBank.GetFocusedRowCellValue("BankName").ToString();
                string BankNo = gvBank.GetFocusedRowCellValue("BankNo").ToString();
                Dictionary<string, object> BankData = new Dictionary<string, object>()
                {
                    { "BankId", BankId },
                    { "IsActive", 0 }
                };
                oper.UpdateWithTransaction("Main.Bank_tbl", BankData, $"BankId = {BankId}");
                Dictionary<string, object> AccountData = new Dictionary<string, object>()
                {
                    { "AccountCode", BankAccount },
                    { "IsActive", 0 }
                };
                oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", AccountData, $"AccountCode = {BankAccount}");
                userMove.LogUserMove($"قام المستخدم {User.FullName} بحذف البنك {BankName} برقم {BankNo}");
                con.CommitTransaction();
            }
            catch (Exception ex)
            {
                Alarm.Error($"حدث خطأ أثناء حذف البنك: {ex.Message}");
                con.RollbackTransaction();
                // يمكن إضافة تسجيل الخطأ هنا
                Log.LogError(ex);
            }
            finally
            {
                con.CloseConnection();
                FillGridControl();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvBank.RowCount == 0)
            {
                Alarm.Warning("لا يوجد بنوك لحذفها");
                return;
            }
            if (gvBank.GetFocusedRowCellValue("BankId") == null)
            {
                Alarm.Warning("لا يوجد بنك محدد للحذف");
                return;
            }
            if (gvBank.FocusedRowHandle < 0)
            {
                Alarm.Warning("يجب تحديد بنك  للحذف");
                return;
            }
            DialogResult result = MyBox.Show("هل تريد حذف البنك المحدد؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            {

                DeleteBank();
            }
            
            
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (gcBank.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Export to Excel";
            saveFileDialog.FileName = "BankData.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcBank.ExportToXlsx(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات الحسابات البنكية إلي PDF على المسار التالي * {filePath} *");
                    con.CommitTransaction();
                    
                }
                catch (Exception ex)
                {
                    con.RollbackTransaction();
                    Alarm.Error($"حدث خطأ أثناء حذف البنك: {ex.Message}");
                    // يمكن إضافة تسجيل الخطأ هنا
                    Log.LogError(ex);
                }
                finally
                {
                    con.CloseConnection();
                }
                Alarm.Success("تم تصدير البيانات بنجاح");
            }
        }

        private void btnExportToPdf_Click(object sender, EventArgs e)
        {
            if (gcBank.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Export to PDF";
            saveFileDialog.FileName = "BankData.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcBank.ExportToPdf(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات الحسابات البنكية إلي PDF على المسار التالي * {filePath} *");
                    con.CommitTransaction();
                }
                catch (Exception ex)
                {
                    con.RollbackTransaction();
                    Alarm.Error($"حدث خطأ أثناء حذف البنك: {ex.Message}");
                    // يمكن إضافة تسجيل الخطأ هنا
                    Log.LogError(ex);
                }
                finally
                {
                    con.CloseConnection();
                }
                Alarm.Success("تم تصدير البيانات بنجاح");
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (gvBank.GetFocusedRowCellValue("BankId") == null)
            {
                Alarm.Warning("لا يوجد بنك محدد لمتاعبة حركته");
                return;
            }
            if (gvBank.FocusedRowHandle < 0)
            {
                Alarm.Warning("يجب تحديد بنك  لمتاعبة حركته");
                return;
            }
            int bankId = Convert.ToInt32(gvBank.GetFocusedRowCellValue("BankId"));
            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today.AddHours(24);
            frmBankMove frm = new frmBankMove(fromDate, toDate, bankId);
            frm.Show();
        }
    }
}