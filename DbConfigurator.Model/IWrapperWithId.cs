using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public interface IWrapperWithId
    {
        public int Id { get; }
    }
}
