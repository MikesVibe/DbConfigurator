using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.Windows;

namespace DbConfigurator.UI.Services
{

    public class DialogService : IDialogService
    {
        public bool? ShowDialog(IEditingViewModel viewModel)
        {
            var window = new EditingWindow(viewModel);
            return window.ShowDialog();
        }
    }
}
