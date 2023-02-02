using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.Data.Repositories;
using DbConfigurator.UI.View;
using DbConfigurator.UI.Wrapper;
using Microsoft.VisualBasic;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class DistributionInformationTableViewModel : TableViewModelBase, IDistributionInformationTableView
    {
        public DistributionInformationTableViewModel(
            IEventAggregator eventAggregator,
            IDistributionInformationRepository distributionInformationRepository
            ) : base(eventAggregator)
        {
            _distributionInformationRepository = distributionInformationRepository;

            DisInfoLookup_ObservableCollection = new ObservableCollection<DistributionInfoLookupWrapper>();


        }

        public override async Task LoadAsync()
        {
            var distributionInformations = await _distributionInformationRepository.GetAllAsync();


            foreach (var distInfo in distributionInformations)
            {
                foreach (var country in distInfo.BuisnessUnit.Countries)
                {
                    var temp = new DistributionInfoLookup
                    {
                        Area = distInfo.BuisnessUnit.Area.Name,
                        BuisnessUnit = distInfo.BuisnessUnit.Name,
                        Priority = distInfo.Priority.Name,
                        Country = country.Name
                    };

                    DisInfoLookup_ObservableCollection.Add(new DistributionInfoLookupWrapper(temp));
                }
            }



            //foreach (var distributionInformation in distributionInformations)
            //{
            //    var wrapper = new DistributionInformationWrapper(distributionInformation);
            //    DistributionInformation_ObservableCollection.Add(wrapper);
            //    wrapper.PropertyChanged += DistributionInformation_ObservableCollection_PropertyChanged;
            //}
        }
        private void DistributionInformation_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _distributionInformationRepository.HasChanges();
            }
            if (e.PropertyName == nameof(DistributionInfoLookupWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }
        protected override bool OnSaveCanExecute()
        {
            return SelectedDisInfoLookup != null
                && !SelectedDisInfoLookup.HasErrors
                && HasChanges;
        }
        protected override void OnSaveExecute()
        {
            _distributionInformationRepository.SaveAsync();
            HasChanges = _distributionInformationRepository.HasChanges();
            Id = SelectedDisInfoLookup.Id;

        }



        public int DefaultRowIndex { get { return 0; } }
        //public DistributionInformationWrapper SelectedDistributionInformation
        //{
        //    get { return _selectedDistributionInformation; }
        //    set
        //    {
        //        _selectedDistributionInformation = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public ObservableCollection<DistributionInformationWrapper> DistributionInformation_ObservableCollection { get; set; }

        public DistributionInfoLookupWrapper SelectedDisInfoLookup
        {
            get { return _selectedDisInfoLookup; }
            set
            {
                _selectedDisInfoLookup = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DistributionInfoLookupWrapper> DisInfoLookup_ObservableCollection { get; set; }


        private IDistributionInformationRepository _distributionInformationRepository;
        private IEventAggregator _eventAggregator;
        //private DistributionInformationWrapper _selectedDistributionInformation;
        private DistributionInfoLookupWrapper _selectedDisInfoLookup;
    }
}
