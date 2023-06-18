using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Table;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.Model.Entities.Wrapper.Table;
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
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;

        public DistributionInformationPanelViewModel(
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            IDataModel dataModel,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;


        }

        public async override Task LoadAsync()
        {
            await PopulateComboBoxesWithData();

            var distributionInformations = await _dataModel.GetAllDistributionInformationAsync();

            foreach (var distributionInformation in distributionInformations)
            {
                var mapped = _autoMapper.Mapper.Map<DistributionInformationTableItem>(distributionInformation);
                var wrapped = new DistributionInformationTableItemWrapper(mapped);
                Items.Add(wrapped);
            }
        }

        private async Task PopulateComboBoxesWithData()
        {
            //var areas = EnumerableToObservableCollection(_dataModel.AreasDto);
            //Area_Collection = areas;
            //await PopulateBuisnessUnitCombobox();
            //await PopulateCountryCombobox();
            //var priorities = EnumerableToObservableCollection(_dataModel.PrioritiesDto);
            //Priority_Collection = priorities;
        }
        private async Task PopulateBuisnessUnitCombobox(int? areaId = null)
        {
            //IEnumerable<BuisnessUnit> avilableBuisnessUnits;
            //if (areaId == null)
            //    avilableBuisnessUnits = await _dataModel.GetAllBuisnessUnitsAsync();
            //else
            //    avilableBuisnessUnits = await _dataModel.GetBuisnessUnitsAsync((int)areaId);

            //BuisnessUnit_Collection.Clear();
            //foreach (var bu in avilableBuisnessUnits)
            //{
            //    var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(bu);
            //    BuisnessUnit_Collection.Add(mapped);
            //}
        }
        private async Task PopulateCountryCombobox(int? buisnessUnitId = null)
        {
            //IEnumerable<Country> countries;
            //if (buisnessUnitId == null)
            //    countries = await _dataModel.GetAllCountriesAsync();
            //else
            //    countries = await _dataModel.GetCountriesAsync((int)buisnessUnitId);

            //Country_Collection.Clear();
            //foreach (var country in countries)
            //{
            //    var mapped = _autoMapper.Mapper.Map<CountryDto>(country);
            //    Country_Collection.Add(mapped);
            //}
        }

        protected override void OnAddExecute()
        {
            ////Create New Distribution Infrotmaion
            //var defaultPriotrity = _dataModel.DefaultPriority;
            //var defaultRegion = _dataModel.DefaultRegion;
            //var distributionInformation = new DistributionInformation(defaultRegion, defaultPriotrity);

            //await _dataModel.AddAsync(distributionInformation);
            //await _dataModel.SaveChangesAsync();

            //var distributionInformationEntity = await _dataModel.GetDistributionInformationByIdAsync(distributionInformation.Id);
            //var distributionInformationDto = _autoMapper.Mapper.Map<DistributionInformationDto>(distributionInformationEntity);
            //var wrappedDisInfo = new DistributionInformationDtoWrapper(distributionInformationDto);

            //DistributionInformation_ObservableCollection.Add(wrappedDisInfo);
            //SelectedDistributionInformation = wrappedDisInfo;



            //DO NOT REMOVE (This is for future update)
            var distributionInformationViewModel = new AddDistibutionInformationViewModel();
            bool? result = DialogService.ShowDialog(distributionInformationViewModel);

            if (result == false)
                return;

            var distributionInformation = _autoMapper.Mapper.Map<DistributionInformation>(distributionInformationViewModel.DistributionInformation);

            _dataModel.Add(distributionInformation);
            _dataModel.SaveChanges();
            var mapped = _autoMapper.Mapper.Map<DistributionInformationTableItem>(distributionInformation);
            var wrapped = new DistributionInformationTableItemWrapper(mapped);
            Items.Add(wrapped);
        }
        protected override void OnRemoveExecute()
        {
            //var distributionInformationToRemove = await _dataModel.GetDistributionInformationByIdAsync(SelectedDistributionInformation.Id);
            //_dataModel.Remove(distributionInformationToRemove);
            ////_dataModel.Remove(_dataModel.DistributionInformations.Where(d => d.Id == SelectedDistributionInformation.Id).First());
            //var deletedDistributionInfo = SelectedDistributionInformation;
            //await _dataModel.SaveChangesAsync();

            //DistributionInformation_ObservableCollection.Remove(SelectedDistributionInformation);
            //SelectedDistributionInformation = null;
            //RecipientsTo_ListView.Clear();
            //RecipientsCc_ListView.Clear();

            //((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();

        }


        protected override void OnSelectionChangedExecute()
        {
            //if (SelectedDistributionInformation == null)
            //    return;

            //PopulateComboBoxTo();
            //PopulateComboBoxCc();

            ////Setting selected items in comboBoxes
            //SelectAreaComboBox();
            //SelectBuisnessUnitComboBox();
            //SelectCountryComboBox();
            //SelectPriorityComboBox();

            ////Setting Items in ListViews
            //RecipientsTo_ListView = EnumerableToObservableCollection(SelectedDistributionInformation.RecipientsTo);
            //RecipientsCc_ListView = EnumerableToObservableCollection(SelectedDistributionInformation.RecipientsCc);


            base.OnSelectionChangedExecute();
        }

        protected override void OnEditExecute()
        {
        }
    }
}
