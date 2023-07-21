using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.DTOs.Core;

namespace DbConfigurator.UI.Contracts
{
    public interface IEventArgs<T>
        where T : IEntityDto
    {
        T Entity { get; set; }
    }
}