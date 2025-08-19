using DataBaseOperations;
using DevExpress.Printing.Core.PdfExport.Metafile;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Model;
using EPREAGLE.Extensions;
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
using System.Xml.Linq;

namespace EPREAGLE.AccountFrm.Frm
{
    public partial class frmJournalEntry : DevExpress.XtraEditors.XtraForm
    {
        private List<int> recordIds = new List<int>(); // نخزن فيه كل الـ Ids
        private int currentIndex = 0;                  // المؤشر الحالي
        DatabaseConnection con;
        IFillComboBox fillComboBox = new FillComboBox();
        public frmJournalEntry(int Id = -1)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            con = new DatabaseConnection(Conn.str);
            LoadAllIds();
            ShowRecord(Id);
        }
        private void LoadAllIds()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                SqlCommand cmd = new SqlCommand("SELECT ID FROM ACCOUANT.JournalEntries_tbl Where IsActive = 1 ORDER BY ID", con.Connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    recordIds.Clear();
                    while (reader.Read())
                    {
                        recordIds.Add(reader.GetInt32(0));
                    }
                }
            }
            catch (Exception ex)
            {
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally { con.CloseConnection(); }
        }
        private void ShowRecord(int id = -1)
        {
            fillComboBox.FillComboBoxes(cmbCostCenter, "ACCOUANT.CostCenters_tbl", "CostCenterID", "CostCenterName");
            cmbCostCenter.SelectedIndex = -1;
            if (recordIds.Count == 0 && id == -1) return;
            if(id == -1)
            {
                id = recordIds[currentIndex];
            }
            if (con.Connection.State == ConnectionState.Closed)
            {
                con.OpenConnection();
            }
            try
            {
                string Query = @"
                                SELECT 
                                je.ID,
                                je.EntryNumber,
                                je.EntryDate,
                                je.Description,
                                je.EntryReference,
                                je.EntryTypeId,
                                je.IsPosted,
                                je.PostedDate,
                                je.CreatedBy,
                                ISNULL(u1.UserName, '') AS CreatedByName,
                                je.CreatedDate,
                                je.ApprovedBy,
                                ISNULL(u2.UserName, '') AS ApprovedByName,
                                je.ApprovedDate,
                                je.TotalDebit,
                                je.TotalCredit,
                                je.CostCenterID,
                                je.IsActive
                            FROM ACCOUANT.JournalEntries_tbl je
                            LEFT JOIN Main.User_tbl u1 ON je.CreatedBy = u1.UserId
                            LEFT JOIN Main.User_tbl u2 ON je.ApprovedBy = u2.UserId
                            WHERE je.ID = @Id AND je.IsActive = 1";
                SqlCommand cmd = new SqlCommand(Query, con.Connection);
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtID.Text = reader["ID"].ToString();
                        txtEntryNumber.Text = reader["EntryNumber"].ToString();
                        txtEntryReference.Text = reader["EntryReference"].ToString();
                        cmbEntryType.SelectedIndex = reader["EntryTypeId"].ToInt();
                        txtDescription.Text = reader["Description"].ToString();
                        if(reader["CostCenterID"].ToInt() != -1)
                        {
                            cmbCostCenter.SelectedValue = reader["CostCenterID"].ToInt();
                        }
                        if (Convert.ToBoolean(reader["IsPosted"]))
                        {
                            dePostedDate.EditValue = Convert.ToDateTime(reader["PostedDate"]);
                            txtApprovedBy.Text = reader["ApprovedByName"].ToString();
                            deApprovedDate.EditValue = Convert.ToDateTime(reader["ApprovedDate"]);
                            lblIsPosted.Text = "هذا القيد معتمد و تم ترحيله";
                            lblIsPosted.ForeColor = Color.DarkGreen;
                        }
                        else
                        {

                            dePostedDate.EditValue = null;
                            txtApprovedBy.Text = "";
                            deApprovedDate.EditValue = "";
                            lblIsPosted.Text = "لم يتم اعتماد و ترحيل القيد...";
                            lblIsPosted.ForeColor = Color.Maroon;
                        }
                        txtCreatedBy.Text = reader["CreatedByName"].ToString();
                        deCreatedDate.EditValue = Convert.ToDateTime(reader["CreatedDate"]);
                        
                    }
                }
                ShowJournalDetails(txtID.Text.ToInt());
            }
            catch (Exception ex) 
            {
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally { con.CloseConnection(); }
        }
        // زرار First
        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            ShowRecord();
        }
        private void GetBalance()
        {
            decimal totalDepit = 0, totalCredit = 0; 
            foreach (DataGridViewRow row in dgvJournalEntry.Rows)
            {
                totalDepit += Convert.ToDecimal(row.Cells[0].Value);
                totalCredit += Convert.ToDecimal(row.Cells[1].Value);
            }
            lblTotalDebit.Text = totalDepit.ToString();
            lblTotalCredit.Text = totalCredit.ToString();
            lblBalance.Text = lblTotalDebit.Text.SubtractValue(lblTotalCredit.Text).ToString();
            if(lblBalance.Text.ToNumber() == 0)
            {
                lblBalance.ForeColor = Color.Blue;
                lblStatue.Text = "القيد متعادل";
                lblStatue.ForeColor = Color.Blue;
            }
            if (lblBalance.Text.ToNumber() > 0)
            {
                lblBalance.ForeColor = Color.DarkGreen;
                lblStatue.Text = "قيمة المدين أكبر من الدائن";
                lblStatue.ForeColor = Color.DarkGreen;
            }
            if (lblBalance.Text.ToNumber() < 0)
            {
                lblBalance.ForeColor = Color.Maroon;
                lblStatue.Text = "قيمة الدائن أكبر من المدين";
                lblStatue.ForeColor = Color.Maroon;
            }
        }
        // زرار Last
        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = recordIds.Count - 1;
            ShowRecord();
        }

        // زرار Next
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < recordIds.Count - 1)
            {
                currentIndex++;
                ShowRecord();
            }
        }

        // زرار Previous
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowRecord();
            }
        }
        private string AccountName(string accountCode)
        {
            string Name = string.Empty;
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                SqlCommand cmd = new SqlCommand("SELECT AccountName FROM ACCOUANT.Accounts_tbl Where AccountCode=@AccountCode AND IsActive = 1", con.Connection);
                cmd.Parameters.AddWithValue("@AccountCode",accountCode);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                Name = dt.Rows[0][0].ToString();
                return Name;
            }
            catch (Exception ex)
            {
                Alarm.Error(ex.Message);
                Log.LogError(ex);
                return "";
            }
            finally { con.CloseConnection(); }
        }
        private void ShowJournalDetails(int JournalId)
        {
            dgvJournalEntry.Rows.Clear();
            if (con.Connection.State == ConnectionState.Closed)
            {
                con.OpenConnection();
            }
            try
            {
                string Query = @"
                                SELECT 
                                je.JournalHeaderID,
                                je.AccountCode,
                                je.DebitAmount,
                                je.CreditAmount,
                                je.Description
                            FROM ACCOUANT.JournalDetails_tbl je
                            WHERE je.JournalHeaderID = @Id";
                SqlCommand cmd = new SqlCommand(Query, con.Connection);
                cmd.Parameters.AddWithValue("@Id", JournalId);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        dgvJournalEntry.Rows.Add();
                        dgvJournalEntry.Rows[i].Cells[0].Value = dt.Rows[i]["DebitAmount"];
                        dgvJournalEntry.Rows[i].Cells[1].Value = dt.Rows[i]["CreditAmount"];
                        dgvJournalEntry.Rows[i].Cells[2].Value = dt.Rows[i]["AccountCode"];
                        dgvJournalEntry.Rows[i].Cells[3].Value = AccountName(dgvJournalEntry.Rows[i].Cells[2].Value.ToString());
                        dgvJournalEntry.Rows[i].Cells[4].Value = dt.Rows[i]["Description"];
                    }
                    GetBalance();
                }
            }
            catch (Exception ex)
            {
                Alarm.Error(ex.Message);
                Log.LogError(ex);
            }
            finally { con.CloseConnection(); }
        }
    }
}