using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbConfigurator.UI.Extensions
{
    static class ListExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableCollection)
        {
            return new ObservableCollection<T>(enumerableCollection);
        }
    }
}
