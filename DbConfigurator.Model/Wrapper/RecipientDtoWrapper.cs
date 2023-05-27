using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Wrapper
{
    public class RecipientDtoWrapper : ModelWrapper<RecipientDto>
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
