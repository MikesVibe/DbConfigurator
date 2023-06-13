using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
