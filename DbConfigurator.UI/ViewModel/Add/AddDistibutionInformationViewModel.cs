using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Extensions;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddDistibutionInformationViewModel : EditingViewModelBase
    {
        private IEnumerable<RecipientDto> _allRecipients;
        private ObservableCollection<RecipientDto> _recipientsTo_ListView = new();
        private ObservableCollection<RecipientDto> _recipientsCc_ListView = new();
        private ObservableCollection<RecipientDto> _recipientsToComboBox = new();
        private ObservableCollection<RecipientDto> _recipientsCcComboBox = new();
        private AreaDto? _selectedArea;
        private BuisnessUnitDto? _selectedBuisnessUnit;
        private CountryDto? _selectedCountry;
        private PriorityDto? _selectedPriority;
        private RecipientDto? _selectedRecipientToComboBox;
        private RecipientDto? _selectedRecipientCcComboBox;
        private RecipientDto? _selectedRecipientToListView;
        private RecipientDto? _selectedRecipientCcListView;
        private bool AwaitingComboboxPopulation = false;
        private readonly IDistributionInformationService _dataService;
        private Action _action = Action.Update;

        private enum Action { Create=0, Update=1 }

        public AddDistibutionInformationViewModel(
            IDistributionInformationService dataService
            )
        {
            _dataService = dataService;
            DistributionInformation = new();

            RemoveToRecipientCommand = new DelegateCommand(OnRemoveRecipientToExecuteAsync, OnRemoveRecipientToCanExecute);
            RemoveCcRecipientCommand = new DelegateCommand(OnRemoveRecipientCcExecuteAsync, OnRemovRecipientCCeCanExecute);
            PriorityChangedCommand = new DelegateCommand(OnPriorityChanged);
            AreaChangedCommand = new DelegateCommand(OnAreaChanged);
            BuisnessUnitChangedCommand = new DelegateCommand(OnBuisnessUnitChanged);
            CountryChangedCommand = new DelegateCommand(OnCountryChanged);
            SelectionChangedCommand = new DelegateCommand(OnRegionChanged);
        }



        public ICommand RemoveCcRecipientCommand { get; set; }
        public ICommand RemoveToRecipientCommand { get; set; }
        public ICommand PriorityChangedCommand { get; set; }
        public ICommand AreaChangedCommand { get; set; }
        public ICommand BuisnessUnitChangedCommand { get; set; }
        public ICommand CountryChangedCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }



        public ObservableCollection<RegionDto> AllRegions { get; set; } = new ObservableCollection<RegionDto>();
        public ObservableCollection<RegionDto> FilteredRegions { get; set; } = new ObservableCollection<RegionDto>();
        public ObservableCollection<AreaDto> Area_Collection { get; set; } = new ObservableCollection<AreaDto>();
        public ObservableCollection<BuisnessUnitDto> BuisnessUnit_Collection { get; set; } = new ObservableCollection<BuisnessUnitDto>();
        public ObservableCollection<CountryDto> Country_Collection { get; set; } = new ObservableCollection<CountryDto>();
        public ObservableCollection<PriorityDto> Priority_Collection { get; private set; } = new ObservableCollection<PriorityDto>();
        public ObservableCollection<RecipientDto> AvilableRecipientsTo
        {
            get { return _recipientsToComboBox; }
            set { _recipientsToComboBox = value; OnPropertyChanged(); }
        }
        public ObservableCollection<RecipientDto> AvilableRecipientsCc
        {
            get { return _recipientsCcComboBox; }
            set { _recipientsCcComboBox = value; OnPropertyChanged(); }
        }
        public ObservableCollection<RecipientDto> AddedRecipientsTo
        {
            get { return _recipientsTo_ListView; }
            set
            {
                _recipientsTo_ListView = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RecipientDto> AddedRecipientsCc
        {
            get { return _recipientsCc_ListView; }
            set
            {
                _recipientsCc_ListView = value;
                OnPropertyChanged();
            }
        }
        public DistributionInformationDto DistributionInformation { get; private set; }
        public RecipientDto? SelectedRecipientToComboBox
        {
            get { return _selectedRecipientToComboBox; }
            set
            {
                if (value == null || DistributionInformation == null)
                    return;

                SetNewRecipientTo(value);
            }
        }
        public RecipientDto? SelectedRecipientCcComboBox
        {
            get { return _selectedRecipientCcComboBox; }
            set
            {
                if (value == null || DistributionInformation == null)
                    return;

                SetNewRecipientCc(value);
            }
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
        public RegionDto? SelectedRegion { get; set; }

        public async Task LoadAsync(int DistributionInformationId)
        {
            DistributionInformation = (DistributionInformationId > 0) ?
                    await _dataService.GetByIdAsync(DistributionInformationId) :
                    CreateNewDistributionInformation();


            _allRecipients = await _dataService.GetAllRecipientsAsync();
            await InitializeComboBoxes();
            await InitializeRegionsTable();
            InitializeRecipientsToPickList();
            InitializeRecipientsCcPickList();
        }

        private DistributionInformationDto CreateNewDistributionInformation()
        {
            _action = Action.Create;
            return new DistributionInformationDto();
        }

        private void InitializeRecipientsToPickList()
        {
            var recipientsToIds = DistributionInformation.RecipientsTo.Select(r => r.Id).ToList();

            var addedRecipientsTo = _allRecipients.Where(r => recipientsToIds.Contains(r.Id));
            var availableRecipientsTo = _allRecipients.Except(addedRecipientsTo);

            AddedRecipientsTo.Clear();
            AvilableRecipientsTo.Clear();
            foreach (var addedRecipient in addedRecipientsTo)
            {
                AddedRecipientsTo.Add(addedRecipient);
            }
            foreach (var availableRecipient in availableRecipientsTo)
            {
                AvilableRecipientsTo.Add(availableRecipient);
            }
        }
        private void InitializeRecipientsCcPickList()
        {
            var recipientsCcIds = DistributionInformation.RecipientsCc.Select(r => r.Id).ToList();

            var addedRecipientsCc = _allRecipients.Where(r => recipientsCcIds.Contains(r.Id));
            var availableRecipientsCc = _allRecipients.Except(addedRecipientsCc);

            AddedRecipientsCc.Clear();
            AvilableRecipientsCc.Clear();
            foreach (var addedRecipient in addedRecipientsCc)
            {
                AddedRecipientsCc.Add(addedRecipient);
            }
            foreach (var availableRecipient in availableRecipientsCc)
            {
                AvilableRecipientsCc.Add(availableRecipient);
            }
        }

        private async Task InitializeRegionsTable()
        {
            var regions = await _dataService.GetAllRegionsAsync();
            AllRegions.Clear();
            foreach (var region in regions)
            {
                AllRegions.Add(region);
            }
            FilteredRegions.Clear();
            foreach (var region in regions)
            {
                FilteredRegions.Add(region);
            }

            SelectRegion();
        }

        protected void OnRemoveRecipientToExecuteAsync()
        {
            if (SelectedRecipientToListView is null)
                return;
            var recipientToRemove = DistributionInformation.RecipientsTo.Where(r => r.Id == SelectedRecipientToListView.Id).FirstOrDefault();
            if (recipientToRemove is null)
                return;
            DistributionInformation.RecipientsTo.Remove(recipientToRemove);
            AddedRecipientsTo.Remove(SelectedRecipientToListView);
            PopulateComboBoxTo();
            SelectedRecipientToListView = null;
            ((DelegateCommand)RemoveToRecipientCommand).RaiseCanExecuteChanged();

        }
        protected bool OnRemoveRecipientToCanExecute()
        {
            return SelectedRecipientToListView is not null;
        }
        protected void OnRemoveRecipientCcExecuteAsync()
        {
            if (SelectedRecipientCcListView is null)
                return;

            var recipientToRemove = DistributionInformation.RecipientsCc.Where(r => r.Id == SelectedRecipientCcListView.Id).FirstOrDefault();
            if (recipientToRemove is null)
                return;
            DistributionInformation.RecipientsCc.Remove(recipientToRemove);
            AddedRecipientsCc.Remove(SelectedRecipientCcListView);
            PopulateComboBoxCc();
            SelectedRecipientCcListView = null;
            ((DelegateCommand)RemoveCcRecipientCommand).RaiseCanExecuteChanged();
        }
        protected bool OnRemovRecipientCCeCanExecute()
        {
            return SelectedRecipientCcListView != null;
        }

        private void SelectAreaComboBox()
        {
            if (DistributionInformation!.Region is null || DistributionInformation.Region.Area is null)
                return;

            SelectedArea = Area_Collection?.Where(c => c.Id == DistributionInformation.Region.Area.Id).FirstOrDefault();
        }
        private void SelectBuisnessUnitComboBox()
        {
            if (DistributionInformation!.Region is null || DistributionInformation.Region.BuisnessUnit is null)
                return;

            SelectedBuisnessUnit = BuisnessUnit_Collection?.Where(c => c.Id == DistributionInformation.Region.BuisnessUnit.Id).FirstOrDefault() ??
                BuisnessUnit_Collection?.FirstOrDefault();
        }
        private void SelectCountryComboBox()
        {
            if (DistributionInformation!.Region is null || DistributionInformation.Region.Country is null)
                return;

            SelectedCountry = Country_Collection?.Where(c => c.Id == DistributionInformation.Region.Country.Id).FirstOrDefault() ??
                Country_Collection?.FirstOrDefault();
        }
        private void SelectPriorityComboBox()
        {
            if (DistributionInformation!.Priority is null)
                return;

            SelectedPriority = Priority_Collection?.Where(c => c.Id == DistributionInformation.Priority.Id).FirstOrDefault();
        }

        private void SelectRegion()
        {
            if (DistributionInformation is null || DistributionInformation.Region is null)
                return;

            SelectedRegion = FilteredRegions?.Where(c => c.Id == DistributionInformation.Region.Id).FirstOrDefault();
        }

        private void PopulateComboBoxTo()
        {
            var recipients = _dataService.GetAllRecipients();
            if (DistributionInformation is null)
            {
                AvilableRecipientsTo = recipients.ToObservableCollection();
            }
            else
            {
                var recipientsDtoAfterFiltration = recipients.Where(p => !DistributionInformation.RecipientsTo.Any(p2 => p2.Id == p.Id)).ToList();
                AvilableRecipientsTo = recipientsDtoAfterFiltration.ToObservableCollection();
            }
        }
        private void PopulateComboBoxCc()
        {
            var recipients = _dataService.GetAllRecipients();
            if (DistributionInformation is null)
            {
                AvilableRecipientsTo = recipients.ToObservableCollection();
            }
            else
            {
                var recipientsDtoAfterFiltration = recipients.Where(p => !DistributionInformation.RecipientsCc.Any(p2 => p2.Id == p.Id)).ToList();
                AvilableRecipientsCc = recipientsDtoAfterFiltration.ToObservableCollection();
            }
        }


        public async Task InitializeComboBoxes()
        {
            Area_Collection.Clear();
            BuisnessUnit_Collection.Clear();
            Country_Collection.Clear();
            Priority_Collection.Clear();

            await PopulateAreaCombobox();
            await PopulateBuisnessUnitCombobox();
            await PopulateCountryCombobox();
            await PopulatePriorityCombobox();

            //Filling Recipients
            PopulateComboBoxTo();
            PopulateComboBoxCc();
            SelectPriorityComboBox();
        }
        private async Task PopulateAreaCombobox()
        {
            var areas = await _dataService.GetUniqueAreasFromRegionAsync();
            Area_Collection.Clear();
            foreach (var area in areas)
            {
                Area_Collection.Add(area);
            }
        }
        private async Task PopulateBuisnessUnitCombobox(int? areaId = null)
        {
            var buisnessUnits = await _dataService.GetUniqueBuisnessUnitsFromRegionAsync(areaId);
            BuisnessUnit_Collection.Clear();
            foreach (var buisnessUnit in buisnessUnits)
            {
                BuisnessUnit_Collection.Add(buisnessUnit);
            }
        }
        private async Task PopulateCountryCombobox(int? areaId = null, int? buisnessUnitId = null)
        {
            var countries = await _dataService.GetUniqueCountriesFromRegionAsync(areaId, buisnessUnitId);
            Country_Collection.Clear();
            foreach (var country in countries)
            {
                Country_Collection.Add(country);
            }
        }
        private async Task PopulatePriorityCombobox()
        {
            var priorities = await _dataService.GetAllPrioritiesAsync();
            Priority_Collection = priorities.ToObservableCollection();
        }

        private void OnAreaChanged()
        {
            if (SelectedArea is null || DistributionInformation is null)
                return;

            SelectedBuisnessUnit = null;
            SelectedCountry = null;

            FilteredRegions.Clear();
            foreach (var region in AllRegions)
            {
                if (region.Area.Id == SelectedArea.Id)
                    FilteredRegions.Add(region);
            }

            SelectRegion();
        }
        private void OnBuisnessUnitChanged()
        {
            if (SelectedArea == null || SelectedBuisnessUnit == null || DistributionInformation == null)
                return;

            SelectedCountry = null;

            FilteredRegions.Clear();
            foreach (var region in AllRegions)
            {
                if (region.Area.Id == SelectedArea.Id && region.BuisnessUnit.Id == SelectedBuisnessUnit.Id)
                    FilteredRegions.Add(region);
            }

            SelectRegion();
        }
        private void OnCountryChanged()
        {
            if (DistributionInformation == null || SelectedCountry == null || SelectedArea == null || SelectedBuisnessUnit == null)
                return;

            FilteredRegions.Clear();
            foreach (var region in AllRegions)
            {
                if (region.Area.Id == SelectedArea.Id && region.BuisnessUnit.Id == SelectedBuisnessUnit.Id && region.Country.Id == SelectedCountry.Id)
                    FilteredRegions.Add(region);
            }

            SelectRegion();
        }
        private void OnPriorityChanged()
        {
            if (DistributionInformation is null || SelectedPriority is null)
                return;

            DistributionInformation.Priority = SelectedPriority;
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private void OnRegionChanged()
        {
            if (DistributionInformation is null || SelectedRegion is null)
                return;

            DistributionInformation.Region = SelectedRegion;
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private bool CanConcurencyOccure()
        {
            return AwaitingComboboxPopulation;
        }
        private void SetNewRecipientTo(RecipientDto value)
        {
            _selectedRecipientToComboBox = value;
            AddedRecipientsTo.Add(value);
            DistributionInformation?.RecipientsTo.Add(value);
            AvilableRecipientsTo.Remove(value);
            _selectedRecipientToComboBox = null;
        }
        private void SetNewRecipientCc(RecipientDto value)
        {
            _selectedRecipientCcComboBox = value;
            AddedRecipientsCc.Add(value);
            DistributionInformation?.RecipientsCc.Add(value);
            AvilableRecipientsCc.Remove(value);
            _selectedRecipientCcComboBox = null;
        }

        protected override void OnAddExecute()
        {
            if(_action == Action.Update)
            {
                _dataService.UpdateAsync(DistributionInformation);
            }
            else
            {
                _dataService.AddAsync(DistributionInformation);
            }

            base.OnAddExecute();
        }

        protected override bool OnAddCanExecute()
        {
            return SelectedPriority is not null && SelectedRegion is not null;
        }
    }
}
