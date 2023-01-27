using DbConfigurator.Model;
using DbConfigurator.UI.Data.Repositories;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DbConfigurator.UI.ViewModel
{
    public class RecipientDetailViewModel : DetailViewModelBase, IRecipientDetailViewModel
    {
        public RecipientDetailViewModel(IRecipientRepository recipientRepository)
        {
            _recipientRepository = recipientRepository;

            CellEditEndingCommand = new RelayCommand(CellEditEndingCommandExecute);

            Recipients_ObservableCollection = new ObservableCollection<Recipient>();
        }


        public async Task LoadAsync()
        {
            var recipients = await _recipientRepository.GetAllAsync();

            foreach(var recipient in recipients)
            {
                Recipients_ObservableCollection.Add(recipient);
            }
        }

        public ObservableCollection<Recipient> Recipients_ObservableCollection { get; set; }
        public Recipient SelectedRecipient { get; set; }
        public int DefaultRowIndex { get { return 0; } }
        public RelayCommand CellEditEndingCommand { get; set; }


        private void CellEditEndingCommandExecute(object obj)
        {
            //var recipient = (Recipient)obj;
            //var Id = Recipients_ObservableCollection.Where(rec => recipient.Id == rec.Id).FirstOrDefault()?.Id;
            //if(Id == null)
            //    return;

            //Recipients_ObservableCollection[(int)Id] = recipient;
            //var a = 0;

            var selectedCells = (IEnumerable<DataGridCellInfo>)obj;
            object item;
            DataGridColumn column;
            FrameworkElement value;

            foreach (var cell in selectedCells)
            {
                item = cell.Item;
                column = cell.Column;
                value = column.GetCellContent(item);


            }
        }



        private ObservableCollection<Recipient> _gridDataCollection;
        private IRecipientRepository _recipientRepository;

    }
}
