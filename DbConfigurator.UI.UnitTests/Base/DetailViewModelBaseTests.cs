using DbConfigurator.Core.Contracts;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.ViewModel.Base;
using Moq;
using Prism.Events;
using Xunit;

namespace DbConfigurator.UI.UnitTests.Base
{
    public abstract class DetailViewModelBaseTests<TDataService, TEntity>
        where TDataService : class, IDataService<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected DetailViewModelBase<TDataService, TEntity> DetialViewModel;
        protected Mock<TDataService> DataServiceMock;
        protected Mock<IEventAggregator> EventAgregatorMock;
        private IEntity _entityId;

        public DetailViewModelBaseTests()
        {
            DataServiceMock = new Mock<TDataService>();
            EventAgregatorMock = new Mock<IEventAggregator>();

            //DataServiceMock.Setup(di => di.GetByIdAsync(_entityId))
            //    .ReturnsAsync(CreateNewEntityDtoItem(_entityId));
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
        //[Fact]
        //public async void ShouldLoadEntity()
        //{
        //    await DetialViewModel.LoadAsync(_entityId);

        //    Assert.NotNull(DetialViewModel.EntityDto);
        //    Assert.Equal(_entityId.Id, DetialViewModel.EntityDto.Id);

        //    DataServiceMock.Verify(dp => dp.GetByIdAsync(_entityId), Times.Once);
        //}

        protected abstract TEntity CreateNewEntityDtoItem(int id);
    }
}
