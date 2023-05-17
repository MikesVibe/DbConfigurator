using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.Wrapper;
using Microsoft.EntityFrameworkCore;
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
using static DbConfigurator.DataAccess.DbConfiguratorDbContext;

namespace DbConfigurator.UI.ViewModel
{
    public class RecipientTableViewModel : TableViewModelBase, IRecipientTableViewModel
    {
        public RecipientTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _dataModel = dataModel;


            Recipients_ObservableCollection = new ObservableCollection<RecipientWrapper>();




        }


        public override async Task LoadAsync()
        {
            var recipients = _dataModel.Recipients;

            //foreach (var wrapper in Recipients_ObservableCollection)
            //{
            //    wrapper.PropertyChanged -= Recipients_ObservableCollection_PropertyChanged;

            //}
            //Recipients_ObservableCollection.Clear();

            foreach (var recipient in recipients)
            {
                var wrapper = new RecipientWrapper(recipient);
                Recipients_ObservableCollection.Add(wrapper);
                //wrapper.PropertyChanged += Recipients_ObservableCollection_PropertyChanged;
            }
        }
        private void Recipients_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _dataModel.HasChanges();
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
        protected async override void OnSaveExecute()
        {
            await _dataModel.SaveChangesAsync();
            HasChanges = _dataModel.HasChanges();
            Id = SelectedRecipient.Id;

        }

        protected override void OnAddExecute()
        {
            //throw new NotImplementedException();
        }

        protected override void OnRemoveExecute()
        {
            //throw new NotImplementedException();
        }

        protected override bool OnRemoveCanExecute()
        {
            return false;
            //throw new NotImplementedException();
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

        private RecipientWrapper _selectedRecipient;
        private IEventAggregator _eventAggregator;
        private IDataModel _dataModel;


    }
}
