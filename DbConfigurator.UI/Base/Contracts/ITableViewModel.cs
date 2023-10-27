using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface ITableViewModel
    {
        Task LoadAsync();
        Task Refresh();
        bool HasChanges { get; }
        int Id { get; }
    }
}