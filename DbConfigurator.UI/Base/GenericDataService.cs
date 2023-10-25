using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class GenericDataService<TEntity> : IDataService<TEntity>

        where TEntity : class, new()

    {
        protected readonly AutoMapperConfig _mapper;
        protected readonly string _controllerName;
        protected readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:8443/api/")
        };

        public GenericDataService(AutoMapperConfig mapper, string controllerName)
        {
            _mapper = mapper;
            _controllerName = controllerName;
        }


        public async Task<bool> CreateAsync(TEntity createDto)
        {
            await Task.CompletedTask;
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Task.CompletedTask;
            return true;
        }

        public async Task<bool> ExistsAsync(int entityId)
        {
            await Task.CompletedTask;
            return true;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IEnumerable<TEntity> toReturn;
            try
            {
                var dto = await _httpClient.GetFromJsonAsync<IEnumerable<TEntity>>($"{_controllerName}/all");
                toReturn = _mapper.Mapper.Map<IEnumerable<TEntity>>(dto);
            }
            catch
            {
                return new List<TEntity>();
            }

            return toReturn;
        }

        //public async Task<TEntity> GetByIdAsync(int id)
        //{
        //    await Task.CompletedTask;
        //    return new TEntity();
        //}

        public async Task<bool> UpdateAsync(TEntity createDto)
        {
            await Task.CompletedTask;
            return true;
        }

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
    }
}
