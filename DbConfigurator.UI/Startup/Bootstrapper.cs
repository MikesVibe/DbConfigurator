using Autofac;
using Autofac.Core;
using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Navigation;
using DbConfigurator.UI.Windows;
using Prism.Events;

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
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<AutoMapperConfig>().AsSelf().SingleInstance();

            builder.RegisterType<Windows.MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<EditingWindow>().AsSelf();

            builder.RegisterType<DbConfiguratorDbContext>().InstancePerDependency();

            //View Models
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<RecipientTableViewModel>()
                .Keyed<ITableViewModel>(nameof(RecipientTableViewModel));
            builder.RegisterType<DistributionInformationTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(DistributionInformationTableViewModel));
            builder.RegisterType<RegionTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(RegionTableViewModel));
            builder.RegisterType<CreationTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(CreationTableViewModel));

            builder.RegisterType<AddAreaViewModel>().As<IEditingViewModel>();



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
