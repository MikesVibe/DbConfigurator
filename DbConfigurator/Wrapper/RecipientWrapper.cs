using DbConfigurator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Wrapper
{
    public class RecipientWrapper : ModelWrapper<Recipient>
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
                SetValue<string>(value);
            }
        }
        public string LastName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }
        public string Email
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }

    }
}
