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
        private readonly object balanceLock = new object();
        private bool SettingPriorityExecuting = false;
        private bool SettingCountryExecuting = false;

        public DistributionInformationTableViewModel(
            IEventAggregator eventAggregator,
            IDataModel dataModel,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;
            AutoMapper = autoMapper;

            DistributionInformation_ObservableCollection = new ObservableCollection<DistributionInformationDtoWrapper>();
            RecipientsTo_ListView = new ObservableCollection<RecipientDto>();
            RecipientsCc_ListView = new ObservableCollection<RecipientDto>();


            PopulateComboBoxesWithData();
            SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);
            RemoveToRecipientCommand = new DelegateCommand(OnRemoveRecipientToExecuteAsync, OnRemoveRecipientToCanExecute);
            RemoveCcRecipientCommand = new DelegateCommand(OnRemoveRecipientCcExecuteAsync, OnRemovRecipientCCeCanExecute);
            PriorityChangedCommand = new DelegateCommand(SetNewPriority);
            CountryChangedCommand = new DelegateCommand(SetNewCountry);

        }

        public async override Task LoadAsync()
        {
            DistributionInformation_ObservableCollection = new ObservableCollection<DistributionInformationDtoWrapper>();
            var distributionInformation = await _dataModel.GetAllDistributionInformationAsync();


            foreach (var dis in distributionInformation)
            {
                var mapped = AutoMapper.Mapper.Map<DistributionInformationDto>(dis);
                var wrapped = new DistributionInformationDtoWrapper(mapped);
                DistributionInformation_ObservableCollection.Add(wrapped);
            }
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



            var distributionInformationEntity = await _dataModel.GetDistributionInformationByIdAsync(distributionInformation.Id);
            var distributionInformationDto = AutoMapper.Mapper.Map<DistributionInformationDto>(distributionInformationEntity);
            var wrappedDisInfo = new DistributionInformationDtoWrapper(distributionInformationDto);

            DistributionInformation_ObservableCollection.Add(wrappedDisInfo);
            SelectedDistributionInformation = wrappedDisInfo;
        }
        protected async override void OnRemoveExecute()
        {
            var distributionInformationToRemove = await _dataModel.GetDistributionInformationByIdAsync(SelectedDistributionInformation.Id);
            _dataModel.Remove(distributionInformationToRemove);
            //_dataModel.Remove(_dataModel.DistributionInformations.Where(d => d.Id == SelectedDistributionInformation.Id).First());
            var deletedDistributionInfo = SelectedDistributionInformation;
            await _dataModel.SaveChangesAsync();
            
            DistributionInformation_ObservableCollection.Remove(SelectedDistributionInformation);
            SelectedDistributionInformation = null;
            RecipientsTo_ListView.Clear();
            RecipientsCc_ListView.Clear();

            ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();

        }
        protected override bool OnRemoveCanExecute()
        {
            return SelectedDistributionInformation != null;
        }
        protected async void OnRemoveRecipientToExecuteAsync()
        {
            if (SelectedRecipientToListView == null)
                return;
            var disInfo = await _dataModel.GetDistributionInformationByIdAsync(SelectedDistributionInformation.Id);
            var recipientToRemove = disInfo?.RecipientsTo.Where(r => r.Id == SelectedRecipientToListView.Id).First();
            disInfo?.RecipientsTo.Remove(recipientToRemove!);
            SelectedDistributionInformation.RecipientsTo.Remove(SelectedRecipientToListView);
            RecipientsTo_ListView.Remove(SelectedRecipientToListView);
            PopulateComboBoxTo();
            SelectedRecipientToListView = null;
            ((DelegateCommand)RemoveToRecipientCommand).RaiseCanExecuteChanged();

        }
        protected bool OnRemoveRecipientToCanExecute()
        {
            return SelectedRecipientToListView != null;
        }
        protected async void OnRemoveRecipientCcExecuteAsync()
        {
            if (SelectedRecipientCcListView == null)
                return;

            var disInfo = await _dataModel.GetDistributionInformationByIdAsync(SelectedDistributionInformation.Id);
            var recipientToRemove = disInfo?.RecipientsCc.Where(r => r.Id == SelectedRecipientCcListView.Id).First();
            disInfo?.RecipientsCc.Remove(recipientToRemove!);
            SelectedDistributionInformation.RecipientsCc.Remove(SelectedRecipientCcListView);
            RecipientsCc_ListView.Remove(SelectedRecipientCcListView);
            PopulateComboBoxCc();
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
                //((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        private void OnSelectionChanged()
        {
            if (SelectedDistributionInformation == null)
                return;

            PopulateComboBoxTo();
            PopulateComboBoxCc();

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

        private void PopulateComboBoxTo()
        {
            var allRecipients = _dataModel.RecipientsDto;
            var recipientsDtoAfterFiltration = allRecipients.Where(p => !SelectedDistributionInformation.RecipientsTo.Any(p2 => p2.Id == p.Id)).ToList();
            RecipientsToComboBox = EnumerableToObservableCollection(recipientsDtoAfterFiltration);
        }
        private void PopulateComboBoxCc()
        {
            var recipients = EnumerableToObservableCollection(_dataModel.RecipientsDto);
            RecipientsCcComboBox = EnumerableToObservableCollection(recipients.Where(p => !SelectedDistributionInformation.RecipientsCc.Any(p2 => p2.Id == p.Id)));
        }

        private async void SetNewCountry()
        {
            if (SelectedDistributionInformation == null || SelectedCountry == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            SettingCountryExecuting = true;

            //Get distribution information entity from database
            var disInfoDtoWrapper = SelectedDistributionInformation;
            var distributionInfoEntity = await _dataModel.GetDistributionInformationByIdAsync(disInfoDtoWrapper.Id);

            //Change countryId for entity, save changes to database and reload data in distributionInfoEntity
            distributionInfoEntity.CountryId = SelectedCountry.Id;
            await _dataModel.SaveChangesAsync();
            distributionInfoEntity = await _dataModel.GetDistributionInformationByIdAsync(disInfoDtoWrapper.Id);

            //Map entity to DTO
            var disInfoMapped = AutoMapper.Mapper.Map<DistributionInformationDto>(distributionInfoEntity);

            int buisnessUnitId = 0;
            int areaId = 0;
            //Get Area and BuisnessUnit which are conected with country
            if (_dataModel.IsDefaultCountry(disInfoMapped.CountryId))
            {
                buisnessUnitId = _dataModel.DefaultBuisnessUnit.Id;
                areaId = _dataModel.DefaultArea.Id;
            }
            else
            {
                buisnessUnitId = distributionInfoEntity.Country.BuisnessUnits.First().Id;
                areaId = distributionInfoEntity.Country.BuisnessUnits.First().Areas.First().Id;
            }

            //Assign Area and BuisnessUnit to DistributionInformation Entity and save changes to databse
            distributionInfoEntity.AreaId = areaId;
            distributionInfoEntity.BuisnessUnitId = buisnessUnitId;
            await _dataModel.SaveChangesAsync();

            //Select proper BuisnessUnit and Area in comboBoxes
            SelectedBuisnessUnit = BuisnessUnit_Collection.Where(b => b.Id == buisnessUnitId).First();
            SelectedArea = Area_Collection.Where(a => a.Id == areaId).First();


            //Assign Changed properties to SelectedDistributionInformation variable
            SelectedDistributionInformation.Country = SelectedCountry.CountryName;
            SelectedDistributionInformation.CountryId = SelectedCountry.Id;

            SelectedDistributionInformation.Area = SelectedArea.Name;
            SelectedDistributionInformation.AreaId = SelectedArea.Id;

            SelectedDistributionInformation.BuisnessUnit = SelectedBuisnessUnit.Name;
            SelectedDistributionInformation.BuisnessUnitId = SelectedBuisnessUnit.Id;


            SettingCountryExecuting = false;
        }
        private async void SetNewPriority()
        {
            if (SelectedDistributionInformation == null || SelectedPriority == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            SettingPriorityExecuting = true;


            //Get distribution information entity from database
            var disInfoDtoWrapper = SelectedDistributionInformation;
            var distributionInfoEntity = await _dataModel.GetDistributionInformationByIdAsync(disInfoDtoWrapper.Id);

            //Change countryId for entity, save changes to database and reload data in distributionInfoEntity
            distributionInfoEntity.PriorityId = SelectedPriority.Id;
            await _dataModel.SaveChangesAsync();
            distributionInfoEntity = await _dataModel.GetDistributionInformationByIdAsync(disInfoDtoWrapper.Id);

            //Map entity to DTO
            var disInfoMapped = AutoMapper.Mapper.Map<DistributionInformationDto>(distributionInfoEntity);

            //Assign Changed properties to SelectedDistributionInformation variable
            SelectedDistributionInformation.Priority = disInfoMapped.Priority;
            SelectedDistributionInformation.PriorityId = disInfoMapped.PriorityId;

            SettingPriorityExecuting = false;
        }

        private bool CanConcurencyOccure()
        {
            return SettingPriorityExecuting == true || SettingCountryExecuting == true;
        }

        public int DefaultRowIndex { get { return 0; } }

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

        public ObservableCollection<RecipientDto> RecipientsToComboBox
        {
            get { return _recipientsToComboBox; }
            set { _recipientsToComboBox = value; OnPropertyChanged(); }
        }
        public ObservableCollection<RecipientDto> RecipientsCcComboBox
        {
            get { return _recipientsCcComboBox; }
            set { _recipientsCcComboBox = value; OnPropertyChanged(); }
        }


        public ObservableCollection<DistributionInformationDtoWrapper> DistributionInformation_ObservableCollection { get; set; }
        public ObservableCollection<RecipientDto> RecipientsTo_ListView
        {
            get { return _recipientsTo_ListView; }
            set
            {
                _recipientsTo_ListView = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RecipientDto> RecipientsCc_ListView
        {
            get { return _recipientsCc_ListView; }
            set
            {
                _recipientsCc_ListView = value;
                OnPropertyChanged();
            }
        }


        public RecipientDto? SelectedRecipientToComboBox
        {
            get { return _selectedRecipientToComboBox; }
            set
            {
                if (value == null || SelectedDistributionInformation == null)
                    return;

                SetNewRecipientToAsync(value);
            }
        }

        private async void SetNewRecipientToAsync(RecipientDto value)
        {

            _selectedRecipientToComboBox = value;
            RecipientsTo_ListView.Add(value);
            SelectedDistributionInformation.RecipientsTo.Add(value);
            var disInfo = await _dataModel.GetDistributionInformationByIdAsync(SelectedDistributionInformation.Id);
            var recipient = await _dataModel.GetRecipientByIdAsync(value.Id);
            disInfo?.RecipientsTo.Add(recipient);
            RecipientsToComboBox.Remove(value);
            _selectedRecipientToComboBox = null;
            await _dataModel.SaveChangesAsync();
        }

        public RecipientDto? SelectedRecipientCcComboBox
        {
            get { return _selectedRecipientCcComboBox; }
            set
            {
                if (value == null || SelectedDistributionInformation == null)
                    return;

                SetNewRecipientCcAsync(value);
            }
        }

        private async void SetNewRecipientCcAsync(RecipientDto value)
        {
            _selectedRecipientCcComboBox = value;
            RecipientsCc_ListView.Add(value);
            SelectedDistributionInformation.RecipientsCc.Add(value);
            var disInfo = await _dataModel.GetDistributionInformationByIdAsync(SelectedDistributionInformation.Id);
            var recipient = await _dataModel.GetRecipientByIdAsync(value.Id);
            disInfo?.RecipientsCc.Add(recipient);
            RecipientsCcComboBox.Remove(value);
            _selectedRecipientCcComboBox = null;
            await _dataModel.SaveChangesAsync();
        }

        public RecipientDto? SelectedRecipientToListView
        {
            get { return _selectedRecipientToListView; }
            set
            {
                _selectedRecipientToListView = value;
                ((DelegateCommand)RemoveToRecipientCommand).RaiseCanExecuteChanged();
            }
        }
        public RecipientDto? SelectedRecipientCcListView
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
                OnPropertyChanged();
            }
        }
        public PriorityDto? SelectedPriority
        {
            get { return _selectedPriority; }
            set
            {
                _selectedPriority = value;
                OnPropertyChanged();
            }
        }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand RemoveCcRecipientCommand { get; set; }
        public ICommand RemoveToRecipientCommand { get; set; }
        public ICommand PriorityChangedCommand { get; set; }
        public ICommand CountryChangedCommand { get; set; }



        private readonly IDataModel _dataModel;

        private AutoMapperConfig AutoMapper { get; }

        private IEventAggregator _eventAggregator;
        private DistributionInformationDtoWrapper _selectedDistributionInformation;

        private ObservableCollection<RecipientDto> _recipientsTo_ListView;
        private ObservableCollection<RecipientDto> _recipientsCc_ListView;

        private ObservableCollection<RecipientDto> _recipientsToComboBox;
        private ObservableCollection<RecipientDto> _recipientsCcComboBox;
        private AreaDto? _selectedArea;
        private BuisnessUnitDto? _selectedBuisnessUnit;
        private CountryDto? _selectedCountry;
        private PriorityDto? _selectedPriority;
        private RecipientDto? _selectedRecipientToComboBox;
        private RecipientDto? _selectedRecipientCcComboBox;
        private RecipientDto? _selectedRecipientToListView;
        private RecipientDto? _selectedRecipientCcListView;
    }
}
