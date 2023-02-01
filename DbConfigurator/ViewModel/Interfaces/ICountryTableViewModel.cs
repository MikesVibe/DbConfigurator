using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    public interface ICountryTableViewModel : ITableViewModel
    {
        public Task LoadAsync();
    }
}