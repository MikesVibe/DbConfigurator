using DbConfigurator.Api.Services.Repositories;
using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbConfigurator.API.DataAccess.Repository
{
    public class GenericRepository<T> : IRepository<T>
        where T : class, IEntity
        
    {
        protected readonly DbConfiguratorDbContext _context;

        public GenericRepository(DbConfiguratorDbContext dbContext)
        {
            _context = dbContext;
        }

        //IRepository
        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }
        public virtual T? GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(e => e.Id == id);
        }
        public virtual void Update(T entity)
        {
            var existing = _context.Set<T>().Find(entity.Id);
            if (existing is null)
                return;
            
            _context.Entry(existing).CurrentValues.SetValues(entity);
        }
        public virtual async Task UpdateAsync(T entity)
        {
            var existing = await _context.Set<T>().FindAsync(entity.Id);
            if (existing is null)
                return;

            _context.Entry(existing).CurrentValues.SetValues(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        protected IQueryable<Region> GetRegionsAsQueryable()
        {
            return _context.Set<Region>()
                .Include(r => r.Area)
                .Include(r => r.BuisnessUnit)
                .Include(r => r.Country)
                .AsQueryable();
        }
        protected IQueryable<BuisnessUnit> GetBuisnessUnitsAsQueryable()
        {
            return _context.Set<BuisnessUnit>()
                .AsQueryable();
        }
        protected IQueryable<Country> GetCountriesAsQueryable()
        {
            return _context.Set<Country>()
                .AsQueryable();
        }
    }
}
