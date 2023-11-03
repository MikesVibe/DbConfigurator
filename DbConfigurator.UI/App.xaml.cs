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
using System.Threading.Tasks;
using System.Windows;
namespace DbConfigurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IStatusService _statusService;
        private IContainer _servicesContainer;

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
            _servicesContainer = builder.Build();

            try
            {
                _statusService = _servicesContainer.Resolve<IStatusService>();
                _statusService.StartCheckingConnection();
                //Task.Run(async () => await );

            }
            catch
            {

            }

            MainWindow = _servicesContainer.Resolve<MainWindow>();
            if (MainWindow.DataContext as MainWindowViewModel is not null)
            {
                var mainWindowViewModel = (MainWindowViewModel)MainWindow.DataContext;
                _statusService.StatusChanged += mainWindowViewModel.StatusChanged!;
            }

            if (false)//Debugger.IsAttached)
            {
                RunApp();
            }
            else
            {
                MainWindow.Hide();

                DisplayAuthenticationWindow();

                var securitySettings = _servicesContainer.Resolve<SecuritySettings>();
                if (securitySettings.IsAuthenticated)
                {
                    RunApp();
                    return;
                }

                MainWindow.Close();
            }
        }

        private void DisplayAuthenticationWindow()
        {
            var viewModel = _servicesContainer.Resolve<AuthenticationViewModel>();
            _statusService.StatusChanged += viewModel.StatusChanged!;
            var loginWindow = new AuthenticationView(viewModel);
            viewModel.Window = loginWindow;
            loginWindow.ShowDialog();
        }

        private void RunApp()
        {
            MainWindow.Show();

        }
    }
}
