using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Recipients.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;

namespace DbConfigurator.UI.Features.Recipients
{
    public class RecipientTableViewModel : TableViewModelBase<RecipientWrapper, Recipient, IRecipientService,
        CreateRecipientEvent, CreateRecipientEventArgs,
        EditRecipientEvent, EditRecipientEventArgs>
    {

        public RecipientTableViewModel(
            IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            IRecipientService dataService,
            AutoMapperConfig autoMapper,
            Func<RecipientDetailViewModel> addRecipientViewModelCreator
            ) : base(eventAggregator, dialogService, dataService, addRecipientViewModelCreator, autoMapper)
        {
        }
    }
}
