using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI
{
    public class DistributionInfoLookup : ViewModelBase
    {
        public DistributionInfoLookup(DistributionInformation model)
        {
            _model = model;

            Id = model.Id;
            Area = model.Country.BuisnessUnit.Area.Name;
            AreaId = model.Country.BuisnessUnit.Area.Id;
            BuisnessUnit = model.Country.BuisnessUnit.Name;
            BuisnessUnitId = model.Country.BuisnessUnit.Id;
            Country = model.Country.Name;
            CountryId = model.Country.Id;
            Priority = model.Priority.Name;
            PriorityId = model.Priority.Id;

            var To = model.RecipientsGroup_Collection.Where(g => g.DestinationField.Id == 1).FirstOrDefault();
            var Cc = model.RecipientsGroup_Collection.Where(g => g.DestinationField.Id == 2).FirstOrDefault();
            if (To != null)
                TO = To.Recipients.Select(r => r.Email).ToList();
            if (Cc != null)
                CC = Cc.Recipients.Select(r => r.Email).ToList();

        }

        public DistributionInformation Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public int Id { get; set; }
        public string Area
        {
            get { return _model.Country.BuisnessUnit.Area.Name; }

            set
            {
                //_model.Country.BuisnessUnit.Area.Name = value;
                OnPropertyChanged();
            }
        }
        public int AreaId { get; set; }
        public string BuisnessUnit
        {
            get { return _model.Country.BuisnessUnit.Name; }

            set
            {
                //_model.Country.BuisnessUnit.Name = value;
                OnPropertyChanged();
            }
        }
        public int BuisnessUnitId { get; set; }
        public string Country
        {
            get { return _model.Country.Name; }

            set
            {
                //_model.Country.Name = value;
                OnPropertyChanged();
            }
        }
        public int CountryId 
        { 
            get { return Model.CountryId; }
            
            set 
            {
                Model.CountryId = value;
                OnPropertyChanged();
            }
        }
        public string Priority
        {
            get { return _model.Priority.Name; }

            set
            {
                //_model.Country.BuisnessUnit.Name = value;
                OnPropertyChanged();
            }
        }
        public int PriorityId { get; set; }
        public ICollection<string> TO { get; set; }
        public ICollection<string> CC { get; set; }


        private DistributionInformation _model;

    }
}
