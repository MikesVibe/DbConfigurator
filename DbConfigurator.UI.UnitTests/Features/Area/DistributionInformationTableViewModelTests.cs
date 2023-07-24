using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
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
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.UnitTests.Features.Area
{
    public class DistributionInformationTableViewModelTests : TableViewModelBaseTests
        <DistributionInformationDtoWrapper,
        DistributionInformationDto,
        IDistributionInformationService,
        DistributionInformationDetailViewModel>
    {

        public DistributionInformationTableViewModelTests()
            : base()
        {
        }

        protected override Func<DistributionInformationDetailViewModel> CreateNewDetailViewModel()
        {
            return () => new DistributionInformationDetailViewModel(
                dataServiceMock.Object,
                _eventAggregatorMock.Object);
        }

        protected override DistributionInformationDto CreateNewEntityDtoItem()
        {
            return new DistributionInformationDto
            {
                Id = 1,
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
            IDistributionInformationService>
            CreateViewModel()
        {
            _eventAggregatorMock.Setup(ea => ea.GetEvent<EditDistributionInformationEvent>())
                .Returns(new EditDistributionInformationEvent());
            _eventAggregatorMock.Setup(ea => ea.GetEvent<CreateDistributionInformationEvent>())
                .Returns(new CreateDistributionInformationEvent());
            dataServiceMock = new Mock<IDistributionInformationService>();

            return new DistributionInformationTableViewModel(
                editingWindow.Object,
                _eventAggregatorMock.Object,
                dataServiceMock.Object,
                detailVmCreator,
                new AutoMapperConfig());
        }
    }
}
