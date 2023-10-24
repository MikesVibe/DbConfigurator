using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Services;
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
    public abstract class DetailViewModelBaseTests<TDataService, TEntityDto>
        where TDataService : class, IDataService<TEntityDto>
        where TEntityDto : IEntityDto, new()
    {
        protected DetailViewModelBase<TDataService, TEntityDto> DetialViewModel;
        protected Mock<TDataService> DataServiceMock;
        protected Mock<IEventAggregator> EventAgregatorMock;
        private int _entityId;

        public DetailViewModelBaseTests()
        {
            DataServiceMock = new Mock<TDataService>();
            EventAgregatorMock = new Mock<IEventAggregator>();

            DataServiceMock.Setup(di => di.GetByIdAsync(_entityId))
                .ReturnsAsync(CreateNewEntityDtoItem(_entityId));
        }

        [Fact]
        public void EntityShouldNotBeEmpty()
        {
            Assert.NotNull(DetialViewModel.EntityDto);
        }
        [Fact]
        public async void EntityShouldNotBeEmptyAfterLoadMethodWasCalled()
        {
            await DetialViewModel.LoadAsync(_entityId);
            Assert.NotNull(DetialViewModel.EntityDto);
        }
        [Fact]
        public async void ShouldLoadEntity()
        {
            await DetialViewModel.LoadAsync(_entityId);

            Assert.NotNull(DetialViewModel.EntityDto);
            Assert.Equal(_entityId, DetialViewModel.EntityDto.Id);

            DataServiceMock.Verify(dp => dp.GetByIdAsync(_entityId), Times.Once);
        }

        protected abstract TEntityDto CreateNewEntityDtoItem(int id);
    }
}
