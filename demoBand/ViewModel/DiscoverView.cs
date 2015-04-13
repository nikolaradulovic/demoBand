using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.ViewModel
{
    public class DiscoverView : ViewModelBase
    {
        public static List<SongListItem> allDiscoverSongs;
        public static List<SongListItem> shownSongs = new List<SongListItem>();
        public List<SongListItem> ShownSongs
        {
            get
            {
                return shownSongs;
            }

            set
            {
                shownSongs = value;
                RaisePropertyChanged("DiscoverSongs");
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

        public static void setDiscoverSongs(List<SongListItem> list)
        {
            allDiscoverSongs = list;
        }

        public DiscoverView() { }


        

        public void refrehList()
        {
            RaisePropertyChanged("DiscoverSongs");
        }
    }
}
