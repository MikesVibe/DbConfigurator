﻿using DbConfigurator.Core.Contracts;
using DbConfigurator.Core.Models;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class AreaWrapper : ModelWrapper<Area>, IWrapperWithId
    {
        public AreaWrapper(Area model) : base(model)
        {
        }
        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
