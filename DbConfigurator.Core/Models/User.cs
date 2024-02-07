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
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole UserRole { get; set; } = UserRole.None;

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public static implicit operator string(User user)
        {
            return user.ToString();
        }
    }
}
