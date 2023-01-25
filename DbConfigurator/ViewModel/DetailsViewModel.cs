using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class DetailsViewModel : ViewModelBase, IDetailViewModel
    {
        public DetailsViewModel()
        {
            Recipients_ObservableCollection = new ObservableCollection<Recipient>
            {
                new Recipient()
                {
                    FirstName =  "Mikołaj",
                    LastName =  "Mrukowski",
                    Email =  "mrukowski.m@company.com"
                },
                new Recipient()
                {
                    FirstName =  "Mikołaj",
                    LastName =  "Mrukowski",
                    Email =  "mrukowski.m@company.com"
                },
                new Recipient()
                {
                    FirstName =  "Mikołaj",
                    LastName =  "Mrukowski",
                    Email =  "mrukowski.m@company.com"
                },
                new Recipient()
                {
                    FirstName =  "Mikołaj",
                    LastName =  "Mrukowski",
                    Email =  "mrukowski.m@company.com"
                },
                new Recipient()
                {
                    FirstName =  "Mikołaj",
                    LastName =  "Mrukowski",
                    Email =  "mrukowski.m@company.com"
                }
            };
        }


        public int DefaultRowIndex { get { return 0; } }
        public ObservableCollection<Recipient> Recipients_ObservableCollection { get; }

        public Recipient SelectedRecipient { get; set; }


    }
}
