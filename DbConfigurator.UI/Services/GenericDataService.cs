using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.Model;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class GenericDataService<TEntity, TDto, TRepository> : IGenericDataService<TDto>
        where TEntity : IEntity
        where TDto : IEntityDto
        where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository _repository;
        protected readonly AutoMapperConfig _autoMapper;

        public GenericDataService(TRepository repository, AutoMapperConfig autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public bool RemoveById(int id)
        {
            var entiy = _repository.GetById(id);
            if (entiy is null)
                return false;

            _repository.Remove(entiy);
            _repository.SaveChanges();
            return true;
        }
        public async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _autoMapper.Mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _autoMapper.Mapper.Map<TDto>(await _repository.GetByIdAsync(entity.Id));
        }
        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = _autoMapper.Mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
            dto = _autoMapper.Mapper.Map<TDto>(await _repository.GetByIdAsync(entity.Id));
            return dto;
        }
        public async Task<bool> RemoveByIdAsync(int id)
        {
            await _repository.RemoveByIdAsync(id);
            await _repository.SaveChangesAsync();
            return true;
        }
        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return _autoMapper.Mapper.Map<IEnumerable<TDto>>(await _repository.GetAllAsync());
        }
    }
}
