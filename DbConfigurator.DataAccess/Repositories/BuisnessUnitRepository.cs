using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repositories
{
    public class BusinessUnitRepository : GenericRepository<BusinessUnit>
    {
        public BusinessUnitRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }

        public override IEnumerable<BusinessUnit> GetAll()
        {
            return GetBusinessUnitsAsQueryable().AsNoTracking().ToList();
        }
        public override async Task<IEnumerable<BusinessUnit>> GetAllAsync()
        {
            return await GetBusinessUnitsAsQueryable().AsNoTracking().ToListAsync();
        }
        public override async Task<IEnumerable<BusinessUnit>> GetAllAsync(Expression<Func<BusinessUnit, bool>> predicate)
        {
            return await GetBusinessUnitsAsQueryable().OrderBy(r => r.Id).Where(predicate).AsNoTracking().ToListAsync();
        }

        protected IQueryable<BusinessUnit> GetBusinessUnitsAsQueryable()
        {
            return _context.Set<BusinessUnit>()
                .AsQueryable();
        }
    }
}
