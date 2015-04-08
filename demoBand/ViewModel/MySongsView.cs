using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.ViewModel
{
    public class MySongsView: ViewModelBase
    {
        private static List<SongListItem> mySongs;

        public List<SongListItem> MySongs
        {
            get
            {
                return mySongs;
            }

             set
            {
                mySongs = value;
                RaisePropertyChanged("MySongs");
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

        public MySongsView() { }

        public static void setMySongs(List<SongListItem> list) 
        {
            mySongs = list;
        }

    }
}
