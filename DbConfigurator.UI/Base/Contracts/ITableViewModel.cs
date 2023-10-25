using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface ITableViewModel
    {
        Task LoadAsync();

        bool HasChanges { get; }
        int Id { get; }
    }
}