﻿using DbConfigurator.Authentication;
using DbConfigurator.Core.Contracts;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Account;
using DbConfigurator.UI.Features.Account.Services;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DbConfigurator.UI.Windows.Authentication
{
    public class AuthenticationViewModel : WindowViewModelBase
    {
        private readonly IAccountService _accountService;
        private readonly ISecuritySettings _securitySettings;
        private int _accessFailedCount = 0;

        public AuthenticationViewModel(IAccountService accountService, IStatusService statusService, ISecuritySettings securitySettings)
            : base(statusService)
        {
            _accountService = accountService;
            _securitySettings = securitySettings;
            LoginCommand = new CustomDelegate(OnLoginExecute, () => !_securitySettings.IsAuthenticated);
            LogoutCommand = new CustomDelegate(OnLogoutExecute, () => _securitySettings.IsAuthenticated);
            EnterClickCommand = new CustomDelegate(OnLoginExecute);
        }
        public CustomDelegate LoginCommand { get; }
        public CustomDelegate LogoutCommand { get; }
        public CustomDelegate EnterClickCommand { get; }

        public Window Window { get; set; }
        public string Username { get; set; }

        private async void OnLoginExecute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var clearTextPassword = passwordBox!.Password;

            //tr
            var result = await _accountService.Login(Username, clearTextPassword);
            if (result.IsSuccess)
            {
                _securitySettings.Login(result.Value);
                Window.Close();
            }
            else if(result.Errors.First() is ConnectionError)
            {
                MessageBox.Show("Failed to connect with server. Ensure that you are connected to network.", "Connection error");
            }
            else
            {
                _accessFailedCount++;
                MessageBox.Show("Failed to authenticate user.");
            }

            if (_accessFailedCount == 3)
            {
                Window.Close();
            }
        }
        private void OnLogoutExecute(object parameter)
        {
            _securitySettings.Logout();
        }
    }
}
