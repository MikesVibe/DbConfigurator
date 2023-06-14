using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DbConfigurator.UI.ViewModel.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

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
