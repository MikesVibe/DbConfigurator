using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Extensions;
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
        public DistributionInformationDto DistributionInformation { get; set; }


        public AddDistibutionInformationViewModel(
            IDataModel dataModel,
            AutoMapperConfig autoMapper
            )
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            DistributionInformation = new();

            RemoveToRecipientCommand = new DelegateCommand(OnRemoveRecipientToExecuteAsync, OnRemoveRecipientToCanExecute);
            RemoveCcRecipientCommand = new DelegateCommand(OnRemoveRecipientCcExecuteAsync, OnRemovRecipientCCeCanExecute);
            PriorityChangedCommand = new DelegateCommand(OnPriorityChanged);
            AreaChangedCommand = new DelegateCommand(OnAreaChanged);
            BuisnessUnitChangedCommand = new DelegateCommand(OnBuisnessUnitChanged);
            CountryChangedCommand = new DelegateCommand(OnCountryChanged);

        }

        public async Task LoadAsync()
        {
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
            var allRecipients = _dataModel.RecipientsDto;
            var recipientsDtoAfterFiltration = allRecipients.Where(p => !DistributionInformation.RecipientsTo.Any(p2 => p2.Id == p.Id)).ToList();
            RecipientsToComboBox = EnumerableToObservableCollection(recipientsDtoAfterFiltration);
        }
        private void PopulateComboBoxCc()
        {
            var recipients = EnumerableToObservableCollection(_dataModel.RecipientsDto);
            RecipientsCcComboBox = EnumerableToObservableCollection(recipients.Where(p => !DistributionInformation.RecipientsCc.Any(p2 => p2.Id == p.Id)));
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
            List<AreaDto> areas = await _dataModel.RegionsRepository.GetAreasDtoAsync();
            Area_Collection = areas.ToObservableCollection();
        }
        private async Task PopulateBuisnessUnitCombobox(int? areaId = null)
        {
            var buisnessUnits = await _dataModel.RegionsRepository.GetBuisnessUnitsDtoAsync(areaId);
            BuisnessUnit_Collection.Clear();
            foreach (var buisnessUnit in buisnessUnits)
            {
                BuisnessUnit_Collection.Add(buisnessUnit);
            }
        }
        private async Task PopulateCountryCombobox(int? buisnessUnitId = null)
        {
            var countries = await _dataModel.RegionsRepository.GetCountriesDtoAsync(buisnessUnitId);
            Country_Collection.Clear();
            foreach (var country in countries)
            {
                Country_Collection.Add(country);
            }
        }
        private async Task PopulatePriorityCombobox()
        {
            var priorities = _dataModel.PrioritiesDto;
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

            var region = await _dataModel.GetRegionAsync(SelectedArea.Id, SelectedBuisnessUnit.Id, SelectedCountry.Id);
            if (region == null)
                throw new ArgumentNullException(nameof(region));

            var mapped = _autoMapper.Mapper.Map<RegionDto>(region);
            DistributionInformation.Region = mapped;

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

        protected void OnRemoveRecipientToExecuteAsync()
        {
            if (SelectedRecipientToListView is null)
                return;
            DistributionInformation.RecipientsTo.Remove(SelectedRecipientToListView);
            RecipientsTo_ListView.Remove(SelectedRecipientToListView);
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
            RecipientsCc_ListView.Remove(SelectedRecipientCcListView);
            PopulateComboBoxCc();
            SelectedRecipientCcListView = null;
            ((DelegateCommand)RemoveCcRecipientCommand).RaiseCanExecuteChanged();
        }
        protected bool OnRemovRecipientCCeCanExecute()
        {
            return SelectedRecipientCcListView != null;
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
                if (value == null || DistributionInformation == null)
                    return;

                SetNewRecipientToAsync(value);
            }
        }

        private void SetNewRecipientToAsync(RecipientDto value)
        {
            _selectedRecipientToComboBox = value;
            RecipientsTo_ListView.Add(value);
            DistributionInformation.RecipientsTo.Add(value);
            RecipientsToComboBox.Remove(value);
            _selectedRecipientToComboBox = null;
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

        private void SetNewRecipientCcAsync(RecipientDto value)
        {
            _selectedRecipientCcComboBox = value;
            RecipientsCc_ListView.Add(value);
            DistributionInformation.RecipientsCc.Add(value);
            RecipientsCcComboBox.Remove(value);
            _selectedRecipientCcComboBox = null;
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
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;
    }
}
