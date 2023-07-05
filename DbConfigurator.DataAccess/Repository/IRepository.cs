using DbConfigurator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.Api.Services.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);
        void Delete(TEntity value);
        void Update(TEntity entity);
        void SaveChanges();
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(int id);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
    }
}
