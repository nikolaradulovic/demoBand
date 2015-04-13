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
        private static List<SongListItem> allDiscoverSongs = new List<SongListItem>();
        public static List<SongListItem> shownSongs;
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
            shownSongs = list;
        }

        public DiscoverView() { }

    }
}
