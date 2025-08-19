using EPREAGLE.AccountFrm.Frm;
using EPREAGLE.MainFrm.Frm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EPREAGLE
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        private void OpenForm<T>(Func<T> formFactory) where T : DevExpress.XtraEditors.XtraForm
        {
            // لو الفورم مفتوح بالفعل، أظهره فقط
            var existingForm = this.MdiChildren.FirstOrDefault(f => f is T);
            if (existingForm != null)
            {
                existingForm.Activate();
                return;
            }

            // إنشاء فورم جديد
            var form = formFactory();
            form.MdiParent = this;
            xtraTabbedMdiManager1.FloatForms.Add(form);
            form.Show();
        }
        private void btnCurrency_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Create a new instance of the frmCurrency form
            frmCurrency frm= new frmCurrency();
            //frm.MdiParent = this;
            //xtraTabbedMdiManager1.FloatForms.Add(frm);
            frm.Show();
        }

        private void btnBranch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBranch frm = new frmBranch();
            //frm.MdiParent = this;
            //xtraTabbedMdiManager1.FloatForms.Add(frm);
            frm.Show();
        }

        private void btnAccountTree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAccountTree frm = new frmAccountTree();
            //frm.MdiParent = this;
            //xtraTabbedMdiManager1.FloatForms.Add(frm);
            frm.Show();
        }

        private void btnAddSafe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAddEditSafe frm = new frmAddEditSafe();
            //frm.MdiParent = this;
            //xtraTabbedMdiManager1.FloatForms.Add(frm);
            frm.Show();
        }

        private void btnAddNewBank_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAddEditBank frm = new frmAddEditBank();
            //frm.MdiParent = this;
            //xtraTabbedMdiManager1.FloatForms.Add(frm);
            frm.Show();
        }

        private void btnBanksManagement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(() => new frmBanksManagement());
        }

        private void btnSafesManagement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(() => new frmSafesManagement());
        }

        private void btnBanksMove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(() => new frmBankMove(DateTime.Today, DateTime.Today.AddHours(24)));
            
        }

        private void btnSafeMove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(() => new frmSafesMove(DateTime.Today, DateTime.Today.AddHours(24)));
        }

        private void btnAddNewWallet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAddEditWallet frm = new frmAddEditWallet();
            //frm.MdiParent = this;
            //xtraTabbedMdiManager1.FloatForms.Add(frm);
            frm.Show();
        }

        private void btnWalletMove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(() => new frmWalletMove(DateTime.Today, DateTime.Today.AddHours(24)));
        }

        private void btnWalletManagement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(() => new frmWalletsManagement());
        }

        private void btnJournalEntry_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(() => new frmJournalEntry());
        }
    }
}
