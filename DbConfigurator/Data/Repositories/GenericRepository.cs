using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        public GenericRepository(TContext context)
        {
            this.Context = context;
        }

        public void Add(TEntity model)
        {
            Context.Set<TEntity>().Add(model);
        }
        public virtual async Task<TEntity> GetByIDAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }
        public void Remove(TEntity model)
        {
            Context.Set<TEntity>().Remove(model);
        }
        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }


        protected TContext Context { get; }
    }
}
