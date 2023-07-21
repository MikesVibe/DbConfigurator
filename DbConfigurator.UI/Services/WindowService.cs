using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.Windows;

namespace DbConfigurator.UI.Services
{

    public class WindowService : IWindowService
    {
        public bool? ShowDialog(IDetailViewModel viewModel)
        {
            var window = new EditingWindow(viewModel);
            return window.ShowDialog();
        }
    }
}
