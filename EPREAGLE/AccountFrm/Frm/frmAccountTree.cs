using DataBaseOperations;
using DevExpress.Utils.VisualEffects;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using EPREAGLE.AccountFrm.Services.Classes;
using EPREAGLE.AccountFrm.Services.Interfaces;
using EPREAGLE.Data;
using EPREAGLE.Helper;
using EPREAGLE.Services.Classes;
using EPREAGLE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EPREAGLE.AccountFrm.Frm
{
    public partial class frmAccountTree : DevExpress.XtraEditors.XtraForm
    {
        ClearControl clear = new ClearControl();
        DatabaseConnection con;
        DatabaseOperation oper;
        IGetAccountCode getAccountCode = new GetAccountCode();
        IFillAccountParents fillAccountParents = new FillAccountParents();
        IMaxId GetMaxId = new MaxId();
        IFillComboBox fillComboBox = new FillComboBox();
        private bool isEditMode = false;
        DataTable dt_Accounts = new DataTable();
        public frmAccountTree()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
            LoadData();


        }
        private void CLR()
        {
            clear.Clear(this);
        }
        private void LoadData()
        {
            fillAccountParents.FillComboboxFromTable(cmbAccountParents);
            fillComboBox.FillComboBoxes(cmbAccountCurrency, "Main.Currency_tbl", "CurrencyId", "CurrencyName");
            CLR();
            FillTables(dt_Accounts);
            tvAccount.Nodes.Clear();
            FillTreeView("0", "شجرة الحسابات", null, 0);

        }
        private void FillTables(DataTable dt)
        {
            //DataTable dt = new DataTable();
            try
            {
                if (con.Connection.State != ConnectionState.Closed) { con.OpenConnection(); }
                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter("select AccountCode,AccountName,AccountParents,AccountType from ACCOUANT.Accounts_tbl WHERE IsActive = 1", con.Connection);
                da.Fill(dt);
                da.Dispose();

            }
            catch (Exception ex)
            {
                Alarm.Error(ex.Message);
                // تسجل الخطأ
                Log.LogError(ex);
            }
        }
        private void FillTreeView(string key, string NodeName, TreeNode N, int NodeColor)
        {
            TreeNode NN;

            if (N == null)
            {
                NN = this.tvAccount.Nodes.Add(key, NodeName, 5, 5);
                NN.Tag = "";
            }
            else
            {
                NN = N.Nodes.Add(key, NodeName, NodeColor, NodeColor + 3);
                NN.Tag = key;
            }

            DataView dv = dt_Accounts.DefaultView;
            Int64 xkey = Convert.ToInt64(key);

            dv.RowFilter = "AccountParents ='" + xkey + "'";

            foreach (DataRow dr in dv.ToTable().Rows)
            {
                FillTreeView(Convert.ToString(dr["AccountCode"]), Convert.ToString(dr["AccountName"]), NN, Convert.ToInt32(dr["AccountType"]));
            }
        }
        private void GetAccountCode(object sender, EventArgs e)
        {
            try
            {
                if (cmbAccountType.SelectedIndex == 0 && cmbAccountParents.Text.Trim() == "")
                {
                    this.txtAccountCode.Text = getAccountCode.GetAccountNo("0", cmbAccountType.SelectedIndex).ToString();
                }
                else if (cmbAccountType.SelectedIndex == 0 && cmbAccountParents.Text.Trim() != "")
                {
                    this.txtAccountCode.Text = Convert.ToString(getAccountCode.GetAccountNo(Convert.ToString(this.cmbAccountParents.SelectedValue), cmbAccountType.SelectedIndex));
                }
                else if (cmbAccountType.SelectedIndex == 1 && cmbAccountParents.Text.Trim() != "")
                {
                    this.txtAccountCode.Text = Convert.ToString(getAccountCode.GetAccountNo(Convert.ToString(this.cmbAccountParents.SelectedValue), cmbAccountType.SelectedIndex));
                }
            }

            catch(Exception ex)
            {
                Log.LogError(ex);
                return;
            }
        }
        public void Show_Account_Details()
        {
            try
            {
                DataTable DT = new DataTable();
                DT.Clear();
                TreeNode N;
                N = tvAccount.SelectedNode;
                string sql;
                if(con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                sql = ("Select * FROM ACCOUANT.Accounts_tbl where AccountCode = '" + N.Tag + "'");

                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter(sql, con.Connection);
                da.Fill(DT);
                if ((DT.Rows.Count > 0))
                {
                    txtID.Text = DT.Rows[0]["ID"].ToString();
                    txtAccountName.Text = DT.Rows[0]["AccountName"].ToString();
                    cmbAccountType.SelectedIndex = Convert.ToInt32(DT.Rows[0]["AccountType"]);
                    cmbAccountParents.SelectedValue = DT.Rows[0]["AccountParents"];
                    cmbAccountKind.SelectedIndex = Convert.ToInt32(DT.Rows[0]["AccountKind"]);
                    cmbAccountFinal.SelectedIndex = Convert.ToInt32(DT.Rows[0]["AccountFinal"]);
                    cmbAccountCurrency.SelectedValue = Convert.ToInt32(DT.Rows[0]["AccountCurrency"]);
                    txtAccountCode.Text = DT.Rows[0]["AccountCode"].ToString();
                    isEditMode = true;

                }
                con.CloseConnection();
            }
            catch (Exception ex)
            {
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
        }
        private void tvAccount_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Show_Account_Details();
        }
        private void SavenewAccount()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                int AccountParents = 0;
                if (cmbAccountParents.SelectedIndex != -1)
                {
                    AccountParents = Convert.ToInt32(cmbAccountParents.SelectedValue);
                }
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "ID", GetMaxId.MaxId("ACCOUANT.Accounts_tbl", "ID") + 1 },
                    { "AccountName", txtAccountName.Text },
                    { "AccountCode", txtAccountCode.Text },
                    { "AccountType", cmbAccountType.SelectedIndex },
                    { "AccountParents", AccountParents},
                    { "AccountKind", cmbAccountKind.SelectedIndex },
                    { "AccountFinal", cmbAccountFinal.SelectedIndex },
                    { "AccountCurrency", cmbAccountCurrency.SelectedValue },
                    { "IsActive", true },
                    { "CreateBy", User.UserId },
                    { "CreateDate", DateTime.Now }
                };
                oper.InsertWithTransaction("ACCOUANT.Accounts_tbl", parameters);
                Dictionary<string, object> userMove = new Dictionary<string, object>
                {
                    { "UserId", User.UserId },
                    { "MoveDate", DateTime.Now },
                    { "MoveDescription", $"قام المستخدم {User.FullName} بإنشاء الحساب {txtAccountName.Text} برقم حساب {txtAccountCode.Text}"  }
                };
                oper.InsertWithTransaction("FIANCE.UserMove_tbl", userMove);
                con.CommitTransaction();
                Alarm.Success("تم إضافة حساب جديد بنجاح");
                LoadData();
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
        private void UpdateAccount()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                int AccountParents = 0;
                if(cmbAccountParents.SelectedIndex != -1)
                {
                    AccountParents = Convert.ToInt32(cmbAccountParents.SelectedValue);
                }
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "ID", Convert.ToInt32(txtID.Text) },
                    { "AccountName", txtAccountName.Text },
                    { "AccountCode", txtAccountCode.Text },
                    { "AccountType", cmbAccountType.SelectedIndex },
                    { "AccountParents", AccountParents },
                    { "AccountKind", cmbAccountKind.SelectedIndex },
                    { "AccountFinal", cmbAccountFinal.SelectedIndex },
                    { "AccountCurrency", cmbAccountCurrency.SelectedValue },
                    { "IsActive", true },
                    { "ModifyBy", User.UserId },
                    { "ModifyIn", DateTime.Now }
                };
                oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", parameters, $"ID = {Convert.ToInt32(txtID.Text)}");
                Dictionary<string, object> userMove = new Dictionary<string, object>
                {
                    { "UserId", User.UserId },
                    { "MoveDate", DateTime.Now },
                    { "MoveDescription", $"قام المستخدم {User.FullName} تعديل الحساب {txtAccountName.Text} برقم حساب {txtAccountCode.Text}"  }
                };
                oper.InsertWithTransaction("FIANCE.UserMove_tbl", userMove);
                con.CommitTransaction();
                Alarm.Success("تم تحديث بيانات الحساب بنجاح");
                LoadData();
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((txtAccountName.Text.Trim() == ""))
            {
                Alarm.Warning("يجب كتابة اسم الحساب");
                txtAccountName.Focus();
                return;
            }
            if (((cmbAccountType.SelectedIndex == 1) && (cmbAccountParents.Text.Trim() == null)))
            {
                Alarm.Warning("الحساب الفرعي يجب أن يكون له أب");
                cmbAccountParents.Focus();
                return;

            }
            if ((cmbAccountFinal.Text.Trim() == ""))
            {
                Alarm.Warning("من فضلك اختر الحساب الختامي ");
                cmbAccountFinal.Focus();
            }
            if (txtAccountCode.Text.Length > 17)
            {
                Alarm.Warning(" معذرة، لقد وصلت الى المستوى الاخير في دليل الحسابات لا يمكنك اضافة حساب في هذا المستوى");
                return;
            }
            if (!isEditMode)
            {
                SavenewAccount();

            }
            else
            {
                UpdateAccount();
            }

        }
    }
}