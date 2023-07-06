using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class DistributionInformationService : GenericDataService<Region, RegionDto, RegionRepository>, IDistributionInformationService
    {
        public DistributionInformationService(RegionRepository repository, AutoMapperConfig autoMapper)
            : base(repository, autoMapper)
        {

        }

        public async Task<IEnumerable<AreaDto>> GetAreasAsync()
        {
            var countries = await _repository.GetAreasAsync();

            return _autoMapper.Mapper.Map<IEnumerable<AreaDto>>(countries);
        }
        public async Task<IEnumerable<BuisnessUnitDto>> GetBuisnessUnitsAsync(int? areaId = null)
        {
            var buisnessUnits = await _repository.GetBuisnessUnitsAsync();

            return _autoMapper.Mapper.Map<IEnumerable<BuisnessUnitDto>>(buisnessUnits);
        }
        public async Task<IEnumerable<CountryDto>> GetCountriesAsync(int? buisnessUnitId = null)
        {
            await Task.CompletedTask;
            var countries = await _repository.GetCountriesAsync();

            return _autoMapper.Mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public IEnumerable<RecipientDto> GetAllRecipients()
        {
            //var collection = _context.Set<Recipient>().AsNoTracking().ToList();
            //return collection;

            return new List<RecipientDto>();
        }

        public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
        {
            await Task.CompletedTask;
            return new List<PriorityDto>();
        }

        public async Task<RegionDto?> GetRegionAsync(int areaId, int buisnessUnitId, int countryId)
        {
            //return await GetRegionsAsQueryable()
            //    .Where(r =>
            //    r.AreaId == areaId &&
            //    r.BuisnessUnitId == buisnessUnitId &&
            //    r.CountryId == countryId).AsNoTracking().FirstOrDefaultAsync();
            await Task.CompletedTask;
            return new RegionDto();
        }
    }
}
