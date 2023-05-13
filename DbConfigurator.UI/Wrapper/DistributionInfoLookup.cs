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

        public DistributionInfoLookup(Area area, BuisnessUnit buisnessUnit, Country country, Priority priority) 
        {
            TO = new ObservableCollection<Recipient>();
            CC = new ObservableCollection<Recipient>();
            Model = new DistributionInformation(area, buisnessUnit, country, priority);


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
                    Area = _model.Area.Name;
                    AreaId = _model.Area.Id;
                    BuisnessUnit = _model.BuisnessUnit.Name;
                    BuisnessUnitId = _model.BuisnessUnit.Id;
                    Country = _model.Country.Name;
                    //CountryId = _model.Country.Id;
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

            
                //if (Model.RecipientGroup != null)
                //{
                //    if (Model.RecipientGroup.RecipientsTo!= null)
                //        TO = EnumerableToObservableCollection(Model.RecipientGroup.RecipientsTo);
                //}

                //if (Model.RecipientGroup != null)
                //{
                //    if (Model.RecipientGroup.RecipientsCc != null)
                //        CC = EnumerableToObservableCollection(Model.RecipientGroup.RecipientsCc);
                //}
            }
        }
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }
        public string Area
        {
            get { return Model.Area.Name; }
            set
            {
                Model.Area.Name = value;
                OnPropertyChanged();
            }
        }
        public int AreaId
        {
            get { return Model.Area.Id; }
            private set { Model.Area.Id = value; }
        }
        public string BuisnessUnit
        {
            get { return Model.BuisnessUnit.Name; }
            set
            {
                Model.BuisnessUnit.Name = value;
                OnPropertyChanged();
            }
        }
        public int BuisnessUnitId
        {
            get { return Model.BuisnessUnit.Id; }
            private set { Model.BuisnessUnit.Id = value; }
        }
        public string Country
        {
            get { return Model.Country.Name; }
            set
            {
                Model.Country.Name = value;
                OnPropertyChanged();
            }
        }
        public int CountryId
        {
            get { return Model.Country.Id; }
            private set { Model.Country.Id = value; }
        }
        public string Priority
        {
            get { return Model.Priority.Name; }
            set
            {
                Model.Priority.Name = value;
                OnPropertyChanged();
            }
        }
        public int PriorityId
        {
            get { return Model.Priority.Id; }
            private set { Model.Priority.Id = value; }
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
