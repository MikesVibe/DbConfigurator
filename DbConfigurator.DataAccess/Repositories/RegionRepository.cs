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

        public override async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await GetRegionsAsQueryable().OrderBy(r => r.Id).AsNoTracking().ToListAsync();
        }
    }
}
