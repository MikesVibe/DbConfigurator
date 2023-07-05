using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class RegionService : GenericDataService<Region, RegionDto, RegionRepository>, IRegionService
    {
        public RegionService(
            RegionRepository regionRepository,
            AutoMapperConfig autoMapper) : base(regionRepository, autoMapper)
        {
        }

        public async Task<IEnumerable<AreaDto>> GetAreasAsync()
        {
            await Task.CompletedTask;

            return new List<AreaDto>();
        }
        public async Task<IEnumerable<BuisnessUnitDto>> GetBuisnessUnitsAsync()
        {
            await Task.CompletedTask;

            return new List<BuisnessUnitDto>();
        }
        public async Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            await Task.CompletedTask;

            return new List<CountryDto>();
        }

        public async Task<List<BuisnessUnitDto>> GetBuisnessUnitsAsync(int? areaId = null)
        {
            await Task.CompletedTask;

            return new List<BuisnessUnitDto>();
        }
        public async Task<List<CountryDto>> GetCountriesAsync(int? buisnessUnitId = null)
        {
            await Task.CompletedTask;

            return new List<CountryDto>();
        }

    }
}
