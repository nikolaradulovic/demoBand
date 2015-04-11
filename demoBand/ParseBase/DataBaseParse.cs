using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;
using demoBand.Domen;

using Windows.Storage.Streams;
using System.IO;
using System.Net.Http;
using demoBand.Util;

namespace demoBand.ParseBase
{
    public class DataBaseParse
    {

        public static async void saveSongToRecord(byte[] song, string songName, string artist, string username, string instrument, string collaborator, double length,string exist)
        {
            ParseFile file = new ParseFile("song.mp3", song);
            await file.SaveAsync();

            var record = new ParseObject("Record");

            record["file"] = file;
            record["songname"] = songName;
            record["songartist"] = artist;
            record["username"] = username;
            record["instrument"] = instrument;
            record["collaborator"] = collaborator;
            record["length"] = length;
            record["exist"] = exist;

            await record.SaveAsync();
        }


        public async static Task<List<SongListItem>> getSongListItemAuthor(string username)
        {
            List<SongListItem> list = new List<SongListItem>();
            var query = from song in ParseObject.GetQuery("Record")
                         where song.Get<String>("songartist") == username
                             
                         select song;
            IEnumerable<ParseObject> results = await query.FindAsync();
            foreach (ParseObject resultObject in results)
            {
                SongListItem sli = new SongListItem();
                sli.SongName = resultObject.Get<string>("songname");
                sli.ArtistName = resultObject.Get<string>("songartist");
                if (!list.Contains(sli))
                    list.Add(sli);

                
            }
            return list;
        }

        public async static Task<List<SongListItem>> getSongListItemCollaborator(string username)
        {
            List<SongListItem> list = new List<SongListItem>();
            var query = from song in ParseObject.GetQuery("Record")
                        where (song.Get<string>("collaborator") == username
                           && song.Get<string>("songartist") != username)
                        select song;
            IEnumerable<ParseObject> results = await query.FindAsync();
            
            
            foreach (ParseObject resultObject in results)
            {
                
                string songname = resultObject.Get<string>("songname");
                string songartist = resultObject.Get<string>("songartist");
                await populateMelodiesCollaborator(songname, songartist, list);
                //list.AddRange(sli);
            }
            return list;
        }

        private async static Task<List<SongListItem>> populateMelodiesCollaborator(string songname, string songartist, List<SongListItem> list)
        {
            //List<SongListItem> list = new List<SongListItem>();
            var query = from song in ParseObject.GetQuery("Record")
                        where song.Get<String>("songname") == songname
                           && song.Get<String>("songartist") == songartist
                           //&& song.Get<String>("exist") == "YES" 
                        select song;
            IEnumerable<ParseObject> results = await query.FindAsync();
            foreach (ParseObject resultObject in results)
            {
                SongListItem sli = new SongListItem();
                sli.SongName = resultObject.Get<string>("songname");
                sli.ArtistName = resultObject.Get<string>("songartist");
            
                if (!list.Contains(sli))
                    list.Add(sli);
            }
            return list;
        }







        public async static Task<List<Collaborator>> getCollaborator(string songname, string songartist, string insturment)
        {
            List<Collaborator> list = new List<Collaborator>();

            var query = from record in ParseObject.GetQuery("Record")
                        where record.Get<string>("songname") == songname
                           && record.Get<string>("songartist") == songartist
                           && record.Get<string>("exist") == "YES"
                           && record.Get<string>("instrument") == insturment
                        select record;
            IEnumerable<ParseObject> results = await query.FindAsync();

            foreach (ParseObject resultObject in results)
            {
                Instrument i = new Instrument();
                var songFile = resultObject.Get<ParseFile>("file");
                i.Path = songFile.Url.ToString();
                i.TypeOfInstrument = (type)Enum.Parse(typeof(type), insturment);
                

                Collaborator col = new Collaborator();
                col.CollaboratorName = resultObject.Get<string>("collaborator");
                col.Instrument = i;
                
                if(!list.Contains(col))
                    list.Add(col);
                
            }
            return list;

        }


        public async static Task<int> getLengthOfSong(string songname, string songartist) {
            var query = from record in ParseObject.GetQuery("Record")
                        where record.Get<string>("songname") == songname
                           && record.Get<string>("songartist") == songartist
                        select record;

            ParseObject result = await query.FirstAsync();
            int length = result.Get<int>("length");
            return length;

        }
        


     

        

    }
}
   






