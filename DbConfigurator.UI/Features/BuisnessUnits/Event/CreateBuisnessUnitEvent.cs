﻿using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateBusinessUnitEvent : PubSubEvent<CreateBusinessUnitEventArgs>
    {
    }
    public class CreateBusinessUnitEventArgs : IEventArgs<BusinessUnit>
    {
        public BusinessUnit Entity { get; set; } = default!;
    }
}
