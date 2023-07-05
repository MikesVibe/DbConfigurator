using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.Services.Interfaces;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class RecipientTableViewModel : TableViewModelBase<RecipientDtoWrapper>
    {
        private readonly AutoMapperConfig _autoMapper;
        private readonly ICombinedDataService _dataModel;
        private readonly Func<AddRecipientViewModel> _addRecipientViewModelCreator;

        public RecipientTableViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            ICombinedDataService dataModel,
            AutoMapperConfig autoMapper,
            Func<AddRecipientViewModel> addRecipientViewModelCreator
            ) : base(eventAggregator, dialogService)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            _addRecipientViewModelCreator = addRecipientViewModelCreator;

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

            await _dataModel.AddAsync(recipientEntity);
            await _dataModel.SaveChangesAsync();

            var mapped = _autoMapper.Mapper.Map<RecipientDto>(recipientEntity);
            var wrapped = new RecipientDtoWrapper(mapped);

            Items.Add(wrapped);
            SelectedItem = wrapped;
        }
        protected async override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;

            var recipient = await _dataModel.GetRecipientByIdAsync(SelectedItem.Id);
            if (recipient is null)
            {
                //Log some error mesage here
                return;
            }

            _dataModel.Remove(recipient);
            _dataModel.SaveChanges();

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

            var recipientEntity = await _dataModel.GetRecipientByIdAsync(SelectedItem!.Id);
            if (recipientEntity is null)
            {
                //Log some error
                return;
            }
            _autoMapper.Mapper.Map(recipient.Model, recipientEntity);

            _dataModel.SaveChanges();
            SelectedItem.FirstName = recipient.FirstName;
        }
    }
}
