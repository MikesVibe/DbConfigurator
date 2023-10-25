using System;
using System.Net.Http;

namespace DbConfigurator.DataAccess
{
    public class DbConfiguratorApiClient : IDbConfiguratorApiClient
    {
        //protected static HttpClient _httpClient;

        //public DbConfiguratorApiClient()
        //{
        //    _httpClient = CreateClient();
        //}
        public HttpClient CreateClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:8443/api/")
            };
        }

        //public HttpClient HttpClient { get { return _httpClient; } private set { _httpClient = value; } }
    }
}
