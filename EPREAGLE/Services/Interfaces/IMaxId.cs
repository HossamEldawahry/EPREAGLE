using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Services.Interfaces
{
    internal interface IMaxId
    {
        int MaxId(string tableName, string columnName);
    }
}
