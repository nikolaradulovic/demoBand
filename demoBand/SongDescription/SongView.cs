using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace demoBand.SongDescription
{
    public class SongView
    {
        private List<Strophe> strophes;

        public List<Strophe> Strophes
        {
            get { return strophes; }
            set { strophes = value; }
        }

        public void addStrophe(Strophe strophe) 
        {
            strophes.Add(strophe);
        }

        public SongView() 
        {
            strophes = new List<Strophe>();
        }

        

        public  async Task populateList(string path)
        {
          
            try
            {
                StorageFile sFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"Assets\Json\otherside.txt");
                 var fileStream = await sFile.OpenStreamForReadAsync();
            using (StreamReader sr = new StreamReader(fileStream))
            {
                string line = await sr.ReadToEndAsync();
           
                strophes = (List<Strophe>)Newtonsoft.Json.JsonConvert.DeserializeObject(line, typeof(List<Strophe>));
            }
            }
            catch (Exception ex)
            {

                string ex1 = ex.Message;
            }

           



        }

        public async static Task<SongView> createSongView(string path) 
        {
            SongView sw = new SongView();
            await sw.populateList(path);
            return sw;
        }

    }
}
