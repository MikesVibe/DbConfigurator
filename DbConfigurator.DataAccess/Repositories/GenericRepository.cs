using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.Model;
using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repository
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
        public virtual void Update(T entity)
        {
            var existing = _context.Set<T>().Find(entity.Id);
            if (existing is null)
                return;

            _context.Entry(existing).CurrentValues.SetValues(entity);
        }
        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public virtual bool RemoveById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity is null)
                return false;

            _context.Set<T>().Remove(entity);
            return true;
        }
        public virtual T? GetById(int id)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
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
        public virtual async Task UpdateAsync(T entity)
        {
            var existing = await _context.Set<T>().FindAsync(entity.Id);
            if (existing is null)
                return;

            _context.Entry(existing).CurrentValues.SetValues(entity);
        }
        public virtual async Task<bool> RemoveByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is null)
                return false;

            _context.Set<T>().Remove(entity);
            return true;
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
