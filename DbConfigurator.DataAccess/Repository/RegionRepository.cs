using DbConfigurator.API.DataAccess.Repository;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repository
{
    public class RegionRepository : GenericRepository<Region>
    {
        public RegionRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Area>> GetAreasAsync()
        {
            var regions = GetRegionsAsQueryable();
            var AreasIdList = await regions.Select(r => r.AreaId).Distinct().ToListAsync();
            var areas = await _context.Area.Where(a => AreasIdList.Contains(a.Id)).ToListAsync();

            return areas;
        }
        public async Task<List<BuisnessUnit>> GetBuisnessUnitsAsync(int? areaId = null)
        {
            var regions = areaId is null ?
                GetRegionsAsQueryable() :
                GetRegionsAsQueryable().Where(r => r.AreaId == areaId);

            var buisnessUnitsIdList = await regions.Select(r => r.BuisnessUnitId).ToListAsync();

            var buisnessUnits = await GetBuisnessUnitsAsQueryable().Where(b => buisnessUnitsIdList.Contains(b.Id)).ToListAsync();


            return buisnessUnits;
        }
        public async Task<List<Country>> GetCountriesAsync(int? buisnessUnitId = null)
        {
            var regions = buisnessUnitId is null ?
                GetRegionsAsQueryable() :
                GetRegionsAsQueryable().Where(r => r.BuisnessUnitId == buisnessUnitId);

            var countriesIdList = await regions.Select(r => r.CountryId).ToListAsync();

            var countries = await GetCountriesAsQueryable().Where(c => countriesIdList.Contains(c.Id)).ToListAsync();


            return countries;
        }



        public override async Task<Region?> GetByIdAsync(int id)
        {
            var regions = GetRegionsAsQueryable();
            var region = await regions.Where(r => r.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return region;
        }
        public override Region? GetById(int id)
        {
            return GetRegionsAsQueryable().Where(r => r.Id == id).AsNoTracking().FirstOrDefault();
        }
    }
}
