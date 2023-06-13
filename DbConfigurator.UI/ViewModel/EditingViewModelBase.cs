using DbConfigurator.UI.View.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class EditingViewModelBase : ViewModelBase, IEditingViewModel
    {
        public Action<bool> CloseAction { get; set; }

    }
}
