using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Extensions;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.ViewModel.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.Features.DistributionInformations
{
    public class DistributionInformationDetailViewModel : DetailViewModelBase<IDistributionInformationService, DistributionInformation>
    {
        private IEnumerable<Recipient> _allRecipients;
        private ObservableCollection<Recipient> _recipientsTo_ListView = new();
        private ObservableCollection<Recipient> _recipientsCc_ListView = new();
        private ObservableCollection<Recipient> _recipientsToComboBox = new();
        private ObservableCollection<Recipient> _recipientsCcComboBox = new();
        private Area? _selectedArea;
        private Country? _selectedBusinessUnit;
        private Country? _selectedCountry;
        private PriorityDto? _selectedPriority;
        private Recipient? _selectedRecipientToComboBox;
        private Recipient? _selectedRecipientCcComboBox;
        private Recipient? _selectedRecipientToListView;
        private Recipient? _selectedRecipientCcListView;
        private bool AwaitingComboboxPopulation = false;


        public DistributionInformationDetailViewModel(
            IDistributionInformationService dataService, IEventAggregator eventAggregator
            ) : base(dataService, eventAggregator)
        {
            Title = "DistibutionInformation";
            ViewWidth = 955;
            ViewHeight = 610;

            RemoveToRecipientCommand = new DelegateCommand(OnRemoveRecipientToExecuteAsync, OnRemoveRecipientToCanExecute);
            RemoveCcRecipientCommand = new DelegateCommand(OnRemoveRecipientCcExecuteAsync, OnRemovRecipientCCeCanExecute);
            PriorityChangedCommand = new DelegateCommand(OnPriorityChanged);
            AreaChangedCommand = new DelegateCommand(OnAreaChanged);
            BusinessUnitChangedCommand = new DelegateCommand(OnBusinessUnitChanged);
            CountryChangedCommand = new DelegateCommand(OnCountryChanged);
            SelectionChangedCommand = new DelegateCommand(OnRegionChanged);


        }


        public ICommand RemoveCcRecipientCommand { get; set; }
        public ICommand RemoveToRecipientCommand { get; set; }
        public ICommand PriorityChangedCommand { get; set; }
        public ICommand AreaChangedCommand { get; set; }
        public ICommand BusinessUnitChangedCommand { get; set; }
        public ICommand CountryChangedCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }



        public ObservableCollection<Region> AllRegions { get; set; } = new ObservableCollection<Region>();
        public ObservableCollection<Region> FilteredRegions { get; set; } = new ObservableCollection<Region>();
        public ObservableCollection<Area> Area_Collection { get; set; } = new ObservableCollection<Area>();
        public ObservableCollection<BusinessUnit> BusinessUnit_Collection { get; set; } = new ObservableCollection<BusinessUnit>();
        public ObservableCollection<Country> Country_Collection { get; set; } = new ObservableCollection<Country>();
        public ObservableCollection<PriorityDto> Priority_Collection { get; private set; } = new ObservableCollection<PriorityDto>();
        public ObservableCollection<Recipient> AvilableRecipientsTo
        {
            get { return _recipientsToComboBox; }
            set { _recipientsToComboBox = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Recipient> AvilableRecipientsCc
        {
            get { return _recipientsCcComboBox; }
            set { _recipientsCcComboBox = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Recipient> AddedRecipientsTo
        {
            get { return _recipientsTo_ListView; }
            set
            {
                _recipientsTo_ListView = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Recipient> AddedRecipientsCc
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
                if (value == null || EntityDto == null)
                    return;

                SetNewRecipientTo(value);
            }
        }
        public Recipient? SelectedRecipientCcComboBox
        {
            get { return _selectedRecipientCcComboBox; }
            set
            {
                if (value == null || EntityDto == null)
                    return;

                SetNewRecipientCc(value);
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
        public Area? SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                _selectedArea = value;
                OnPropertyChanged();
            }
        }
        public Country? SelectedBusinessUnit
        {
            get { return _selectedBusinessUnit; }
            set
            {
                _selectedBusinessUnit = value;
                OnPropertyChanged();
            }
        }
        public Country? SelectedCountry
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
        public Region? SelectedRegion { get; set; }

        public override async Task LoadAsync(int DistributionInformationId)
        {
            await base.LoadAsync(DistributionInformationId);


            _allRecipients = await DataService.GetAllRecipientsAsync();
            await InitializeComboBoxes();
            await InitializeRegionsTable();
            InitializeRecipientsToPickList();
            InitializeRecipientsCcPickList();
        }

        private void InitializeRecipientsToPickList()
        {
            //var recipientsToIds = EntityDto!.RecipientsTo.Select(r => r.Id).ToList();

            //var addedRecipientsTo = _allRecipients.Where(r => recipientsToIds.Contains(r.Id));
            //var availableRecipientsTo = _allRecipients.Except(addedRecipientsTo);

            //AddedRecipientsTo.Clear();
            //AvilableRecipientsTo.Clear();
            //foreach (var addedRecipient in addedRecipientsTo)
            //{
            //    AddedRecipientsTo.Add(addedRecipient);
            //}
            //foreach (var availableRecipient in availableRecipientsTo)
            //{
            //    AvilableRecipientsTo.Add(availableRecipient);
            //}
        }
        private void InitializeRecipientsCcPickList()
        {
            //var recipientsCcIds = EntityDto!.RecipientsCc.Select(r => r.Id).ToList();

            //var addedRecipientsCc = _allRecipients.Where(r => recipientsCcIds.Contains(r.Id));
            //var availableRecipientsCc = _allRecipients.Except(addedRecipientsCc);

            //AddedRecipientsCc.Clear();
            //AvilableRecipientsCc.Clear();
            //foreach (var addedRecipient in addedRecipientsCc)
            //{
            //    AddedRecipientsCc.Add(addedRecipient);
            //}
            //foreach (var availableRecipient in availableRecipientsCc)
            //{
            //    AvilableRecipientsCc.Add(availableRecipient);
            //}
        }

        private async Task InitializeRegionsTable()
        {
            var regions = await DataService.GetAllRegionsAsync();
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
            //if (SelectedRecipientToListView is null)
            //    return;
            //var recipientToRemove = EntityDto!.RecipientsTo.Where(r => r.Id == SelectedRecipientToListView.Id).FirstOrDefault();
            //if (recipientToRemove is null)
            //    return;
            //EntityDto!.RecipientsTo.Remove(recipientToRemove);
            //AddedRecipientsTo.Remove(SelectedRecipientToListView);
            //PopulateComboBoxTo();
            //SelectedRecipientToListView = null;
            //((DelegateCommand)RemoveToRecipientCommand).RaiseCanExecuteChanged();

        }
        protected bool OnRemoveRecipientToCanExecute()
        {
            return SelectedRecipientToListView is not null;
        }
        protected void OnRemoveRecipientCcExecuteAsync()
        {
            //if (SelectedRecipientCcListView is null)
            //    return;

            //var recipientToRemove = EntityDto!.RecipientsCc.Where(r => r.Id == SelectedRecipientCcListView.Id).FirstOrDefault();
            //if (recipientToRemove is null)
            //    return;
            //EntityDto!.RecipientsCc.Remove(recipientToRemove);
            //AddedRecipientsCc.Remove(SelectedRecipientCcListView);
            //PopulateComboBoxCc();
            //SelectedRecipientCcListView = null;
            //((DelegateCommand)RemoveCcRecipientCommand).RaiseCanExecuteChanged();
        }
        protected bool OnRemovRecipientCCeCanExecute()
        {
            return SelectedRecipientCcListView != null;
        }

        private void SelectPriorityComboBox()
        {
            if (EntityDto is null || EntityDto.Priority is null)
                return;

            SelectedPriority = Priority_Collection?.Where(c => c.Id == EntityDto!.Priority.Id).FirstOrDefault();
        }

        private void SelectRegion()
        {
            if (EntityDto is null || EntityDto!.Region is null)
                return;

            SelectedRegion = FilteredRegions?.Where(c => c.Id == EntityDto!.Region.Id).FirstOrDefault();
        }

        private void PopulateComboBoxTo()
        {
            var recipients = DataService.GetAllRecipients();
            //if (EntityDto is null)
            //{
            //    AvilableRecipientsTo = recipients.ToObservableCollection();
            //}
            //else
            //{
            //    var recipientsDtoAfterFiltration = recipients.Where(p => !EntityDto!.RecipientsTo.Any(p2 => p2.Id == p.Id)).ToList();
            //    AvilableRecipientsTo = recipientsDtoAfterFiltration.ToObservableCollection();
            //}
        }
        private void PopulateComboBoxCc()
        {
            //var recipients = DataService.GetAllRecipients();
            //if (EntityDto is null)
            //{
            //    AvilableRecipientsTo = recipients.ToObservableCollection();
            //}
            //else
            //{
            //    var recipientsDtoAfterFiltration = recipients.Where(p => !EntityDto!.RecipientsCc.Any(p2 => p2.Id == p.Id)).ToList();
            //    AvilableRecipientsCc = recipientsDtoAfterFiltration.ToObservableCollection();
            //}
        }


        public async Task InitializeComboBoxes()
        {
            Area_Collection.Clear();
            BusinessUnit_Collection.Clear();
            Country_Collection.Clear();
            Priority_Collection.Clear();

            await PopulateAreaCombobox();
            await PopulateBusinessUnitCombobox();
            await PopulateCountryCombobox();
            await PopulatePriorityCombobox();

            //Filling Recipients
            PopulateComboBoxTo();
            PopulateComboBoxCc();
            SelectPriorityComboBox();
        }
        private async Task PopulateAreaCombobox()
        {
            var areas = await DataService.GetUniqueAreasFromRegionAsync();
            Area_Collection.Clear();
            foreach (var area in areas)
            {
                Area_Collection.Add(area);
            }
        }
        private async Task PopulateBusinessUnitCombobox(int? areaId = null)
        {
            var BusinessUnits = await DataService.GetUniqueBusinessUnitsFromRegionAsync(areaId);
            BusinessUnit_Collection.Clear();
            foreach (var BusinessUnit in BusinessUnits)
            {
                BusinessUnit_Collection.Add(BusinessUnit);
            }
        }
        private async Task PopulateCountryCombobox(int? areaId = null, int? BusinessUnitId = null)
        {
            var countries = await DataService.GetUniqueCountriesFromRegionAsync(areaId, BusinessUnitId);
            Country_Collection.Clear();
            foreach (var country in countries)
            {
                Country_Collection.Add(country);
            }
        }
        private async Task PopulatePriorityCombobox()
        {
            var priorities = await DataService.GetAllPrioritiesAsync();
            Priority_Collection = priorities.ToObservableCollection();
        }

        private void OnAreaChanged()
        {
            if (SelectedArea is null || EntityDto is null)
                return;

            SelectedBusinessUnit = null;
            SelectedCountry = null;

            FilteredRegions.Clear();
            foreach (var region in AllRegions)
            {
                if (region.Area.Id == SelectedArea.Id)
                    FilteredRegions.Add(region);
            }

            SelectRegion();
        }
        private void OnBusinessUnitChanged()
        {
            if (SelectedArea == null || SelectedBusinessUnit == null || EntityDto! == null)
                return;

            SelectedCountry = null;

            FilteredRegions.Clear();
            foreach (var region in AllRegions)
            {
                if (region.Area.Id == SelectedArea.Id && region.BusinessUnit.Id == SelectedBusinessUnit.Id)
                    FilteredRegions.Add(region);
            }

            SelectRegion();
        }
        private void OnCountryChanged()
        {
            if (EntityDto! == null || SelectedCountry == null || SelectedArea == null || SelectedBusinessUnit == null)
                return;

            FilteredRegions.Clear();
            foreach (var region in AllRegions)
            {
                if (region.Area.Id == SelectedArea.Id && region.BusinessUnit.Id == SelectedBusinessUnit.Id && region.Country.Id == SelectedCountry.Id)
                    FilteredRegions.Add(region);
            }

            SelectRegion();
        }
        private void OnPriorityChanged()
        {
            //if (EntityDto is null || SelectedPriority is null)
            //    return;

            //EntityDto!.Priority = SelectedPriority;
            //((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private void OnRegionChanged()
        {
            if (EntityDto! is null || SelectedRegion is null)
                return;

            EntityDto!.Region = SelectedRegion;
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private void SetNewRecipientTo(Recipient value)
        {
            //_selectedRecipientToComboBox = value;
            //AddedRecipientsTo.Add(value);
            //EntityDto!?.RecipientsTo.Add(value);
            //AvilableRecipientsTo.Remove(value);
            //_selectedRecipientToComboBox = null;
        }
        private void SetNewRecipientCc(Recipient value)
        {
            //_selectedRecipientCcComboBox = value;
            //AddedRecipientsCc.Add(value);
            //EntityDto!?.RecipientsCc.Add(value);
            //AvilableRecipientsCc.Remove(value);
            //_selectedRecipientCcComboBox = null;
        }


        protected override bool OnSaveCanExecute()
        {
            return SelectedPriority is not null && SelectedRegion is not null;
        }

        protected override void OnCreate()
        {
            //if (EntityDto is null)
            //    return;

            //EventAggregator.GetEvent<CreateDistributionInformationEvent>()
            //      .Publish(
            //    new CreateDistributionInformationEventArgs
            //    {
            //        Entity = new DistributionInformation
            //        {
            //            Id = EntityDto.Id,
            //            Priority = EntityDto.Priority,
            //            Region = EntityDto.Region,
            //            RecipientsCc = EntityDto.RecipientsCc,
            //            RecipientsTo = EntityDto.RecipientsTo
            //        }
            //    });
        }

        protected override void OnUpdate()
        {
            //if (EntityDto is null)
            //    return;

            //EventAggregator.GetEvent<EditDistributionInformationEvent>()
            //      .Publish(
            //    new EditDistributionInformationEventArgs
            //    {
            //        Entity = new DistributionInformation
            //        {
            //            Id = EntityDto.Id,
            //            Priority = EntityDto.Priority,
            //            Region = EntityDto.Region,
            //            RecipientsCc = EntityDto.RecipientsCc,
            //            RecipientsTo = EntityDto.RecipientsTo
            //        }
            //    });
        }
    }
}
