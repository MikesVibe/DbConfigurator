using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class RecipientPanelViewModel : TableViewModelBase, IRecipientTableViewModel, IMainPanelViewModel
    {
        private readonly AutoMapperConfig _autoMapper;
        private readonly IDataModel _dataModel;

        public RecipientPanelViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            IDataModel dataModel,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            Recipients_ObservableCollection = new ObservableCollection<RecipientDtoWrapper>();
        }



        public override async Task LoadAsync()
        {
            var recipients = await _dataModel.GetAllRecipientsAsync();

            foreach (var wrapper in Recipients_ObservableCollection)
            {
                wrapper.PropertyChanged -= Recipients_ObservableCollection_PropertyChanged;

            }
            Recipients_ObservableCollection.Clear();

            foreach (var recipient in recipients)
            {
                var mapped = _autoMapper.Mapper.Map<RecipientDto>(recipient);
                var wrapper = new RecipientDtoWrapper(mapped);
                Recipients_ObservableCollection.Add(wrapper);
                wrapper.PropertyChanged += Recipients_ObservableCollection_PropertyChanged;
            }
        }
        private void Recipients_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (!HasChanges)
            //{
            //    HasChanges = _dataModel.HasChanges();
            //}
            //if (e.PropertyName == nameof(RecipientDtoWrapper.HasErrors))
            //{
            //    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            //}
        }


        protected async override void OnAddExecute()
        {
            //Create New Recipient
            var recipient = new Recipient()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty
            };


            await _dataModel.AddAsync(recipient);
            await _dataModel.SaveChangesAsync();

            var recipientEntity = await _dataModel.GetRecipientByIdAsync(recipient.Id);
            var recipientDto = _autoMapper.Mapper.Map<RecipientDto>(recipientEntity);
            var recipientWrapped = new RecipientDtoWrapper(recipientDto);

            Recipients_ObservableCollection.Add(recipientWrapped);
            SelectedRecipient = recipientWrapped;
        }

        protected async override void OnRemoveExecute()
        {
            var recipient = await _dataModel.GetRecipientByIdAsync(SelectedRecipient.Id);
            _dataModel.Remove(recipient);
            await _dataModel.SaveChangesAsync();

            Recipients_ObservableCollection.Remove(SelectedRecipient);
            SelectedRecipient = null;
        }

        protected override void OnEditExecute()
        {
            throw new NotImplementedException();
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
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RecipientDtoWrapper> Recipients_ObservableCollection { get; set; }

        private RecipientDtoWrapper _selectedRecipient;

    }
}
