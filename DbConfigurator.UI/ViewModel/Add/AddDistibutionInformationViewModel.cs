﻿using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Extensions;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
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
        private ObservableCollection<RecipientDto> _recipientsTo_ListView = new();
        private ObservableCollection<RecipientDto> _recipientsCc_ListView = new();
        private ObservableCollection<RecipientDto> _recipientsToComboBox = new();
        private ObservableCollection<RecipientDto> _recipientsCcComboBox = new();
        private DistributionInformationDto _distributionInformation;
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
        private readonly AutoMapperConfig _autoMapper;

        public AddDistibutionInformationViewModel(
            IDistributionInformationService dataService,
            AutoMapperConfig autoMapper
            )
        {
            _dataService = dataService;
            _autoMapper = autoMapper;
            DistributionInformation = new();

            RemoveToRecipientCommand = new DelegateCommand(OnRemoveRecipientToExecuteAsync, OnRemoveRecipientToCanExecute);
            RemoveCcRecipientCommand = new DelegateCommand(OnRemoveRecipientCcExecuteAsync, OnRemovRecipientCCeCanExecute);
            PriorityChangedCommand = new DelegateCommand(OnPriorityChanged);
            AreaChangedCommand = new DelegateCommand(OnAreaChanged);
            BuisnessUnitChangedCommand = new DelegateCommand(OnBuisnessUnitChanged);
            CountryChangedCommand = new DelegateCommand(OnCountryChanged);

        }

        public ICommand RemoveCcRecipientCommand { get; set; }
        public ICommand RemoveToRecipientCommand { get; set; }
        public ICommand PriorityChangedCommand { get; set; }
        public ICommand AreaChangedCommand { get; set; }
        public ICommand BuisnessUnitChangedCommand { get; set; }
        public ICommand CountryChangedCommand { get; set; }

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
        public DistributionInformationDto? DistributionInformation { get; private set; }
        public RecipientDto? SelectedRecipientToComboBox
        {
            get { return _selectedRecipientToComboBox; }
            set
            {
                if (value == null || DistributionInformation == null)
                    return;

                SetNewRecipientToAsync(value);
            }
        }
        public RecipientDto? SelectedRecipientCcComboBox
        {
            get { return _selectedRecipientCcComboBox; }
            set
            {
                if (value == null || DistributionInformation == null)
                    return;

                SetNewRecipientCcAsync(value);
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

        public async Task LoadAsync(DistributionInformationDto? distributionInformation = null)
        {
            DistributionInformation = distributionInformation;
            await PopulateComboBoxesWithData();

            if (DistributionInformation is null)
                return;
            SelectAreaComboBox();
            SelectBuisnessUnitComboBox();
            SelectCountryComboBox();
            SelectPriorityComboBox();
            var recipientsTo = DistributionInformation.RecipientsTo.ToList();
            DistributionInformation.RecipientsTo.Clear();
            var recipientsCc = DistributionInformation.RecipientsCc.ToList();
            DistributionInformation.RecipientsCc.Clear();
            foreach (var recipient in recipientsTo)
            {
                SetNewRecipientToAsync(recipient);
            }
            foreach (var recipient in recipientsCc)
            {
                SetNewRecipientCcAsync(recipient);
            }
        }

        protected void OnRemoveRecipientToExecuteAsync()
        {
            if (SelectedRecipientToListView is null)
                return;
            DistributionInformation.RecipientsTo.Remove(SelectedRecipientToListView);
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

            DistributionInformation.RecipientsCc.Remove(SelectedRecipientCcListView);
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
            if (DistributionInformation.Region is null || DistributionInformation.Region.Area is null)
                return;

            SelectedArea = Area_Collection?.Where(c => c.Id == DistributionInformation.Region.Area.Id).FirstOrDefault();
        }
        private void SelectBuisnessUnitComboBox()
        {
            if (DistributionInformation.Region is null || DistributionInformation.Region.BuisnessUnit is null)
                return;

            SelectedBuisnessUnit = BuisnessUnit_Collection?.Where(c => c.Id == DistributionInformation.Region.BuisnessUnit.Id).FirstOrDefault() ??
                BuisnessUnit_Collection?.FirstOrDefault();
        }
        private void SelectCountryComboBox()
        {
            if (DistributionInformation.Region is null || DistributionInformation.Region.Country is null)
                return;

            SelectedCountry = Country_Collection?.Where(c => c.Id == DistributionInformation.Region.Country.Id).FirstOrDefault() ??
                Country_Collection?.FirstOrDefault();
        }
        private void SelectPriorityComboBox()
        {
            if (DistributionInformation.Priority is null)
                return;

            SelectedPriority = Priority_Collection?.Where(c => c.Id == DistributionInformation.Priority.Id).FirstOrDefault();
        }
        private void PopulateComboBoxTo()
        {
            var recipients = _dataService.GetAllRecipients();

            var recipientsDtoAfterFiltration = recipients.Where(p => !DistributionInformation.RecipientsTo.Any(p2 => p2.Id == p.Id)).ToList();
            AvilableRecipientsTo = recipientsDtoAfterFiltration.ToObservableCollection();
        }
        private void PopulateComboBoxCc()
        {
            var recipients = _dataService.GetAllRecipients();
            var mapped = _autoMapper.Mapper.Map<IEnumerable<RecipientDto>>(recipients);

            var recipientsDtoAfterFiltration = mapped.Where(p => !DistributionInformation.RecipientsCc.Any(p2 => p2.Id == p.Id)).ToList();

            AvilableRecipientsCc = recipientsDtoAfterFiltration.ToObservableCollection();
        }
        private async void OnAreaChanged()
        {
            if (SelectedArea is null || DistributionInformation is null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            AwaitingComboboxPopulation = true;
            await PopulateBuisnessUnitCombobox(SelectedArea.Id);
            AwaitingComboboxPopulation = false;

            SelectBuisnessUnitComboBox();
        }

        public async Task PopulateComboBoxesWithData()
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
        }
        private async Task PopulateAreaCombobox()
        {
            var areas = await _dataService.GetAreasAsync();
            Area_Collection = areas.ToObservableCollection();
        }
        private async Task PopulateBuisnessUnitCombobox(int? areaId = null)
        {
            var buisnessUnits = await _dataService.GetBuisnessUnitsAsync(areaId);
            BuisnessUnit_Collection.Clear();
            foreach (var buisnessUnit in buisnessUnits)
            {
                BuisnessUnit_Collection.Add(buisnessUnit);
            }
        }
        private async Task PopulateCountryCombobox(int? buisnessUnitId = null)
        {
            var countries = await _dataService.GetCountriesAsync(buisnessUnitId);
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

        private async void OnBuisnessUnitChanged()
        {
            if (SelectedBuisnessUnit == null || DistributionInformation == null)
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
            if (DistributionInformation == null || SelectedCountry == null || SelectedArea == null || SelectedBuisnessUnit == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            AwaitingComboboxPopulation = true;

            var regions = await _dataService.GetRegionsWithAsync(SelectedArea.Id, SelectedBuisnessUnit.Id, SelectedCountry.Id);
            if (regions.Count() == 0)
                throw new Exception();
            if (regions.Count() > 1)
                throw new Exception();

            DistributionInformation.Region = regions.First();

            AwaitingComboboxPopulation = false;
        }
        private void OnPriorityChanged()
        {
            if (DistributionInformation is null || SelectedPriority is null)
                return;

            DistributionInformation.Priority = SelectedPriority;
        }
        private bool CanConcurencyOccure()
        {
            return AwaitingComboboxPopulation;
        }
        private void SetNewRecipientToAsync(RecipientDto value)
        {
            _selectedRecipientToComboBox = value;
            AddedRecipientsTo.Add(value);
            DistributionInformation?.RecipientsTo.Add(value);
            AvilableRecipientsTo.Remove(value);
            _selectedRecipientToComboBox = null;
        }
        private void SetNewRecipientCcAsync(RecipientDto value)
        {
            _selectedRecipientCcComboBox = value;
            AddedRecipientsCc.Add(value);
            DistributionInformation?.RecipientsCc.Add(value);
            AvilableRecipientsCc.Remove(value);
            _selectedRecipientCcComboBox = null;
        }
    }
}
