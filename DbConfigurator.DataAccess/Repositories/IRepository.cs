using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);
        void Remove(TEntity value);
        void Update(TEntity entity);
        void SaveChanges();
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(int id);

        Task<int> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        bool RemoveById(int id);
        Task<bool> RemoveByIdAsync(int id);
    }
}
