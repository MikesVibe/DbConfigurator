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
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);


        }

        private async void OnSaveExecute()
        {
            //Console.WriteLine("Testing Save button.");
            var recipientEntity = await _dataModel.GetRecipientByIdAsync(SelectedRecipient.Id);
            recipientEntity.FirstName = SelectedRecipientFirstName;
            recipientEntity.LastName = SelectedRecipientLastName;
            recipientEntity.Email = SelectedRecipientEmail;
            //SelectedRecipient.FirstName = SelectedRecipientFirstName;
            //SelectedRecipient.LastName = SelectedRecipientLastName;
            //SelectedRecipient.Email = SelectedRecipientEmail;

            await _dataModel.SaveChangesAsync();
        }

        private bool OnSaveCanExecute()
        {
            return true;
        }

        public override async Task LoadAsync()
        {
            var recipients = await _dataModel.GetAllRecipientsAsync();

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
            var recipient = new Recipient()
            {
                FirstName = String.Empty,
                LastName = String.Empty,
                Email = String.Empty
            };




            await _dataModel.AddAsync(recipient);
            await _dataModel.SaveChangesAsync();

            var recipientEntity = await _dataModel.GetRecipientByIdAsync(recipient.Id);
            var recipientDto = _autoMapper.Mapper.Map<RecipientDto>(recipientEntity);
            var recipientWrapped = new RecipientDtoWrapper(recipientDto);

            Recipients_ObservableCollection.Add(recipientWrapped);
            SelectedRecipient = recipientWrapped;
        }

        protected override void OnRemoveExecute()
        {
        }

        protected override bool OnRemoveCanExecute()
        {
            return false;
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
            }
        }
        public string SelectedRecipientFirstName
        {
            get { return _selectedRecipientFirstName; }
            set 
            {
                _selectedRecipientFirstName = value;
                SelectedRecipient.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string SelectedRecipientLastName
        {
            get { return _selectedRecipientLastName; }
            set
            {
                _selectedRecipientLastName = value;
                SelectedRecipient.LastName = value;
                OnPropertyChanged();
            }
        }
        public string SelectedRecipientEmail
        {
            get { return _selectedRecipientEmail; }
            set
            {
                _selectedRecipientEmail = value;
                SelectedRecipient.Email = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<RecipientDtoWrapper> Recipients_ObservableCollection { get; set; }
        public ICommand SaveCommand { get; set; }

        private string _selectedRecipientFirstName;
        private string _selectedRecipientLastName;
        private string _selectedRecipientEmail;
        private RecipientDtoWrapper _selectedRecipient;

    }
}
