using DataBaseOperations;
using DevExpress.XtraEditors;
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
    public partial class frmWalletMove : DevExpress.XtraEditors.XtraForm
    {
        DateTime fromDate;
        DateTime toDate;
        DatabaseConnection con;
        IUserMove userMove = new UserMove();
        IFillComboBox fillComboBox = new FillComboBox();
        int walletId;
        public frmWalletMove(DateTime _fromDate, DateTime _toDate, int _Id = -1)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            fillComboBox.FillComboBoxes(cmbWalletNumber, "Main.Wallet_tbl", "WalletId", "WalletNumber");
            cmbWalletNumber.SelectedIndex = -1;
            fromDate = _fromDate;
            toDate = _toDate;
            deFromDate.EditValue = _fromDate;
            deToDate.EditValue = _toDate;
            walletId = _Id;
            LaodMove();
        }
        private void DateChange(object sender, EventArgs e)
        {
            fromDate = Convert.ToDateTime(deFromDate.EditValue);
            toDate = Convert.ToDateTime(deToDate.EditValue);
            if (fromDate > toDate)
            {
                //Alarm.Warning("تاريخ البداية يجب أن يكون قبل تاريخ النهاية!");
                return;
            }
            LaodMove();
        }
        private void LaodMove()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                string mainQuery = @"SELECT WalletName, WalletId, WalletAccount
                                FROM Main.Wallet_tbl
                                WHERE WalletId = @WalletId";
                string query = @"SELECT m.MoveDate AS MoveDate,
                                m.MoveDescription AS MoveDescription,
                                m.MoveDebit AS MoveDebit,
                                m.MoveCredit AS MoveCredit,
                                u.FullName As FullName
                                FROM FIANCE.WalletMove_tbl AS m
                                INNER JOIN Main.User_tbl AS u 
                                ON u.UserId = m.UserId
                                WHERE m.WalletId = @WalletId AND m.MoveDate BETWEEN @FromDate AND @ToDate";
                using (var command = new SqlCommand(mainQuery, con.Connection))
                {
                    command.Parameters.AddWithValue("@WalletId", walletId);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count < 1)
                        {
                            //Alarm.Warning("لا توجد بيانات لهذا الحساب البنكي!");
                            lblWalletName.Text = string.Empty;
                            cmbWalletNumber.SelectedIndex = -1;
                            lblWalletAccount.Text = string.Empty;
                            gcWalletMove.DataSource = null;
                            return;
                        }
                        lblWalletName.Text = dataTable.Rows[0]["WalletName"].ToString();
                        cmbWalletNumber.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["WalletId"]);
                        lblWalletAccount.Text = dataTable.Rows[0]["WalletAccount"].ToString();
                    }
                }
                using (var command = new SqlCommand(query, con.Connection))
                {
                    command.Parameters.AddWithValue("@WalletId", walletId);
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count < 1)
                        {
                            Alarm.Warning("لا توجد حركات لهذة المحفظة في الفترة المحددة!");
                            gcWalletMove.DataSource = null;
                            return;
                        }
                        gcWalletMove.DataSource = dataTable;
                        Date.FieldName = "MoveDate";
                        Time.FieldName = "MoveDate";
                        Description.FieldName = "MoveDescription";
                        Debit.FieldName = "MoveDebit";
                        Credit.FieldName = "MoveCredit";
                        User.FieldName = "FullName";
                    }
                }
            }
            catch (Exception ex)
            {
                //Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally
            {
                con.CloseConnection();
            }
        }
        private void cmbWalletNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                walletId = Convert.ToInt32(cmbWalletNumber.SelectedValue);
                LaodMove();
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
        }
    }
}