using Autofac;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static void AddDataAccessServices(this ContainerBuilder builder)
        {
            builder.RegisterType<DbConfiguratorApiClient>().As<IDbConfiguratorApiClient>().SingleInstance();
            builder.RegisterType<StatusService>().As<IStatusService>().SingleInstance();

        }
    }
}
