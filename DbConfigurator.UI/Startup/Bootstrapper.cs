using Autofac;
using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.DataAccess.Repository;
using DbConfigurator.UI.Features.Areas;
using DbConfigurator.UI.Features.BuisnessUnits;
using DbConfigurator.UI.Features.Countries;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Features.Panel;
using DbConfigurator.UI.Features.Recipients;
using DbConfigurator.UI.Features.Regions;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Interfaces;
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
            builder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();
            builder.RegisterType<DistributionInformationService>().As<IDistributionInformationService>().SingleInstance();
            builder.RegisterType<RegionService>().As<IRegionService>().SingleInstance();
            builder.RegisterType<AreaService>().As<IAreaService>().SingleInstance();
            builder.RegisterType<BuisnessUnitService>().As<IBuisnessUnitService>().SingleInstance();
            builder.RegisterType<CountryService>().As<ICountryService>().SingleInstance();
            builder.RegisterType<RecipientService>().As<IRecipientService>().SingleInstance();

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
            builder.RegisterType<AreaDetailViewModel>()
                 .Keyed<IDetailViewModel>(nameof(AreaDetailViewModel));
            builder.RegisterType<BuisnessUnitDetailViewModel>()
                 .Keyed<IDetailViewModel>(nameof(BuisnessUnitDetailViewModel));
            builder.RegisterType<CountryDetailViewModel>()
                 .Keyed<IDetailViewModel>(nameof(CountryDetailViewModel));
            builder.RegisterType<RecipientDetailViewModel>()
                 .Keyed<IDetailViewModel>(nameof(RecipientDetailViewModel));


            builder.RegisterType<DistibutionInformationDetailViewModel>().AsSelf();
            builder.RegisterType<RecipientDetailViewModel>().AsSelf();
            builder.RegisterType<RegionDetailViewModel>().AsSelf();
            builder.RegisterType<AreaDetailViewModel>().AsSelf();
            builder.RegisterType<BuisnessUnitDetailViewModel>().AsSelf();
            builder.RegisterType<CountryDetailViewModel>().AsSelf();

        }
        public static IContainer Build(this ContainerBuilder builder)
        {
            return builder.Build();
        }
    }
}
