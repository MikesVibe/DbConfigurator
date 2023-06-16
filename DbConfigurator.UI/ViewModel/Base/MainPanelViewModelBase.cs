using DbConfigurator.UI.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class MainPanelViewModelBase : IMainPanelViewModel
    {
        public bool HasChanges { get; set; }
        public int Id { get; set; }

        public abstract Task LoadAsync();
    }
}
