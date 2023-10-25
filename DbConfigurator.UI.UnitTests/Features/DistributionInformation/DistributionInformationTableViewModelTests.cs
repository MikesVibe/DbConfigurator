using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.UnitTests.Base;
using DbConfigurator.UI.ViewModel.Base;
using System.Collections.Generic;

namespace DbConfigurator.UI.UnitTests.Features.DistributionInformation
{
    public class DistributionInformationTableViewModelTests : TableViewModelBaseTests<DistributionInformationDtoWrapper,
        DistributionInformationDto,
        IDistributionInformationService,
        DistributionInformationDetailViewModel,
        CreateDistributionInformationEvent, CreateDistributionInformationEventArgs,
        EditDistributionInformationEvent, EditDistributionInformationEventArgs>
    {

        public DistributionInformationTableViewModelTests()
        : base()
        {
        }

        protected override IEnumerable<DistributionInformationDtoWrapper> CreateItemsList()
        {
            var list = new List<DistributionInformationDtoWrapper>();
            list.Add(new DistributionInformationDtoWrapper(CreateNewEntityDtoItem(1)));
            list.Add(new DistributionInformationDtoWrapper(CreateNewEntityDtoItem(2)));
            return list;
        }

        protected override DistributionInformationDetailViewModel CreateNewDetailViewModel()
        {
            return new DistributionInformationDetailViewModel(
                DataServiceMock.Object,
                EventAggregatorMock.Object);
        }

        protected override DistributionInformationDto CreateNewEntityDtoItem(int id)
        {
            return new DistributionInformationDto
            {
                Id = id,
                Region = new RegionDto
                {
                    Id = 1,
                    Area = new AreaDto
                    {
                        Id = 1,
                        Name = "Americas"
                    },
                    BusinessUnit = new BusinessUnitDto
                    {
                        Id = 1,
                        Name = "NAO"
                    },
                    Country = new CountryDto
                    {
                        Id = 1,
                        CountryName = "Canada",
                        CountryCode = "CA"
                    }
                },
                Priority = new PriorityDto
                {
                    Id = 1,
                    Name = "P1"
                }
            };
        }



        protected override TableViewModelBase
            <DistributionInformationDtoWrapper,
            DistributionInformationDto,
            IDistributionInformationService,
            CreateDistributionInformationEvent, CreateDistributionInformationEventArgs,
            EditDistributionInformationEvent, EditDistributionInformationEventArgs>
            CreateViewModel()
        {
            return new DistributionInformationTableViewModel(
                 EditingWindow.Object,
                 EventAggregatorMock.Object,
                 DataServiceMock.Object,
                 DetailVmCreator,
                 new AutoMapperConfig());
        }


    }
}
