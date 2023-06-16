using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Table;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class DistributionInformationPanelViewModel : TableViewModelBase, IMainPanelViewModel
    {
        private bool AwaitingComboboxPopulation = false;
        private readonly IDataModel _dataModel;
        private readonly IDialogService _dialogService;
        private readonly AutoMapperConfig _autoMapper;

        public DistributionInformationPanelViewModel(
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            IDataModel dataModel,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            _dialogService = dialogService;
            _dataModel = dataModel;
            _autoMapper = autoMapper;

            DistributionInformation_ObservableCollection = new ObservableCollection<DistributionInformationDtoWrapper>();
            
            
            
            RecipientsTo_ListView = new ObservableCollection<RecipientDto>();
            RecipientsCc_ListView = new ObservableCollection<RecipientDto>();
            RemoveToRecipientCommand = new DelegateCommand(OnRemoveRecipientToExecuteAsync, OnRemoveRecipientToCanExecute);
            RemoveCcRecipientCommand = new DelegateCommand(OnRemoveRecipientCcExecuteAsync, OnRemovRecipientCCeCanExecute);
            PriorityChangedCommand = new DelegateCommand(OnPriorityChanged);
            AreaChangedCommand = new DelegateCommand(SetNewArea);
            BuisnessUnitChangedCommand = new DelegateCommand(OnBuisnessUnitChanged);
            CountryChangedCommand = new DelegateCommand(OnCountryChanged);

        }

        public async override Task LoadAsync()
        {
            await PopulateComboBoxesWithData();

            var distributionInformation = await _dataModel.GetAllDistributionInformationAsync();


            foreach (var dis in distributionInformation)
            {
                var mapped = _autoMapper.Mapper.Map<DistributionInformationDto>(dis);
                var wrapped = new DistributionInformationDtoWrapper(mapped);
                DistributionInformation_ObservableCollection.Add(wrapped);
            }
        }

        private async Task PopulateComboBoxesWithData()
        {
            var areas = EnumerableToObservableCollection(_dataModel.AreasDto);
            Area_Collection = areas;
            await PopulateBuisnessUnitCombobox();
            await PopulateCountryCombobox();
            var priorities = EnumerableToObservableCollection(_dataModel.PrioritiesDto);
            Priority_Collection = priorities;
        }
        private async Task PopulateBuisnessUnitCombobox(int? areaId = null)
        {
            IEnumerable<BuisnessUnit> avilableBuisnessUnits;
            if (areaId == null)
                avilableBuisnessUnits = await _dataModel.GetAllBuisnessUnitsAsync();
            else
                avilableBuisnessUnits = await _dataModel.GetBuisnessUnitsAsync((int)areaId);

            BuisnessUnit_Collection.Clear();
            foreach (var bu in avilableBuisnessUnits)
            {
                var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(bu);
                BuisnessUnit_Collection.Add(mapped);
            }
        }
        private async Task PopulateCountryCombobox(int? buisnessUnitId = null)
        {
            IEnumerable<Country> countries;
            if (buisnessUnitId == null)
                countries = await _dataModel.GetAllCountriesAsync();
            else
                countries = await _dataModel.GetCountriesAsync((int)buisnessUnitId);

            Country_Collection.Clear();
            foreach (var country in countries)
            {
                var mapped = _autoMapper.Mapper.Map<CountryDto>(country);
                Country_Collection.Add(mapped);
            }
        }

        protected override async void OnAddExecute()
        {
            //Create New Distribution Infrotmaion
            var defaultPriotrity = _dataModel.DefaultPriority;
            var defaultRegion = _dataModel.DefaultRegion;
            var distributionInformation = new DistributionInformation(defaultRegion, defaultPriotrity);

            await _dataModel.AddAsync(distributionInformation);
            await _dataModel.SaveChangesAsync();

            var distributionInformationEntity = await _dataModel.GetDistributionInformationByIdAsync(distributionInformation.Id);
            var distributionInformationDto = AutoMapper.Mapper.Map<DistributionInformationDto>(distributionInformationEntity);
            var wrappedDisInfo = new DistributionInformationDtoWrapper(distributionInformationDto);

            DistributionInformation_ObservableCollection.Add(wrappedDisInfo);
            SelectedDistributionInformation = wrappedDisInfo;



            //DO NOT REMOVE (This is for future update)
            //var distributionInformationViewModel = new AddDistibutionInformationViewModel();
            //bool? result = _dialogService.ShowDialog(distributionInformationViewModel);

            //if (result == false)
            //    return;

            //var distributionInformation = new DistributionInformation
            //{
            //    Name = areaName
            //};

            //_dataModel.Add(distributionInformation);
            //_dataModel.SaveChanges();
            //var mapped = _autoMapper.Mapper.Map<DistributionInformationTableItem>(distributionInformation);
            //Items.Add(mapped);
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
        protected override void OnSelectionChangedExecute()
        {
            if (SelectedDistributionInformation == null)
                return;

            PopulateComboBoxTo();
            PopulateComboBoxCc();

            //Setting selected items in comboBoxes
            SelectAreaComboBox();
            SelectBuisnessUnitComboBox();
            SelectCountryComboBox();
            SelectPriorityComboBox();

            //Setting Items in ListViews
            RecipientsTo_ListView = EnumerableToObservableCollection(SelectedDistributionInformation.RecipientsTo);
            RecipientsCc_ListView = EnumerableToObservableCollection(SelectedDistributionInformation.RecipientsCc);


            base.OnSelectionChangedExecute();
        }

        private void SelectPriorityComboBox()
        {
            SelectedPriority = Priority_Collection?.Where(c => c.Id == SelectedDistributionInformation.Priority.Id).FirstOrDefault();
        }
        private void SelectCountryComboBox()
        {
            if (SelectedDistributionInformation.Region.Country == null)
                return;

            SelectedCountry = Country_Collection?.Where(c => c.Id == SelectedDistributionInformation.Region.Country.Id).FirstOrDefault() ??
                Country_Collection?.FirstOrDefault();
        }
        private void SelectBuisnessUnitComboBox()
        {
            if (SelectedDistributionInformation.Region.BuisnessUnit == null)
                return;

            SelectedBuisnessUnit = BuisnessUnit_Collection?.Where(c => c.Id == SelectedDistributionInformation.Region.BuisnessUnit.Id).FirstOrDefault() ??
                BuisnessUnit_Collection?.FirstOrDefault();
        }
        private void SelectAreaComboBox()
        {
            if (SelectedDistributionInformation.Region.Area == null)
                return;

            SelectedArea = Area_Collection?.Where(c => c.Id == SelectedDistributionInformation.Region.Area.Id).FirstOrDefault();
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

        private async void SetNewArea()
        {
            if (SelectedArea == null || SelectedDistributionInformation == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            AwaitingComboboxPopulation = true;
            await PopulateBuisnessUnitCombobox(SelectedArea.Id);
            AwaitingComboboxPopulation = false;

            SelectBuisnessUnitComboBox();
        }



        private async void OnBuisnessUnitChanged()
        {
            if (SelectedBuisnessUnit == null || SelectedDistributionInformation == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            AwaitingComboboxPopulation = true;
            await PopulateCountryCombobox(SelectedBuisnessUnit.Id);
            AwaitingComboboxPopulation = false;

            SelectCountryComboBox();
        }
        private async void OnCountryChanged()
        {
            if (SelectedDistributionInformation == null || SelectedCountry == null || SelectedArea == null || SelectedBuisnessUnit == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            AwaitingComboboxPopulation = true;

            var region = await _dataModel.GetRegionAsync(SelectedArea.Id, SelectedBuisnessUnit.Id, SelectedCountry.Id);
            if (region == null)
                throw new ArgumentNullException(nameof(region));

            var mapped = _autoMapper.Mapper.Map<RegionDto>(region);
            SelectedDistributionInformation.Region = mapped;
            var distributionInformation = await _dataModel.GetDistributionInformationByIdAsync(SelectedDistributionInformation.Id);
            distributionInformation.RegionId = region.Id;
            await _dataModel.SaveChangesAsync();

            AwaitingComboboxPopulation = false;
        }
        private async void OnPriorityChanged()
        {
            if (SelectedDistributionInformation == null || SelectedPriority == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            AwaitingComboboxPopulation = true;


            SelectedDistributionInformation.Priority = SelectedPriority;

            //Get distribution information entity from database
            var distributionInfoEntity = await _dataModel.GetDistributionInformationByIdAsync(SelectedDistributionInformation.Id);

            //Change countryId for entity, save changes to database and reload data in distributionInfoEntity
            distributionInfoEntity.PriorityId = SelectedPriority.Id;
            await _dataModel.SaveChangesAsync();


            AwaitingComboboxPopulation = false;
        }

        private bool CanConcurencyOccure()
        {
            return AwaitingComboboxPopulation;
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
        public ObservableCollection<AreaDto> Area_Collection { get; set; } = new ObservableCollection<AreaDto>();
        public ObservableCollection<BuisnessUnitDto> BuisnessUnit_Collection { get; set; } = new ObservableCollection<BuisnessUnitDto>();
        public ObservableCollection<CountryDto> Country_Collection { get; set; } = new ObservableCollection<CountryDto>();
        public ObservableCollection<PriorityDto> Priority_Collection { get; private set; } = new ObservableCollection<PriorityDto>();

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


        public ObservableCollection<DistributionInformationDtoWrapper> DistributionInformation_ObservableCollection { get; set; } = new ObservableCollection<DistributionInformationDtoWrapper>();
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

        public ICommand RemoveCcRecipientCommand { get; set; }
        public ICommand RemoveToRecipientCommand { get; set; }
        public ICommand PriorityChangedCommand { get; set; }
        public ICommand AreaChangedCommand { get; set; }
        public ICommand BuisnessUnitChangedCommand { get; set; }
        public ICommand CountryChangedCommand { get; set; }





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
