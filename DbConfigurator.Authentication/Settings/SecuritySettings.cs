using DbConfigurator.Core;
using DbConfigurator.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Authentication
{
    public class SecuritySettings : ISecuritySettings
    {
        public event EventHandler<UserLoggedInEventArgs>? UserLoggedIn;
        public User User { get; set; } = new User();
        public Role.UserRole UserRole { get; set; } = Role.UserRole.None;
        public bool IsAuthenticated { get; set; } = false;

        public void Login(User user)
        {
            IsAuthenticated = true;
            User = user;
            UserLoggedIn?.Invoke(this, new UserLoggedInEventArgs());
            var role = new Role(user.UserRole);
            UserRole = role.URole;
        }
        public void Logout() 
        {
            IsAuthenticated = false;
            User = new();
        }
        //public bool IsAuthorized(List<Role> authorizedRoles)
        //{
        //    foreach (Role role in authorizedRoles)
        //    {
        //        if (User.UserRole.Contains(role.Name))
        //            return true;
        //    }
        //    return false;
        //}
    }
}
