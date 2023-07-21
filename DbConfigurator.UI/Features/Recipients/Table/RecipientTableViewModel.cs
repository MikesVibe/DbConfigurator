using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Recipients
{
    public class RecipientTableViewModel : TableViewModelBase<RecipientDtoWrapper, RecipientDto, IRecipientService>
    {

        public RecipientTableViewModel(
            IEventAggregator eventAggregator,
            IWindowService dialogService,
            IRecipientService dataService,
            AutoMapperConfig autoMapper,
            Func<RecipientDetailViewModel> addRecipientViewModelCreator
            ) : base(eventAggregator, dialogService, dataService, addRecipientViewModelCreator)
        {
        }


        public override async Task LoadAsync()
        {
            var recipients = await DataService.GetAllAsync();

            foreach (var recipient in recipients)
            {
                var wrapper = new RecipientDtoWrapper(recipient);
                Items.Add(wrapper);
            }
        }
        //protected async override void OnAddExecute()
        //{
        //    var recipientViewModel = _addRecipientViewModelCreator();
        //    bool? result = DialogService.ShowDialog(recipientViewModel);
        //    if (result == false)
        //        return;

        //    var recipientDto = recipientViewModel.Recipient;
        //    //Create New Recipient
        //    var recipientEntity = new Recipient()
        //    {
        //        FirstName = recipientDto.FirstName,
        //        LastName = recipientDto.LastName,
        //        Email = recipientDto.Email
        //    };

        //    await _dataService.AddAsync(recipientEntity);
        //    await _dataService.SaveChangesAsync();

        //    var mapped = _autoMapper.Mapper.Map<RecipientDto>(recipientEntity);
        //    var wrapped = new RecipientDtoWrapper(mapped);

        //    Items.Add(wrapped);
        //    SelectedItem = wrapped;
        //}

        //protected override async void OnEditExecute()
        //{
        //    var recipientWrapper = _autoMapper.Mapper.Map<RecipientDtoWrapper>(SelectedItem!.Model);
        //    var recipientViewModel = _addRecipientViewModelCreator();
        //    recipientViewModel.Recipient = recipientWrapper;
        //    bool? result = DialogService.ShowDialog(recipientViewModel);
        //    if (result == false)
        //        return;

        //    var recipient = recipientViewModel.Recipient;

        //    var recipientEntity = await _dataService.GetRecipientByIdAsync(SelectedItem!.Id);
        //    if (recipientEntity is null)
        //    {
        //        //Log some error
        //        return;
        //    }
        //    _autoMapper.Mapper.Map(recipient.Model, recipientEntity);

        //    _dataService.SaveChanges();
        //    SelectedItem.FirstName = recipient.FirstName;
        //}
    }
}
