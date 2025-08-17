using DataBaseOperations;
using EPREAGLE.Helper;
using EPREAGLE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Services.Classes
{
    internal class SequenceNo : GetSequenceNo
    {

        public string GeneratePrefixNumber(string Coulmn, string Table, string IdCoulmn, string PatternStart)
        {
            // الحصول على السنة الحالية
            string year = DateTime.Now.ToString("yyyy");

            // الحصول على الرقم المتسلسل من قاعدة البيانات
            int sequenceNumber = GetNextSequenceNumber(Coulmn, Table, IdCoulmn, PatternStart);

            // تكوين رقم القيد
            return $"{PatternStart}-{year}-{sequenceNumber.ToString("D8")}";
        }
        

    }
}
