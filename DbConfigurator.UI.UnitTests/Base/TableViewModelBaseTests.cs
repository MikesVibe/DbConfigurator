using DbConfigurator.Model.Contracts;
using DbConfigurator.UI.Contracts;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Base;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DbConfigurator.UI.UnitTests.Base
{
    public abstract class TableViewModelBaseTests<TWrapper, TDto, TDataService, TDetailVm,
        TCreateEvent, TCreateEventArgs,
        TEditEvent, TEditEventArgs>
        where TWrapper : IWrapperWithId
        where TDto : class, IEntityDto
        where TDataService : class, IDataService<TDto>
        where TDetailVm : IDetailViewModel
        where TCreateEvent : PubSubEvent<TCreateEventArgs>, new()
        where TCreateEventArgs : IEventArgs<TDto>, new()
        where TEditEvent : PubSubEvent<TEditEventArgs>, new()
        where TEditEventArgs : IEventArgs<TDto>, new()
    {
        protected Mock<IEventAggregator> EventAggregatorMock;

        protected Mock<IEditingWindowService> EditingWindow;
        protected Mock<TDataService> DataServiceMock;
        protected Func<TDetailVm> DetailVmCreator;
        protected TableViewModelBase<TWrapper, TDto, TDataService,
        TCreateEvent, TCreateEventArgs,
        TEditEvent, TEditEventArgs> ViewModel;
        private TCreateEvent _createItemEvent;
        private TEditEvent _editItemEvent;

        public TableViewModelBaseTests()
        {
            EventAggregatorMock = new Mock<IEventAggregator>();
            _createItemEvent = new TCreateEvent();
            _editItemEvent = new TEditEvent();
            EventAggregatorMock.Setup(ea => ea.GetEvent<TCreateEvent>())
                .Returns(_createItemEvent);
            EventAggregatorMock.Setup(ea => ea.GetEvent<TEditEvent>())
                .Returns(_editItemEvent);

            EditingWindow = new Mock<IEditingWindowService>();
            DataServiceMock = new Mock<TDataService>();

            DetailVmCreator = CreateNewDetailViewModel;
            ViewModel = CreateViewModel();
        }


        protected abstract TableViewModelBase<TWrapper, TDto, TDataService, TCreateEvent, TCreateEventArgs,
        TEditEvent, TEditEventArgs> CreateViewModel();
        protected abstract TDetailVm CreateNewDetailViewModel();
        protected abstract TDto CreateNewEntityDtoItem(int id);
        protected abstract IEnumerable<TWrapper> CreateItemsList();

        [Fact]
        public async Task ShouldLoadItemsOnLoadAsyncMethod()
        {
            // Arrange
            var testData = new List<TDto>();
            testData.Add(CreateNewEntityDtoItem(1));
            testData.Add(CreateNewEntityDtoItem(2));
            testData.Add(CreateNewEntityDtoItem(3));

            DataServiceMock.Setup(ds => ds.GetAllAsync()).ReturnsAsync(testData);

            // Act
            await ViewModel.LoadAsync();

            // Assert
            // Check if the Items collection was populated with the test data
            Assert.Equal(testData.Count, ViewModel.Items.Count);

            foreach (var item in testData)
            {
                var wrappedItem = ViewModel.Items.FirstOrDefault(w => w.Id == item.Id);
                Assert.NotNull(wrappedItem);
                // Perform additional assertions if needed.
            }
        }


        [Fact]
        public void ShouldOpenEditingWindowAfterPressingAddButton()
        {
            ViewModel.Items = new ObservableCollection<TWrapper>(CreateItemsList());
            ViewModel.SelectedItem = ViewModel.Items.First();

            //ViewModel.SelectedItem = new TWrapper();
            ViewModel.EditCommand.Execute(null);

            EditingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        }

        [Fact]
        public void ShouldOpenEditingWindowAfterPressingEditButton()
        {
            ViewModel.AddCommand.Execute(null);

            EditingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        }
        [Fact]
        public void ShouldAddNewItemToListAfterCreateEventWasCalled()
        {
            _createItemEvent.Publish(new TCreateEventArgs
            {
                Entity = CreateNewEntityDtoItem(1)
            });
            var items = ViewModel.Items;
            Assert.Equal(1, items.Count);
        }
        //protected override void PublishCreateEvent()
        //{
        //    var _openTableViewEvent = new CreateDistributionInformationEvent();

        //    _openTableViewEvent.Publish(new CreateDistributionInformationEventArgs
        //    {
        //        Entity = CreateNewEntityDtoItem(1)
        //    });
        //}

    }
}
