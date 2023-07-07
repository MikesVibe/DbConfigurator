using Autofac;
using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.DataAccess.Repository;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Panel;
using DbConfigurator.UI.ViewModel.Tables;
using DbConfigurator.UI.Windows;
using Prism.Events;

namespace DbConfigurator.UI.Startup
{
    public static class ServicesBuilder
    {
        public static void AddApplicationServices(this ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<Seeder>().As<ISeeder>().SingleInstance();
            builder.RegisterType<AutoMapperConfig>().AsSelf().SingleInstance();

            builder.RegisterType<Windows.MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<EditingWindow>().AsSelf();

            builder.RegisterType<DbConfiguratorDbContext>().InstancePerDependency();

            //Repositories
            builder.RegisterType<AreaRepository>().AsSelf();
            builder.RegisterType<BuisnessUnitRepository>().AsSelf();
            builder.RegisterType<CountryRepository>().AsSelf();
            builder.RegisterType<RegionRepository>().AsSelf();
            builder.RegisterType<PriorityRepository>().AsSelf();
            builder.RegisterType<RecipientRepository>().AsSelf();
            builder.RegisterType<DistributionInformationRepository>().AsSelf();

            //Services
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<CombinedDataService>().As<ICombinedDataService>().SingleInstance();
            builder.RegisterType<DistributionInformationService>().As<IDistributionInformationService>().SingleInstance();
            builder.RegisterType<RegionService>().As<IRegionService>().SingleInstance();
            builder.RegisterType<AreaService>().As<IAreaService>().SingleInstance();
            //builder.RegisterType<BuisnessUnitService>().As<IBuisnessUnitService>().SingleInstance();
            //builder.RegisterType<CountryService>().As<IRegionService>().SingleInstance();

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
            builder.RegisterType<RegionTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(RegionTableViewModel));

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

        }
        public static IContainer Build(this ContainerBuilder builder)
        {
            return builder.Build();
        }
    }
}
