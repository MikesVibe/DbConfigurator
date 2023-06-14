using DbConfigurator.UI.Startup;
using DbConfigurator.UI.Windows;
using System.Windows;
namespace DbConfigurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Bootstrap();

            var mainWindow = Bootstrapper.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
