using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Authentication
{
    public class User
    {
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = new List<string>();
    }
}
