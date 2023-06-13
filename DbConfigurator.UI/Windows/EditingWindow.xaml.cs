﻿using DbConfigurator.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DbConfigurator.UI.Windows
{
    /// <summary>
    /// Interaction logic for EditingWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {
        public EditingWindow(IEditingViewModel editingViewModel)
        {
            InitializeComponent();
            DataContext = editingViewModel;

            editingViewModel.CloseAction = new(CloseWindow);
        }

        private void CloseWindow(bool dialogResult)
        {
            this.DialogResult = dialogResult;
            this.Close();
        }
    }
}
