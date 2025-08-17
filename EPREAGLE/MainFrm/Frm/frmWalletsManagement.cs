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

namespace EPREAGLE.MainFrm.Frm
{
    public partial class frmWalletsManagement : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        IUserMove userMove = new UserMove();
        public frmWalletsManagement()
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

                const string query = "SELECT WalletId, WalletName, WalletNumber, Amount, WalletAccount FROM Main.Wallet_tbl WHERE IsActive = 1";

                using (var command = new SqlCommand(query, con.Connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gcWallet.DataSource = dataTable;

                    // تعيين أسماء الحقول
                    ID.FieldName = "WalletId";
                    WalletName.FieldName = "WalletName";
                    WalletNumber.FieldName = "WalletNumber";
                    Amount.FieldName = "Amount";
                    WalletAccount.FieldName = "WalletAccount";
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
            frmAddEditWallet frm = new frmAddEditWallet();
            frm.ShowDialog();
            FillGridControl();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int WalletId = Convert.ToInt32(gvWallet.GetFocusedRowCellValue("WalletId"));
            frmAddEditWallet frm = new frmAddEditWallet(true, WalletId);
            frm.ShowDialog();
            FillGridControl();
        }
        private void DeleteWallet()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                con.BeginTransaction();
                int WalletId = Convert.ToInt32(gvWallet.GetFocusedRowCellValue("WalletId"));
                string WalletAccount = gvWallet.GetFocusedRowCellValue("WalletAccount").ToString();
                string WalletName = gvWallet.GetFocusedRowCellValue("WalletName").ToString();
                string WalletNumber = gvWallet.GetFocusedRowCellValue("WalletNumber").ToString();
                Dictionary<string, object> WalletData = new Dictionary<string, object>()
                {
                    { "WalletId", WalletId },
                    { "IsActive", 0 }
                };
                oper.UpdateWithTransaction("Main.Wallet_tbl", WalletData, $"WalletId = {WalletId}");
                Dictionary<string, object> AccountData = new Dictionary<string, object>()
                {
                    { "AccountCode", WalletAccount },
                    { "IsActive", 0 }
                };
                oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", AccountData, $"AccountCode = {WalletAccount}");
                userMove.LogUserMove($"قام المستخدم {User.FullName} بحذف المحفظة {WalletName} برقم {WalletNumber}");
                con.CommitTransaction();
            }
            catch (Exception ex)
            {
                Alarm.Error($"حدث خطأ أثناء حذف المحفظة: {ex.Message}");
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
            if (gvWallet.RowCount == 0)
            {
                Alarm.Warning("لا يوجد محافظ لحذفها");
                return;
            }
            if (gvWallet.GetFocusedRowCellValue("WalletId") == null)
            {
                Alarm.Warning("لا يوجد محفظة محددة للحذف");
                return;
            }
            if (gvWallet.FocusedRowHandle < 0)
            {
                Alarm.Warning("يجب تحديد محفظة  للحذف");
                return;
            }
            DialogResult result = MyBox.Show("هل تريد حذف المحفظة المحددة؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                DeleteWallet();
            }


        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (gcWallet.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Export to Excel";
            saveFileDialog.FileName = "WalletData.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcWallet.ExportToXlsx(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات حسابات المحافظ إلي Excel على المسار التالي * {filePath} *");
                    con.CommitTransaction();

                }
                catch (Exception ex)
                {
                    con.RollbackTransaction();
                    Alarm.Error($"حدث خطأ أثناء تصدير البيانات: {ex.Message}");
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
            if (gcWallet.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Export to PDF";
            saveFileDialog.FileName = "WalletData.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcWallet.ExportToPdf(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات حسابات المحافظ إلي PDF على المسار التالي * {filePath} *");
                    con.CommitTransaction();
                }
                catch (Exception ex)
                {
                    con.RollbackTransaction();
                    Alarm.Error($"حدث خطأ أثناء تصدير البيانات: {ex.Message}");
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
            if (gvWallet.GetFocusedRowCellValue("WalletId") == null)
            {
                Alarm.Warning("لا يوجد بنك محدد لمتاعبة حركته");
                return;
            }
            if (gvWallet.FocusedRowHandle < 0)
            {
                Alarm.Warning("يجب تحديد بنك  لمتاعبة حركته");
                return;
            }
            int WalletId = Convert.ToInt32(gvWallet.GetFocusedRowCellValue("WalletId"));
            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today.AddHours(24);
            frmWalletMove frm = new frmWalletMove(fromDate, toDate, WalletId);
            frm.Show();
        }
    }
}