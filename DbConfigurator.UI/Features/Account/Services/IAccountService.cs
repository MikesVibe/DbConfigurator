using DbConfigurator.Authentication;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Account.Services
{
    public interface IAccountService
    {
        Task<Result<User>> Login(string userName, string password);
    }
}
