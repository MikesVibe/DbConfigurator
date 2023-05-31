using DbConfigurator.UI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    class AddCountryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _areaName;
        public string AreaName
        {
            get { return _areaName; }
            set
            {
                _areaName = value;
                OnPropertyChanged(nameof(AreaName));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public AddCountryViewModel()
        {
            //AddCommand = new RelayCommand(Add, CanAdd);
            //CancelCommand = new RelayCommand(Cancel);
        }

        private void Add(object parameter)
        {
            // Add button click logic here
            // Perform any desired operations with the entered area name
            // For example, you could close the window and return the value to the main window
            CloseWindow(true);
        }

        private bool CanAdd(object parameter)
        {
            // Enable or disable the Add button based on any conditions
            // For example, you can check if the area name is not empty
            return !string.IsNullOrEmpty(AreaName);
        }

        private void Cancel(object parameter)
        {
            // Cancel button click logic here
            // For example, you could simply close the window without performing any operations
            CloseWindow(false);
        }

        private void CloseWindow(bool dialogResult)
        {
            // Close the window and set the dialog result
            if (Window != null)
            {
                Window.DialogResult = dialogResult;
                Window.Close();
            }
        }

        // Reference to the window
        public AddCountryWindow Window { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
