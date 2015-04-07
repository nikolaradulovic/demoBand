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
            listItems.Add(new MenuItem("Create new song", "../Assets/Images/Icons/new-song-small.png"));
            listItems.Add(new MenuItem("My songs", "../Assets/Images/Icons/my-songs.png"));
            listItems.Add(new MenuItem("Discover", "../Assets/Images/Icons/discover.png"));
            listItems.Add(new MenuItem("Settings", "../Assets/Images/Icons/settings.png"));
            listItems.Add(new MenuItem("Help", "../Assets/Images/Icons/help.png"));

        }
    }
}
