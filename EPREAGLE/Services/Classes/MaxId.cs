using DataBaseOperations;
using EPREAGLE.Helper;
using EPREAGLE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Services.Classes
{
    internal class MaxId : IMaxId
    {
        DatabaseConnection con;
        public MaxId()
        {
            con = new DatabaseConnection(Conn.str);
        }
        int IMaxId.MaxId(string tableName, string columnName)
        {
            int result;
            try
            {
                if(con.Connection.State == System.Data.ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                using(SqlCommand command = new SqlCommand($"SELECT MAX({columnName}) FROM {tableName}", con.Connection))
                {
                    object maxId = command.ExecuteScalar();
                    if (maxId != DBNull.Value)
                    {
                        result = Convert.ToInt32(maxId);
                    }
                    else
                    {
                        result = 0;
                    }
                }
                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                con.CloseConnection();
            }
        }
    }
}
