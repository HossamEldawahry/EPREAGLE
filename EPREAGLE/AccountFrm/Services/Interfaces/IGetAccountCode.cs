using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.AccountFrm.Services.Interfaces
{
    internal interface IGetAccountCode
    {
        string GetAccountNo(string Result, int AccouantType); // Get Account Code
        
    }
}
