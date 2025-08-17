using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notification;
using EPREAGLE.Helper;
using DataBaseOperations;
using System.Data.SqlClient;
using EPREAGLE.Services.Interfaces;
using EPREAGLE.Services.Classes;
using MessageBoxes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using EPREAGLE.Data;

namespace EPREAGLE.MainFrm.Frm
{
    public partial class frmCurrency : DevExpress.XtraEditors.XtraForm
    {
        ClearControl clear = new ClearControl();
        DatabaseConnection con;
        DatabaseOperation oper;
        IMaxId GetMaxId = new MaxId();
        IUserMove userMove = new UserMove();
        private bool isEditMode = false;
        public frmCurrency()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
            CLR();
            fillGridControl();
        }
        private void OnlyENGChar(object sender, KeyPressEventArgs e )
        {
            // السماح فقط بالأحرف الإنجليزية الكبيرة والصغيرة
            if (!char.IsLetter(e.KeyChar) && !char.IsSymbol(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            // تحويل الأحرف إلى كبيرة تلقائياً (اختياري)
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void fillGridControl()
        {
            try
            {
                if(con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                string query = "SELECT * FROM Main.Currency_tbl Where IsActive = 1";
                using (SqlCommand command = new SqlCommand(query, con.Connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gcCurrency.DataSource = dataTable;
                    ID.FieldName = "CurrencyId";
                    CurrencyName.FieldName = "CurrencyName";
                    Code.FieldName = "CurrencyCode";
                    Symbol.FieldName = "Symbol";
                }
                con.CloseConnection();
            }
            catch (Exception ex)
            {
                Alarm.Error(ex.Message);
                Log.LogError(ex);
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
                if(con.Connection.State == ConnectionState.Closed)
                { con.OpenConnection();}
                string query = "SELECT * FROM Main.Currency_tbl WHERE CurrencyId = @ID";
                using (SqlCommand command = new SqlCommand(query, con.Connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtID.Text = reader["CurrencyId"].ToString();
                        txtName.Text = reader["CurrencyName"].ToString();
                        txtCode.Text = reader["CurrencyCode"].ToString();
                        txtSymbol.Text = reader["Symbol"].ToString();
                        seDecimalPoint.Value = Convert.ToInt32(reader["DecimalPlaces"]);
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
        private void SavenewCurrency()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "CurrencyId", GetMaxId.MaxId("Main.Currency_tbl", "CurrencyId") + 1 },
                    { "CurrencyName", txtName.Text },
                    { "CurrencyCode", txtCode.Text },
                    { "Symbol", txtSymbol.Text },
                    { "DecimalPlaces", seDecimalPoint.Value }
                };
                oper.InsertWithTransaction("Main.Currency_tbl", parameters);
                userMove.LogUserMove($"قام المستخدم {User.FullName} بإضافة عملة جديدة {txtName.Text}");
                Alarm.Success("تم إضافة العملة بنجاح");
                con.CommitTransaction();
            }
            catch (Exception ex)
            {
                con.RollbackTransaction();
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally {con.CloseConnection(); }
        }
        private void UpdateCurrency()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "CurrencyId", txtID.Text },
                    { "CurrencyName", txtName.Text },
                    { "CurrencyCode", txtCode.Text },
                    { "Symbol", txtSymbol.Text },
                    { "DecimalPlaces", seDecimalPoint.Value }
                };
                oper.UpdateWithTransaction("Main.Currency_tbl", parameters, $"CurrencyId = {Convert.ToInt32(txtID.Text)}");
                userMove.LogUserMove($"قام المستخدم {User.FullName} بتحديث بيانات العملة {txtName.Text}");
                Alarm.Success("تم تحديث بيانات العملة بنجاح");
                isEditMode = false;
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
        private void DeleteCurrency(int CurrencyId)
        {
           
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "CurrencyId", CurrencyId },
                    { "IsActive", 0 },
                };
                oper.UpdateWithTransaction("Main.Currency_tbl", parameters, $"CurrencyId = {CurrencyId}");
                userMove.LogUserMove($"قام المستخدم {User.FullName} بحذف العملة {gvCurrency.GetFocusedRowCellValue("CurrencyName")}");
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

        private void gvCurrency_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int ID = Convert.ToInt32(gvCurrency.GetFocusedRowCellValue("ID"));
            GetDataToEdit(ID);
           
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtSymbol.Text))
            {
                Alarm.Warning("يجب ملئ جميع الخانات");
                return;
            }
            if (!isEditMode)
            {
                SavenewCurrency();
                
            }
            else
            {
                UpdateCurrency();
            }
            CLR();
            fillGridControl();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            CLR();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(gvCurrency.RowCount == 0)
            {
                Alarm.Warning("لا يوجد بيانات للحذف");
                return;
            }
            if (gvCurrency.FocusedRowHandle < 0)
            {
                Alarm.Warning("يجب تحديد عملة  للحذف");
                return;
            }
            var result = MyBox.Show("هل تريد حذف العملة", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int ID = Convert.ToInt32(gvCurrency.GetFocusedRowCellValue("CurrencyId"));
                DeleteCurrency(ID);
                Alarm.Success("تم حذف العملة بنجاح");
                fillGridControl();
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (gcCurrency.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Export to Excel";
            saveFileDialog.FileName = "CurrencyData.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcCurrency.ExportToXlsx(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات العملات إلي Excel على المسار التالي * {filePath} *");
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
            if (gcCurrency.DataSource == null)
            {
                Alarm.Warning("لا يوجد بيانات للتصدير");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Export to PDF";
            saveFileDialog.FileName = "CurrencyData.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                gcCurrency.ExportToPdf(filePath);
                try
                {
                    if (con.Connection.State == ConnectionState.Closed)
                    {
                        con.OpenConnection();
                    }
                    con.BeginTransaction();
                    userMove.LogUserMove($"قام المستخدم {User.FullName} بتصدير بيانات العملات إلي PDF على المسار التالي * {filePath} *");
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
        private void gvCurrency_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                int ID = Convert.ToInt32(gvCurrency.GetFocusedRowCellValue("CurrencyId"));
                GetDataToEdit(ID);
            }
        }

        private void frmCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
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