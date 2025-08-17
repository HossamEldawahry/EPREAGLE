using DataBaseOperations;
using EPREAGLE.Data;
using EPREAGLE.Helper;
using EPREAGLE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EPREAGLE.Services.Classes
{
    internal class UserMove : IUserMove
    {
        DatabaseConnection con;
        DatabaseOperation oper;

        public UserMove()
        {
            con = new DatabaseConnection(Conn.str);
            oper = new DatabaseOperation(con);
        }

        public void LogUserMove(string Descreption)
        {
            Dictionary<string, object> userMove = new Dictionary<string, object>
                {
                    { "UserId", User.UserId },
                    { "MoveDate", DateTime.Now },
                    { "MoveDescription", Descreption }
                };
            oper.InsertWithTransaction("FIANCE.UserMove_tbl", userMove);
        }
    }
}
