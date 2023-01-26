using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DbConfigurator.UI.ViewModel
{
    public class DetailsViewModel : ViewModelBase, IDetailViewModel
    {
        public DetailsViewModel()
        {
            Recipients_ObservableCollection = new ObservableCollection<Recipient>();



            for (int i = 0; i < 5; i++)
            {
                Recipients_ObservableCollection.Add(new Recipient
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "John.Doe@comp.net"
                });
            }

            GridDataCollection = Recipients_ObservableCollection;
        }



        private void GridDataCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GridDataCollectionChangedCommand.Execute(e);
        }
        private void ExecuteGridDataCollectionChangedCommand(NotifyCollectionChangedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }


        public int DefaultRowIndex { get { return 0; } }
        public ObservableCollection<Recipient> Recipients_ObservableCollection { get; }
        public ObservableCollection<Recipient> GridDataCollection
        {
            get => _gridDataCollection;
            set
            {
                _gridDataCollection = value;
                _gridDataCollection.CollectionChanged += GridDataCollectionOnCollectionChanged;
            }
        }
        public Recipient SelectedRecipient { get; set; }
        public DelegateCommand<NotifyCollectionChangedEventArgs> GridDataCollectionChangedCommand =>
             _gridDataCollectionChangedCommand ?? (_gridDataCollectionChangedCommand = new DelegateCommand<NotifyCollectionChangedEventArgs>(ExecuteGridDataCollectionChangedCommand));


        public DelegateCommand<NotifyCollectionChangedEventArgs> _gridDataCollectionChangedCommand;
        private ObservableCollection<Recipient> _gridDataCollection;


    }
}
