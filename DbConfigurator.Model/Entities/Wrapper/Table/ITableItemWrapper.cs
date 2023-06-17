using DbConfigurator.Model.Entities.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Entities.Wrapper.Table
{
    public interface ITableItemWrapper : ITableItem, INotifyPropertyChanged
    {
        public ITableItem Model { get; }
    }
}
