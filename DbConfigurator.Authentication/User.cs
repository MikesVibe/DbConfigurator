using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Authentication
{
    public class User
    {
        //public User()
        //{
        //    UserName = "Anonymous";
        //    Token = "";
        //}
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
