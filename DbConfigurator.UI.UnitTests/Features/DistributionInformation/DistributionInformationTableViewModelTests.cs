using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.UnitTests.Base;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.Windows;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

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
                    BuisnessUnit = new BuisnessUnitDto
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
