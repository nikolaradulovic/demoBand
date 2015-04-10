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

        public SongListItem() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() == typeof(SongListItem))
            {
                SongListItem sli = obj as SongListItem;
                if (sli.ArtistName == artistName && sli.SongName == songName)
                    return true;
            }
            return false;
        }

    }
}
