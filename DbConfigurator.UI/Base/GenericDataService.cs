using DbConfigurator.Model.Contracts;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class GenericDataService<TEntity> : IDataService<TEntity>

        where TEntity : class, new()
        
    {
        //where TCreateDto : class
        //where TUpdateDto : class
        //where TEntity : class, new() 

        //protected readonly TRepository _repository;
        //protected readonly AutoMapperConfig _autoMapper;

        //public GenericDataService(TRepository repository, AutoMapperConfig autoMapper)
        //{
        //    _repository = repository;
        //    _autoMapper = autoMapper;
        //}

        //public virtual TDto Add(TDto dto)
        //{
        //    var entity = _autoMapper.Mapper.Map<TEntity>(dto);
        //    _repository.Add(entity);
        //    _repository.SaveChanges();

        //    return _autoMapper.Mapper.Map<TDto>(_repository.GetById(entity.Id));
        //}
        //public virtual bool Update(TDto dto)
        //{
        //    var entity = _autoMapper.Mapper.Map<TEntity>(dto);
        //    _repository.Update(entity);
        //    _repository.SaveChanges();
        //    return true;
        //}
        //public virtual TDto GetById(int id)
        //{
        //    var entity = _repository.GetById(id);
        //    return _autoMapper.Mapper.Map<TDto>(entity);
        //}
        //public virtual bool RemoveById(int id)
        //{
        //    var entiy = _repository.GetById(id);
        //    if (entiy is null)
        //        return false;

        //    _repository.Remove(entiy);
        //    _repository.SaveChanges();
        //    return true;
        //}

        //public virtual async Task<TDto> GetByIdAsync(int id)
        //{
        //    var entity = await _repository.GetByIdAsync(id);
        //    return _autoMapper.Mapper.Map<TDto>(entity);
        //}

        //public virtual async Task<int> AddAsync(TDto dto)
        //{
        //    var entity = _autoMapper.Mapper.Map<TEntity>(dto);
        //    var returnedId = await _repository.AddAsync(entity);

        //    //var test = await _repository.GetByIdAsync(returnedId);
        //    //var testMapped = _autoMapper.Mapper.Map<TDto>(test);
        //    return returnedId;
        //}
        //public virtual async Task<bool> UpdateAsync(TDto dto)
        //{
        //    var entity = _autoMapper.Mapper.Map<TEntity>(dto);
        //    await _repository.UpdateAsync(entity);
        //    await _repository.SaveChangesAsync();
        //    return true;
        //}
        //public virtual async Task<bool> RemoveByIdAsync(int id)
        //{
        //    await _repository.RemoveByIdAsync(id);
        //    await _repository.SaveChangesAsync();
        //    return true;
        //}
        //public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        //{
        //    return _autoMapper.Mapper.Map<IEnumerable<TDto>>(await _repository.GetAllAsync());
        //}
        //public Task<int> AddAsync(TDto dto)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public async Task<IEnumerable<TDto>> GetAllAsync()
        //{
        //    await Task.CompletedTask;
        //    return new List<TDto>();
        //}

        //public Task<TDto> GetByIdAsync(int id)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task<bool> RemoveByIdAsync(int id)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task<bool> UpdateAsync(TDto dto)
        //{
        //    throw new System.NotImplementedException();
        //}
        public TEntity Create(TEntity createDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            await Task.CompletedTask;
            return new List<TEntity>();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public TEntity Update(TEntity createDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
