using DbConfigurator.DataAccess.DTOs.AreaDtos;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.Core.Models;
using DbConfigurator.UI.UnitTests.Base;
using DbConfigurator.UI.ViewModel.Base;
using System.Collections.Generic;

namespace DbConfigurator.UI.UnitTests.Features.AreaFeatures
{

    public class AreaTableViewMmodelTests : TableViewModelBaseTests
        <AreaWrapper,
        Area,
        IAreaService,
        AreaDetailViewModel,
        CreateAreaEvent, CreateAreaEventArgs,
        EditAreaEvent, EditAreaEventArgs>
    {
        public AreaTableViewMmodelTests()
            : base()
        {

        }

        protected override IEnumerable<AreaWrapper> CreateItemsList()
        {
            var list = new List<AreaWrapper>();
            list.Add(new AreaWrapper(CreateNewEntityDtoItem(1)));
            list.Add(new AreaWrapper(CreateNewEntityDtoItem(2)));
            return list;
        }

        protected override AreaDetailViewModel CreateNewDetailViewModel()
        {
            return new AreaDetailViewModel(
                DataServiceMock.Object,
                EventAggregatorMock.Object);
        }

        protected override Area CreateNewEntityDtoItem(int id)
        {
            return new Area() { Id = id, Name = "Americas" };
        }

        protected override TableViewModelBase<AreaWrapper, Area, IAreaService, CreateAreaEvent, CreateAreaEventArgs, EditAreaEvent, EditAreaEventArgs> CreateViewModel()
        {
            return new AreaTableViewModel(
                EventAggregatorMock.Object,
                EditingWindow.Object,
                DataServiceMock.Object,
                DetailVmCreator,
                new AutoMapperConfig(),
                SecuritySettings.Object);
        }

        //protected override TableViewModelBase
        //    <AreaWrapper,
        //    Area,
        //    IAreaService,
        //    CreateAreaEvent, CreateAreaEventArgs,
        //    EditAreaEvent, EditAreaEventArgs>
        //    CreateViewModel()
        //{
        //    return new AreaTableViewModel(
        //        EventAggregatorMock.Object,
        //        EditingWindow.Object,
        //        DataServiceMock.Object,
        //        DetailVmCreator,
        //        new AutoMapperConfig(),
        //        new Authentication.SecuritySettings());
        //}
    }
}
