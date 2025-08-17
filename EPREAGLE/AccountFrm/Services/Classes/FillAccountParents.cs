using EPREAGLE.AccountFrm.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseOperations;
using EPREAGLE.Helper;

namespace EPREAGLE.AccountFrm.Services.Classes
{
    internal class FillAccountParents : IFillAccountParents
    {
        DatabaseConnection con;
        public FillAccountParents()
        {
            con = new DatabaseConnection(Conn.str);
        }
        public void FillComboboxFromTable(ComboBox cmb)
        {
            try
            {
                if (con.Connection.State != ConnectionState.Closed) { con.OpenConnection(); }
                DataTable dt = new DataTable();
                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter("select  AccountCode,AccountName from ACCOUANT.Accounts_tbl where AccountType = " + 0 + " And IsActive = 1 ", con.Connection);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cmb.DataSource = dt;
                    cmb.DisplayMember = "AccountName";
                    cmb.ValueMember = "AccountCode";
                    cmb.Text = null;

                }
                else
                {
                    cmb.DataSource = null;
                }
                da.Dispose();
                con.CloseConnection();
            }
            catch(Exception ex)
            {
                Log.LogError(ex);
            }
        }
    }
}
