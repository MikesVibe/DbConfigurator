using DbConfigurator.UI.Features.Account;
using DbConfigurator.UI.Features.Account.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DbConfigurator.UI.Windows.Authentication
{
    public class AuthenticationViewModel
    {
        private readonly IAccountService _accountService;

        public AuthenticationViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            LoginCommand = new CustomDelegate(OnLoginExecute, () => !IsAuthenticated);
            LogoutCommand = new CustomDelegate(OnLogoutExecute, () => IsAuthenticated);
            EnterClickCommand = new CustomDelegate(OnLoginExecute);
        }
        public CustomDelegate LoginCommand { get; }
        public CustomDelegate LogoutCommand { get; }
        public CustomDelegate EnterClickCommand { get; }

        public Window Window { get; set; }
        public bool IsAuthenticated { get; set; } = false;
        public User? User { get; set; }
        public string Username { get; set; }

        private async void OnLoginExecute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var clearTextPassword = passwordBox!.Password;

            var result = await _accountService.Login(new LoginDto { UserName = Username, Password = clearTextPassword});
            if(result.IsSuccess)
            {
                IsAuthenticated = true;
                User = result.Value;
                Window.Close();
            }
            else
            {
                IsAuthenticated = false;
                Window.Close();
            }
        }
        private void OnLogoutExecute(object parameter)
        {
            User = default;
        }
    }
}
