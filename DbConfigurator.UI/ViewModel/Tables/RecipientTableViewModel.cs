using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class RecipientTableViewModel : TableViewModelBase<RecipientDtoWrapper>
    {
        private readonly AutoMapperConfig _autoMapper;
        private readonly ICombinedDataService _dataService;
        private readonly Func<AddRecipientViewModel> _addRecipientViewModelCreator;

        public RecipientTableViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            ICombinedDataService dataModel,
            AutoMapperConfig autoMapper,
            Func<AddRecipientViewModel> addRecipientViewModelCreator
            ) : base(eventAggregator, dialogService)
        {
            _dataService = dataModel;
            _autoMapper = autoMapper;
            _addRecipientViewModelCreator = addRecipientViewModelCreator;

        }


        public override async Task LoadAsync()
        {
            var recipients = await _dataService.GetAllRecipientsAsync();
            //foreach (var wrapper in Recipients_ObservableCollection)
            //{
            //    wrapper.PropertyChanged -= Recipients_ObservableCollection_PropertyChanged;

            //}
            //Recipients_ObservableCollection.Clear();

            foreach (var recipient in recipients)
            {
                var mapped = _autoMapper.Mapper.Map<RecipientDto>(recipient);
                var wrapper = new RecipientDtoWrapper(mapped);
                Items.Add(wrapper);
                //wrapper.PropertyChanged += Recipients_ObservableCollection_PropertyChanged;
            }
        }
        protected async override void OnAddExecute()
        {
            var recipientViewModel = _addRecipientViewModelCreator();
            bool? result = DialogService.ShowDialog(recipientViewModel);
            if (result == false)
                return;

            var recipientDto = recipientViewModel.Recipient;
            //Create New Recipient
            var recipientEntity = new Recipient()
            {
                FirstName = recipientDto.FirstName,
                LastName = recipientDto.LastName,
                Email = recipientDto.Email
            };

            await _dataService.AddAsync(recipientEntity);
            await _dataService.SaveChangesAsync();

            var mapped = _autoMapper.Mapper.Map<RecipientDto>(recipientEntity);
            var wrapped = new RecipientDtoWrapper(mapped);

            Items.Add(wrapped);
            SelectedItem = wrapped;
        }
        protected async override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;

            var recipient = await _dataService.GetRecipientByIdAsync(SelectedItem.Id);
            if (recipient is null)
            {
                //Log some error mesage here
                return;
            }

            _dataService.Remove(recipient);
            _dataService.SaveChanges();

            base.OnRemoveExecute();
        }
        protected override async void OnEditExecute()
        {
            var recipientWrapper = _autoMapper.Mapper.Map<RecipientDtoWrapper>(SelectedItem!.Model);
            var recipientViewModel = _addRecipientViewModelCreator();
            recipientViewModel.Recipient = recipientWrapper;
            bool? result = DialogService.ShowDialog(recipientViewModel);
            if (result == false)
                return;

            var recipient = recipientViewModel.Recipient;

            var recipientEntity = await _dataService.GetRecipientByIdAsync(SelectedItem!.Id);
            if (recipientEntity is null)
            {
                //Log some error
                return;
            }
            _autoMapper.Mapper.Map(recipient.Model, recipientEntity);

            _dataService.SaveChanges();
            SelectedItem.FirstName = recipient.FirstName;
        }
    }
}
