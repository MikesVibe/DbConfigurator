using Autofac;
using Autofac.Core;
using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Startup
{
    public class Bootstrapper
    {
        public static IContainer Container 
        {
            get { return _container; }
            set { _container = value; }
        }
        private static IContainer _container;
        public void Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<DataModel>().As<IDataModel>().SingleInstance();
            builder.RegisterType<Seeder>().As<ISeeder>().SingleInstance();
            builder.RegisterType<AutoMapperConfig>().AsSelf().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<DbConfiguratorDbContext>().InstancePerDependency();

            //View Models
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<RecipientTableViewModel>()
                .Keyed<ITabelViewModel>(nameof(RecipientTableViewModel));
            builder.RegisterType<CountryTableViewModel>()
                 .Keyed<ITabelViewModel>(nameof(CountryTableViewModel));
            builder.RegisterType<DistributionInformationTableViewModel>()
                 .Keyed<ITabelViewModel>(nameof(DistributionInformationTableViewModel));
            builder.RegisterType<CountryTableViewModel>()
                 .Keyed<ITabelViewModel>(nameof(RegionCreatorViewModel));




            Container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>(new Parameter[0]);
        }
        public static T Resolve<T>(Parameter[] parameters)
        {
            return Container.Resolve<T>(new Parameter[0]);
        }
    }
}
