using Autofac;
using DbConfigurator.Core.Contracts;
using DbConfigurator.DataAccess;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Account.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Authentication
{
    public static class AuthenticationServiceRegistration
    {
        public static void AddAuthenticationServices(this ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            builder.RegisterType<SecuritySettings>().As<ISecuritySettings>().SingleInstance();
        }
    }
}
