using EPREAGLE.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPREAGLE.Helper
{
    internal class ClearControl
    {
        /// <summary>
        /// حذف جميع عناصر التحكم في النموذج
        /// يستخدم في بداية التحميل من النموذج أو عند الحاجة لمسح البيانات
        /// يمكن استخدامه لمسح عناصر التحكم في أي حاوية تحوي عناصر تحكم أخرى
        /// </summary>
        public void Clear(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is DevExpress.XtraEditors.TextEdit)
                {
                    ((DevExpress.XtraEditors.TextEdit)c).Text = "";
                }
                else if (c is DevExpress.XtraEditors.ComboBoxEdit)
                {
                    ((DevExpress.XtraEditors.ComboBoxEdit)c).SelectedIndex = -1;
                }
                else if (c is DevExpress.XtraEditors.DateEdit)
                {
                    ((DevExpress.XtraEditors.DateEdit)c).DateTime = DateTime.Now;
                }
                else if (c is DevExpress.XtraEditors.CheckEdit)
                {
                    ((DevExpress.XtraEditors.CheckEdit)c).Checked = false;
                }
                else if (c is DevExpress.XtraEditors.RadioGroup)
                {
                    ((DevExpress.XtraEditors.RadioGroup)c).SelectedIndex = -1;
                }
                else if (c is DevExpress.XtraEditors.MemoEdit)
                {
                    ((DevExpress.XtraEditors.MemoEdit)c).Text = "";
                }
                else if (c is DevExpress.XtraEditors.LookUpEdit)
                {
                    ((DevExpress.XtraEditors.LookUpEdit)c).EditValue = null;
                }
                else if (c is DevExpress.XtraEditors.SpinEdit)
                {
                    ((DevExpress.XtraEditors.SpinEdit)c).Value = 0;
                }
                else if (c is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)c).Text = "";
                }
                else if (c is System.Windows.Forms.ComboBox)
                {
                    ((System.Windows.Forms.ComboBox)c).SelectedIndex = -1;
                }
                else if (c is System.Windows.Forms.DateTimePicker)
                {
                    ((System.Windows.Forms.DateTimePicker)c).Value = DateTime.Now;
                }
                else if (c is System.Windows.Forms.CheckBox)
                {
                    ((System.Windows.Forms.CheckBox)c).Checked = false;
                }
                else if (c is System.Windows.Forms.RadioButton)
                {
                    ((System.Windows.Forms.RadioButton)c).Checked = false;
                }
                else if (c is System.Windows.Forms.ListBox)
                {
                    ((System.Windows.Forms.ListBox)c).ClearSelected();
                }
                else if(c.IsContainer())
                {
                    Clear(c);
                }
            }

        }
    }
}
