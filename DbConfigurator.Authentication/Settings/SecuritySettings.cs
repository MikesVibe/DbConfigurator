﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Authentication
{
    public class SecuritySettings
    {
        public event EventHandler<UserLoggedInEventArgs>? UserLoggedIn;
        public User User { get; set; } = new User();
        public bool IsAuthenticated { get; set; } = false;

        public void Login(User user)
        {
            IsAuthenticated = true;
            User = user;
            UserLoggedIn?.Invoke(this, new UserLoggedInEventArgs());
        }
        public void Logout() 
        {
            IsAuthenticated = false;
            User = new();
        }
        public bool IsAuthorized(List<Role> authorizedRoles)
        {
            foreach (Role role in authorizedRoles)
            {
                if (User.UserRoles.Contains(role.Name))
                    return true;
            }
            return false;
        }
    }
}
