using EPREAGLE.AccountFrm.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseOperations;
using EPREAGLE.Helper;

namespace EPREAGLE.AccountFrm.Services.Classes
{
    internal class GetAccountCode : IGetAccountCode
    {
        DatabaseConnection con;
        public GetAccountCode()
        {
            con = new DatabaseConnection(Conn.str);
        }
        public string GetAccountNo(string Result, int AccouantType) // Account Type 0 = Parent Account, 1 = Child Account   Result = Parent Account Code
        {
            long ResultNo = 0;
            try
            {
                if (con.Connection.State == System.Data.ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                DataTable dt = new DataTable();
                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter("select isnull(MAX( AccountCode),0) from ACCOUANT.Accounts_tbl where AccountParents = " + Result + " ", con.Connection);
                da.Fill(dt);
                ResultNo = Convert.ToInt64(dt.Rows[0][0]);
                if (AccouantType == 0)
                {
                    if (ResultNo == 0)
                    {
                        ResultNo = long.Parse(Result.ToString() + "0".ToString() + 1);
                    }

                    else
                    {
                        ResultNo = (Convert.ToInt64(dt.Rows[0][0]) + 1);
                    }

                }
                else if (AccouantType == 1)
                {
                    if (ResultNo == 0)
                    {

                        ResultNo = long.Parse(Result.ToString() + "0".ToString() + "0".ToString() + "0".ToString() + 1);
                    }

                    else
                    {
                        ResultNo = (Convert.ToInt64(dt.Rows[0][0]) + 1);
                    }


                }

                return ResultNo.ToString();

            }
            catch(Exception ex) 
            {
                Log.LogError(ex);
                ResultNo = 0;
                return ResultNo.ToString();
            }
            finally
            {
                con.CloseConnection();
            }
        }
    }
}
