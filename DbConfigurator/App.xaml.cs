using Autofac;
using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI;
using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
