﻿using DbConfigurator.Core.Contracts;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface IEventArgs<T>
        where T : IEntity
    {
        T Entity { get; set; }
    }
}