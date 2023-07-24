using DbConfigurator.Model.Contracts;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Services.Interfaces;
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

namespace DbConfigurator.UI.UnitTests.Base
{
    public abstract class TableViewModelBaseTests<TWrapper, TDto, TDataService, TDetailVm>
        where TWrapper : IWrapperWithId
        where TDto : class, IEntityDto
        where TDataService : class, IDataService<TDto>
        where TDetailVm : IDetailViewModel
    {
        protected Mock<IEventAggregator> _eventAggregatorMock;

        protected Mock<IEditingWindowService> editingWindow;
        protected Mock<TDataService> dataServiceMock;
        protected Func<TDetailVm> detailVmCreator;
        protected TableViewModelBase<TWrapper, TDto, TDataService> viewModel;

        public TableViewModelBaseTests()
        {
            _eventAggregatorMock = new Mock<IEventAggregator>();
            editingWindow = new Mock<IEditingWindowService>();

            detailVmCreator = CreateNewDetailViewModel();
            viewModel = CreateViewModel();
        }


        protected abstract TableViewModelBase<TWrapper, TDto, TDataService> CreateViewModel();
        protected abstract Func<TDetailVm> CreateNewDetailViewModel();


        [Fact]
        public async Task ShouldLoadItemsOnLoadAsyncMethod()
        {
            // Arrange
            var testData = new List<TDto>();
            testData.Add(CreateNewEntityDtoItem());
            testData.Add(CreateNewEntityDtoItem());
            testData.Add(CreateNewEntityDtoItem());
            
            dataServiceMock.Setup(ds => ds.GetAllAsync()).ReturnsAsync(testData);


            // Act
            await viewModel.LoadAsync();

            // Assert
            // Check if the Items collection was populated with the test data
            Assert.Equal(testData.Count, viewModel.Items.Count);

            foreach (var item in testData)
            {
                var wrappedItem = viewModel.Items.FirstOrDefault(w => w.Id == item.Id);
                Assert.NotNull(wrappedItem);
                // Perform additional assertions if needed.
            }
        }

        protected abstract TDto CreateNewEntityDtoItem();

        [Fact]
        public void ShouldOpenEditingWindowAfterPressingAddButton()
        {
            var test = detailVmCreator();
            viewModel.AddCommand.Execute(null);

            editingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        }
        [Fact]
        public async void ShouldOpenEditingWindowAfterPressingEditButton()
        {
            int distributionInformationId = 7;

            var test = detailVmCreator();
            await test.LoadAsync(distributionInformationId);
            viewModel.AddCommand.Execute(null);

            editingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        }
    }
}
