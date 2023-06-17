using DbConfigurator.Model.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Entities.Wrapper
{
    public interface IWrapper<T>
    {
        public T Model { get; }
    }
}
