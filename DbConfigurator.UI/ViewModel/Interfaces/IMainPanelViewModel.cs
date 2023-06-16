using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    public interface IMainPanelViewModel
    {
        int Id { get; }
     
        Task LoadAsync();
    }
}
