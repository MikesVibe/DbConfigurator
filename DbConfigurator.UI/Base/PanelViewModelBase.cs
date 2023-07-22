﻿using DbConfigurator.UI.ViewModel.Interfaces;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class PanelViewModelBase : IMainPanelViewModel
    {
        protected bool _idHasBeenSet = false;
        public int Id { get; protected set; }

        public abstract Task LoadAsync();

        public void SetId(int id)
        {
            if (_idHasBeenSet)
                return;

            Id = id;
            _idHasBeenSet = true;
        }
    }
}
