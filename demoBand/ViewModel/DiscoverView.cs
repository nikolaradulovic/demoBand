using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.ViewModel
{
    public class DiscoverView : ViewModelBase
    {
        private static ObservableCollection<SongListItem> allDiscoverSongs;

        public static ObservableCollection<SongListItem> AllDiscoverSongs
        {
            get { return DiscoverView.allDiscoverSongs; }
            set { DiscoverView.allDiscoverSongs = value; }
        }
        
        public static ObservableCollection<SongListItem> shownSongs;
        //  shownSongs = new ObservableCollection<SongListItem>();
        public ObservableCollection<SongListItem> ShownSongs
        {
            get
            {
                return shownSongs;
            }

            set
            {
                shownSongs = value;
                RaisePropertyChanged("ShownSongs");
            }
        }

        private string selectedSong;
        public string SelectedSong
        {
            get
            {
                return selectedSong;
            }
            set
            {
                selectedSong = value;
                if (selectedSong != null)
                {
                    //
                }
                RaisePropertyChanged("SelectedSong");
            }

        }

        public static void setDiscoverSongs(ObservableCollection<SongListItem> list)
        {
            
            allDiscoverSongs = list;

        }

        public DiscoverView() 
        {
          
        }


        

        public void refrehList()
        {
            RaisePropertyChanged("DiscoverSongs");
        }
    }
}
