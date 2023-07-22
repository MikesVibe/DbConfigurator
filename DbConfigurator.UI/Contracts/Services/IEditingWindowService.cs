using DbConfigurator.UI.ViewModel;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IEditingWindowService
    {
        void ShowWindow(IDetailViewModel viewModel);
    }
}
