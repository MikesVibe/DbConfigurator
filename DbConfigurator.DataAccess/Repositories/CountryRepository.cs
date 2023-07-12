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
    public class CountryRepository : GenericRepository<Country>
    {
        public CountryRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }

        public override IEnumerable<Country> GetAll()
        {
            return GetCountriesAsQueryable().AsNoTracking().ToList();
        }
        public override async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await GetCountriesAsQueryable().AsNoTracking().ToListAsync();
        }
        public override async Task<IEnumerable<Country>> GetAllAsync(Expression<Func<Country, bool>> predicate)
        {
            return await GetCountriesAsQueryable().OrderBy(r => r.Id).Where(predicate).AsNoTracking().ToListAsync();
        }

        protected IQueryable<Country> GetCountriesAsQueryable()
        {
            return _context.Set<Country>()
                .AsQueryable();
        }
    }
}
