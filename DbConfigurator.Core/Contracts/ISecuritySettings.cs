using DbConfigurator.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Core.Contracts
{
    public interface ISecuritySettings
    {
        User User { get; set; }
        bool IsAuthenticated { get; set; }
        Role.UserRole UserRole { get; set; }

        event EventHandler<UserLoggedInEventArgs>? UserLoggedIn;

        //bool IsAuthorized(List<Role> authorizedRoles);
        void Login(User user);
        void Logout();
    }
}
