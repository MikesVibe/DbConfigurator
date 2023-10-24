using DbConfigurator.Model.Contracts;

namespace DbConfigurator.UI.Contracts
{
    public interface IEventArgs<T>
        where T : IEntity
    {
        T Entity { get; set; }
    }
}