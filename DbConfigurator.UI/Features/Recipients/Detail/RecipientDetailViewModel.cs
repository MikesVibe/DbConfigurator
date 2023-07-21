using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;

namespace DbConfigurator.UI.Features.Recipients
{
    public class RecipientDetailViewModel : DetailViewModelBase<IRecipientService, RecipientDto>
    {
        public RecipientDetailViewModel(IRecipientService recipientService, IEventAggregator eventAggregator) : base(recipientService, eventAggregator)
        {
            Title = "Recipient";
            ViewWidth = 560;
            ViewHeight = 340;
        }

        protected override void OnCreate()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
