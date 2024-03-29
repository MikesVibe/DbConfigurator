﻿using DbConfigurator.UI.Base.Contracts;
using System.Windows;

namespace DbConfigurator.UI.Windows
{
    /// <summary>
    /// Interaction logic for EditingWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {
        public EditingWindow(IDetailViewModel editingViewModel)
        {
            InitializeComponent();
            DataContext = editingViewModel;

            editingViewModel.CloseAction = new(CloseWindow);
        }

        private void CloseWindow(bool obj)
        {
            this.Close();
        }
    }
}
