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

        public async override Task LoadAsync()
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
            Area_Collection = EnumerableToObservableCollection(_dataModel.Areas);
            BuisnessUnit_Collection = EnumerableToObservableCollection(_dataModel.BuisnessUnits);
            Country_Collection = EnumerableToObservableCollection(_dataModel.Countries);
            Priority_Collection = EnumerableToObservableCollection(_dataModel.Priorities);
            RecipientsToComboBox = EnumerableToObservableCollection(_dataModel.Recipients);
            RecipientsCcComboBox = EnumerableToObservableCollection(_dataModel.Recipients);
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }
        protected override bool OnSaveCanExecute()
        {
            return true;
        }
        protected override void OnSaveExecute()
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
                SelectedCountry = Country_Collection?.Where(c => c.Id == SelectedDistributionInformation.CountryId).FirstOrDefault();
                SelectedBuisnessUnit = BuisnessUnit_Collection?.Where(c => c.Id == SelectedDistributionInformation.BuisnessUnitId).FirstOrDefault();
                SelectedArea = Area_Collection?.Where(c => c.Id == SelectedDistributionInformation.AreaId).FirstOrDefault();
                SelectedPriority = Priority_Collection?.Where(c => c.Id == SelectedDistributionInformation.PriorityId).FirstOrDefault();
            }
            else
            {
                SelectedCountry = null;
                SelectedBuisnessUnit = null;
                SelectedArea = null;
                SelectedPriority = null;
            }
        }
        private void SetNewCountry()
        {

            var disInfo = SelectedDistributionInformation.Model;
            disInfo.CountryId = _selectedCountry.Id;
            _dataModel.ReloadEntryCountry(disInfo);

            SelectedDistributionInformation.Model = disInfo;

            SelectedBuisnessUnit = BuisnessUnit_Collection.Where(c => c.Id == SelectedDistributionInformation.BuisnessUnitId).FirstOrDefault();
            SelectedArea = Area_Collection.Where(c => c.Id == SelectedDistributionInformation.AreaId).FirstOrDefault();
        }
        private void SetNewPriority()
        {
            var disInfo = SelectedDistributionInformation.Model;
            disInfo.PriorityId = _selectedPriority.Id;
            _dataModel.ReloadEntryPriority(disInfo);

            SelectedDistributionInformation.Model = disInfo;
        }



        public int DefaultRowIndex { get { return 0; } }


        public DistributionInfoLookup SelectedDistributionInformation
        {
            get { return _selectedDistributionInformation; }
            set
            {
                _selectedDistributionInformation = value;
                OnPropertyChanged();
                if(value != null)
                {
                    TO_Collection = _selectedDistributionInformation.TO;
                    CC_Collection = _selectedDistributionInformation.CC;
                }
            }
        }
        public ObservableCollection<DistributionInfoLookup> DisInfoLookup_ObservableCollection { get; set; }
        public ObservableCollection<Area> Area_Collection { get; set; }
        public ObservableCollection<BuisnessUnit> BuisnessUnit_Collection { get; set; }
        public ObservableCollection<Country> Country_Collection { get; set; }
        public ObservableCollection<Priority> Priority_Collection { get; private set; }

        public ObservableCollection<Recipient> TO_Collection
        {
            get { return _to_Collection; }
            set 
            { 
                _to_Collection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Recipient> CC_Collection
        {
            get { return _cc_Collection; }
            set
            {
                _cc_Collection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Recipient> RecipientsToComboBox
        {
            get { return _recipientsToComboBox; }
            set { _recipientsToComboBox = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Recipient> RecipientsCcComboBox
        {
            get { return _recipientsCcComboBox; }
            set { _recipientsCcComboBox = value; OnPropertyChanged(); }
        }

        public Recipient? SelectedRecipientTo
        {
            get { return _selectedRecipientTo; }
            set 
            {
                if (value == null || SelectedDistributionInformation == null)
                    return;

                _selectedRecipientTo = value;
                //TODO: Add selected recipient to TO_Collection
                TO_Collection.Add(value);
                
            }
        }
        public Recipient? SelectedRecipientCc { get; set; }
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
                if (SelectedDistributionInformation != null && _selectedCountry != null)
                    SetNewCountry();
                OnPropertyChanged();
            }
        }
        public Priority? SelectedPriority
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
        public ICommand SelectionChangedCommand { get; set; }



        private IDataModel _dataModel;
        private IEventAggregator _eventAggregator;
        private DistributionInfoLookup _selectedDistributionInformation;

        private ObservableCollection<Recipient> _to_Collection;
        private ObservableCollection<Recipient> _cc_Collection;
        private ObservableCollection<Recipient> _recipientsToComboBox;
        private ObservableCollection<Recipient> _recipientsCcComboBox;
        private Area? _selectedArea;
        private BuisnessUnit? _selectedBuisnessUnit;
        private Country? _selectedCountry;
        private Priority? _selectedPriority;
        private Recipient? _selectedRecipientTo;
        private Recipient? _selectedRecipientCc;
    }
}
