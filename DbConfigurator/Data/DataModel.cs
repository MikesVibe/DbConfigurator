using DbConfigurator.UI.Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class DataModel : IDataModel
    {
        public DataModel(IDistributionInformationRepository distributionInformationRepository)
        {
            _distributionInformationRepository = distributionInformationRepository;

            LoadData();
        }

        private async void LoadData()
        {
            DistributionInformation = await _distributionInformationRepository.GetAllAsync();
        }



        public IEnumerable<DistributionInformation> DistributionInformation { get; set; }




        private IDistributionInformationRepository _distributionInformationRepository;
    }
}
