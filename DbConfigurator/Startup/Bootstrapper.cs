using Autofac;
using DbConfigurator.DataAccess;
using DbConfigurator.UI.Data.Repositories;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<DbConfiguratorDbContext>().AsSelf();


            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<RecipientDetailViewModel>().As<IRecipientDetailViewModel>();

            
            builder.RegisterType<RecipientRepository>().AsImplementedInterfaces();


            return builder.Build();
        }
    }
}
