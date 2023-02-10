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

        public DistributionInformation Model
        {
            get { return _model; }
            set 
            { 
                _model = value;
                OnPropertyChanged(nameof(Priority));
                OnPropertyChanged(nameof(Country));
                OnPropertyChanged(nameof(BuisnessUnit));
                OnPropertyChanged(nameof(Area));
            }
        }
        public int Id { get { return Model.Id; } }
        public string Area
        {
            get { return _model.Country.BuisnessUnits.First().Areas.First().Name; }
            set
            {
                //_model.Country.BuisnessUnit.Area.Name = value;
                OnPropertyChanged();
            }
        }
        public int AreaId { get { return Model.Country.BuisnessUnits.First().Areas.First().Id; } }
        public string BuisnessUnit
        {
            get { return _model.Country.BuisnessUnits.First().Name; }

            set
            {
                //_model.Country.BuisnessUnit.Name = value;
                OnPropertyChanged();
            }
        }
        public int BuisnessUnitId { get { return Model.Country.BuisnessUnits.First().Id; } }
        public string Country
        {
            get { return Model.Country.Name; }
            set
            {
                OnPropertyChanged();
            }
        }
        public int CountryId
        {
            get { return Model.CountryId; }
            set { Model.CountryId = value; }
        }
        public string Priority
        {
            get { return Model.Priority.Name; }
            set
            {
                OnPropertyChanged();
            }
        }
        public int PriorityId 
        { 
            get { return Model.PriorityId; } 
            set { Model.PriorityId = value; }
        }
        public ICollection<string> TO { get; set; }
        public ICollection<string> CC { get; set; }


        private DistributionInformation _model;


    }
}
