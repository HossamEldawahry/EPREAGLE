using EPREAGLE.Data;
using MessageBoxes;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Helper
{
    internal class Alarm
    {
        public static void Success(string message)
        {
            if(!GeneralSettings.ISMessage)
            {
                MyNotify.Show(message, frmNotification.NotificationIcon.Success);
            }
            else
            {
                MyBox.Show(message,"إجراء سليم",System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
            }
        }
        public static void Warning(string message)
        {
            if (!GeneralSettings.ISMessage)
            {
                MyNotify.Show(message, frmNotification.NotificationIcon.Warning);
            }
            else
            {
                MyBox.Show(message, "تنبيه", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }
        public static void Error(string message)
        {
            if (!GeneralSettings.ISMessage)
            {
                MyNotify.Show(message, frmNotification.NotificationIcon.Error);
            }
            else
            {
                MyBox.Show(message, "خطأ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public static void Info(string message)
        {
            if (!GeneralSettings.ISMessage)
            {
                MyNotify.Show(message, frmNotification.NotificationIcon.Info);
            }
            else
            {
                MyBox.Show(message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }
    }
}
