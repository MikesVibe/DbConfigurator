using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.Windows;

namespace DbConfigurator.UI.Services
{

    public class EditingWindowService : IEditingWindowService
    {
        public void ShowWindow(IDetailViewModel viewModel)
        {
            var window = new EditingWindow(viewModel);
            window.Show();
        }
    }
}
