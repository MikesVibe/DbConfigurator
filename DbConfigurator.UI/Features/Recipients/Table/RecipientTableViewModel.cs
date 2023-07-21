using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Linq;
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
            ) : base(eventAggregator, dialogService, dataService, addRecipientViewModelCreator, autoMapper)
        {
            EventAggregator.GetEvent<CreateRecipientEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditRecipientEvent>()
                .Subscribe(OnEditExecute);
        }


        private void OnCreateExecute(CreateRecipientEventArgs obj)
        {
            var wrapped = new RecipientDtoWrapper(obj.Entity);
            Items.Add(wrapped);
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
    }
}
