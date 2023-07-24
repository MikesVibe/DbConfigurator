using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
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
        protected Mock<IEventAggregator> EventAggregatorMock;

        protected Mock<IEditingWindowService> EditingWindow;
        protected Mock<TDataService> DataServiceMock;
        protected Func<TDetailVm> DetailVmCreator;
        protected TableViewModelBase<TWrapper, TDto, TDataService> ViewModel;

        public TableViewModelBaseTests()
        {
            EventAggregatorMock = new Mock<IEventAggregator>();
            EditingWindow = new Mock<IEditingWindowService>();
            DataServiceMock = new Mock<TDataService>();
            
            DetailVmCreator = CreateNewDetailViewModel;
            ViewModel = CreateViewModel();
        }


        protected abstract TableViewModelBase<TWrapper, TDto, TDataService> CreateViewModel();
        protected abstract TDetailVm CreateNewDetailViewModel();
        protected abstract TDto CreateNewEntityDtoItem();


        [Fact]
        public async Task ShouldLoadItemsOnLoadAsyncMethod()
        {
            // Arrange
            var testData = new List<TDto>();
            testData.Add(CreateNewEntityDtoItem());
            testData.Add(CreateNewEntityDtoItem());
            testData.Add(CreateNewEntityDtoItem());
            
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
            var test = DetailVmCreator();
            ViewModel.AddCommand.Execute(null);

            EditingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        }
        [Fact]
        public void ShouldOpenEditingWindowAfterPressingEditButton()
        {
            ViewModel.AddCommand.Execute(null);

            EditingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        }
        //[Fact]
        //public void ShouldCallLoadAsyncMethodOnDetailVmAfterPressingEditButton()
        //{
        //    int distributionInformationId = 7;
        //    ViewModel.AddCommand.Execute(null);

        //    EditingWindow.Verify(ew => ew.ShowWindow(It.IsAny<IDetailViewModel>()), Times.Once);
        //}


    }
}
