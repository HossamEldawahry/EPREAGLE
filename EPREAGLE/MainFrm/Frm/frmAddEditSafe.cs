using DataBaseOperations;
using DevExpress.XtraEditors;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPREAGLE.MainFrm.Frm
{
    public partial class frmAddEditSafe : DevExpress.XtraEditors.XtraForm
    {
        ClearControl clear = new ClearControl();
        DatabaseConnection con;
        DatabaseOperation oper;
        IMaxId GetMaxId = new MaxId();
        AccountData accountData = new AccountData();
        private bool isEditMode;
        SequenceNo sequenceNo = new SequenceNo();
        IGetAccountCode getAccountCode = new GetAccountCode();
        IFillComboBox fillComboBox = new FillComboBox();
        IUserMove userMove = new UserMove();
        public frmAddEditSafe(bool _isEditMode = false,int Id = -1)
        {
            InitializeComponent();
            isEditMode = _isEditMode;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
            txtBalance.Enabled = !_isEditMode;
            LoadData(_isEditMode, Id);
        }
        private void LoadData(bool ISEDit = false,int Id = -1)
        {
            fillComboBox.FillComboBoxes(cmbBranch, "Main.Branch_tbl", "BranchId", "BranchName");
            if (!ISEDit)
            {
                clear.Clear(this);
                this.Text = "إضافة خزينة جديدة";
            }
            else
            {
                try
                {
                    clear.Clear(this);
                    txtBalance.Enabled = !ISEDit;
                    this.Text = "تعديل بيانات خزينة";
                    string query = "Select * From Main.Safe_tbl Where SafeId = " + Id + "";
                    if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                    using (SqlCommand command = new SqlCommand(query, con.Connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["SafeName"].ToString();
                                txtBalance.Text = reader["Amount"].ToString();
                                cmbBranch.SelectedValue = reader["BranchId"].ToString();
                                txtID.Text = reader["SafeId"].ToString();
                                txtSafeAccount.Text = reader["SafeAccount"].ToString();
                                txtName.Focus();
                            }
                        }
                    }
                }
                catch(Exception ex) { Alarm.Error(ex.Message); Log.LogError(ex); }
                
            }

        }
        private void UpdateSafe()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> safeData = new Dictionary<string, object>()
                {
                    { "SafeId", Convert.ToInt32(txtID.Text) },
                    { "SafeAccount", txtSafeAccount.Text },
                    { "SafeName", txtName.Text },
                    { "BranchId", cmbBranch.SelectedValue },
                };
                oper.UpdateWithTransaction("Main.Safe_tbl", safeData, $"SafeId = {Convert.ToInt32(txtID.Text)}");
                Dictionary<string, object> safeAccount = new Dictionary<string, object>()
                {
                    { "AccountName", txtName.Text },
                    { "AccountCode", txtSafeAccount.Text },
                    { "ModifyBy", User.UserId },
                    { "ModifyIn", DateTime.Now }
                };
                oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", safeAccount, $"AccountCode = {txtSafeAccount.Text}");
                Dictionary<string, object> safefirstMove = new Dictionary<string, object>()
                {
                    { "SafeId", Convert.ToInt32(txtID.Text) },
                    { "MoveDate", DateTime.Now },
                    { "MoveDebit", 0 },
                    { "MoveCredit", 0},
                    { "UserId", User.UserId },
                    { "MoveDescription", $"تم تعديل بيانات الخزينة بواسطة {User.FullName}" }
                };
                oper.InsertWithTransaction("FIANCE.SafeMove_tbl", safefirstMove);
                userMove.LogUserMove($"قام المستخدم {User.FullName} بتعديل بيانات خزينة {txtName.Text} الخاصة بالفرع {cmbBranch.Text}");
                con.CommitTransaction();
                Alarm.Success("تم تعديل بيانات الخزينة بنجاح");
                isEditMode = false;
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
                if (con.Connection.State == ConnectionState.Open) { con.CloseConnection(); }
            }
        }
        private void WriteOnlyNumber(object sender, KeyPressEventArgs e) // Make TextBoxes Don't Accept Chars Only Number Or Decimal
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1) || (e.KeyChar == '.' && (sender as TextEdit).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private decimal GetInitialBalance()
        {
            return string.IsNullOrEmpty(txtBalance.Text)
                ? 0
                : Convert.ToDecimal(txtBalance.Text);
        }
        private Dictionary<string, object> CreateSafeData(decimal initialBalance)
        {
            return new Dictionary<string, object>
                        {
                            { "SafeId", GetMaxId.MaxId("Main.Safe_tbl", "SafeId") + 1 },
                            { "SafeName", txtName.Text },
                            { "Amount", initialBalance },
                            { "BranchId", cmbBranch.SelectedValue },
                            { "SafeAccount", getAccountCode.GetAccountNo(accountData.SafeAccount, 1) }
                        };
        }
        private void LogUserSafeCreation(Dictionary<string, object> safeData)
        {
            userMove.LogUserMove($"قام المستخدم {User.FullName} بإضافة خزينة {txtName.Text} خاصة للفرع {cmbBranch.Text}");
        }
        private Dictionary<string, object> CreateAccountData(Dictionary<string, object> safeData)
        {
            return new Dictionary<string, object>
                        {
                            { "ID", GetMaxId.MaxId("ACCOUANT.Accounts_tbl", "ID") + 1 },
                            { "AccountName", txtName.Text },
                            { "AccountCode", safeData["SafeAccount"] },
                            { "AccountType", 1 },
                            { "AccountParents", accountData.SafeAccount },
                            { "AccountKind", 0 },
                            { "AccountFinal", 2 },
                            { "AccountCurrency", 1 },
                            { "IsActive", true },
                            { "CreateBy", User.UserId },
                            { "CreateDate", DateTime.Now }
                        };
        }
        private void LogUserAccountCreation(Dictionary<string, object> safeData)
        {
            userMove.LogUserMove($"قام المستخدم {User.FullName} بإضافة كود محاسبي لخزينة {txtName.Text} خاصة للفرع {cmbBranch.Text} رقم {safeData["SafeAccount"]}");
        }
        private void InsertInitialSafeMove(Dictionary<string, object> safeData, decimal initialBalance)
        {
            var safeFirstMove = new Dictionary<string, object>
                                    {
                                        { "SafeId", safeData["SafeId"] },
                                        { "MoveDate", DateTime.Now },
                                        { "MoveDebit", initialBalance > 0 ? initialBalance : 0 },
                                        { "MoveCredit", initialBalance < 0 ? Math.Abs(initialBalance) : 0 },
                                        { "UserId", User.UserId },
                                        { "MoveDescription", $"تم انشاء الخزينة بواسطة {User.FullName}" }
                                    };

            oper.InsertWithTransaction("FIANCE.SafeMove_tbl", safeFirstMove);
        }
        private void CreateOpeningJournalEntry(Dictionary<string, object> safeData, Dictionary<string, object> accountData, decimal initialBalance)
        {
            bool isDebit = initialBalance > 0;

            var journalHeader = new Dictionary<string, object>
                                    {
                                        { "ID", GetMaxId.MaxId("ACCOUANT.JournalEntries_tbl", "ID") + 1 },
                                        { "EntryNumber", sequenceNo.GeneratePrefixNumber("EntryNumber", "ACCOUANT.JournalEntries_tbl", "ID", "JRNL") },
                                        { "EntryDate", DateTime.Now },
                                        { "Description", $"رصيد أول مدة ل{txtName.Text}" },
                                        { "EntryReference", sequenceNo.GeneratePrefixNumber("EntryReference", "ACCOUANT.JournalEntries_tbl", "ID", "JRNL") },
                                        { "EntryTypeId", 3 },
                                        { "IsPosted", 1 },
                                        { "PostedDate", DateTime.Now },
                                        { "CreatedBy", User.UserId },
                                        { "CreatedDate", DateTime.Now },
                                        { "ApprovedBy", User.UserId },
                                        { "ApprovedDate", DateTime.Now },
                                        { "TotalDebit", Math.Abs(initialBalance) },
                                        { "TotalCredit", Math.Abs(initialBalance) },
                                    };

            oper.InsertWithTransaction("ACCOUANT.JournalEntries_tbl", journalHeader);

            AddJournalDetail(journalHeader["ID"], safeData["SafeAccount"], isDebit ? initialBalance : 0, !isDebit ? Math.Abs(initialBalance) : 0);
            AddJournalDetail(journalHeader["ID"], this.accountData.CapitalAccount, !isDebit ? initialBalance : 0, isDebit ? initialBalance : 0);

            userMove.LogUserMove($"قام المستخدم {User.FullName} بإنشاء قيد محاسبى افتتاحي رقم {journalHeader["EntryNumber"]}");
            userMove.LogUserMove($"قام المستخدم {User.FullName} بإعتماد قيد محاسبى افتتاحي رقم {journalHeader["EntryNumber"]}");
        }
        private void AddJournalDetail(object journalId, object accountCode, decimal debit, decimal credit)
        {
            var detail = new Dictionary<string, object>
                            {
                                { "JournalHeaderID", journalId },
                                { "AccountCode", accountCode },
                                { "DebitAmount", debit },
                                { "CreditAmount", credit },
                                { "Description", $"رصيد أول مدة ل{txtName.Text}" }
                            };

            oper.InsertWithTransaction("ACCOUANT.JournalDetails_tbl", detail);
        }
        private void UpdateAccountBalance(Dictionary<string, object> safeData, decimal initialBalance)
        {
            var updateData = new Dictionary<string, object>
                                {
                                    { "AccountCode", safeData["SafeAccount"] },
                                    { "FirstValue", initialBalance },
                                    { "DepitValue", initialBalance > 0 ? initialBalance : 0 },
                                    { "CreditValue", initialBalance < 0 ? Math.Abs(initialBalance) : 0 },
                                    { "ModifyBy", User.UserId },
                                    { "ModifyIn", DateTime.Now }
                                };

            oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", updateData, $"AccountCode = '{safeData["SafeAccount"]}'");
        }
        private void SavenewSafe()
        {
            try
            {
                // التحقق من حالة الاتصال وإعادة فتحه إن لزم
                if (con.Connection.State == ConnectionState.Closed)
                    con.OpenConnection();

                // بدء المعاملة
                con.BeginTransaction();

                // 1. جلب الرصيد الأولي
                decimal initialBalance = GetInitialBalance();

                // 2. إنشاء بيانات الخزينة
                var safeData = CreateSafeData(initialBalance);
                oper.InsertWithTransaction("Main.Safe_tbl", safeData);

                // تسجيل إضافة الخزينة
                LogUserSafeCreation(safeData);

                // 3. إنشاء الحساب المحاسبي للخزينة
                var accountData = CreateAccountData(safeData);
                oper.InsertWithTransaction("ACCOUANT.Accounts_tbl", accountData);

                // تسجيل إنشاء الحساب المحاسبي
                LogUserAccountCreation(safeData);

                // 4. إدخال الحركة الأولى في الخزينة
                InsertInitialSafeMove(safeData, initialBalance);

                // 5. إذا كان هناك رصيد أولي، يتم إنشاء القيد المحاسبي
                if (initialBalance != 0)
                {
                    CreateOpeningJournalEntry(safeData, accountData, initialBalance);
                    UpdateAccountBalance(safeData, initialBalance);
                }

                // إنهاء المعاملة بنجاح
                con.CommitTransaction();

                // عرض رسالة نجاح للمستخدم
                Alarm.Success("تم إضافة الخزينة بنجاح");

                // إعادة تحميل البيانات
                isEditMode = false;
                LoadData();
            }
            catch (Exception ex)
            {
                // التراجع عن المعاملة عند حدوث خطأ
                con.RollbackTransaction();

                // إظهار رسالة الخطأ
                Alarm.Error(ex.Message);

                // تسجيل الخطأ
                Log.LogError(ex);
            }
            finally
            {
                // إغلاق الاتصال إن كان مفتوحًا
                if (con.Connection.State == ConnectionState.Open)
                    con.CloseConnection();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text))
            {
                Alarm.Error("يرجى إدخال اسم الخزينة");
                txtName.Focus();
                return;
            }
            if (Convert.ToInt32(cmbBranch.SelectedValue) == -1)
            {
                Alarm.Error("يرجى إدخال اسم الفرع");
                cmbBranch.Focus();
                return;
            }
            if(isEditMode)
            {
                UpdateSafe();
            }
            else
            {
                SavenewSafe();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}