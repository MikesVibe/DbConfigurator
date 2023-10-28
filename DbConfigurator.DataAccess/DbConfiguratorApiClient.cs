using DbConfigurator.Authentication;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;

namespace DbConfigurator.DataAccess
{
    public class DbConfiguratorApiClient : IDbConfiguratorApiClient
    {
        private readonly SecuritySettings _settings;

        public DbConfiguratorApiClient(SecuritySettings settings)
        {
            _settings = settings;
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:8443/api/")
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.User.Token);
            
            return client;
        }
    }
}
