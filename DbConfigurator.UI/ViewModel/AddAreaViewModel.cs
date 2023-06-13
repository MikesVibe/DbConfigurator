using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Entities;
using DbConfigurator.Model.Wrapper;
using DbConfigurator.UI.Windows;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public class AddAreaViewModel : IEditingViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AreaDtoWrapper Area { get; set; }

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }
        public Action<bool> CloseAction { get; set; }

        public AddAreaViewModel()
        {
            AreaDto area = new();
            Area = new(area);
            Area.Name = "";

            AddCommand = new DelegateCommand(Add, CanAdd);
            CancelCommand = new DelegateCommand(() => CloseWindow(false));
        }

        private void Add()
        {
            CloseWindow(true);
        }

        private bool CanAdd()
        {
            return true;
        }

        private void CloseWindow(bool dialogResult)
        {
            CloseAction?.Invoke(dialogResult);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
