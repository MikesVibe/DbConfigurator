using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.UI.Startup;
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
        private readonly AutoMapperConfig _autoMapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataModel _dataModel;

        public RecipientTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            Recipients_ObservableCollection = new ObservableCollection<RecipientDtoWrapper>();



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
                var mapped = _autoMapper.Mapper.Map<RecipientDto>(recipient);
                var wrapper = new RecipientDtoWrapper(mapped);
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
            if (e.PropertyName == nameof(RecipientDtoWrapper.HasErrors))
            {
                //((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }



        protected async override void OnAddExecute()
        {
            //Create New Recipient
            //var recipient = new Recipient()
            //{
            //    FirstName = String.Empty,
            //    LastName = String.Empty,
            //    Email = String.Empty
            //};

            var recipient = new Recipient()
            {
                FirstName = "Krzysiu",
                LastName = "Testwoy",
                Email = "Krzysiu.Testowy@company.com"
            };


            await _dataModel.AddAsync(recipient);
            await _dataModel.SaveChangesAsync();

            var recipientEntity = await _dataModel.GetRecipientAsync(recipient.Id);
            var recipientDto = _autoMapper.Mapper.Map<RecipientDto>(recipientEntity);
            var recipientWrapped = new RecipientDtoWrapper(recipientDto);

            Recipients_ObservableCollection.Add(recipientWrapped);
            SelectedRecipient = recipientWrapped;
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
        public RecipientDtoWrapper SelectedRecipient
        {
            get { return _selectedRecipient; }
            set
            {
                if (value == null)
                    return;

                _selectedRecipient = value;
                SelectedRecipientFirstName = SelectedRecipient.FirstName;
                SelectedRecipientLastName = SelectedRecipient.LastName;
                SelectedRecipientEmail = SelectedRecipient.Email;

                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedRecipientFirstName));
                OnPropertyChanged(nameof(SelectedRecipientLastName));
                OnPropertyChanged(nameof(SelectedRecipientEmail));
            }
        }
        public string SelectedRecipientFirstName
        {
            get { return _selectedRecipientFirstName; }
            set 
            {
                _selectedRecipientFirstName = value;
                SelectedRecipient.FirstName = value;
            }
        }

        public string SelectedRecipientLastName { get; set; }
        public string SelectedRecipientEmail { get; set; }




        public ObservableCollection<RecipientDtoWrapper> Recipients_ObservableCollection { get; set; }


        private string _selectedRecipientFirstName;
        private RecipientDtoWrapper _selectedRecipient;


    }
}
