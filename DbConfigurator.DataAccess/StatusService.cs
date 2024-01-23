using DbConfigurator.Authentication;
using DbConfigurator.DataAccess;
using DbConfigurator.UI.Base.Contracts;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DbConfigurator.Core.Contracts;
using System.Net.Http.Json;
using System.Threading;
using System.Net.Sockets;

namespace DbConfigurator.UI.Base
{
    public class StatusService : IStatusService
    {
        private readonly IDbConfiguratorApiClient _apiClient;
        private bool _isConnected = false;


        public StatusService(IDbConfiguratorApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public bool IsConnected 
        { 
            get => _isConnected;
            set => _isConnected = value;
        }
        public event EventHandler<bool> StatusChanged;

        public async Task<bool> CanConnect()
        {
            try
            {
                using (var client = _apiClient.CreateClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"Status");
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException ex) when (ex.InnerException is SocketException)
            {
                // Handle the specific case where a SocketException is thrown
                Console.WriteLine("SocketException caught - might be a network or firewall issue.");
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task StartCheckingConnection()
        {
            while (true)
            {
                var result = await CanConnect();
                if(result != IsConnected)
                {
                    IsConnected = result;
                    StatusChanged?.Invoke(this, result);
                }
                await Task.Delay(5000);
            }
        }
    }
}
