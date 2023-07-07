using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repository
{
    public class RegionRepository : GenericRepository<Region>
    {
        public RegionRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
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

        public override IEnumerable<Region> GetAll()
        {
            return GetRegionsAsQueryable().AsNoTracking().ToList();
        }
        public override async Task<IEnumerable<Region>> GetAllAsync(Expression<Func<Region, bool>> predicate)
        {
            return await GetRegionsAsQueryable().OrderBy(r => r.Id).Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Region>> GetRegionsWithAsync(int areaId, int buisnessUnitId, int countryId)
        {
            return await GetRegionsAsQueryable()
                .Where(r =>
                r.AreaId == areaId &&
                r.BuisnessUnitId == buisnessUnitId &&
                r.CountryId == countryId).AsNoTracking().ToListAsync();
        }
    }
}
