using DbConfigurator.Authentication;
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
    public class AuthenticationViewModel : NotifyBase
    {
        private readonly IAccountService _accountService;
        private readonly IStatusService _statusService;
        private readonly SecuritySettings _securitySettings;
        private int _accessFailedCount = 0;
        private bool _isConnected = true;
        private string _statusMessage;

        public AuthenticationViewModel(IAccountService accountService, IStatusService statusService, SecuritySettings securitySettings)
        {
            _accountService = accountService;
            _statusService = statusService;
            _securitySettings = securitySettings;
            LoginCommand = new CustomDelegate(OnLoginExecute, () => !_securitySettings.IsAuthenticated);
            LogoutCommand = new CustomDelegate(OnLogoutExecute, () => _securitySettings.IsAuthenticated);
            EnterClickCommand = new CustomDelegate(OnLoginExecute);
            UpdateStatusMessage();
        }
        public CustomDelegate LoginCommand { get; }
        public CustomDelegate LogoutCommand { get; }
        public CustomDelegate EnterClickCommand { get; }

        public Window Window { get; set; }
        public string Username { get; set; }



        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (_isConnected == value) return;
                _isConnected = value;
                OnPropertyChanged();
                UpdateStatusMessage();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            private set
            {
                if (_statusMessage == value) return;
                _statusMessage = value;
                OnPropertyChanged();
            }
        }


        private async void OnLoginExecute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var clearTextPassword = passwordBox!.Password;

            var result = await _accountService.Login(new LoginDto { UserName = Username, Password = clearTextPassword });
            if (result.IsSuccess)
            {
                _securitySettings.Login(result.Value);
                Window.Close();
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
        private void UpdateStatusMessage()
        {
            StatusMessage = IsConnected ? "Connected" : "Disconnected";
        }
    }
}
