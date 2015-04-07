using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace demoBand.Domen
{
    public class SongListItem
    {
        private string songName;
        private string artistName;
        
        public string ArtistName
        {
            get { return artistName; }
            set { artistName = value; }
        }
        public string SongName
        {
            get { return songName; }
            set { songName = value; }
        }
    }
}
