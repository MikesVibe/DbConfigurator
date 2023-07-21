using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System.ComponentModel;

namespace DbConfigurator.UI.Features.Areas
{
    public class AreaDetailViewModel : DetailViewModelBase<IAreaService, AreaDto>, IDetailViewModel, INotifyPropertyChanged
    {
        public AreaDetailViewModel(IAreaService areaService, IEventAggregator eventAggregator) : base(areaService, eventAggregator)
        {
            Title = "Area";
            ViewWidth = 560;
            ViewHeight = 340;
        }

        protected override void OnCreate()
        {
            EventAggregator.GetEvent<CreateAreaEvent>()
                  .Publish(
                new CreateAreaEventArgs
                {
                    Area = new AreaDto
                    {
                        Id = EntityDto!.Id,
                        Name = EntityDto!.Name,
                    }
                });
        }
        protected override void OnUpdate()
        {
            EventAggregator.GetEvent<EditAreaEvent>()
                  .Publish(
                new EditAreaEventArgs
                {
                    Area = new AreaDto
                    {
                        Id = EntityDto!.Id,
                        Name = EntityDto!.Name,
                    }
                });
        }
    }
}
