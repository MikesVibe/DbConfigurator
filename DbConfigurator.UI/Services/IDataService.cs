using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IDataService<TCreateDto, TUpdateDto, TEntity>
        where TCreateDto : class
        where TUpdateDto : class
        where TEntity : class, new()
    {
        //Task<int> AddAsync(TDto dto);
        //Task<IEnumerable<TDto>> GetAllAsync();
        //Task<TDto> GetByIdAsync(int id);
        //Task<bool> RemoveByIdAsync(int id);
        //Task<bool> UpdateAsync(TDto dto);

        TEntity Create(TCreateDto createDto);
        TEntity Update(TUpdateDto createDto);
        Task<TEntity> DeleteAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}