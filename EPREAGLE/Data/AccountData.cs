using DataBaseOperations;
using EPREAGLE.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Data
{
    internal class AccountData
    {
        DatabaseConnection con;
        /// <summary>
        /// تحميل البيانات و وضعها في الخصائص الخاصة بالحسابات.
        /// </summary>
        public AccountData()
        {
            con = new DatabaseConnection(Conn.str);
            GetAccounteredData();
        }
        public string SafeAccount { get; set; }
        public string BankAccount { get; set; }
        public string WalletAccount { get; set; }
        public string CustomerAccount { get; set; }
        public string ImporterAccount { get; set; }
        public string PartnerAccount { get; set; }
        public string CapitalAccount { get; set; }
        public string CapitalBalanceAccount { get; set; }
        /// <summary>
        /// تحميل بيانات الحسابات من قاعدة البيانات.
        /// عبارة عن الحسابات الأب المختلفة لأغلب العمليات المالية.
        /// </summary>
        private void GetAccounteredData()
        {
            try
            {
                if(con.Connection.State == System.Data.ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                string query = "Select SafeAccount,BankAccount,WalletAccount,CustomerAccount,ImporterAccount,PartnerAccount, CapitalAccount,CapitalBalanceAccount From ACCOUANT.AccountSettings_tbl Where ID = 1";
                using (SqlCommand command = new SqlCommand(query, con.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SafeAccount = reader["SafeAccount"].ToString();
                            BankAccount = reader["BankAccount"].ToString();
                            WalletAccount = reader["WalletAccount"].ToString();
                            CustomerAccount = reader["CustomerAccount"].ToString();
                            ImporterAccount = reader["ImporterAccount"].ToString();
                            PartnerAccount = reader["PartnerAccount"].ToString();
                            CapitalAccount = reader["CapitalAccount"].ToString();
                            CapitalBalanceAccount = reader["CapitalBalanceAccount"].ToString();
                        }
                        
                    }
                }
            }
            catch(Exception ex) { Log.LogError(ex); }
        }

    }
}
