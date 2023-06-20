using Autofac;
using Autofac.Core;
using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.Data;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Panel;
using DbConfigurator.UI.ViewModel.Tables;
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
            builder.RegisterType<RegionsRepository>().AsSelf();

            builder.RegisterType<DbConfiguratorDbContext>().InstancePerDependency();

            //Navigation Panel
            builder.RegisterType<NavigationPanelViewModel>().As<INavigationPanelViewModel>();

            //Main Panels
            builder.RegisterType<RecipientPanelViewModel>()
                .Keyed<IMainPanelViewModel>(nameof(RecipientPanelViewModel));
            builder.RegisterType<DistributionInformationPanelViewModel>()
                 .Keyed<IMainPanelViewModel>(nameof(DistributionInformationPanelViewModel));
            builder.RegisterType<RegionPanelViewModel>()
                 .Keyed<IMainPanelViewModel>(nameof(RegionPanelViewModel));
            builder.RegisterType<CreationPanelViewModel>()
                 .Keyed<IMainPanelViewModel>(nameof(CreationPanelViewModel));

            //Table View Models
            builder.RegisterType<AreaTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(AreaTableViewModel));
            builder.RegisterType<BuisnessUnitTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(BuisnessUnitTableViewModel));
            builder.RegisterType<CountryTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(CountryTableViewModel));
            builder.RegisterType<DistributionInformationTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(DistributionInformationTableViewModel));
            builder.RegisterType<RecipientTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(RecipientTableViewModel));

            //Editing View Models
            builder.RegisterType<AddAreaViewModel>()
                 .Keyed<IEditingViewModel>(nameof(AddAreaViewModel));
            builder.RegisterType<AddBuisnessUnitViewModel>()
                 .Keyed<IEditingViewModel>(nameof(AddBuisnessUnitViewModel));
            builder.RegisterType<AddCountryViewModel>()
                 .Keyed<IEditingViewModel>(nameof(AddCountryViewModel));
            builder.RegisterType<AddRecipientViewModel>()
                 .Keyed<IEditingViewModel>(nameof(AddRecipientViewModel));


            builder.RegisterType<AddAreaViewModel>().As<IEditingViewModel>();
            builder.RegisterType<AddDistibutionInformationViewModel>().AsSelf();
            builder.RegisterType<AddRecipientViewModel>().AsSelf();
            builder.RegisterType<AddRegionViewModel>().AsSelf();



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
