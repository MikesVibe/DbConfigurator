using DbConfigurator.UI.Data.Repositories;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class BuisnessUnitTableViewModel : TableViewModelBase, IBuisnessUnitTableViewModel
    {
        public BuisnessUnitTableViewModel(IBuisnessRepository countryRepository,
        IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _buisnessUnitRepository = countryRepository;

            BuisnessUnit_ObservableCollection = new ObservableCollection<BuisnessUnitWrapper>();
        }


        public async Task LoadAsync()
        {
            var buisnessUnits = await _buisnessUnitRepository.GetAllAsync();

            foreach (var wrapper in BuisnessUnit_ObservableCollection)
            {
                wrapper.PropertyChanged -= BuisnessUnits_ObservableCollection_PropertyChanged; 

            }
            BuisnessUnit_ObservableCollection.Clear();

            foreach (var buisnessUnit in buisnessUnits)
            {
                var wrapper = new BuisnessUnitWrapper(buisnessUnit);
                BuisnessUnit_ObservableCollection.Add(wrapper);
                wrapper.PropertyChanged += BuisnessUnits_ObservableCollection_PropertyChanged;
            }
        }
        private void BuisnessUnits_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _buisnessUnitRepository.HasChanges();
            }
            if (e.PropertyName == nameof(BuisnessUnitWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }
        protected override bool OnSaveCanExecute()
        {
            return SelectedBuisnessUnit != null
                && !SelectedBuisnessUnit.HasErrors
                && HasChanges;
        }
        protected override void OnSaveExecute()
        {
            _buisnessUnitRepository.SaveAsync();
            HasChanges = _buisnessUnitRepository.HasChanges();
            Id = SelectedBuisnessUnit.Id;

        }


        public int DefaultRowIndex { get { return 0; } }
        public BuisnessUnitWrapper SelectedBuisnessUnit
        {
            get { return _selectedBuisnessUnit; }
            set
            {
                _selectedBuisnessUnit = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BuisnessUnitWrapper> BuisnessUnit_ObservableCollection { get; set; }


        private IBuisnessRepository _buisnessUnitRepository;
        private BuisnessUnitWrapper _selectedBuisnessUnit;

    }
}
