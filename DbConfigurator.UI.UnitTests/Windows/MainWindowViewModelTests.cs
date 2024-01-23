using Autofac.Features.Indexed;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Panels.DistributionInformationPanel;
using DbConfigurator.UI.ViewModel;
using Moq;
using Prism.Events;
using System.Linq;
using Xunit;

namespace DbConfigurator.UI.UnitTests.Windows
{
    public class MainWindowViewModelTests
    {
        private MainWindowViewModel _viewModel;
        private Mock<INavigationPanelViewModel> _navigationPanelViewModelMock;
        private Mock<IEventAggregator> _eventAggregatorMock;
        private OpenPanelViewEvent _openTableViewEvent;
        private Mock<IIndex<string, IMainPanelViewModel>> _tableViewModelCreatorMock;
        private Mock<IStatusService> _statusServiceMock;

        public MainWindowViewModelTests()
        {
            // Create a mock for INavigationPanelViewModel
            _navigationPanelViewModelMock = new Mock<INavigationPanelViewModel>();

            // Create a mock for OpenPanelViewEvent and setup the EventAggregator to return it when GetEvent is called
            _openTableViewEvent = new OpenPanelViewEvent();
            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<OpenPanelViewEvent>())
                .Returns(_openTableViewEvent);
            _statusServiceMock = new Mock<IStatusService>();

            // Create a mock for IDistributionInformationPanelViewModel and setup IIndex to return it for the specified key
            var fake = new Mock<IIndex<string, ITableViewModel>>();
            var mockDistributionInformationPanelViewModel = new DistributionInformationPanelViewModel(fake.Object, _statusServiceMock.Object);
            //var mockDistributionInformationPanelViewModel = new Mock<DistributionInformationPanelViewModel>();


            _tableViewModelCreatorMock = new Mock<IIndex<string, IMainPanelViewModel>>();
            _tableViewModelCreatorMock.Setup(i => i[nameof(DistributionInformationPanelViewModel)])
                .Returns(mockDistributionInformationPanelViewModel);

            // Create the MainWindowViewModel instance by passing the mock objects as dependencies
            _viewModel = new MainWindowViewModel(
                _navigationPanelViewModelMock.Object,
                _tableViewModelCreatorMock.Object,
                _eventAggregatorMock.Object,
                _statusServiceMock.Object);
        }


        [Fact]
        public async void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            await _viewModel.LoadAsync();

            _navigationPanelViewModelMock.Verify(vm => vm.LoadAsync(), Times.Once);
        }

        [Fact]
        public void ShouldOpenPanelAfterOpenPanelViewEventWasCalled()
        {
            string panelName = nameof(DistributionInformationPanelViewModel);

            _openTableViewEvent.Publish(new OpenPanelViewEventArgs
            {
                Id = 1,
                ViewModelName = panelName
            });

            var models = _viewModel.MainViewModels;
            Assert.Equal(1, models.Count);
            Assert.Equal(panelName, models.First().GetType().Name);
        }

        [Fact]
        public void ShouldOpenMainPanelOnlyOnce()
        {
            string panelName = nameof(DistributionInformationPanelViewModel);
            var @event = new OpenPanelViewEventArgs
            {
                Id = 1,
                ViewModelName = panelName
            };

            _openTableViewEvent.Publish(@event);
            _openTableViewEvent.Publish(@event);

            var models = _viewModel.MainViewModels;
            Assert.Equal(1, models.Count);
        }
    }
}
