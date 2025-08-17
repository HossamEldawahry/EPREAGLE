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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace EPREAGLE.MainFrm.Frm
{
    public partial class frmBranch : DevExpress.XtraEditors.XtraForm
    {
        ClearControl clear = new ClearControl();
        DatabaseConnection con;
        DatabaseOperation oper;
        IMaxId GetMaxId = new MaxId();
        IUserMove userMove = new UserMove();
        private bool isEditMode = false;
        public frmBranch()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
            CLR();
            FillGridControl();
        }
        private string GetBranchState(int state)
        {
            switch (state)
            {
                case 0:
                    return "مفتوح";
                case 1:
                    return "مغلق";
                case 2:
                    return "تحت الانشاء";
                default:
                    return "مفتوح";
            }
        }
        private void FillGridControl()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }

                const string query = "SELECT BranchId, BranchName, BranchPhone, BranchMobile, BranchAddress, BranchStatue FROM Main.Branch_tbl WHERE IsActive = 1";

                using (var command = new SqlCommand(query, con.Connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // إضافة عمود الحالة كنص
                    dataTable.Columns.Add("BranchStateText", typeof(string));
                    foreach (DataRow row in dataTable.Rows)
                    {
                        int state = Convert.ToInt32(row["BranchStatue"]);
                        row["BranchStateText"] = GetBranchState(state);
                    }

                    gcBranch.DataSource = dataTable;

                    // تعيين أسماء الحقول
                    ID.FieldName = "BranchId";
                    BranchName.FieldName = "BranchName";
                    BranchPhone.FieldName = "BranchPhone";
                    BranchMobil.FieldName = "BranchMobile";
                    BranchAddress.FieldName = "BranchAddress";
                    BranchState.FieldName = "BranchStateText"; // استخدام العمود الجديد
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
        private void CLR()
        {
            clear.Clear(this);
        }
        private void GetDataToEdit(int ID)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                { con.OpenConnection(); }
                string query = "SELECT BranchId, BranchName, BranchPhone, BranchMobile, BranchAddress, BranchStatue FROM Main.Branch_tbl WHERE BranchId = @ID";
                using (SqlCommand command = new SqlCommand(query, con.Connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtID.Text = reader["BranchId"].ToString();
                        txtName.Text = reader["BranchName"].ToString();
                        txtPhone.Text = reader["BranchPhone"].ToString();
                        txtMobil.Text = reader["BranchMobile"].ToString();
                        txtAddress.Text = reader["BranchAddress"].ToString();
                        cmbStatue.SelectedIndex = Convert.ToInt32(reader["BranchStatue"]);
                        isEditMode = true;
                    }
                }
                con.CloseConnection();
            }
            catch (Exception ex)
            {
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
        }
        private void SavenewBranch()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "BranchId", GetMaxId.MaxId("Main.Branch_tbl", "BranchId") + 1 },
                    { "BranchName", txtName.Text },
                    { "BranchPhone", txtPhone.Text },
                    { "BranchMobile", txtMobil.Text },
                    { "BranchAddress", txtAddress.Text },
                    { "BranchStatue", cmbStatue.SelectedIndex }
                };
                oper.InsertWithTransaction("Main.Branch_tbl", parameters);

               userMove.LogUserMove($"قام المستخدم {User.FullName} بإضافة فرع جديد {txtName.Text}");
                con.CommitTransaction();
                Alarm.Success("تم إضافة الفرع بنجاح");
            }
            catch (Exception ex)
            {
                con.RollbackTransaction();
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally
            {
                con.CloseConnection();
            }
        }
        private void UpdateBranch()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "BranchId", Convert.ToInt32(txtID.Text)},
                    { "BranchName", txtName.Text },
                    { "BranchPhone", txtPhone.Text },
                    { "BranchMobile", txtMobil.Text },
                    { "BranchAddress", txtAddress.Text },
                    { "BranchStatue", cmbStatue.SelectedIndex }
                };
                oper.UpdateWithTransaction("Main.Branch_tbl", parameters, $"BranchId = {Convert.ToInt32(txtID.Text)}");
                userMove.LogUserMove($"قام المستخدم {User.FullName} بتحديث بيانات الفرع {txtName.Text}");
                con.CommitTransaction();
                Alarm.Success("تم تحديث بيانات الفرع بنجاح");
                isEditMode = false;
            }
            catch (Exception ex)
            {
                con.RollbackTransaction();
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally
            {
                con.CloseConnection();
            }
        }
        private void DeleteBranch(int BranchId)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "BranchId", BranchId },
                    { "IsActive", 0 },
                };
                oper.UpdateWithTransaction("Main.Branch_tbl", parameters, $"BranchId = {BranchId}");
                string BranchName = gvBranch.GetFocusedRowCellValue("BranchName").ToString();
                userMove.LogUserMove($"قام المستخدم {User.FullName} بحذف الفرع {BranchName}");
                con.CommitTransaction();
            }
            catch (Exception ex)
            {
                con.RollbackTransaction();
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally { con.CloseConnection(); }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            CLR();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text) || Convert.ToInt32(cmbStatue.SelectedIndex) == -1)
            {
                Alarm.Warning("يجب ملئ جميع الخانات");
                return;
            }
            if (!isEditMode)
            {
                SavenewBranch();

            }
            else
            {
                UpdateBranch();
            }
            CLR();
            FillGridControl();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvBranch.RowCount == 0)
            {
                Alarm.Warning("لا يوجد بيانات للحذف");
                return;
            }
            if (gvBranch.FocusedRowHandle < 0)
            {
                Alarm.Warning("يجب تحديد فرع  للحذف");
                return;
            }
            var result = MyBox.Show("هل تريد حذف الفرع", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int ID = Convert.ToInt32(gvBranch.GetFocusedRowCellValue("BranchId"));
                DeleteBranch(ID);
                Alarm.Success("تم حذف الفرع بنجاح");
                FillGridControl();
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (gcBranch.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Export to Excel";
            saveFileDialog.FileName = "BranchData.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcBranch.ExportToXlsx(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات الفروع إلى ملف Excel إلي المسار *{filePath}*");
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
            if(gcBranch.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Export to PDF";
            saveFileDialog.FileName = "BranchData.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcBranch.ExportToPdf(filePath);
                try
                    {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات الفروع إلى ملف PDF إلي المسار *{filePath}*");
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

        private void gvBranch_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                int ID = Convert.ToInt32(gvBranch.GetFocusedRowCellValue("BranchId"));
                GetDataToEdit(ID);
            }
        }

        private void frmBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(null, null);
            }
            if (e.KeyCode == Keys.F3)
            {
                btnSave_Click(null, null);
            }
            if (e.KeyCode == Keys.F2)
            {
                btnNew_Click(null, null);
            }
            if (e.KeyCode == Keys.F5)
            {
                btnExportToExcel_Click(null, null);
            }
            if (e.KeyCode == Keys.F6)
            {
                btnExportToPdf_Click(null, null);
            }
        }
    }
}