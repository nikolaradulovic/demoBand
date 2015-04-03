using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.ViewModel
{
    public class MenuView: ViewModelBase
    {
        private List<MenuItem> listItems;

        public List<MenuItem> ListItems
        {
            get
            {
                return listItems;
            }

            set
            {
                listItems = value;
                RaisePropertyChanged("ListItems");
            }
        }

        private string selectedItem;
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                if (selectedItem != null)
                {
                    //
                }
                RaisePropertyChanged("SelectedItem");
            }
        }

        public MenuView()
        {
            listItems = new List<MenuItem>();
            listItems.Add(new MenuItem("Create new song", ""));
            listItems.Add(new MenuItem("My songs", ""));
            listItems.Add(new MenuItem("Discover", ""));
            listItems.Add(new MenuItem("Settings", ""));
            listItems.Add(new MenuItem("Help", ""));

        }
    }
}
