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
    public partial class frmAddEditBank : DevExpress.XtraEditors.XtraForm
    {
        ClearControl clear = new ClearControl();
        DatabaseConnection con;
        DatabaseOperation oper;
        IMaxId GetMaxId = new MaxId();
        AccountData accountData = new AccountData();
        private bool isEditMode;
        SequenceNo sequenceNo = new SequenceNo();
        IGetAccountCode getAccountCode = new GetAccountCode();
        IUserMove userMove = new UserMove();
        public frmAddEditBank(bool _isEditMode = false, int Id = -1)
        {
            InitializeComponent();
            isEditMode = _isEditMode;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
            txtBalance.Enabled = !_isEditMode;
            LoadData(_isEditMode, Id);
        }
        private void LoadData(bool ISEDit = false, int Id = -1)
        {
            if (!ISEDit)
            {
                clear.Clear(this);
                this.Text = "إضافة حساب بنكي جديد";
            }
            else
            {
                try
                {
                    clear.Clear(this);
                    txtBalance.Enabled = !ISEDit;
                    this.Text = "تعديل بيانات حساب بنكي";
                    string query = "Select * From Main.Bank_tbl Where BankId = " + Id + "";
                    if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                    using (SqlCommand command = new SqlCommand(query, con.Connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["BankName"].ToString();
                                txtBalance.Text = reader["Amount"].ToString();
                                txtBankNo.Text = reader["BankNo"].ToString();
                                txtID.Text = reader["BankId"].ToString();
                                txtBankAccount.Text = reader["BankAccount"].ToString();
                                txtName.Focus();
                            }
                        }
                    }
                }
                catch (Exception ex) { Alarm.Error(ex.Message); Log.LogError(ex); }
                
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
        ///<summary>
        /// حفظ بيانات الحساب البنكي
        ///</summary>
        private void SaveNewBank()
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

                // 2. إنشاء بيانات البنك
                var bankData = CreateBankData(initialBalance);
                oper.InsertWithTransaction("Main.Bank_tbl", bankData);

                // تسجيل الحركة الخاصة بإضافة الحساب البنكي
                LogUserBankCreation(bankData);

                // 3. إنشاء الحساب المحاسبي المرتبط بالبنك
                var accountData = CreateAccountData(bankData);
                oper.InsertWithTransaction("ACCOUANT.Accounts_tbl", accountData);

                // تسجيل الحركة الخاصة بإنشاء الحساب المحاسبي
                LogUserAccountCreation(bankData);

                // 4. إدخال حركة أولية في حساب البنك
                InsertInitialBankMove(bankData, initialBalance);

                // 5. إذا كان هناك رصيد أولي، إنشاء القيد المحاسبي
                if (initialBalance != 0)
                {
                    CreateOpeningJournalEntry(bankData, accountData, initialBalance);
                    UpdateAccountBalance(bankData, initialBalance);
                }

                // إنهاء المعاملة بنجاح
                con.CommitTransaction();

                // عرض رسالة نجاح للمستخدم
                Alarm.Success("تم إضافة الحساب البنكي بنجاح");

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
        /// <summary>
        /// رصيد أول مدة للحساب البنكي
        /// </summary>
        private decimal GetInitialBalance()
        {
            return string.IsNullOrEmpty(txtBalance.Text)
                ? 0 
                : Convert.ToDecimal(txtBalance.Text);
        }
        /// <summary>
        /// انشاء حساب بنكي جديد
        /// جلب البيانات الخاصة بالحساب البنكى
        /// </summary>
        private Dictionary<string, object> CreateBankData(decimal initialBalance)
        {
            return new Dictionary<string, object>
                        {
                            { "BankId", GetMaxId.MaxId("Main.Bank_tbl", "BankId") + 1 },
                            { "BankName", txtName.Text },
                            { "Amount", initialBalance },
                            { "BankNo", txtBankNo.Text },
                            { "BankAccount", getAccountCode.GetAccountNo(accountData.BankAccount, 1) }
                        };
        }
        /// <summary>
        /// تسجيل حركة أولية للمستخدم لستجيل الحساب البنكي البنكى
        ///</summary>
        private void LogUserBankCreation(Dictionary<string, object> bankData)
        {
            userMove.LogUserMove($"قام المستخدم {User.FullName} بإضافة حساب بنكي في {bankData["BankName"]} برقم حساب {bankData["BankNo"]}");
        }
        /// <summary>
        /// انشاء حساب محاسبي جديد
        /// </summary>
        private Dictionary<string, object> CreateAccountData(Dictionary<string, object> bankData)
        {
            return new Dictionary<string, object>
                    {
                        { "ID", GetMaxId.MaxId("ACCOUANT.Accounts_tbl", "ID") + 1 },
                        { "AccountName", $"{bankData["BankName"]} - {bankData["BankNo"]}" },
                        { "AccountCode", bankData["BankAccount"] },
                        { "AccountType", 1 },
                        { "AccountParents", accountData.BankAccount },
                        { "AccountKind", 0 },
                        { "AccountFinal", 2 },
                        { "AccountCurrency", 1 },
                        { "IsActive", true },
                        { "CreateBy", User.UserId },
                        { "CreateDate", DateTime.Now }
                    };
        }
        /// <summary>
        /// تسجيل حركة المستخدم لإضافة حساب محاسبي جديد
        /// </summary>
        private void LogUserAccountCreation(Dictionary<string, object> bankData)
        {
            userMove.LogUserMove($"قام المستخدم {User.FullName} بإنشاء حساب محاسبي جديد في {bankData["BankName"]}-{bankData["BankNo"]} برقم حساب {bankData["BankAccount"]}");
        }
        /// <summary>
        /// انشاء حركة أولى للحساب البنكي
        /// </summary>
        private void InsertInitialBankMove(Dictionary<string, object> bankData, decimal initialBalance)
        {
            var bankFirstMove = new Dictionary<string, object>
                                    {
                                        { "BankId", bankData["BankId"] },
                                        { "MoveDate", DateTime.Now },
                                        { "MoveDebit", initialBalance > 0 ? initialBalance : 0 },
                                        { "MoveCredit", initialBalance < 0 ? Math.Abs(initialBalance) : 0 },
                                        { "UserId", User.UserId },
                                        { "MoveDescription", $"تم انشاء الحساب البنكي بواسطة {User.FullName}" }
                                    };

            oper.InsertWithTransaction("FIANCE.BankMove_tbl", bankFirstMove);
        }
        /// <summary>
        /// انشاء قيد محاسبي افتتاحي
        /// </summary>
        private void CreateOpeningJournalEntry(Dictionary<string, object> bankData, Dictionary<string, object> accountData, decimal initialBalance)
        {
            bool isDebit = initialBalance > 0;

            var journalHeader = new Dictionary<string, object>
                                    {
                                        { "ID", GetMaxId.MaxId("ACCOUANT.JournalEntries_tbl", "ID") + 1 },
                                        { "EntryNumber", sequenceNo.GeneratePrefixNumber("EntryNumber", "ACCOUANT.JournalEntries_tbl", "ID", "JRNL") },
                                        { "EntryDate", DateTime.Now },
                                        { "Description", $"رصيد أول مدة ل{txtName.Text} - {txtBankNo.Text}" },
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

            AddJournalDetail(journalHeader["ID"], bankData["BankAccount"], isDebit ? initialBalance : 0, !isDebit ? Math.Abs(initialBalance) : 0);
            AddJournalDetail(journalHeader["ID"], this.accountData.CapitalAccount, !isDebit ? initialBalance : 0, isDebit ? initialBalance : 0);

            userMove.LogUserMove($"قام المستخدم {User.FullName} بإنشاء قيد محاسبى افتتاحي رقم {journalHeader["EntryNumber"]}");
            userMove.LogUserMove($"قام المستخدم {User.FullName} بإعتماد قيد محاسبى افتتاحي رقم {journalHeader["EntryNumber"]}");
        }
        /// <summary>
        /// تفاصيل القيد المحاسبي
        /// </summary>
        /// <param name="journalId"> رقم القيد</param>
        /// <param name="accountCode"> القيد </param>
        /// <param name="debit"> المدين</param>
        /// <param name="credit"> الدائن</param>
        private void AddJournalDetail(object journalId, object accountCode, decimal debit, decimal credit)
        {
            var detail = new Dictionary<string, object>
                            {
                                { "JournalHeaderID", journalId },
                                { "AccountCode", accountCode },
                                { "DebitAmount", debit },
                                { "CreditAmount", credit },
                                { "Description", $"رصيد أول مدة ل{txtName.Text} - {txtBankNo.Text}" }
                            };

            oper.InsertWithTransaction("ACCOUANT.JournalDetails_tbl", detail);
        }
        /// <summary>
        /// تحديث رصيد الحساب المحاسبي
        /// </summary>
        private void UpdateAccountBalance(Dictionary<string, object> bankData, decimal initialBalance)
        {
            var updateData = new Dictionary<string, object>
                                {
                                    { "AccountCode", bankData["BankAccount"] },
                                    { "FirstValue", initialBalance },
                                    { "DepitValue", initialBalance > 0 ? initialBalance : 0 },
                                    { "CreditValue", initialBalance < 0 ? Math.Abs(initialBalance) : 0 },
                                    { "ModifyBy", User.UserId },
                                    { "ModifyIn", DateTime.Now }
                                };

            oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", updateData, $"AccountCode = '{bankData["BankAccount"]}'");
        }
        
        private void UpdateBank()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> BankData = new Dictionary<string, object>()
                {
                    { "BankId", Convert.ToInt32(txtID.Text) },
                    { "BankAccount", txtBankAccount.Text },
                    { "BankName", txtName.Text },
                    { "BankNo", txtBankNo.Text },
                };
                oper.UpdateWithTransaction("Main.Bank_tbl", BankData, $"BankId = {Convert.ToInt32(txtID.Text)}");
                Dictionary<string, object> BankAccount = new Dictionary<string, object>()
                {
                    { "AccountName", $"{txtName.Text} - {txtBankNo.Text}"  },
                    { "AccountCode", txtBankAccount.Text },
                    { "ModifyBy", User.UserId },
                    { "ModifyIn", DateTime.Now }
                };
                oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", BankAccount, $"AccountCode = {txtBankAccount.Text}");
                Dictionary<string, object> BankfirstMove = new Dictionary<string, object>()
                {
                    { "BankId", Convert.ToInt32(txtID.Text) },
                    { "MoveDate", DateTime.Now },
                    { "MoveDebit", 0 },
                    { "MoveCredit", 0},
                    { "UserId", User.UserId },
                    { "MoveDescription", $"تم تعديل بيانات الحساب البنكي بواسطة {User.FullName}" }
                };
                oper.InsertWithTransaction("FIANCE.BankMove_tbl", BankfirstMove);
                userMove.LogUserMove($"قام المستخدم {User.FullName} بتعديل بيانات حساب بنكي في {txtName.Text} برقم حساب {txtBankNo.Text}");
                con.CommitTransaction();
                Alarm.Success("تم تعديل بيانات الحساب البنكي بنجاح");
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Alarm.Error("يرجى إدخال اسم البنك");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtBankNo.Text))
            {
                Alarm.Error("يرجى إدخال رقم الحساب البنكي");
                txtBankNo.Focus();
                return;
            }
            if (isEditMode)
            {
                UpdateBank();
            }
            else
            {
                SaveNewBank();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}