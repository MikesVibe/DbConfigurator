﻿using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
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
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<CreateRecipientEvent>()
                  .Publish(
                new CreateRecipientEventArgs
                {
                    Recipient = new RecipientDto
                    {
                        Id = EntityDto.Id,
                        FirstName = EntityDto.FirstName,
                        LastName = EntityDto.LastName,
                        Email= EntityDto.Email
                    }
                });
        }

        protected override void OnUpdate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<EditRecipientEvent>()
                  .Publish(
                new EditRecipientEventArgs
                {
                    Recipient = new RecipientDto
                    {
                        Id = EntityDto.Id,
                        FirstName = EntityDto.FirstName,
                        LastName = EntityDto.LastName,
                        Email = EntityDto.Email
                    }
                });
        }
    }
}