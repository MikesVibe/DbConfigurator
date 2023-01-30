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
    public class CountryTableDetailViewModel : DetailViewModelBase, ICountryTableDetailViewModel
    {
        public CountryTableDetailViewModel(IBuisnessRepository countryRepository,
        IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _countryRepository = countryRepository;

            Recipients_ObservableCollection = new ObservableCollection<BuisnessUnitWrapper>();
        }


        public async Task LoadAsync()
        {
            var countries = await _countryRepository.GetAllAsync();

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target
            foreach (var wrapper in Recipients_ObservableCollection)
            {
                wrapper.PropertyChanged -= Recipients_ObservableCollection_PropertyChanged;

            }
            Recipients_ObservableCollection.Clear();

            foreach (var country in countries)
            {
                var wrapper = new BuisnessUnitWrapper(country);
                Recipients_ObservableCollection.Add(wrapper);
                wrapper.PropertyChanged += Recipients_ObservableCollection_PropertyChanged;
            }
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target
        }
        private void Recipients_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _countryRepository.HasChanges();
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
            return SelectedRecipient != null
                && !SelectedRecipient.HasErrors
                && HasChanges;
        }
        protected override void OnSaveExecute()
        {
            _countryRepository.SaveAsync();
            HasChanges = _countryRepository.HasChanges();
            Id = SelectedRecipient.Id;

        }


        public int DefaultRowIndex { get { return 0; } }
        public BuisnessUnitWrapper SelectedRecipient
        {
            get { return _selectedRecipient; }
            set
            {
                _selectedRecipient = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BuisnessUnitWrapper> Recipients_ObservableCollection { get; set; }


        private ObservableCollection<BuisnessUnitWrapper> _gridDataCollection;
        private IBuisnessRepository _countryRepository;
        private BuisnessUnitWrapper _selectedRecipient;

    }
}
