using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Authentication
{
    public class SecuritySettings
    {
        public User User { get; set; } = new User();
        public bool IsAuthenticated { get; set; } = false;

        public void Login(User user)
        {
            IsAuthenticated = true;
            User = user;
        }
        public void Logout() 
        {
            IsAuthenticated = false;
            User = new();
        }
    }
}
