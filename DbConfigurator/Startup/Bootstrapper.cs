using Autofac;
using DbConfigurator.DataAccess;
using DbConfigurator.UI.Data.Repositories;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
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

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<DbConfiguratorDbContext>().AsSelf();


            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<RecipientTableViewModel>().As<IRecipientTableViewModel>();
            builder.RegisterType<BuisnessUnitTableViewModel>().As<IBuisnessUnitTableViewModel>();

            
            builder.RegisterType<RecipientRepository>().AsImplementedInterfaces();
            builder.RegisterType<BuisnessRepository>().AsImplementedInterfaces();


            return builder.Build();
        }
    }
}
