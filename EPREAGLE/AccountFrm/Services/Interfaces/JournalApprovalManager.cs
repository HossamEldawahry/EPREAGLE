using DataBaseOperations;
using DevExpress.XtraEditors;
using EPREAGLE.Data;
using EPREAGLE.Helper;
using EPREAGLE.Services.Classes;
using EPREAGLE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.AccountFrm.Services.Interfaces
{
    internal class JournalApprovalManager
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        IMaxId GetMaxId = new MaxId();
        public JournalApprovalManager()
        {
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
        }
        /// <summary>
        /// ترحيل القيد لدفتر الأستاذ
        /// </summary>
        /// <param name="JournalDetailID"></param>
        /// <param name="JournalHeaderID"></param>
        /// <param name="AccountCode"></param>
        /// <param name="DebitAmount"></param>
        /// <param name="CreditAmount"></param>
        /// <param name="Description"></param>
        public void PostedToLedger(int JournalDetailID,
            int JournalHeaderID,
            string AccountCode,
            decimal DebitAmount,
            decimal CreditAmount,
            string Description)
        {
            var Ledger = new Dictionary<string, object>
            {
                {"JournalDetailID", JournalDetailID },
                {"JournalHeaderID", JournalHeaderID },
                {"AccountCode", AccountCode },
                {"DebitAmount", DebitAmount },
                {"CreditAmount", CreditAmount },
                {"EntryDate", DateTime.Now },
                {"PostedDate", DateTime.Now },
                {"Description", Description }
            };
            oper.InsertWithTransaction("ACCOUANT.GeneralLedger_tbl",Ledger);
        }
        /// <summary>
        /// اعتماد القيد و ترحيله
        /// </summary>
        /// <param name="JournalHeaderID"></param>
        public void ApprovedJournal(int JournalHeaderID)
        {
            var Approved = new Dictionary<string, object>
            {
                {"IsPosted", 1 },
                {"PostedDate", DateTime.Now },
                {"ApprovedBy", User.UserId },
                {"ApprovedDate", DateTime.Now },
            };
            oper.UpdateWithTransaction("ACCOUANT.JournalEntries_tbl", Approved, $"ID = {JournalHeaderID}");
        }
        /// <summary>
        /// تحديث قيمة الحساب و اضافة رصيد أول المدة في جدول الحسابات
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="initialBalance"></param>
        public void UpdateAccountBalanceFirst(string accountCode, decimal initialBalance)
        {
            var updateData = new Dictionary<string, object>
                                {
                                    { "AccountCode", accountCode },
                                    { "FirstValue", initialBalance },
                                    { "DepitValue", initialBalance > 0 ? initialBalance : 0 },
                                    { "CreditValue", initialBalance < 0 ? Math.Abs(initialBalance) : 0 },
                                    { "ModifyBy", User.UserId },
                                    { "ModifyIn", DateTime.Now }
                                };

            oper.UpdateWithTransaction("ACCOUANT.Accounts_tbl", updateData, $"AccountCode = '{accountCode}'");
        }
        /// <summary>
        /// اضافة رصيد أول مدة حسب السنة
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="initialBalance"></param>
        public void InsertOpeningValueInYear(string accountCode, decimal initialBalance)
        {
            var openingData = new Dictionary<string, object>
                                {
                                    { "OpeningId", GetMaxId.MaxId("ACCOUANT.OpeningBalance_tbl", "OpeningId") + 1 },
                                    { "FiscalYear", DateTime.Now.Year },
                                    { "AccountCode", accountCode},
                                    { "OpeningValue", initialBalance}
                                };
            oper.InsertWithTransaction("ACCOUANT.OpeningBalance_tbl", openingData);
        }
        /// <summary>
        /// تعديل قيمة المدين للحساب
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="initialBalance"></param>
        public void UpdateAccountDepit(string accountCode, decimal initialBalance)
        {
            oper.UpQtyOrBalnceWithTransaction("ACCOUANT.Accounts_tbl", "DepitValue", initialBalance, $"AccountCode = '{accountCode}'");
        }
        /// <summary>
        /// تعديل قيمة الدائن للحساب
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="initialBalance"></param>
        public void UpdateAccountCredit(string accountCode, decimal initialBalance)
        {
            oper.UpQtyOrBalnceWithTransaction("ACCOUANT.Accounts_tbl", "CreditValue", initialBalance, $"AccountCode = '{accountCode}'");
        }
    }
}
