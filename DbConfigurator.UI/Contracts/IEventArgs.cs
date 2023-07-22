using DbConfigurator.Model.Contracts;

namespace DbConfigurator.UI.Contracts
{
    public interface IEventArgs<T>
        where T : IEntityDto
    {
        T Entity { get; set; }
    }
}