using DbConfigurator.Model;
using DbConfigurator.UI.Startup;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class CreationTableViewModel : TableViewModelBase
    {
        private readonly IDataModel dataModel;
        private readonly AutoMapperConfig autoMapper;

        public CreationTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            this.dataModel = dataModel;
            this.autoMapper = autoMapper;
        }

        public override Task LoadAsync()
        {
            return Task.CompletedTask;
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
