using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.UnitTests.Base;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Base;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DbConfigurator.UI.UnitTests.Features.DistributionInformation
{
    
    public class AreaTableViewModelTests : TableViewModelBaseTests
        <AreaDtoWrapper,
        AreaDto,
        IAreaService,
        AreaDetailViewModel>
    {

        public AreaTableViewModelTests()
            : base()
        {
        }

        protected override Func<AreaDetailViewModel> CreateNewDetailViewModel()
        {
            return () => new AreaDetailViewModel(
                dataServiceMock.Object,
                _eventAggregatorMock.Object);
        }

        protected override AreaDto CreateNewEntityDtoItem()
        {
            return new AreaDto();
        }

        protected override TableViewModelBase
            <AreaDtoWrapper,
            AreaDto,
            IAreaService>
            CreateViewModel()
        {
            _eventAggregatorMock.Setup(ea => ea.GetEvent<EditAreaEvent>())
                .Returns(new EditAreaEvent());
            _eventAggregatorMock.Setup(ea => ea.GetEvent<CreateAreaEvent>())
                .Returns(new CreateAreaEvent());
            dataServiceMock = new Mock<IAreaService>();

            return new AreaTableViewModel(
                _eventAggregatorMock.Object,
                editingWindow.Object,
                dataServiceMock.Object,
                detailVmCreator,
                new AutoMapperConfig());
        }
    }
}
