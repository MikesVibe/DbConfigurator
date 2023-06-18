using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddDistibutionInformationViewModel : EditingViewModelBase
    {
        public DistributionInformationDto DistributionInformation { get; set; }

        public AddDistibutionInformationViewModel()
        {
            DistributionInformation = new();
            
        }
        public AddDistibutionInformationViewModel(DistributionInformationDto areaDto)
        {
            DistributionInformation = areaDto;

            RecipientsTo_ListView = new ObservableCollection<RecipientDto>();
            RecipientsCc_ListView = new ObservableCollection<RecipientDto>();
            RemoveToRecipientCommand = new DelegateCommand(OnRemoveRecipientToExecuteAsync, OnRemoveRecipientToCanExecute);
            RemoveCcRecipientCommand = new DelegateCommand(OnRemoveRecipientCcExecuteAsync, OnRemovRecipientCCeCanExecute);
            PriorityChangedCommand = new DelegateCommand(OnPriorityChanged);
            AreaChangedCommand = new DelegateCommand(SetNewArea);
            BuisnessUnitChangedCommand = new DelegateCommand(OnBuisnessUnitChanged);
            CountryChangedCommand = new DelegateCommand(OnCountryChanged);
        }

        private void SelectPriorityComboBox()
        {
            SelectedPriority = Priority_Collection?.Where(c => c.Id == DistributionInformation.Priority.Id).FirstOrDefault();
        }
        private void SelectCountryComboBox()
        {
            if (DistributionInformation.Region.Country == null)
                return;

            SelectedCountry = Country_Collection?.Where(c => c.Id == DistributionInformation.Region.Country.Id).FirstOrDefault() ??
                Country_Collection?.FirstOrDefault();
        }
        private void SelectBuisnessUnitComboBox()
        {
            if (DistributionInformation.Region.BuisnessUnit == null)
                return;

            SelectedBuisnessUnit = BuisnessUnit_Collection?.Where(c => c.Id == DistributionInformation.Region.BuisnessUnit.Id).FirstOrDefault() ??
                BuisnessUnit_Collection?.FirstOrDefault();
        }
        private void SelectAreaComboBox()
        {
            if (DistributionInformation.Region.Area == null)
                return;

            SelectedArea = Area_Collection?.Where(c => c.Id == DistributionInformation.Region.Area.Id).FirstOrDefault();
        }

        private void PopulateComboBoxTo()
        {
            //var allRecipients = _dataModel.RecipientsDto;
            //var recipientsDtoAfterFiltration = allRecipients.Where(p => !DistributionInformation.RecipientsTo.Any(p2 => p2.Id == p.Id)).ToList();
            //RecipientsToComboBox = EnumerableToObservableCollection(recipientsDtoAfterFiltration);
        }
        private void PopulateComboBoxCc()
        {
            //var recipients = EnumerableToObservableCollection(_dataModel.RecipientsDto);
            //RecipientsCcComboBox = EnumerableToObservableCollection(recipients.Where(p => !DistributionInformation.RecipientsCc.Any(p2 => p2.Id == p.Id)));
        }

        private async void SetNewArea()
        {
            if (SelectedArea == null || DistributionInformation == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            AwaitingComboboxPopulation = true;
            //await PopulateBuisnessUnitCombobox(SelectedArea.Id);
            AwaitingComboboxPopulation = false;

            SelectBuisnessUnitComboBox();
        }



        private async void OnBuisnessUnitChanged()
        {
            if (SelectedBuisnessUnit == null || DistributionInformation == null)
                return;

            //Prevents concurency between threads
            if (CanConcurencyOccure())
                return;

            AwaitingComboboxPopulation = true;
            //await PopulateCountryCombobox(SelectedBuisnessUnit.Id);
            AwaitingComboboxPopulation = false;

            SelectCountryComboBox();
        }
        private async void OnCountryChanged()
        {
            //if (DistributionInformation == null || SelectedCountry == null || SelectedArea == null || SelectedBuisnessUnit == null)
            //    return;

            ////Prevents concurency between threads
            //if (CanConcurencyOccure())
            //    return;

            //AwaitingComboboxPopulation = true;

            //var region = await _dataModel.GetRegionAsync(SelectedArea.Id, SelectedBuisnessUnit.Id, SelectedCountry.Id);
            //if (region == null)
            //    throw new ArgumentNullException(nameof(region));

            //var mapped = _autoMapper.Mapper.Map<RegionDto>(region);
            //DistributionInformation.Region = mapped;
            //var distributionInformation = await _dataModel.GetDistributionInformationByIdAsync(DistributionInformation.Id);
            //distributionInformation.RegionId = region.Id;
            //await _dataModel.SaveChangesAsync();

            //AwaitingComboboxPopulation = false;
        }
        private async void OnPriorityChanged()
        {
            //if (DistributionInformation == null || SelectedPriority == null)
            //    return;

            ////Prevents concurency between threads
            //if (CanConcurencyOccure())
            //    return;

            //AwaitingComboboxPopulation = true;


            //DistributionInformation.Priority = SelectedPriority;

            ////Get distribution information entity from database
            //var distributionInfoEntity = await _dataModel.GetDistributionInformationByIdAsync(DistributionInformation.Id);

            ////Change countryId for entity, save changes to database and reload data in distributionInfoEntity
            //distributionInfoEntity.PriorityId = SelectedPriority.Id;
            //await _dataModel.SaveChangesAsync();


            //AwaitingComboboxPopulation = false;
        }

        private bool CanConcurencyOccure()
        {
            return AwaitingComboboxPopulation;
        }

        protected async void OnRemoveRecipientToExecuteAsync()
        {
            //if (SelectedRecipientToListView == null)
            //    return;
            //var disInfo = await _dataModel.GetDistributionInformationByIdAsync(DistributionInformation.Id);
            //var recipientToRemove = disInfo?.RecipientsTo.Where(r => r.Id == SelectedRecipientToListView.Id).First();
            //disInfo?.RecipientsTo.Remove(recipientToRemove!);
            //DistributionInformation.RecipientsTo.Remove(SelectedRecipientToListView);
            //RecipientsTo_ListView.Remove(SelectedRecipientToListView);
            //PopulateComboBoxTo();
            //SelectedRecipientToListView = null;
            //((DelegateCommand)RemoveToRecipientCommand).RaiseCanExecuteChanged();

        }
        protected bool OnRemoveRecipientToCanExecute()
        {
            return SelectedRecipientToListView != null;
        }
        protected async void OnRemoveRecipientCcExecuteAsync()
        {
            //if (SelectedRecipientCcListView == null)
            //    return;

            //var disInfo = await _dataModel.GetDistributionInformationByIdAsync(DistributionInformation.Id);
            //var recipientToRemove = disInfo?.RecipientsCc.Where(r => r.Id == SelectedRecipientCcListView.Id).First();
            //disInfo?.RecipientsCc.Remove(recipientToRemove!);
            //DistributionInformation.RecipientsCc.Remove(SelectedRecipientCcListView);
            //RecipientsCc_ListView.Remove(SelectedRecipientCcListView);
            //PopulateComboBoxCc();
            //SelectedRecipientCcListView = null;
            //((DelegateCommand)RemoveCcRecipientCommand).RaiseCanExecuteChanged();
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

        private async void SetNewRecipientToAsync(RecipientDto value)
        {

            //_selectedRecipientToComboBox = value;
            //RecipientsTo_ListView.Add(value);
            //DistributionInformation.RecipientsTo.Add(value);
            //var disInfo = await _dataModel.GetDistributionInformationByIdAsync(DistributionInformation.Id);
            //var recipient = await _dataModel.GetRecipientByIdAsync(value.Id);
            //disInfo?.RecipientsTo.Add(recipient);
            //RecipientsToComboBox.Remove(value);
            //_selectedRecipientToComboBox = null;
            //await _dataModel.SaveChangesAsync();
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

        private async void SetNewRecipientCcAsync(RecipientDto value)
        {
            //_selectedRecipientCcComboBox = value;
            //RecipientsCc_ListView.Add(value);
            //DistributionInformation.RecipientsCc.Add(value);
            //var disInfo = await _dataModel.GetDistributionInformationByIdAsync(DistributionInformation.Id);
            //var recipient = await _dataModel.GetRecipientByIdAsync(value.Id);
            //disInfo?.RecipientsCc.Add(recipient);
            //RecipientsCcComboBox.Remove(value);
            //_selectedRecipientCcComboBox = null;
            //await _dataModel.SaveChangesAsync();
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
        private bool AwaitingComboboxPopulation = false;

    }
}
