using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    public interface ITableViewModel
    {
        bool HasChanges { get; }
        int Id { get; }
    }
}