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
            None,
            Admin,
            SecurityAnalyst,
            DatabaseManager
        }
        public Role()
        {
            new Role(UserRole.None);
        }
        public Role(UserRole userRole)
        {
            Name = userRole.ToString();
        }
        public Role(string userRole)
        {
            Name = userRole.ToString();
            if (Enum.TryParse<UserRole>(userRole, out var role))
            {
                URole = role;
            }
        }
        public string Name { get; set; }
        public UserRole URole { get; set; }
    }
}
