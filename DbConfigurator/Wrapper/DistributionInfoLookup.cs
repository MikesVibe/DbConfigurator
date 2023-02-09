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

            var To = model.RecipientsGroup_Collection.Where(g => g.DestinationField.Id == 1).FirstOrDefault();
            var Cc = model.RecipientsGroup_Collection.Where(g => g.DestinationField.Id == 2).FirstOrDefault();
            if (To != null)
                TO = To.Recipients.Select(r => r.Email).ToList();
            if (Cc != null)
                CC = Cc.Recipients.Select(r => r.Email).ToList();

        }

        public void SetNewCountry(Country country)
        {
            Model.Country = country;
            Country = country.Name;
            BuisnessUnit = country.BuisnessUnit.Name;
            Area = country.BuisnessUnit.Area.Name;
        }
        public void SetNewPriority(Priority priority)
        {
            Model.Priority = priority;
            Priority = priority.Name;
        }

        public DistributionInformation Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public int Id { get { return Model.Id; } }
        public string Area
        {
            get { return _model.Country.BuisnessUnit.Area.Name; }

            set
            {
                //_model.Country.BuisnessUnit.Area.Name = value;
                OnPropertyChanged();
            }
        }
        public int AreaId { get { return Model.Country.BuisnessUnit.AreaId; } }
        public string BuisnessUnit
        {
            get { return _model.Country.BuisnessUnit.Name; }

            set
            {
                //_model.Country.BuisnessUnit.Name = value;
                OnPropertyChanged();
            }
        }
        public int BuisnessUnitId { get { return Model.Country.BuisnessUnitId; } }
        public string Country
        {
            get { return _model.Country.Name; }

            set
            {
                //_model.Country.Name = value;
                OnPropertyChanged();
            }
        }
        public int CountryId { get { return Model.CountryId; } }
        public string Priority
        {
            get { return _model.Priority.Name; }

            set
            {
                OnPropertyChanged();
            }
        }
        public int PriorityId { get { return Model.PriorityId; } }
        public ICollection<string> TO { get; set; }
        public ICollection<string> CC { get; set; }


        private DistributionInformation _model;


    }
}
