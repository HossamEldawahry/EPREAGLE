using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Services.Interfaces
{
    internal interface IFillComboBox
    {
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
        void FillComboBoxes(System.Windows.Forms.ComboBox comboBox,
                               string tableName,
                               string valueColumn,
                               string displayColumn);
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
        void FillComboBoxes(System.Windows.Forms.ComboBox comboBox,
                               string tableName,
                               string valueColumn,
                               string displayColumn,
                               string whereColumn,
                               object whereValue);

    }
}
