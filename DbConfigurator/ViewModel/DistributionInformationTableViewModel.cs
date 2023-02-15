using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.View;
using DbConfigurator.UI.Wrapper;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.VisualBasic;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public class DistributionInformationTableViewModel : TableViewModelBase, IDistributionInformationTableView
    {
        public DistributionInformationTableViewModel(
            IEventAggregator eventAggregator,
            IDataModel dataModel
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;

            DisInfoLookup_ObservableCollection = new ObservableCollection<DistributionInfoLookup>();

            SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);

            PopulateComboBoxesWithData();
        }

        public override async Task LoadAsync()
        {
            var distributionInformations = _dataModel.DistributionInformations;
            var distributionInformationsLookup = new ObservableCollection<DistributionInfoLookup>();

            //foreach (var dis in DisInfoLookup_ObservableCollection)
            //{
            //    dis.PropertyChanged -= DistributionInformation_ObservableCollection_PropertyChanged;
            //    dis.Priority.PropertyChanged -= DistributionInformation_ObservableCollection_PropertyChanged;

            //}
            //DisInfoLookup_ObservableCollection.Clear();

            foreach (var dis in distributionInformations)
            {
                //var wrapper = new DistributionInformationWrapper(dis);
                //wrapper.PropertyChanged += DistributionInformation_ObservableCollection_PropertyChanged;
                //wrapper.Priority.PropertyChanged += DistributionInformation_ObservableCollection_PropertyChanged;
                //distributionInformationsLookup.Add(wrapper);
                distributionInformationsLookup.Add(new DistributionInfoLookup(dis));
            }
            DisInfoLookup_ObservableCollection = distributionInformationsLookup;


        }

        private void PopulateComboBoxesWithData()
        {
            Area_Collection = new ObservableCollection<Area>();
            BuisnessUnit_Collection = new ObservableCollection<BuisnessUnit>();
            Country_Collection = new ObservableCollection<Country>();
            Priority_Collection = new ObservableCollection<PriorityWrapper>();

            var areas = _dataModel.Areas;
            foreach (var area in areas)
            {
                Area_Collection.Add(area);
            }

            var buisnessUnits = _dataModel.BuisnessUnits;
            foreach (var bu in buisnessUnits)
            {
                BuisnessUnit_Collection.Add(bu);
            }
            var countries = _dataModel.Countries;
            foreach (var country in countries)
            {
                Country_Collection.Add(country);
            }
            var priorities = _dataModel.Priorities;
            foreach (var priority in priorities)
            {
                Priority_Collection.Add(new PriorityWrapper(priority));
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }
        protected override bool OnSaveCanExecute()
        {
            return true;
        }
        protected async override void OnSaveExecute()
        {
            _dataModel.SaveChangesAsync();
            HasChanges = _dataModel.HasChanges();

        }
        protected override void OnAddExecute()
        {
            var disInfo = new DistributionInformation();
            var disInfoLookup = new DistributionInfoLookup(disInfo);
            DisInfoLookup_ObservableCollection.Add(disInfoLookup);

            _dataModel.Add(disInfo);
        }
        protected override void OnRemoveExecute()
        {
            throw new NotImplementedException();
        }
        protected override bool OnRemoveCanExecute()
        {
            return false;
        }
        private void DistributionInformation_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _dataModel.HasChanges();
            }
            if (e.PropertyName == nameof(DistributionInformationWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        private void OnSelectionChanged()
        {
            if (SelectedDistributionInformation != null)
            {
                SelectedCountry = Country_Collection.Where(c => c.Id == SelectedDistributionInformation.CountryId).FirstOrDefault();
                SelectedBuisnessUnit = BuisnessUnit_Collection.Where(c => c.Id == SelectedDistributionInformation.BuisnessUnitId).FirstOrDefault();
                SelectedArea = Area_Collection.Where(c => c.Id == SelectedDistributionInformation.AreaId).FirstOrDefault();
                SelectedPriority = Priority_Collection.Where(c => c.Id == SelectedDistributionInformation.PriorityId).FirstOrDefault();
            }
            else
            {
                SelectedCountry = null;
                SelectedBuisnessUnit = null;
                SelectedArea = null;
                SelectedPriority = null;
            }

        }

        


        public Area? SelectedArea
        {
            get { return _selectedArea; }
            set 
            { 
                _selectedArea = value;
                OnPropertyChanged();
            }
        }
        public BuisnessUnit? SelectedBuisnessUnit
        {
            get { return _selectedBuisnessUnit; }
            set
            {
                _selectedBuisnessUnit = value;
                OnPropertyChanged();
            }
        }
        public Country? SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {

                _selectedCountry = value;
                if(SelectedDistributionInformation != null && _selectedCountry != null)
                    SetNewCountry();
                OnPropertyChanged();
            }
        }
        public PriorityWrapper? SelectedPriority
        {
            get { return _selectedPriority; }
            set
            {
                _selectedPriority = value;
                if (SelectedDistributionInformation != null && _selectedPriority != null)
                    SetNewPriority();
                OnPropertyChanged();

            }
        }
        private void SetNewCountry()
        {
            
            var disInfo = SelectedDistributionInformation.Model;
            disInfo.CountryId = _selectedCountry.Id;
            _dataModel.ReloadEntryCountry(disInfo);

            SelectedDistributionInformation.Model = disInfo;

            var buisnessUnit = BuisnessUnit_Collection.Where(b => b.Id == SelectedDistributionInformation.BuisnessUnitId).FirstOrDefault();
            if (buisnessUnit != null)
                SelectedBuisnessUnitIndex = buisnessUnit.Id - 1;

            var area = Area_Collection.Where(a => a.Id == SelectedDistributionInformation.AreaId).FirstOrDefault();
            if (area != null)
                SelectedAreaIndex = area.Id - 1;
        }
        private void SetNewPriority()
        {
            var disInfo = SelectedDistributionInformation.Model;
            disInfo.PriorityId = _selectedPriority.Id;
            _dataModel.ReloadEntryPriority(disInfo);

            SelectedDistributionInformation.Model = disInfo;
        }



        public int DefaultRowIndex { get { return 0; } }
        public int SelectedAreaIndex
        {
            get { return _selectedAreaIndex; }
            set 
            {
                _selectedAreaIndex = value;
                OnPropertyChanged();
            }
        }
        public int SelectedBuisnessUnitIndex
        {
            get { return _selectedBuisnessUnitIndex; }
            set
            {
                _selectedBuisnessUnitIndex = value;
                OnPropertyChanged();
            }
        }
        public int SelectedcCountryIndex
        {
            get { return _selectedcCountryIndex; }
            set
            {
                _selectedcCountryIndex = value;
                OnPropertyChanged();
            }
        }
        public int SelectedcPriorityIndex
        {
            get { return _selectedcPriorityIndex; }
            set
            {
                _selectedcPriorityIndex = value;
                OnPropertyChanged();
            }
        }
        public DistributionInfoLookup SelectedDistributionInformation
        {
            get { return _selectedDistributionInformation; }
            set
            {
                _selectedDistributionInformation = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DistributionInfoLookup> DisInfoLookup_ObservableCollection { get; set; }
        public ObservableCollection<Area> Area_Collection { get; set; }
        public ObservableCollection<BuisnessUnit> BuisnessUnit_Collection { get; set; }
        public ObservableCollection<Country> Country_Collection { get; set; }
        public ObservableCollection<PriorityWrapper> Priority_Collection { get; private set; }
        public ICommand SelectionChangedCommand { get; set; }



        private IDataModel _dataModel;
        private IEventAggregator _eventAggregator;
        private DistributionInfoLookup _selectedDistributionInformation;
        private int _selectedAreaIndex = -1;
        private int _selectedBuisnessUnitIndex = -1;
        private int _selectedcCountryIndex = -1;
        private int _selectedcPriorityIndex = -1;

        private Area? _selectedArea;
        private BuisnessUnit? _selectedBuisnessUnit;
        private Country? _selectedCountry;
        private PriorityWrapper? _selectedPriority;
    }
}
