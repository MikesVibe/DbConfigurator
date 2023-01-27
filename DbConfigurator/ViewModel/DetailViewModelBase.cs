using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        public bool HasChanges => throw new NotImplementedException();

        public int Id => throw new NotImplementedException();

        public Task LoadAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
