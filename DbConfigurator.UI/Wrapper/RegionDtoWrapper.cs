using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class RegionWrapper : ModelWrapper<Region>, IWrapperWithId
    {
        public RegionWrapper(Region model) : base(model)
        {
        }
        public int Id
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public Area Area
        {
            get { return GetValue<Area>(); }
            set
            {
                SetValue(value);
            }
        }
        public BusinessUnit BusinessUnit
        {
            get { return GetValue<BusinessUnit>(); }
            set
            {
                SetValue(value);
            }
        }
        public Country Country
        {
            get { return GetValue<Country>(); }
            set
            {
                SetValue(value);
            }
        }
    }
}
