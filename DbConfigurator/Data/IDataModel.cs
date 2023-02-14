using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;

namespace DbConfigurator.Model
{
    public interface IDataModel
    {
        IEnumerable<DistributionInformation> DistributionInformation { get; set; }
    }
}