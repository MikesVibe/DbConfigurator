using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Windows
{
    public class WindowViewModelBase : NotifyBase
    {
        private readonly IStatusService _statusService;
        private string _statusMessage = "";
        private bool _isConnected = false;

        public WindowViewModelBase(IStatusService statusService)
        {
            UpdateStatusMessage();
            _statusService = statusService;
            _statusService.StatusChanged += StatusChanged!;
        }

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

        public void StatusChanged(object sender, bool isConnected)
        {
            IsConnected = isConnected;
            UpdateStatusMessage();
        }
        private void UpdateStatusMessage()
        {
            StatusMessage = IsConnected ? "Connected" : "Disconnected";
        }
    }
}
