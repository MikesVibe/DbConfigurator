using Autofac;
using DbConfigurator.Authentication;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Account.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.Windows;
using DbConfigurator.UI.Windows.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
namespace DbConfigurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;

            string errorMessage = string.Format("An unhandled exception occurred: {0}", ex.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.AddApplicationServices();
            var app = builder.Build();
            MainWindow = app.Resolve<MainWindow>();

            if (false)//Debugger.IsAttached)
            {
                RunApp();
            }
            else
            {
                MainWindow.Hide();


                var accountService = app.Resolve<IAccountService>();
                var securitySettings = app.Resolve<SecuritySettings>();
                var statusService = app.Resolve<IStatusService>();
                var viewModel = new AuthenticationViewModel(accountService, statusService, securitySettings);
                var loginWindow = new AuthenticationView(viewModel);
                viewModel.Window = loginWindow;
                loginWindow.ShowDialog();

                if (securitySettings.IsAuthenticated)
                {
                    RunApp();
                    return;
                }

                MainWindow.Close();
            }
        }

        private void RunApp()
        {
            MainWindow.Show();
        }
    }
}
