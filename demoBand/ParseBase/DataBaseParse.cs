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

        public static async void saveSongToRecord(byte[] song, string songName, string artist, string username, string instrument, string collaborator, double length)
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
                list.Add(sli);
            }
            return list;
        }

        public async static Task<List<SongListItem>> getSongListItemCollaborator(string username)
        {
            List<SongListItem> list = new List<SongListItem>();
            var query = from song in ParseObject.GetQuery("Record")
                        where song.Get<String>("collaborator") == username
                        select song;
            IEnumerable<ParseObject> results = await query.FindAsync();
            foreach (ParseObject resultObject in results)
            {
                SongListItem sli = new SongListItem();
                sli.SongName = resultObject.Get<string>("songname");
                sli.ArtistName = resultObject.Get<string>("songartist");
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
                           && record.Get<string>("collaborator") == songartist
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
        


        public async static Task<List<RecordParse>> getAllFiles(string songname, string artistsong)
        {
            List<RecordParse> list = new List<RecordParse>();

            var query = from record in ParseObject.GetQuery("Record")
                        where record.Get<string>("songname") == songname
                             || record.Get<string>("artistsong") == artistsong
                        select record;
            IEnumerable<ParseObject> results = await query.FindAsync();

            foreach (ParseObject resultObject in results)
            {
                RecordParse rp = new RecordParse();

                var songFile = resultObject.Get<ParseFile>("file");
                byte[] byteFile = await new HttpClient().GetByteArrayAsync(songFile.Url);

                rp.UrlFile = songFile.Url;
                rp.File = byteFile;
                rp.Instrument = resultObject.Get<string>("instrument");
                rp.Length = resultObject.Get<double>("length");
                list.Add(rp);

            }
            return list;



        }

    }
}
   






