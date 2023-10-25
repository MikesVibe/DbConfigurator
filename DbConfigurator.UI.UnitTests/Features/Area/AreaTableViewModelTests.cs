﻿using DbConfigurator.DataAccess.DTOs.AreaDtos;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.UnitTests.Base;
using DbConfigurator.UI.ViewModel.Base;
using System.Collections.Generic;

namespace DbConfigurator.UI.UnitTests.Features.Area
{

    public class AreaTableViewMmodelTests : TableViewModelBaseTests
        <AreaDtoWrapper,
        AreaDto,
        IAreaService,
        AreaDetailViewModel,
        CreateAreaEvent, CreateAreaEventArgs,
        EditAreaEvent, EditAreaEventArgs>
    {
        public AreaTableViewMmodelTests()
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
            IAreaService,
            CreateAreaEvent, CreateAreaEventArgs,
            EditAreaEvent, EditAreaEventArgs>
            CreateViewModel()
        {
            return new AreaTableViewModel(
                EventAggregatorMock.Object,
                EditingWindow.Object,
                DataServiceMock.Object,
                DetailVmCreator,
                new AutoMapperConfig());
        }
    }
}
