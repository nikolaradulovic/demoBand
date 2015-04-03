using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace demoBand.ViewModel
{
    public class PracticeView: ViewModelBase
    {
        private static List<Song> listSongs;


        public List<Song> ListSongs
        {
            get
            {
                return listSongs;
            }

            set
            {
                listSongs = value;
                RaisePropertyChanged("ListSongs");
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

        public PracticeView() { }

        public static async Task populateList()
        {

            StorageFile sFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"Assets\Json\practiceSongs.txt");

            var fileStream = await sFile.OpenStreamForReadAsync();
            using (StreamReader sr = new StreamReader(fileStream))
            {
                string line = await sr.ReadToEndAsync();
                listSongs = (List<Song>)Newtonsoft.Json.JsonConvert.DeserializeObject(line, typeof(List<Song>));
            }



        }  

    }
}
