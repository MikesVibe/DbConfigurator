using DbConfigurator.Authentication;
using DbConfigurator.Core.Contracts;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Notifications.Event;
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
    public abstract class TableViewModelBaseTests<TEntityWrapper, TEntity, TDataService, TDetailVm,
        TCreateEvent, TCreateEventArgs,
        TEditEvent, TEditEventArgs>
        where TEntityWrapper : IWrapperWithId
        where TEntity : class, IEntity, new()
        where TDataService : class, IDataService<TEntity>
        where TDetailVm : IDetailViewModel
        where TCreateEvent : PubSubEvent<TCreateEventArgs>, new()
        where TCreateEventArgs : IEventArgs<TEntity>, new()
        where TEditEvent : PubSubEvent<TEditEventArgs>, new()
        where TEditEventArgs : IEventArgs<TEntity>, new()
    {
        protected Mock<IEventAggregator> EventAggregatorMock;

        protected Mock<IEditingWindowService> EditingWindow;
        protected Mock<TDataService> DataServiceMock;
        protected Func<TDetailVm> DetailVmCreator;
        protected TableViewModelBase<TEntityWrapper, TEntity, TDataService,
            TCreateEvent, TCreateEventArgs,
        TEditEvent, TEditEventArgs> ViewModel;
        protected Mock<ISecuritySettings> SecuritySettings;
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
            EventAggregatorMock.Setup(ea => ea.GetEvent<SelectedNotificationDistributionList>())
                .Returns(new SelectedNotificationDistributionList());

            EditingWindow = new Mock<IEditingWindowService>();
            DataServiceMock = new Mock<TDataService>();
            SecuritySettings = new Mock<ISecuritySettings>();
            SecuritySettings.Setup(s => s.IsAuthorized(It.IsAny<List<Role>>())).Returns(true);

            DetailVmCreator = CreateNewDetailViewModel;
            ViewModel = CreateViewModel();
        }


        protected abstract TableViewModelBase<TEntityWrapper, TEntity, TDataService, TCreateEvent, TCreateEventArgs,
        TEditEvent, TEditEventArgs> CreateViewModel();
        protected abstract TDetailVm CreateNewDetailViewModel();
        protected abstract TEntity CreateNewEntityDtoItem(int id);
        protected abstract IEnumerable<TEntityWrapper> CreateItemsList();

        [Fact]
        public async Task ShouldLoadItemsOnLoadAsyncMethod()
        {
            // Arrange
            var testData = new List<TEntity>();
            testData.Add(CreateNewEntityDtoItem(1));
            testData.Add(CreateNewEntityDtoItem(2));
            testData.Add(CreateNewEntityDtoItem(3));

            DataServiceMock.Setup(ds => ds.GetAllAsyncResult()).ReturnsAsync(testData);
            DataServiceMock.Setup(ds => ds.CanConnect()).ReturnsAsync(true);


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
            ViewModel.Items = new ObservableCollection<TEntityWrapper>(CreateItemsList());
            ViewModel.SelectedItem = ViewModel.Items.First();

            //ViewModel.SelectedItem = new TEntityWrapper();
            ViewModel.EditCommand.Execute(null);

            EditingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        }

        [Fact]
        public void ShouldOpenEditingWindowAfterPressingEditButton()
        {
            ViewModel.AddCommand.Execute(null);

            EditingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        }
        //[Fact]
        //public void ShouldAddNewItemToListAfterCreateEventWasCalled()
        //{
        //    _createItemEvent.Publish(new TCreateEventArgs
        //    {
        //        Entity = CreateNewEntityDtoItem(1)
        //    });
        //    var items = ViewModel.Items;
        //    Assert.Equal(1, items.Count);
        //}
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
