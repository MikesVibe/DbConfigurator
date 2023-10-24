using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IDataService<TDto> where TDto : IEntityDto
    {
        Task<int> AddAsync(TDto dto);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<bool> RemoveByIdAsync(int id);
        Task<bool> UpdateAsync(TDto dto);
    }
}