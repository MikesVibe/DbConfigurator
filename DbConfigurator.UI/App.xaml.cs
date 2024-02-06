using Autofac;
using DbConfigurator.Authentication;
using DbConfigurator.Core.Contracts;
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
using static DbConfigurator.Authentication.Role;

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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeApplicationServices();
            InitializeMainWindow();
            StartCheckingApiConnection();

            if (false)//Debugger.IsAttached)
            {
                var securitySettings = _servicesContainer.Resolve<ISecuritySettings>();
                //securitySettings.Login(new User { UserName = "Anonymous" });
                securitySettings.Login(new User { UserName = "Anonymous", UserRoles = new() { "Admin" }, FirstName = "Mikołaj", LastName = "Admin"});
                //securitySettings.Login(new User { UserName = "Anonymous", UserRoles = new() { "SecurityAnalyst" } });

                LoginIntoApplication();
            }
            else
            {
                DisplayAuthenticationWindow();

                LoginIntoApplication();
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;

            string errorMessage = string.Format("An unhandled exception occurred: {0}", ex.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void LoginIntoApplication()
        {
            var securitySettings = _servicesContainer.Resolve<ISecuritySettings>();
            if (securitySettings.IsAuthenticated)
            {
                RunApp();
            }
        }

        private void InitializeApplicationServices()
        {
            var builder = new ContainerBuilder();
            builder.AddApplicationServices();
            _servicesContainer = builder.Build();
        }

        private void StartCheckingApiConnection()
        {
            _statusService = _servicesContainer.Resolve<IStatusService>();
            _statusService.StartCheckingConnection();
        }

        private void InitializeMainWindow()
        {
            MainWindow = _servicesContainer.Resolve<MainWindow>();
            MainWindow.Hide();
        }

        private void DisplayAuthenticationWindow()
        {
            var viewModel = _servicesContainer.Resolve<AuthenticationViewModel>();
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
