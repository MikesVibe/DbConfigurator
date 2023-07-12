using DbConfigurator.Model.DTOs.Core;

namespace DbConfigurator.Model.Entities.Wrapper
{
    public class RecipientDtoWrapper : ModelWrapper<RecipientDto>, IWrapperWithId
    {
        public RecipientDtoWrapper(RecipientDto model) : base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }
        }
        public string FirstName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
        public string LastName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
        public string Email
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

    }
}
