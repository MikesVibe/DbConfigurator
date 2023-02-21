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
            _model = new DistributionInformation();
        }

        public DistributionInfoLookup(DistributionInformation model)
        {
            _model = model;

            InitializeModel();          
        }

        private void InitializeModel()
        {
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
            if(TO_RecipientsGroup != null)
            {
                TO_RecipientsGroup = Model.ToRecipientsGroup;
            }
            if (CC_RecipientsGroup != null)
            {
                CC_RecipientsGroup = Model.CcRecipientsGroup;
            }

            if (Model.ToRecipientsGroup != null)
            {
                TO = new ObservableCollection<Recipient>();
                
                var To = Model.ToRecipientsGroup;

                if (To != null && To.Recipients != null)
                {
                    var to_lsit = To.Recipients.ToList();
                    foreach (var to in to_lsit)
                    {
                        TO.Add(to);
                    }
                }
            }

            if (Model.CcRecipientsGroup != null)
            {
                CC = new ObservableCollection<Recipient>();

                var Cc = Model.CcRecipientsGroup;

                if (Cc != null && Cc.Recipients != null)
                {
                    var cc_lsit = Cc.Recipients.ToList();
                    foreach (var cc in cc_lsit)
                    {
                        CC.Add(cc);
                    }
                }
            }

        }

        public DistributionInformation Model
        {
            get { return _model; }

           
        }
        public int? Id
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
        public ObservableCollection<Recipient> TO { get; set; }
        public ObservableCollection<Recipient> CC { get; set; }
        
        public RecipientsGroup CC_RecipientsGroup { get; set; }
        public RecipientsGroup TO_RecipientsGroup { get; set; }




        private int? _id;
        private string? _area;
        private int? _areaId;
        private string? _buisnessUnit;
        private int? _buisnessUnitId;
        private string? _country;
        private int? _countryId;
        private string? _priority;
        private int? _priorityId;
        private DistributionInformation _model;
    }
}
