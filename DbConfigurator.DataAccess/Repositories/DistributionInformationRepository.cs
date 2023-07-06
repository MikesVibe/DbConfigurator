using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repository
{
    public class DistributionInformationRepository : GenericRepository<DistributionInformation>
    {
        public DistributionInformationRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<DistributionInformation>> GetAllAsync()
        {
            return await _context.Set<DistributionInformation>()
                .Include(d => d.Region)
                    .ThenInclude(r => r.Area)
                .Include(d => d.Region)
                    .ThenInclude(r => r.BuisnessUnit)
                .Include(d => d.Region)
                    .ThenInclude(r => r.Country)
                .Include(d => d.RecipientsCc)
                .Include(d => d.RecipientsTo)
                .AsNoTracking().ToListAsync();
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
    }
}
