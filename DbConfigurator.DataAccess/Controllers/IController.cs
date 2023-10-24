using System.Collections.Generic;

namespace DbConfigurator.DataAccess.Controllers
{
    public interface IController<TCreateDto, TUpdateDto, TDto>
    {
        TDto Create(TCreateDto createDto);
        TDto Delete(int id);
        IEnumerable<TDto> GetAll();
        TDto GetById(int id);
        TDto Update(TUpdateDto createDto);
    }
}