using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.Model.Contracts;
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

        public virtual TDto Add(TDto dto)
        {
            var entity = _autoMapper.Mapper.Map<TEntity>(dto);
            _repository.Add(entity);
            _repository.SaveChanges();

            return _autoMapper.Mapper.Map<TDto>(_repository.GetById(entity.Id));
        }
        public virtual bool Update(TDto dto)
        {
            var entity = _autoMapper.Mapper.Map<TEntity>(dto);
            _repository.Update(entity);
            _repository.SaveChanges();
            return true;
        }
        public virtual TDto GetById(int id)
        {
            var entity = _repository.GetById(id);
            return _autoMapper.Mapper.Map<TDto>(entity);
        }
        public virtual bool RemoveById(int id)
        {
            var entiy = _repository.GetById(id);
            if (entiy is null)
                return false;

            _repository.Remove(entiy);
            _repository.SaveChanges();
            return true;
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _autoMapper.Mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _autoMapper.Mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _autoMapper.Mapper.Map<TDto>(await _repository.GetByIdAsync(entity.Id));
        }
        public virtual async Task<bool> UpdateAsync(TDto dto)
        {
            var entity = _autoMapper.Mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
        public virtual async Task<bool> RemoveByIdAsync(int id)
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
