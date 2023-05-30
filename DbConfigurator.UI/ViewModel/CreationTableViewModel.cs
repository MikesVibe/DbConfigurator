using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.UI.Startup;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class CreationTableViewModel : TableViewModelBase
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;

        public ObservableCollection<CountryDto> Countries { get; set; } = new ObservableCollection<CountryDto>();

        public CreationTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var countries = await _dataModel.GetAllCountriesAsync(); 
            foreach(var country in countries)
            {
                var mapped = _autoMapper.Mapper.Map<CountryDto>(country);
                Countries.Add(mapped);
            }


        }

        protected override void OnAddExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnRemoveCanExecute()
        {
            return false;
        }

        protected override void OnRemoveExecute()
        {
            throw new NotImplementedException();
        }
    }
}
