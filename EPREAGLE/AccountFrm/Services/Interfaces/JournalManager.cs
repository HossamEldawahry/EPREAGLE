using DataBaseOperations;
using EPREAGLE.Data;
using EPREAGLE.Extensions;
using EPREAGLE.Helper;
using EPREAGLE.Services.Classes;
using EPREAGLE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EPREAGLE.AccountFrm.Services.Interfaces
{
    internal class JournalManager
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        IMaxId GetMaxId = new MaxId();
        SequenceNo sequenceNo = new SequenceNo();
        public JournalManager(DatabaseConnection _con, DatabaseOperation _oper)
        {
            con = _con;
            oper = _oper;
        }
        /// <summary>
        /// تسجيل قيد يومية جديد بدون اعتماد
        /// -
        /// مع تسجيل تفاصيل القيد
        /// </summary>
        /// <param name="initialBalance"></param>
        /// <param name="description"></param>
        /// <param name="EntryType"></param>
        public int CreateJournalEntryWithTransiction(decimal initialBalance, string description, int EntryType)
        {
            bool isDebit = initialBalance > 0;

            var journalHeader = new Dictionary<string, object>
                                    {
                                        { "ID", GetMaxId.MaxId("ACCOUANT.JournalEntries_tbl", "ID") + 1 },
                                        { "EntryNumber", sequenceNo.GeneratePrefixNumber("EntryNumber", "ACCOUANT.JournalEntries_tbl", "ID", "JRNL") },
                                        { "EntryDate", DateTime.Now },
                                        { "Description", description },
                                        { "EntryReference", sequenceNo.GeneratePrefixNumber("EntryReference", "ACCOUANT.JournalEntries_tbl", "ID", "JRNL") },
                                        { "EntryTypeId", EntryType },
                                        { "IsPosted", 0 },
                                        { "CreatedBy", User.UserId },
                                        { "CreatedDate", DateTime.Now },
                                        { "TotalDebit", Math.Abs(initialBalance) },
                                        { "TotalCredit", Math.Abs(initialBalance) },
                                    };
                oper.InsertWithTransaction("ACCOUANT.JournalEntries_tbl", journalHeader);

            return journalHeader["ID"].ToInt();


        }
        public void CreateJournalDetailsWithTransiction(object journalId, object accountCode, decimal debit, decimal credit,string description)
        {
            var detail = new Dictionary<string, object>
                            {
                                { "JournalHeaderID", journalId },
                                { "AccountCode", accountCode },
                                { "DebitAmount", debit },
                                { "CreditAmount", credit },
                                { "Description", description }
                            };

            oper.InsertWithTransaction("ACCOUANT.JournalDetails_tbl", detail);
        }
    }
}
