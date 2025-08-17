using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPREAGLE.Helper
{
    internal class Log
    {
        // مسار ملف السجل - يمكن تعديله حسب الحاجة
        private static string logFilePath = $"{Application.StartupPath}/Logs/AppLog.txt";

        static Log()
        {
            // التأكد من وجود المجلد
            string folder = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        /// <summary>
        /// تسجيل رسالة خطأ
        /// </summary>
        public static void LogError(Exception ex)
        {
            string message = FormatMessage("ERROR", ex.Message + Environment.NewLine + ex.StackTrace, ex);
            WriteToFile(message);
        }

        /// <summary>
        /// تسجيل رسالة معلومات
        /// </summary>
        public static void LogInfo(string message)
        {
            string formattedMessage = FormatMessage("INFO", message);
            WriteToFile(formattedMessage);
        }

        /// <summary>
        /// تسجيل رسالة تحذير
        /// </summary>
        public static void LogWarning(string message)
        {
            string formattedMessage = FormatMessage("WARNING", message);
            WriteToFile(formattedMessage);
        }

        // تنسيق الرسالة قبل كتابتها
        private static string FormatMessage(string type, string message, Exception ex = null)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}]\n{message}\n----------------------------------------\n";
        }

        // الكتابة إلى ملف النص
        private static void WriteToFile(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, message);
            }
            catch (Exception writeEx)
            {
               Alarm.Error($"فشل في كتابة السجل: {writeEx.Message}");
            }
        }

        /// <summary>
        /// تغيير مسار ملف السجل
        /// </summary>
        public static void SetLogFile(string filePath)
        {
            logFilePath = filePath;

            string folder = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}
