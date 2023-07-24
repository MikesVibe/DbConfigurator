﻿using DbConfigurator.Model.DTOs.Core;
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

namespace DbConfigurator.UI.UnitTests.Features.Area
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

        protected override IEnumerable<AreaDtoWrapper> CreateItemsList()
        {
            var list = new List<AreaDtoWrapper>();
            list.Add(new AreaDtoWrapper(CreateNewEntityDtoItem(1)));
            list.Add(new AreaDtoWrapper(CreateNewEntityDtoItem(2)));
            return list;
        }

        protected override AreaDetailViewModel CreateNewDetailViewModel()
        {
            return new AreaDetailViewModel(
                DataServiceMock.Object,
                EventAggregatorMock.Object);
        }

        protected override AreaDto CreateNewEntityDtoItem(int id)
        {
            return new AreaDto() { Id = id, Name = "Americas" };
        }

        protected override TableViewModelBase
            <AreaDtoWrapper,
            AreaDto,
            IAreaService>
            CreateViewModel()
        {
            EventAggregatorMock.Setup(ea => ea.GetEvent<EditAreaEvent>())
                .Returns(new EditAreaEvent());
            EventAggregatorMock.Setup(ea => ea.GetEvent<CreateAreaEvent>())
                .Returns(new CreateAreaEvent());
            DataServiceMock = new Mock<IAreaService>();

            return new AreaTableViewModel(
                EventAggregatorMock.Object,
                EditingWindow.Object,
                DataServiceMock.Object,
                DetailVmCreator,
                new AutoMapperConfig());
        }
    }
}