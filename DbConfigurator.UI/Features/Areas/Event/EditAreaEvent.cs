﻿using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditAreaEvent : PubSubEvent<EditAreaEventArgs>
    {
    }
    public class EditAreaEventArgs : IEventArgs<Area>
    {
        public Area Entity { get; set; } = default!;
    }
}
