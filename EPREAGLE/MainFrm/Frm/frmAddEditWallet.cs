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
    public partial class frmAddEditWallet : DevExpress.XtraEditors.XtraForm
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
        public frmAddEditWallet(bool _isEditMode = false, int Id = -1)
        {
            InitializeComponent();
            isEditMode = _isEditMode;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
            txtBalance.Enabled = !_isEditMode;
        }
        private void LoadData(bool ISEDit = false, int Id = -1)
        {
            if (!ISEDit)
            {
                clear.Clear(this);
                this.Text = "إضافة حساب محفظة جديد";
            }
            else
            {
                try
                {
                    clear.Clear(this);
                    txtBalance.Enabled = !ISEDit;
                    this.Text = "تعديل بيانات حساب محفظة";
                    string query = "Select * From Main.Wallet_tbl Where WalletId = " + Id + "";
                    if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                    using (SqlCommand command = new SqlCommand(query, con.Connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["WalletName"].ToString();
                                txtBalance.Text = reader["Amount"].ToString();
                                txtWalletNumber.Text = reader["WalletNumber"].ToString();
                                txtID.Text = reader["WalletId"].ToString();
                                txtWalletAccount.Text = reader["WalletAccount"].ToString();
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
        /// حفظ بيانات حساب المحفظة
        ///</summary>
        private void SaveNewWallet()
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

                // 2. إنشاء بيانات المحفظة
                var walletData = CreateWalletData(initialBalance);
                oper.InsertWithTransaction("Main.Wallet_tbl", walletData);

                // تسجيل الحركة الخاصة بإضافة حساب المحفظة
                LogUserWalletCreation(walletData);

                // 3. إنشاء الحساب المحاسبي المرتبط بالمحفظة
                var accountData = CreateAccountData(walletData);
                oper.InsertWithTransaction("ACCOUANT.Accounts_tbl", accountData);

                // تسجيل الحركة الخاصة بإنشاء الحساب المحاسبي
                LogUserAccountCreation(walletData);

                // 4. إدخال حركة أولية في حساب المحفظة
                InsertInitialWalletMove(walletData, initialBalance);

                // 5. إذا كان هناك رصيد أولي، إنشاء القيد المحاسبي
                if (initialBalance != 0)
                {
                    CreateOpeningJournalEntry(walletData, accountData, initialBalance);
                    UpdateAccountBalance(walletData, initialBalance);
                }

                // إنهاء المعاملة بنجاح
                con.CommitTransaction();

                // عرض رسالة نجاح للمستخدم
                Alarm.Success("تم إضافة حساب المحفظة بنجاح");

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
        /// رصيد أول مدة لحساب المحفظة
        /// </summary>
        private decimal GetInitialBalance()
        {
            return string.IsNullOrEmpty(txtBalance.Text)
                ? 0
                : Convert.ToDecimal(txtBalance.Text);
        }
        /// <summary>
        /// انشاء حساب محفظة جديد
        /// جلب البيانات الخاصة بحساب المحفظة
        /// </summary>
        private Dictionary<string, object> CreateWalletData(decimal initialBalance)
        {
            return new Dictionary<string, object>
                        {
                            { "WalletId", GetMaxId.MaxId("Main.Wallet_tbl", "WalletId") + 1 },
                            { "WalletName", txtName.Text },
                            { "Amount", initialBalance },
                            { "WalletNumber", txtWalletNumber.Text },
                            { "WalletAccount", getAccountCode.GetAccountNo(accountData.WalletAccount, 1) }
                        };
        }
        /// <summary>
        /// تسجيل حركة أولية للمستخدم لستجيل حساب المحفظة 
        ///</summary>
        private void LogUserWalletCreation(Dictionary<string, object> walletData)
        {
            userMove.LogUserMove($"قام المستخدم {User.FullName} بإضافة حساب محفظة في {walletData["WalletName"]} برقم  {walletData["WalletNumber"]}");
        }
        /// <summary>
        /// انشاء حساب محاسبي جديد
        /// </summary>
        private Dictionary<string, object> CreateAccountData(Dictionary<string, object> walletData)
        {
            return new Dictionary<string, object>
                    {
                        { "ID", GetMaxId.MaxId("ACCOUANT.Accounts_tbl", "ID") + 1 },
                        { "AccountName", $"{walletData["WalletName"]} - {walletData["WalletNumber"]}" },
                        { "AccountCode", walletData["WalletAccount"] },
                        { "AccountType", 1 },
                        { "AccountParents", accountData.WalletAccount },
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
        private void LogUserAccountCreation(Dictionary<string, object> walletData)
        {
            userMove.LogUserMove($"قام المستخدم {User.FullName} بإنشاء حساب محاسبي جديد في {walletData["WalletName"]}-{walletData["WalletNumber"]} برقم حساب {walletData["WalletAccount"]}");
        }
        /// <summary>
        /// انشاء حركة أولى للحساب البنكي
        /// </summary>
        private void InsertInitialWalletMove(Dictionary<string, object> walletData, decimal initialBalance)
        {
            var WalletFirstMove = new Dictionary<string, object>
                                    {
                                        { "WalletId", walletData["WalletId"] },
                                        { "MoveDate", DateTime.Now },
                                        { "MoveDebit", initialBalance > 0 ? initialBalance : 0 },
                                        { "MoveCredit", initialBalance < 0 ? Math.Abs(initialBalance) : 0 },
                                        { "UserId", User.UserId },
                                        { "MoveDescription", $"تم انشاء حساب المحفظة بواسطة {User.FullName}" }
                                    };

            oper.InsertWithTransaction("FIANCE.WalletMove_tbl", WalletFirstMove);
        }
        /// <summary>
        /// انشاء قيد محاسبي افتتاحي
        /// </summary>
        private void CreateOpeningJournalEntry(Dictionary<string, object> walletData, Dictionary<string, object> accountData, decimal initialBalance)
        {
            bool isDebit = initialBalance > 0;

            var journalHeader = new Dictionary<string, object>
                                    {
                                        { "ID", GetMaxId.MaxId("ACCOUANT.JournalEntries_tbl", "ID") + 1 },
                                        { "EntryNumber", sequenceNo.GeneratePrefixNumber("EntryNumber", "ACCOUANT.JournalEntries_tbl", "ID", "JRNL") },
                                        { "EntryDate", DateTime.Now },
                                        { "Description", $"رصيد أول مدة ل{txtName.Text} - {txtWalletNumber.Text}" },
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

            AddJournalDetail(journalHeader["ID"], walletData["WalletAccount"], isDebit ? initialBalance : 0, !isDebit ? Math.Abs(initialBalance) : 0);
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
                                { "Description", $"رصيد أول مدة ل{txtName.Text} - {txtWalletNumber.Text}" }
                            };

            oper.InsertWithTransaction("ACCOUANT.JournalDetails_tbl", detail);
        }
        /// <summary>
        /// تحديث رصيد الحساب المحاسبي
        /// </summary>
        private void UpdateAccountBalance(Dictionary<string, object> walletData, decimal initialBalance)
        {
            var updateData = new Dictionary<string, object>
                                {
                                    { "AccountCode", walletData["WalletAccount"] },
                                    { "FirstValue", initialBalance },
                                    { "DepitValue", initialBalance > 0 ? initialBalance : 0 },
                                    { "CreditValue", initialBalance < 0 ? Math.Abs(initialBalance) : 0 },
                                    { "ModifyBy", User.UserId },
                                    { "ModifyIn", DateTime.Now }
                                };

            oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", updateData, $"AccountCode = '{walletData["WalletAccount"]}'");
        }
        private void UpdateWallet()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> WalletData = new Dictionary<string, object>()
                {
                    { "WalletId", Convert.ToInt32(txtID.Text) },
                    { "WalletAccount", txtWalletAccount.Text },
                    { "WalletName", txtName.Text },
                    { "WalletNo", txtWalletNumber.Text },
                };
                oper.UpdateWithTransaction("Main.Wallet_tbl", WalletData, $"WalletId = {Convert.ToInt32(txtID.Text)}");
                Dictionary<string, object> WalletAccount = new Dictionary<string, object>()
                {
                    { "AccountName", $"{txtName.Text} - {txtWalletNumber.Text}"  },
                    { "AccountCode", txtWalletAccount.Text },
                    { "ModifyBy", User.UserId },
                    { "ModifyIn", DateTime.Now }
                };
                oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", WalletAccount, $"AccountCode = {txtWalletAccount.Text}");
                Dictionary<string, object> WalletfirstMove = new Dictionary<string, object>()
                {
                    { "WalletId", Convert.ToInt32(txtID.Text) },
                    { "MoveDate", DateTime.Now },
                    { "MoveDebit", 0 },
                    { "MoveCredit", 0},
                    { "UserId", User.UserId },
                    { "MoveDescription", $"تم تعديل بيانات حساب المحفظة بواسطة {User.FullName}" }
                };
                oper.InsertWithTransaction("FIANCE.WalletMove_tbl", WalletfirstMove);
                userMove.LogUserMove($"قام المستخدم {User.FullName} بتعديل بيانات حساب محفظة في {txtName.Text} برقم هاتف {txtWalletNumber.Text}");
                con.CommitTransaction();
                Alarm.Success("تم تعديل بيانات حساب المحفظة بنجاح");
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
                Alarm.Error("يرجى إدخال اسم المحفظة");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtWalletNumber.Text))
            {
                Alarm.Error("يرجى إدخال رقم هاتف المجفظة");
                txtWalletNumber.Focus();
                return;
            }
            if (isEditMode)
            {
                UpdateWallet();
            }
            else
            {
                SaveNewWallet();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}