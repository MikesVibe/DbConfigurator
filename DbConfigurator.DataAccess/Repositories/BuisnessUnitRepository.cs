using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repositories
{
    public class BuisnessUnitRepository : GenericRepository<BuisnessUnit>
    {
        public BuisnessUnitRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }

        public override IEnumerable<BuisnessUnit> GetAll()
        {
            return GetBuisnessUnitsAsQueryable().AsNoTracking().ToList();
        }
        public override async Task<IEnumerable<BuisnessUnit>> GetAllAsync()
        {
            return await GetBuisnessUnitsAsQueryable().AsNoTracking().ToListAsync();
        }
        public override async Task<IEnumerable<BuisnessUnit>> GetAllAsync(Expression<Func<BuisnessUnit, bool>> predicate)
        {
            return await GetBuisnessUnitsAsQueryable().OrderBy(r => r.Id).Where(predicate).AsNoTracking().ToListAsync();
        }

        protected IQueryable<BuisnessUnit> GetBuisnessUnitsAsQueryable()
        {
            return _context.Set<BuisnessUnit>()
                .AsQueryable();
        }
    }
}
