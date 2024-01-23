using DbConfigurator.Authentication;
using DbConfigurator.Core.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;

namespace DbConfigurator.DataAccess
{
    public class DbConfiguratorApiClient : IDbConfiguratorApiClient
    {
        private readonly ISecuritySettings _settings;

        public DbConfiguratorApiClient(ISecuritySettings settings)
        {
            _settings = settings;
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient()
            {
                //BaseAddress = new Uri("http://localhost:8001/api/")
                BaseAddress = new Uri("https://localhost:8443/api/")
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.User.Token);
            
            return client;
        }
    }
}
