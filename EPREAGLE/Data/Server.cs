using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Data
{
    internal class Server
    {
        public static string ServerName { get; set; } = @".\EagleServer";
        public static string UserName { get; set; } = "sa";
        public static string Password { get; set; } = "123456";
    }
}
