using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DbConfigurator.DataAccess;
using System.Text.Json;
using FluentResults;
using DbConfigurator.Authentication;

namespace DbConfigurator.UI.Features.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDbConfiguratorApiClient _client;

        public AccountService(IDbConfiguratorApiClient client)
        {
            _client = client;
        }

        public async Task<Result<User>> Login(string userName, string password)
        {
            LoginDto login = new LoginDto() { UserName = userName, Password = password };
            using (HttpClient client = _client.CreateClient())
            {
                // Convert ClassDto to JSON
                string jsonData = JsonSerializer.Serialize(login);

                // Set content type to JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Create the HTTP request content
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send PUT request
                HttpResponseMessage response = await client.PostAsync($"Account/login", content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    var resultValue = await response.Content.ReadAsStringAsync();
                    var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var user = JsonSerializer.Deserialize<User>(resultValue, serializerOptions);
                    if(user is null)
                    {
                        return Result.Fail("Could not login user.");
                    }
                    return user;
                }
                else
                {
                    //Console.WriteLine($"Error sending data. Status code: {response.StatusCode}");
                    return Result.Fail("Could not login user.");
                }
            }
        }
    }
}
