using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class DistributionInfoLookup : ViewModelBase
    {

        public DistributionInfoLookup() 
        {
            TO = new ObservableCollection<Recipient>();
            CC = new ObservableCollection<Recipient>();
            Model = new DistributionInformation();


        }

        public DistributionInfoLookup(DistributionInformation model)
        {
            TO = new ObservableCollection<Recipient>();
            CC = new ObservableCollection<Recipient>();
            Model = model;

            InitializeModel();          
        }

        private void InitializeModel()
        {
          

        }

        public DistributionInformation Model
        {
            get { return _model; }
            set
            {
                if (value == null)
                    return;
                _model = value;

                Id = _model.Id;

                if (_model.Country != null)
                {
                    Area = _model.Country.BuisnessUnits.First().Areas.First().Name;
                    AreaId = _model.Country.BuisnessUnits.First().Areas.First().Id;
                    BuisnessUnit = _model.Country.BuisnessUnits.First().Name;
                    BuisnessUnitId = _model.Country.BuisnessUnits.First().Id;
                    Country = _model.Country.Name;
                    CountryId = _model.Country.Id;
                }
                else
                {
                    Area = "";
                    BuisnessUnit = "";
                    Country = "";
                }
                if (_model.Priority != null)
                {
                    Priority = _model.Priority.Name;
                    PriorityId = _model.Priority.Id;
                }
                else
                {
                    Priority = "";
                }

                if (Model.ToRecipientsGroup != null)
                {
                    if (Model.ToRecipientsGroup.Recipients != null)
                        TO = EnumerableToObservableCollection(Model.ToRecipientsGroup.Recipients);
                }

                if (Model.CcRecipientsGroup != null)
                {
                    if (Model.CcRecipientsGroup.Recipients != null)
                        CC = EnumerableToObservableCollection(Model.CcRecipientsGroup.Recipients);
                }


             
            }
        }
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }
        public string? Area
        {
            get { return _area; }
            set
            {
                _area = value;
                OnPropertyChanged();
            }
        }
        public int? AreaId
        {
            get { return _areaId; }
            private set { _areaId = value; }
        }
        public string? BuisnessUnit
        {
            get { return _buisnessUnit; }
            set
            {
                _buisnessUnit = value;
                OnPropertyChanged();
            }
        }
        public int? BuisnessUnitId
        {
            get { return _buisnessUnitId; }
            private set { _buisnessUnitId = value; }
        }
        public string? Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }
        public int? CountryId
        {
            get { return _countryId; }
            private set { _countryId = value; }
        }
        public string? Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged();
            }
        }
        public int? PriorityId
        {
            get { return _priorityId; }
            private set { _priorityId = value; }
        }
        public ObservableCollection<Recipient> TO
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Recipient> CC
        {
            get { return _cc; }
            set
            {
                _cc = value;
                OnPropertyChanged();
            }
        }

        //public RecipientsGroup CC_RecipientsGroup { get; set; }
        //public RecipientsGroup TO_RecipientsGroup { get; set; }




        private int _id;
        private string? _area;
        private int? _areaId;
        private string? _buisnessUnit;
        private int? _buisnessUnitId;
        private string? _country;
        private int? _countryId;
        private string? _priority;
        private int? _priorityId;
        private DistributionInformation _model;
        public ObservableCollection<Recipient> _to;
        public ObservableCollection<Recipient> _cc;

    }
}
