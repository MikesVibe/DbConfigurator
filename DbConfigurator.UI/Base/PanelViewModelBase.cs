using DbConfigurator.UI.Base.Contracts;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class PanelViewModelBase : NotifyBase, IMainPanelViewModel
    {
        private readonly IStatusService _statusService;
        protected bool _idHasBeenSet = false;
        private bool _blur;
        public PanelViewModelBase(IStatusService statusService)
        {
            _statusService = statusService;
            _statusService.StatusChanged += StatusChanged!;
        }
        public int Id { get; protected set; }
        public bool FirstRun { get; set; } = true;
        public bool Blur 
        { 
            get
            { 
                return _blur; 
            }
            set 
            { 
                _blur = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            if(FirstRun && _statusService.IsConnected)
            {
                await LoadDataAsync();
            }
            else
            {
                Blur = true;
            }
        }
        protected abstract Task LoadDataAsync();
        public abstract Task RefreshAsync();

        public void StatusChanged(object sender, bool isConnected)
        {
            Blur = !isConnected;
        }

        public void SetId(int id)
        {
            if (_idHasBeenSet)
                return;

            Id = id;
            _idHasBeenSet = true;
        }

    }
}
