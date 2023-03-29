using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.UI.Startup;
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
            IDataModel dataModel,
            AutoMapperConfig autoMapperConfig
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;
            AutoMapper = autoMapperConfig;

            DistributionInformation_ObservableCollection = new ObservableCollection<DistributionInformationDtoWrapper>();
            RecipientsTo_ListView = new ObservableCollection<Recipient>();
            RecipientsCc_ListView = new ObservableCollection<Recipient>();


            PopulateComboBoxesWithData();
            SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);
            RemoveToRecipientCommand = new DelegateCommand(OnRemoveRecipientToExecute, OnRemoveRecipientToCanExecute);
            RemoveCcRecipientCommand = new DelegateCommand(OnRemoveRecipientCcExecute, OnRemovRecipientCCeCanExecute);

        }

        public async override Task LoadAsync()
        {
            List<DistributionInformation> distributionInformations = default;
            for (int i = 1; i <= 5; i++)
            {
                distributionInformations = _dataModel.DistributionInformations.ToList();
                if (distributionInformations != null)
                    break;
                await Task.Delay(100 * i);
            }
            if (distributionInformations == null)
            {
                DistributionInformation_ObservableCollection = new ObservableCollection<DistributionInformationDtoWrapper>();
                return;
            }

            var distributionInformationsDto = new ObservableCollection<DistributionInformationDtoWrapper>();

            foreach (var dis in distributionInformations)
            {
                var mapped = AutoMapper.Mapper.Map<DistributionInformationDto>(dis);
                var wrapped = new DistributionInformationDtoWrapper(mapped);
                distributionInformationsDto.Add(wrapped);
            }
            DistributionInformation_ObservableCollection = distributionInformationsDto;

        }

        private void PopulateComboBoxesWithData()
        {
            var areas = EnumerableToObservableCollection(_dataModel.AreasDto);
            Area_Collection = areas;
            var buisnessUnits = EnumerableToObservableCollection(_dataModel.BuisnessUnitsDto);
            BuisnessUnit_Collection = buisnessUnits;
            var countries = EnumerableToObservableCollection(_dataModel.CountriesDto);
            Country_Collection = countries;
            var priorities = EnumerableToObservableCollection(_dataModel.PrioritiesDto);
            Priority_Collection = priorities;      
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
            var distributionInformation = new DistributionInformation(defaultArea, defaultBuisnessUnit, defaultCountry, defaultPriotrity);
            await _dataModel.AddAsync(distributionInformation);
            await _dataModel.SaveChangesAsync();

            //Create New Recipients Group
            RecipientsGroup recipientsGroup = new RecipientsGroup(distributionInformation, "dummy");
            await _dataModel.AddAsync(recipientsGroup);
            distributionInformation.RecipientsGroup = recipientsGroup;
            await _dataModel.SaveChangesAsync();

            var distributionInformationDto = _dataModel.GetDistributionInformationDto(distributionInformation.Id);
            var mappedDisInfo = new DistributionInformationDtoWrapper(distributionInformationDto);
            DistributionInformation_ObservableCollection.Add(mappedDisInfo);
            SelectedDistributionInformation = mappedDisInfo;
        }
        protected override void OnRemoveExecute()
        {
            _dataModel.Remove(_dataModel.DistributionInformations.Where(d => d.Id == SelectedDistributionInformation.Id).First());
            DistributionInformation_ObservableCollection.Remove(SelectedDistributionInformation);
            SelectedDistributionInformation = null;
            ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
        }
        protected override bool OnRemoveCanExecute()
        {
            return SelectedDistributionInformation!=null;
        }
        protected void OnRemoveRecipientToExecute()
        {
            if (SelectedRecipientToListView == null)
                return;

            _dataModel.DistributionInformations.Where(d => d.Id == SelectedDistributionInformation.Id).First()?.RecipientsGroup?.RecipientsTo.Remove(SelectedRecipientToListView);
            SelectedDistributionInformation.RecipientsTo.Remove(SelectedRecipientToListView);
            RecipientsTo_ListView.Remove(SelectedRecipientToListView);
            SelectedRecipientToListView = null;
            ((DelegateCommand)RemoveToRecipientCommand).RaiseCanExecuteChanged();

        }
        protected bool OnRemoveRecipientToCanExecute()
        {
            return SelectedRecipientToListView != null;
        }
        protected void OnRemoveRecipientCcExecute()
        {
            if (SelectedRecipientCcListView == null)
                return;

            _dataModel.DistributionInformations.Where(d => d.Id == SelectedDistributionInformation.Id).First()?.RecipientsGroup?.RecipientsCc.Remove(SelectedRecipientCcListView);
            SelectedDistributionInformation.RecipientsTo.Remove(SelectedRecipientCcListView);
            RecipientsCc_ListView.Remove(SelectedRecipientCcListView);
            SelectedRecipientCcListView = null;
            ((DelegateCommand)RemoveCcRecipientCommand).RaiseCanExecuteChanged();
        }
        protected bool OnRemovRecipientCCeCanExecute()
        {
            return SelectedRecipientCcListView != null;
        }
        private void DistributionInformation_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _dataModel.HasChanges();
            }
            if (e.PropertyName == nameof(DistributionInformationDtoWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        private void OnSelectionChanged()
        {
            if (SelectedDistributionInformation == null)
                return;

            //PopulateComboBoxesWithData();
            var recipients = EnumerableToObservableCollection(_dataModel.Recipients);
            RecipientsToComboBox = EnumerableToObservableCollection(recipients.Where(p => !SelectedDistributionInformation.RecipientsTo.Any(p2 => p2.Id == p.Id)));
            RecipientsCcComboBox = EnumerableToObservableCollection(recipients.Where(p => !SelectedDistributionInformation.RecipientsCc.Any(p2 => p2.Id == p.Id)));

            //Setting selected items in comboBoxes
            SelectedCountry = Country_Collection?.Where(c => c.Id == SelectedDistributionInformation.CountryId).FirstOrDefault();
            SelectedBuisnessUnit = BuisnessUnit_Collection?.Where(c => c.Id == SelectedDistributionInformation.BuisnessUnitId).FirstOrDefault();
            SelectedArea = Area_Collection?.Where(c => c.Id == SelectedDistributionInformation.AreaId).FirstOrDefault();
            SelectedPriority = Priority_Collection?.Where(c => c.Id == SelectedDistributionInformation.PriorityId).FirstOrDefault();

            //Setting Items in ListViews
            RecipientsTo_ListView = EnumerableToObservableCollection(SelectedDistributionInformation.RecipientsTo);
            RecipientsCc_ListView = EnumerableToObservableCollection(SelectedDistributionInformation.RecipientsCc);


            ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
        }
        private void SetNewCountry()
        {
            //if (SelectedDistributionInformationDtoWrapper == null || SelectedCountry == null)
            //    return;
            //var disInfo = SelectedDistributionInformationDtoWrapper.Model;

            ////Change countryId and get country from database
            //disInfo.CountryId = SelectedCountry.Id;
            //_dataModel.ReloadEntryCountry(disInfo);

            ////Assign it back to the model
            //SelectedDistributionInformationDtoWrapper.Model = disInfo;



        }
        private void SetNewPriority()
        {
            //var disInfo = SelectedDistributionInformationDtoWrapper;
            //disInfo.PriorityId = _selectedPriority.Id;
            //_dataModel.ReloadEntryPriority(disInfo);

            //SelectedDistributionInformationDtoWrapper = disInfo;
        }



        public int DefaultRowIndex { get { return 0; } }


        //public DistributionInfoLookup SelectedDistributionInformationDtoWrapper
        //{
        //    get { return _selectedDistributionInformation; }
        //    set
        //    {
        //        _selectedDistributionInformation = value;
        //        OnPropertyChanged();
        //    }
        //}
        public DistributionInformationDtoWrapper SelectedDistributionInformation
        {
            get { return _selectedDistributionInformation; }
            set
            {
                _selectedDistributionInformation = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AreaDto> Area_Collection { get; set; }
        public ObservableCollection<BuisnessUnitDto> BuisnessUnit_Collection { get; set; }
        public ObservableCollection<CountryDto> Country_Collection { get; set; }
        public ObservableCollection<PriorityDto> Priority_Collection { get; private set; }

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


        //public ObservableCollection<DistributionInfoLookup> DisInfoLookup_ObservableCollection { get; set; }
        public ObservableCollection<DistributionInformationDtoWrapper> DistributionInformation_ObservableCollection { get; set; }
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


        public Recipient? SelectedRecipientToComboBox
        {
            get { return _selectedRecipientToComboBox; }
            set 
            {
                if (value == null || SelectedDistributionInformation == null)
                    return;

                _selectedRecipientToComboBox = value;
                RecipientsTo_ListView.Add(value);
                SelectedDistributionInformation.RecipientsTo.Add(value);
                var disInfo = _dataModel.DistributionInformations.Where(d => d.Id == SelectedDistributionInformation.Id).First();
                disInfo?.RecipientsGroup?.RecipientsTo.Add(_dataModel.GetRecipient(value.Id));
                RecipientsToComboBox.Remove(value);
                _selectedRecipientToComboBox = null;
            }
        }
        public Recipient? SelectedRecipientCcComboBox
        {
            get { return _selectedRecipientCcComboBox; }
            set
            {
                if (value == null || SelectedDistributionInformation == null)
                    return;

                _selectedRecipientCcComboBox = value;
                RecipientsCc_ListView.Add(value);
                SelectedDistributionInformation.RecipientsCc.Add(value);
                var disInfo = _dataModel.DistributionInformations.Where(d => d.Id == SelectedDistributionInformation.Id).First();
                disInfo?.RecipientsGroup?.RecipientsCc.Add(_dataModel.GetRecipient(value.Id));
                RecipientsCcComboBox.Remove(value);
                _selectedRecipientCcComboBox = null;

            }
        }
        public Recipient? SelectedRecipientToListView
        {
            get { return _selectedRecipientToListView; }
            set
            {
                _selectedRecipientToListView = value;
                ((DelegateCommand)RemoveToRecipientCommand).RaiseCanExecuteChanged();
            }
        }
        public Recipient? SelectedRecipientCcListView
        {
            get { return _selectedRecipientCcListView; }
            set
            {
                _selectedRecipientCcListView = value;
                ((DelegateCommand)RemoveCcRecipientCommand).RaiseCanExecuteChanged();
            }
        }
        public AreaDto? SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                _selectedArea = value;
                OnPropertyChanged();
            }
        }
        public BuisnessUnitDto? SelectedBuisnessUnit
        {
            get { return _selectedBuisnessUnit; }
            set
            {
                _selectedBuisnessUnit = value;
                OnPropertyChanged();
            }
        }
        public CountryDto? SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {

                _selectedCountry = value;
                SetNewCountry();
                OnPropertyChanged();
            }
        }
        public PriorityDto? SelectedPriority
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
        public ICommand RemoveCcRecipientCommand { get; set; }
        public ICommand RemoveToRecipientCommand { get; set; }



        private IDataModel _dataModel;

        public AutoMapperConfig AutoMapper { get; }

        private IEventAggregator _eventAggregator;
        private DistributionInformationDtoWrapper _selectedDistributionInformation;

        private ObservableCollection<Recipient> _recipientsTo_ListView;
        private ObservableCollection<Recipient> _recipientsCc_ListView;

        private ObservableCollection<Recipient> _recipientsToComboBox;
        private ObservableCollection<Recipient> _recipientsCcComboBox;
        private AreaDto? _selectedArea;
        private BuisnessUnitDto? _selectedBuisnessUnit;
        private CountryDto? _selectedCountry;
        private PriorityDto? _selectedPriority;
        private Recipient? _selectedRecipientToComboBox;
        private Recipient? _selectedRecipientCcComboBox;
        private Recipient? _selectedRecipientToListView;
        private Recipient? _selectedRecipientCcListView;
    }
}
