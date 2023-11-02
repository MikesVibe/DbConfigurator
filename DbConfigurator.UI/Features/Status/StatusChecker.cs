using DbConfigurator.UI.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Status
{
    public class StatusChecker
    {
        private Timer _timer;
        public event EventHandler<bool> StatusChanged;
        private readonly IStatusService _statusService;

        public StatusChecker(IStatusService statusService)
        {
            _statusService = statusService;
            _timer = new Timer(CheckStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        private async void CheckStatus(object state)
        {
            bool status = await GetStatusFromApiAsync();
            OnStatusChanged(status);
        }

        protected virtual void OnStatusChanged(bool status)
        {
            StatusChanged?.Invoke(this, status);
        }

        private async Task<bool> GetStatusFromApiAsync()
        {
            // Implement the logic to call the API and determine the status.
            // Return true if connected, false otherwise.
            using (var client = new HttpClient())
            {
                // Example API call
                HttpResponseMessage response = await client.GetAsync("your_api_endpoint");
                return response.IsSuccessStatusCode;
            }
        }

        // Call this to stop the status checker when the application is closing.
        public void Stop()
        {
            _timer?.Change(Timeout.Infinite, 0);
        }
    }
}
