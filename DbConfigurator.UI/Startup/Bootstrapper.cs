﻿using Autofac;
using DbConfigurator.Authentication;
using DbConfigurator.DataAccess;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Account.Services;
using DbConfigurator.UI.Features.Areas;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Features.BuisnessUnits.Services;
using DbConfigurator.UI.Features.BusinessUnits;
using DbConfigurator.UI.Features.Countries;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Features.DistributionInformations.Services;
using DbConfigurator.UI.Features.Panels.Creation;
using DbConfigurator.UI.Features.Panels.DistributionInformation;
using DbConfigurator.UI.Features.Panels.Navigation;
using DbConfigurator.UI.Features.Panels.Recipient;
using DbConfigurator.UI.Features.Panels.Region;
using DbConfigurator.UI.Features.Priorities.Services;
using DbConfigurator.UI.Features.Recipients;
using DbConfigurator.UI.Features.Recipients.Services;
using DbConfigurator.UI.Features.Regions;
using DbConfigurator.UI.Features.Regions.Services;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.Windows;
using Prism.Events;

namespace DbConfigurator.UI.Startup
{
    public static class ServicesBuilder
    {
        public static void AddApplicationServices(this ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<AutoMapperConfig>().AsSelf().SingleInstance();

            builder.RegisterType<Windows.MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<EditingWindow>().AsSelf();
            builder.RegisterType<SecuritySettings>().AsSelf().SingleInstance();

            builder.RegisterType<DbConfiguratorApiClient>().As<IDbConfiguratorApiClient>().SingleInstance();

            //Services
            builder.RegisterType<EditingWindowService>().As<IEditingWindowService>().SingleInstance();
            
            builder.RegisterType<DistributionInformationService>().As<IDistributionInformationService>().SingleInstance();
            builder.RegisterType<RegionService>().As<IRegionService>().SingleInstance();
            builder.RegisterType<AreaService>().As<IAreaService>().SingleInstance();
            builder.RegisterType<BusinessUnitService>().As<IBusinessUnitService>().SingleInstance();
            builder.RegisterType<CountryService>().As<ICountryService>().SingleInstance();
            builder.RegisterType<RecipientService>().As<IRecipientService>().SingleInstance();
            builder.RegisterType<PriorityService>().As<IPriorityService>().SingleInstance();
            builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            builder.RegisterType<StatusService>().As<IStatusService>().SingleInstance();

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
            builder.RegisterType<BusinessUnitTableViewModel>()
                 .Keyed<ITableViewModel>(nameof(BusinessUnitTableViewModel));
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
            builder.RegisterType<BusinessUnitDetailViewModel>()
                 .Keyed<IDetailViewModel>(nameof(BusinessUnitDetailViewModel));
            builder.RegisterType<CountryDetailViewModel>()
                 .Keyed<IDetailViewModel>(nameof(CountryDetailViewModel));
            builder.RegisterType<RecipientDetailViewModel>()
                 .Keyed<IDetailViewModel>(nameof(RecipientDetailViewModel));

            //Detail View Models
            builder.RegisterType<DistributionInformationDetailViewModel>().AsSelf();
            builder.RegisterType<RecipientDetailViewModel>().AsSelf();
            builder.RegisterType<RegionDetailViewModel>().AsSelf();
            builder.RegisterType<AreaDetailViewModel>().AsSelf();
            builder.RegisterType<BusinessUnitDetailViewModel>().AsSelf();
            builder.RegisterType<CountryDetailViewModel>().AsSelf();



        }
        public static IContainer Build(this ContainerBuilder builder)
        {
            return builder.Build();
        }
    }
}
