using DbConfigurator.Core.Models;
using DbConfigurator.DataAccess.DTOs.DistributionInformationDtos;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Features.DistributionInformations.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.UnitTests.Base;
using DbConfigurator.UI.ViewModel.Base;
using System.Collections.Generic;

namespace DbConfigurator.UI.UnitTests.Features.DistributionInformationFeatures
{
    public class DistributionInformationTableViewModelTests : TableViewModelBaseTests<DistributionInformationWrapper,
        DistributionInformation,
        IDistributionInformationService,
        DistributionInformationDetailViewModel,
        CreateDistributionInformationEvent, CreateDistributionInformationEventArgs,
        EditDistributionInformationEvent, EditDistributionInformationEventArgs>
    {

        public DistributionInformationTableViewModelTests()
        : base()
        {
        }

        protected override IEnumerable<DistributionInformationWrapper> CreateItemsList()
        {
            var list = new List<DistributionInformationWrapper>();
            list.Add(new DistributionInformationWrapper(CreateNewEntityDtoItem(1)));
            list.Add(new DistributionInformationWrapper(CreateNewEntityDtoItem(2)));
            return list;
        }

        protected override DistributionInformationDetailViewModel CreateNewDetailViewModel()
        {
            return new DistributionInformationDetailViewModel(
                DataServiceMock.Object,
                EventAggregatorMock.Object);
        }

        protected override DistributionInformation CreateNewEntityDtoItem(int id)
        {
            return new DistributionInformation
            {
                Id = id,
                Region = new Region
                {
                    Id = 1,
                    Area = new Area
                    {
                        Id = 1,
                        Name = "Americas"
                    },
                    BusinessUnit = new BusinessUnit
                    {
                        Id = 1,
                        Name = "NAO"
                    },
                    Country = new Country
                    {
                        Id = 1,
                        CountryName = "Canada",
                        CountryCode = "CA"
                    }
                },
                Priority = new Priority
                {
                    Id = 1,
                    Name = "P1"
                }
            };
        }



        protected override TableViewModelBase
            <DistributionInformationWrapper,
            DistributionInformation,
            IDistributionInformationService,
            CreateDistributionInformationEvent, CreateDistributionInformationEventArgs,
            EditDistributionInformationEvent, EditDistributionInformationEventArgs>
            CreateViewModel()
        {
            var vm = new DistributionInformationTableViewModel(
                 EditingWindow.Object,
                 EventAggregatorMock.Object,
                 DataServiceMock.Object,
                 DetailVmCreator,
                 new AutoMapperConfig(),
                 new Authentication.SecuritySettings());
            return vm;
        }


    }
}
