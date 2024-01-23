using DbConfigurator.Core.Contracts;
using DbConfigurator.Core.Models;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class BusinessUnitWrapper : ModelWrapper<BusinessUnit>, IWrapperWithId
    {
        public BusinessUnitWrapper(BusinessUnit model) : base(model)
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
