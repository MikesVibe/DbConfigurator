using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.Entities.Core;

namespace DbConfigurator.Model.Entities.Wrapper
{
    public class RecipientWrapper : ModelWrapper<Recipient>, IWrapperWithId
    {
        public RecipientWrapper(Recipient model) : base(model)
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
