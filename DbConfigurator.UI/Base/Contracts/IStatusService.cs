using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface IStatusService
    {
        event EventHandler<bool> StatusChanged;

        Task StartCheckingConnection();
    }
}
