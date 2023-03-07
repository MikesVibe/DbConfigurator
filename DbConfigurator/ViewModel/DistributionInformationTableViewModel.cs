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
            RecipientsTo_ListView = new ObservableCollection<Recipient>();
            RecipientsCc_ListView = new ObservableCollection<Recipient>();



            SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);

            PopulateComboBoxesWithData();
        }

        public async override Task LoadAsync()
        {
            var distributionInformations = _dataModel.DistributionInformations;
            var distributionInformationsLookup = new ObservableCollection<DistributionInfoLookup>();


            foreach (var dis in distributionInformations)
            {
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
        protected async override void OnAddExecute()
        {
            //Create New Distribution Infrotmaion
            Area defaultArea = _dataModel.DefaultArea;
            BuisnessUnit defaultBuisnessUnit = _dataModel.DefaultBuisnessUnit;
            Country defaultCountry = _dataModel.DefaultCountry;
            Priority defaultPriotrity = _dataModel.DefaultPriority;
            var distributionInfoLookup = new DistributionInfoLookup(defaultArea, defaultBuisnessUnit, defaultCountry, defaultPriotrity);
            await _dataModel.AddAsync(distributionInfoLookup.Model);
            await _dataModel.SaveChangesAsync();
            await _dataModel.ReloadEntityAsync(distributionInfoLookup.Model);

            //Create New Recipients Group
            RecipientsGroup recipientsGroup = new RecipientsGroup(distributionInfoLookup.Model, "dummy");
            await _dataModel.AddAsync(recipientsGroup);
            //_dataModel.SaveChanges();

            distributionInfoLookup.Model.RecipientsGroup = recipientsGroup;
            await _dataModel.SaveChangesAsync();


            DisInfoLookup_ObservableCollection.Add(distributionInfoLookup);
            SelectedDistributionInformation = distributionInfoLookup;
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
            try
            {
                //Setting selected items in comboBoxes
                SelectedCountry = Country_Collection?.Where(c => c.Id == SelectedDistributionInformation.CountryId).FirstOrDefault();
                SelectedBuisnessUnit = BuisnessUnit_Collection?.Where(c => c.Id == SelectedDistributionInformation.BuisnessUnitId).FirstOrDefault();
                SelectedArea = Area_Collection?.Where(c => c.Id == SelectedDistributionInformation.AreaId).FirstOrDefault();
                SelectedPriority = Priority_Collection?.Where(c => c.Id == SelectedDistributionInformation.PriorityId).FirstOrDefault();

                //Setting Items in ListViews
                RecipientsTo_ListView = _selectedDistributionInformation.TO;
                RecipientsCc_ListView = _selectedDistributionInformation.CC;
            }
            catch (ArgumentNullException ex) 
            {
                SelectedCountry = null;
                SelectedBuisnessUnit = null;
                SelectedArea = null;
                SelectedPriority = null;

                RecipientsTo_ListView = null;
                RecipientsCc_ListView = null;
            }

        }
        private void SetNewCountry()
        {
            if (SelectedDistributionInformation == null || SelectedCountry == null)
                return;
            var disInfo = SelectedDistributionInformation.Model;

            //Change countryId and get country from database
            disInfo.CountryId = SelectedCountry.Id;
            _dataModel.ReloadEntryCountry(disInfo);

            //Assign it back to the model
            SelectedDistributionInformation.Model = disInfo;

            //Adjust selected values in comboboxes
            SelectedBuisnessUnit = BuisnessUnit_Collection.FirstOrDefault(c => c.Id == SelectedDistributionInformation.BuisnessUnitId);
            SelectedArea = Area_Collection.FirstOrDefault(c => c.Id == SelectedDistributionInformation.AreaId);
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
            }
        }
        public ObservableCollection<DistributionInfoLookup> DisInfoLookup_ObservableCollection { get; set; }
        public ObservableCollection<Area> Area_Collection { get; set; }
        public ObservableCollection<BuisnessUnit> BuisnessUnit_Collection { get; set; }
        public ObservableCollection<Country> Country_Collection { get; set; }
        public ObservableCollection<Priority> Priority_Collection { get; private set; }

        public ObservableCollection<Recipient> RecipientsTo_ListView
        {
            get { return _recipientsTo_ListView; }
            set 
            { 
                _recipientsTo_ListView = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Recipient> RecipientsCc_ListView
        {
            get { return _recipientsCc_ListView; }
            set
            {
                _recipientsCc_ListView = value;
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
                RecipientsTo_ListView.Add(value);

                var disInfo = SelectedDistributionInformation.Model;
                disInfo.RecipientsGroup.RecipientsTo.Add(value);
            }
        }
        public Recipient? SelectedRecipientCc
        {
            get { return _selectedRecipientCc; }
            set
            {
                if (value == null || SelectedDistributionInformation == null)
                    return;

                _selectedRecipientCc = value;
                RecipientsCc_ListView.Add(value);

                var disInfo = SelectedDistributionInformation.Model;
                disInfo.RecipientsGroup.RecipientsCc.Add(value);
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

        private ObservableCollection<Recipient> _recipientsTo_ListView;
        private ObservableCollection<Recipient> _recipientsCc_ListView;
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
