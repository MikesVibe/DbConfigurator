using DbConfigurator.Core.Models;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System.ComponentModel;

namespace DbConfigurator.UI.Features.Areas
{
    public class AreaDetailViewModel : DetailViewModelBase<IAreaService, Area, AreaWrapper>, IDetailViewModel, INotifyPropertyChanged
    {
        public AreaDetailViewModel(IAreaService areaService, IEventAggregator eventAggregator) : base(areaService, eventAggregator)
        {
            Title = "Area";
            ViewWidth = 560;
            ViewHeight = 340;
        }

        protected override void OnCreate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<CreateAreaEvent>()
                  .Publish(
                new CreateAreaEventArgs
                {
                    Entity = new Area
                    {
                        Id = EntityDto.Id,
                        Name = EntityDto.Name,
                    }
                });
        }
        protected override void OnUpdate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<EditAreaEvent>()
                  .Publish(
                new EditAreaEventArgs
                {
                    Entity = new Area
                    {
                        Id = EntityDto.Id,
                        Name = EntityDto.Name,
                    }
                });
        }
    }
}
