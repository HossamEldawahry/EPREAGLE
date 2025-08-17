using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Services.Interfaces
{
    internal interface IUserMove
    {
        /// <summary>
        /// تسجيل حركة المستخدم و كتابة الحركة فقط
        /// </summary>
        void LogUserMove(string Descreption);
    }
}
