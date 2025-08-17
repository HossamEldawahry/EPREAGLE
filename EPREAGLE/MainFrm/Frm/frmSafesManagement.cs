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
    public partial class frmSafesManagement : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        IUserMove userMove = new UserMove();
        public frmSafesManagement()
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

                const string query = @"SELECT s.SafeId As SafeId,
                                       s.SafeName As SafeName,
                                       s.Amount As Amount,
                                       s.SafeAccount As SafeAccount,
                                       b.BranchName As BranchName
                                       FROM 
                                       Main.Safe_tbl AS s
                                       INNER JOIN 
                                       Main.Branch_tbl AS b ON b.BranchId = s.BranchId
                                       WHERE s.IsActive = 1";

                using (var command = new SqlCommand(query, con.Connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gcSafe.DataSource = dataTable;

                    // تعيين أسماء الحقول
                    ID.FieldName = "SafeId";
                    SafeName.FieldName = "SafeName";
                    BranchName.FieldName = "BranchName";
                    Amount.FieldName = "Amount";
                    SafeAccount.FieldName = "SafeAccount";
                }
            }
            catch (Exception ex)
            {
                Alarm.Error($"حدث خطأ أثناء تحميل البيانات: {ex.Message}");
                // يمكن إضافة تسجيل الخطأ هنا
            }
            finally
            {
                con.CloseConnection();
            }
        }
        private void DeleteSafe()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                con.BeginTransaction();
                int SafeId = Convert.ToInt32(gvSafe.GetFocusedRowCellValue("SafeId"));
                string SafeName = gvSafe.GetFocusedRowCellValue("SafeName").ToString();
                string SafeAccount = gvSafe.GetFocusedRowCellValue("SafeAccount").ToString();
                Dictionary<string, object> SafeData = new Dictionary<string, object>()
                    {
                        { "SafeId", SafeId },
                        { "IsActive", 0 }
                    };
                oper.UpdateWithTransaction("Main.Safe_tbl", SafeData, $"SafeId = {SafeId}");
                Dictionary<string, object> AccountData = new Dictionary<string, object>()
                    {
                        { "AccountCode", SafeAccount },
                        { "IsActive", 0 }
                    };
                oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", AccountData, $"AccountCode = {SafeAccount}");
                userMove.LogUserMove($"قام المستخدم {User.FullName} بحذف الخزينة {SafeName}");
                con.CommitTransaction();
                Alarm.Success("تم حذف الخزينة بنجاح");
            }
            catch (Exception ex)
            {
                Alarm.Error($"حدث خطأ أثناء حذف الخزينة: {ex.Message}");
                con.RollbackTransaction();
                // يمكن إضافة تسجيل الخطأ هنا
            }
            finally
            {
                con.CloseConnection();
                FillGridControl();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmAddEditSafe frm = new frmAddEditSafe();
            frm.ShowDialog();
            FillGridControl();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int SafeId = Convert.ToInt32(gvSafe.GetFocusedRowCellValue("SafeId"));
            frmAddEditSafe frm = new frmAddEditSafe(true, SafeId);
            frm.ShowDialog();
            FillGridControl();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvSafe.RowCount == 0)
            {
                Alarm.Warning("لا يوجد خزينة لحذفها");
                return;
            }
            if (gvSafe.GetFocusedRowCellValue("SafeId") == null)
            {
                Alarm.Warning("لا يوجد خزينة محددة للحذف");
                return;
            }
            if (gvSafe.FocusedRowHandle < 0)
            {
                Alarm.Warning("يجب تحديد خزينة  للحذف");
                return;
            }
            DialogResult result = MyBox.Show("هل تريد حذف الخزينة المحددة؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                DeleteSafe();
                
            }

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (gcSafe.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Export to Excel";
            saveFileDialog.FileName = "SafeData.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcSafe.ExportToXlsx(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات الخزينة إلي Excel على المسار التالي * {filePath} *");
                    con.CommitTransaction();
                }
                catch (Exception ex)
                {
                    con.RollbackTransaction();
                    Alarm.Error($"حدث خطأ أثناء حذف الخزينة: {ex.Message}");
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
            if (gcSafe.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Export to PDF";
            saveFileDialog.FileName = "SafeData.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcSafe.ExportToPdf(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات الخزينة إلي PDF على المسار التالي * {filePath} *");
                    con.CommitTransaction();
                }
                catch (Exception ex)
                {
                    con.RollbackTransaction();
                    Alarm.Error($"حدث خطأ أثناء حذف الخزينة: {ex.Message}");
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
            if (gvSafe.GetFocusedRowCellValue("SafeId") == null)
            {
                Alarm.Warning("لا يوجد خزينة محددة لمتاعبة حركتها");
                return;
            }
            if (gvSafe.FocusedRowHandle < 0)
            {
                Alarm.Warning("يجب تحديد خزينة  لمتاعبة حركتها");
                return;
            }
            int safeId = Convert.ToInt32(gvSafe.GetFocusedRowCellValue("SafeId"));
            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today.AddHours(24);
            frmSafesMove frm = new frmSafesMove(fromDate, toDate, safeId);
            frm.Show();
        }
    }
}