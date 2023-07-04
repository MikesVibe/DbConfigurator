using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Data.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data
{
    public class RemoteDataService : IDataService
    {
        private readonly HttpClient _client;
        public RemoteDataService()
        {
            _client = new();
        }

        public Country DefaultCountry => throw new NotImplementedException();

        public Priority DefaultPriority => throw new NotImplementedException();

        public Area DefaultArea => throw new NotImplementedException();

        public Region DefaultRegion => throw new NotImplementedException();

        public BuisnessUnit DefaultBuisnessUnit => throw new NotImplementedException();

        public RegionsRepository RegionsRepository => throw new NotImplementedException();

        public void Add<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public Task AddAsync<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Area>> GetAllAreasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BuisnessUnit>> GetAllBuisnessUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Country>> GetAllCountriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<DistributionInformation>> GetAllDistributionInformationAsync()
        {
            throw new NotImplementedException();
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

        public Task<ICollection<Priority>> GetAllPrioritiesAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipient> GetAllRecipients()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Recipient>> GetAllRecipientsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Region>> GetAllRegionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<RegionDto>> GetAllRegionsDtoAsync()
        {
            throw new NotImplementedException();
        }

        public Area? GetAreaById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Area>> GetAreasWithoutDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public BuisnessUnit? GetBuisnessUnitById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BuisnessUnit>> GetBuisnessUnitsAsync(int areaId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BuisnessUnit>> GetBuisnessUnitsWithoutDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> GetCountriesAsync(int buisnessUnitId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Country>> GetCountriesWithoutDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public Country? GetCountryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DistributionInformation> GetDistributionInformationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Recipient> GetRecipientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> GetRegionAsync(int areaId, int buisnessUnitId, int countryId)
        {
            throw new NotImplementedException();
        }

        public Region? GetRegionById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Region>> GetRegionsByAreaIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool HasChanges()
        {
            throw new NotImplementedException();
        }

        public bool IsDefaultCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
