using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;
using demoBand.Domen;

namespace demoBand.ParseBase
{
    public class DataBaseParse
    {

        public static async void saveSongToRecord(byte[] song, string songName, string artist, string username, string instrument, string collaborator)
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

            await record.SaveAsync();
        }

        





        public async static Task<List<SongListItem>> getSongListItemsForUser(string username)
        {
            List<SongListItem> list = new List<SongListItem>();

            // adding where I am author
            //var _query = from song in ParseObject.GetQuery("Song")
            //            where song.Get<String>("artist") == username
            //            select song;
            //IEnumerable<ParseObject> results = await _query.FindAsync();
            //foreach (ParseObject resultObject in results)
            //{
            //    SongListItem sli = new SongListItem();
            //    sli.SongName = resultObject.Get<string>("songname");
            //    sli.ArtistName = resultObject.Get<string>("songartist");
            //    list.Add(sli);
            //}

            
            //adding where I am collaborator
            var query = from record in ParseObject.GetQuery("Record")
                        where record.Get<string>("username") == username 
                             || record.Get<string>("collaborator") == username
                        select record;
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
    }




}
