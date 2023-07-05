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
    public class RegionService : IRegionService
    {
        private readonly RegionRepository _regionRepository;
        private readonly AutoMapperConfig _autoMapper;

        public RegionService(
            RegionRepository regionRepository,
            AutoMapperConfig autoMapper)
        {
            _regionRepository = regionRepository;
            _autoMapper = autoMapper;
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
        public async Task<RegionDto?> GetRegionByIdAsync(int id)
        {
            await Task.CompletedTask;
            return new RegionDto();
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

        public async Task<IEnumerable<RegionDto>> GetAllRegionsAsync()
        {
            return _autoMapper.Mapper.Map<IEnumerable<RegionDto>>(await _regionRepository.GetAllAsync());
        }
        public async Task<RegionDto> AddAsync(RegionDto region)
        {
            var regionEntity = _autoMapper.Mapper.Map<Region>(region);
            await _regionRepository.AddAsync(regionEntity);
            await _regionRepository.SaveChangesAsync();

            return _autoMapper.Mapper.Map<RegionDto>(await _regionRepository.GetByIdAsync(regionEntity.Id));
        }
    }
}
