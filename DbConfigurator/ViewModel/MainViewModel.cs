using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            //DetailViewModels = new ObservableCollection<IDetailViewModel>();
            SelectedDetailViewModel = new DetailsViewModel();
        }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        private IDetailViewModel _selectedDetailViewModel;

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set { _selectedDetailViewModel = value; }
        }

    }
}
