using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }

        public string DisplayMember
        {
            get { return _displayMember; }
            set 
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; }

        private string _displayMember;
        private string _detailViewModelName;

    }
}
