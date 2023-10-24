﻿using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class CountryWrapper : ModelWrapper<Country>, IWrapperWithId
    {
        public CountryWrapper(Country model) : base(model)
        {
        }
        public int Id
        {
            get { return GetValue<int>(); }
        }
        public string CountryName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CountryCode
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
