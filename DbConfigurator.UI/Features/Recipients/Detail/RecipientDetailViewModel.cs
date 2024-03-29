﻿using DbConfigurator.Core.Models;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Recipients.Services;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;

namespace DbConfigurator.UI.Features.Recipients
{
    public class RecipientDetailViewModel : DetailViewModelBase<IRecipientService, Recipient, RecipientWrapper>
    {
        public RecipientDetailViewModel(IRecipientService recipientService, IEventAggregator eventAggregator) : base(recipientService, eventAggregator)
        {
            Title = "Recipient";
            ViewWidth = 640;
            ViewHeight = 440;
        }

        protected override void OnCreate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<CreateRecipientEvent>()
                  .Publish(
                new CreateRecipientEventArgs
                {
                    Entity = new Recipient
                    {
                        Id = EntityDto.Id,
                        FirstName = EntityDto.FirstName,
                        LastName = EntityDto.LastName,
                        Email = EntityDto.Email
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
                    Entity = new Recipient
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
