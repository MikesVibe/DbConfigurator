using DbConfigurator.Model.Entities.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
