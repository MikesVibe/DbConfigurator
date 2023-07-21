using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    public interface ITableViewModel
    {
        Task LoadAsync();

        bool HasChanges { get; }
        int Id { get; }
    }
}