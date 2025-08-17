using DataBaseOperations;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Shape.Native;
using EPREAGLE.Helper;
using EPREAGLE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPREAGLE.Services.Classes
{
    internal class FillComboBox : IFillComboBox
    {
        DatabaseConnection con;
        public FillComboBox()
        {
            con = new DatabaseConnection(Conn.str);
        }
        /// <summary>
        /// مليء ComboBox بالبيانات من جدول معين
        /// يمكن استخدامه لملء ComboBox بقيم من جدول معين في قاعدة البيانات.
        /// البارامترات المطلوبة:
        /// اسم ComboBox الذي تريد ملؤه،
        /// اسم الجدول الذي يحتوي على البيانات،
        /// اسم العمود الذي يمثل القيمة (ValueMember)،
        /// اسم العمود الذي يمثل العرض (DisplayMember).
        /// يتم استخدام معلمات الاستعلام لمنع هجمات SQL Injection.
        /// </summary>
        /// <param name="comboBox">الكومبو بوكس</param>
        /// <param name="tableName">اسم الجدول في قاعدة البيانات</param>
        /// <param name="valueColumn">اسم العمود الذي يمثل القيمة </param>
        /// <param name="displayColumn">اسم العمود الذي يمثل العرض</param>
        public void FillComboBoxes(System.Windows.Forms.ComboBox comboBox, string tableName,string valueColumn, string displayColumn)
        {
            try
            {
                if(con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                // استخدام معلمات الاستعلام لمنع هجمات SQL Injection
                string query = $"SELECT {valueColumn}, {displayColumn} FROM {tableName} WHERE IsActive = 1";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con.Connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        comboBox.DataSource = dataTable;
                        comboBox.ValueMember = valueColumn;
                        comboBox.DisplayMember = displayColumn;
                    }
                    else
                    {
                        comboBox.DataSource = null;
                    }
                }
                
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته بشكل مناسب
                //Alarm.Error($"Error loading data: {ex.Message}");
                Log.LogError(ex);
            }
            finally
            {
                if (con.Connection.State == ConnectionState.Open)
                {
                    con.CloseConnection();
                }
            }
        }
        /// <summary>
        ///يسمح هذا الأسلوب بملء ComboBox بالبيانات من جدول معين في قاعدة البيانات مع تطبيق شرط WHERE.
        ///مبنية على نفس الأسلوب السابق ولكن مع إضافة شرط WHERE لتصفية البيانات.
        ///يحتوى على شرط واحد
        ///يحتوي على البارامترات التالية:
        ///اسم ComboBox الذي تريد ملؤه،
        /// اسم الجدول الذي يحتوي على البيانات،
        /// اسم العمود الذي يمثل القيمة (ValueMember)،
        /// اسم العمود الذي يمثل العرض (DisplayMember).
        /// اسم العمود الذي سيتم تطبيق شرط WHERE عليه (whereColumn)،
        /// قيمة الشرط (whereValue).
        /// </summary>
        /// <param name="comboBox">الكومبو بوكس</param>
        /// <param name="tableName">اسم الجدول في قاعدة البيانات</param>
        /// <param name="valueColumn">اسم العمود الذي يمثل القيمة </param>
        /// <param name="displayColumn">اسم العمود الذي يمثل العرض</param>
        /// <param name="whereColumn">اسم العمود الذي سيتم تطبيق شرط WHERE عليه</param>
        /// <param name="whereValue">قيمة الشرط </param>
        public void FillComboBoxes(System.Windows.Forms.ComboBox comboBox, string tableName, string valueColumn, string displayColumn, string whereColumn, object whereValue)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                // استخدام معلمات الاستعلام لمنع هجمات SQL Injection
                string query = $"SELECT {valueColumn}, {displayColumn} FROM {tableName} WHERE IsActive = 1 AND {whereColumn} = @whereColumn";
                using (var command = new SqlCommand(query, con.Connection))
                {
                    command.Parameters.AddWithValue("@whereColumn", whereValue);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            comboBox.DataSource = dataTable;
                            comboBox.ValueMember = valueColumn;
                            comboBox.DisplayMember = displayColumn;
                        }
                        else
                        {
                            comboBox.DataSource = null;
                        }
                    }
                }
                    

            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته بشكل مناسب
                //Alarm.Error($"Error loading data: {ex.Message}");
                Log.LogError(ex);
            }
            finally
            {
                if (con.Connection.State == ConnectionState.Open)
                {
                    con.CloseConnection();
                }
            }
        }
    }
}
