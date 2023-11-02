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
using DbConfigurator.Model.Contracts;
using System.Net.Http.Json;

namespace DbConfigurator.UI.Base
{
    public class StatusService : IStatusService
    {
        private readonly IDbConfiguratorApiClient _apiClient;

        public StatusService(IDbConfiguratorApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> IsConnected()
        {
            using(var client = _apiClient.CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync($"Status");
                return response.IsSuccessStatusCode;
            }
        }
    }
}
