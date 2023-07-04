using DbConfigurator.DataAccess;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public class RegionsRepository
    {
        private readonly AutoMapperConfig _autoMapper;
        private readonly DbConfiguratorDbContext _context;


        public RegionsRepository(
            DbConfiguratorDbContext dbConfiguratorDbContext,
            AutoMapperConfig autoMapperConfig
            )
        {
            _context = dbConfiguratorDbContext;
            _autoMapper = autoMapperConfig;
        }

        public async Task<Region?> GetByIdAsync(int id)
        {
            var regions = GetRegionsAsQueryable();
            var region = await regions.Where(r => r.Id == id).FirstOrDefaultAsync();
            return region;
        }

        public async Task<List<AreaDto>> GetAreasDtoAsync()
        {
            var regions = GetRegionsAsQueryable();
            var AreasIdList = await regions.Select(r => r.AreaId).Distinct().ToListAsync();

            var areas = await _context.Area.Where(a => AreasIdList.Contains(a.Id)).ToListAsync();
            List<AreaDto> areasDto = new();
            foreach (var area in areas)
            {
                areasDto.Add(_autoMapper.Mapper.Map<AreaDto>(area));
            }

            return areasDto;
        }
        public async Task<List<BuisnessUnitDto>> GetBuisnessUnitsDtoAsync(int? areaId = null)
        {
            var regions = areaId is null ?
                GetRegionsAsQueryable() :
                GetRegionsAsQueryable().Where(r => r.AreaId == areaId);

            var buisnessUnitsIdList = await regions.Select(r => r.BuisnessUnitId).ToListAsync();

            var buisnessUnits = GetBuisnessUnitsAsQueryable().Where(b => buisnessUnitsIdList.Contains(b.Id));
            List<BuisnessUnitDto> buisnessUnitsDto = new();
            foreach (var buisnessUnit in buisnessUnits)
            {
                buisnessUnitsDto.Add(_autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit));
            }

            return buisnessUnitsDto;
        }
        public async Task<List<CountryDto>> GetCountriesDtoAsync(int? buisnessUnitId = null)
        {
            var regions = buisnessUnitId is null ?
                GetRegionsAsQueryable() :
                GetRegionsAsQueryable().Where(r => r.BuisnessUnitId == buisnessUnitId);

            var countriesIdList = await regions.Select(r => r.CountryId).ToListAsync();

            var countries = GetCountriesAsQueryable().Where(c => countriesIdList.Contains(c.Id));
            List<CountryDto> countriesDto = new();
            foreach (var country in countries)
            {
                countriesDto.Add(_autoMapper.Mapper.Map<CountryDto>(country));
            }

            return countriesDto;
        }



        private IQueryable<Region> GetRegionsAsQueryable()
        {
            return _context.Set<Region>()
                .Include(r => r.Area)
                .Include(r => r.BuisnessUnit)
                .Include(r => r.Country)
                .AsQueryable();
        }
        private IQueryable<BuisnessUnit> GetBuisnessUnitsAsQueryable()
        {
            return _context.Set<BuisnessUnit>()
                .AsQueryable();
        }
        private IQueryable<Country> GetCountriesAsQueryable()
        {
            return _context.Set<Country>()
                .AsQueryable();
        }

    }
}
