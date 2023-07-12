using DbConfigurator.UI.ViewModel;
using System.Windows;

namespace DbConfigurator.UI.Windows
{
    /// <summary>
    /// Interaction logic for EditingWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {
        public EditingWindow(IDetailViewModel editingViewModel)
        {
            InitializeComponent();
            DataContext = editingViewModel;

            editingViewModel.CloseAction = new(CloseWindow);
        }

        private void CloseWindow(bool dialogResult)
        {
            this.DialogResult = dialogResult;
            this.Close();
        }
    }
}
