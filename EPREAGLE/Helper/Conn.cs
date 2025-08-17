using EPREAGLE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Helper
{
    internal class Conn
    {
        public static string str
        {
            get
            {
                if (Server.ServerName == "")
                {
                    return @"Server=.\EagleServer;Database=ERPEAGLE;Trusted_Connection=True;";
                }
                else
                {
                    return $@"Server={Server.ServerName};Database=ERPEAGLE;MultipleActiveResultSets=true;User Id= {Server.UserName}; Password= {Server.Password}";
                }
            }
        }
    }
}
