using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected static ObservableCollection<T> EnumerableToObservableCollection<T>(IEnumerable<T> items)
        {
            if (items == null)
                return new ObservableCollection<T>();

            ObservableCollection<T> toReturn = new ObservableCollection<T>();
            foreach (var item in items)
            {
                toReturn.Add(item);
            }
            return toReturn;
        }
    }
}
