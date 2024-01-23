using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Authentication
{
    public class Role
    {
        public enum UserRole
        {
            Admin,
            SecurityAnalyst,
            DatabaseManager
        }
        public Role(UserRole userRole)
        {
            Name = userRole.ToString();
        }
        public string Name { get; set; }
    }
}
