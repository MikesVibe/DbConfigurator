using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Errors
{
    public class AuthorizationError : IError
    {
        public List<IError> Reasons => throw new NotImplementedException();

        public string Message => "User is unauthorized to access this data.";

        public Dictionary<string, object> Metadata => throw new NotImplementedException();
    }
}
