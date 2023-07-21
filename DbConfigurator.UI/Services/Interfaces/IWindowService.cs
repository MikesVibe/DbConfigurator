using DbConfigurator.UI.ViewModel;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IWindowService
    {
        bool? ShowDialog(IDetailViewModel viewModel);
    }
}
