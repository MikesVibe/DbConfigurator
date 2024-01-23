using System.Net.Http;

namespace DbConfigurator.DataAccess
{
    public interface IDbConfiguratorApiClient
    {
        //HttpClient HttpClient { get; }

        HttpClient CreateClient();
    }
}
