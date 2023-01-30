using DbConfigurator.Model;
using DbConfigurator.UI.Data.Repositories;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DbConfigurator.UI.ViewModel
{
    public class RecipientDetailViewModel : DetailViewModelBase, IRecipientDetailViewModel
    {
        public RecipientDetailViewModel(IRecipientRepository recipientRepository,
            IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _recipientRepository = recipientRepository;

            Recipients_ObservableCollection = new ObservableCollection<RecipientWrapper>();
        }


        public async Task LoadAsync()
        {
            var recipients = await _recipientRepository.GetAllAsync();

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target
            foreach (var wrapper in Recipients_ObservableCollection)
            {
                wrapper.PropertyChanged -= Recipients_ObservableCollection_PropertyChanged;

            }
            Recipients_ObservableCollection.Clear();

            foreach (var friendPhoneNumber in recipients)
            {
                var wrapper = new RecipientWrapper(friendPhoneNumber);
                Recipients_ObservableCollection.Add(wrapper);
                wrapper.PropertyChanged += Recipients_ObservableCollection_PropertyChanged;
            }
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target
        }
        private void Recipients_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _recipientRepository.HasChanges();
            }
            if (e.PropertyName == nameof(RecipientWrapper.HasErrors))
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
            _recipientRepository.SaveAsync();
            HasChanges = _recipientRepository.HasChanges();
            Id = SelectedRecipient.Id;

        }

        
        public int DefaultRowIndex { get { return 0; } }
        public RecipientWrapper SelectedRecipient
        {
            get { return _selectedRecipient; }
            set 
            {
                _selectedRecipient = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RecipientWrapper> Recipients_ObservableCollection { get; set; }


        private ObservableCollection<RecipientWrapper> _gridDataCollection;
        private IRecipientRepository _recipientRepository;
        private IEventAggregator _eventAggregator;
        private RecipientWrapper _selectedRecipient;


    }
}
