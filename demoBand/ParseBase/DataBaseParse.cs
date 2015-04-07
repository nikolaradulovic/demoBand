using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;

namespace demoBand.ParseBase
{
    public class DataBaseParse
    {

        public static async void saveSongToRecord(byte[] song, string songName, string artist, string username, string instrument)
        {
            ParseFile file = new ParseFile("song.mp3", song);
            await file.SaveAsync();

            var record = new ParseObject("Record");

            record["file"] = file;
            record["songname"] = songName;
            record["songartist"] = artist;
            record["username"] = username;
            record["instrument"] = instrument;

            await record.SaveAsync();
        }


    }
}
