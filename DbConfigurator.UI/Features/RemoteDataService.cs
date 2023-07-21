using DbConfigurator.Model.DTOs.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class RemoteDataService
    {
        private readonly HttpClient _client;
        public RemoteDataService()
        {
            _client = new();
        }

        public async Task<IEnumerable<DistributionInformationDto>> GetAllDistributionInformationDtoAsync()
        {
            string apiUrl = "https://localhost:7035/api/DistributionInformation";
            HttpResponseMessage response = await _client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var toReturn = JsonConvert.DeserializeObject<ICollection<DistributionInformationDto>>(result);

                return toReturn;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
