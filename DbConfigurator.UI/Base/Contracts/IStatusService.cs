using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface IStatusService
    {
        bool IsConnected { get; set; }

        event EventHandler<bool> StatusChanged;

        Task StartCheckingConnection();
    }
}
