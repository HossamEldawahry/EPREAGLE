using DataBaseOperations;
using EPREAGLE.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Services.Interfaces
{
    abstract class GetSequenceNo
    {
        DatabaseConnection con;
        protected GetSequenceNo() { con = new DatabaseConnection(Conn.str); }
        protected int GetNextSequenceNumber(string Coulmn, string Table, string IdCoulmn, string PatternStart)
        {
            if (con.Connection.State == System.Data.ConnectionState.Closed)
            {
                con.OpenConnection();
            }

            // بدء transaction لضمان عدم تكرار الأرقام

            try
            {
                //if(con.Connection.State == System.Data.ConnectionState.Open && con.Transaction == null) { con.BeginTransaction(); }

                // قراءة آخر رقم متسلسل
                string query = $"SELECT TOP 1 {Coulmn} FROM {Table} " +
                                $"WHERE {Coulmn} LIKE @pattern " +
                                $"ORDER BY {IdCoulmn} DESC";

                string pattern = $"{PatternStart}-{DateTime.Now:yyyy}-%";

                SqlCommand command = new SqlCommand(query, con.Connection);
                command.Parameters.AddWithValue("@pattern", pattern);

                string lastJournalNumber = (string)command.ExecuteScalar();

                int nextNumber = 1;

                if (!string.IsNullOrEmpty(lastJournalNumber))
                {
                    // استخراج الرقم المتسلسل من آخر رقم قيد
                    string[] parts = lastJournalNumber.Split('-');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
                con.CloseConnection();
                //transaction.Commit();
                return nextNumber;
            }
            catch
            {
                //transaction.Rollback();
                throw;
            }


        }
    }
}
