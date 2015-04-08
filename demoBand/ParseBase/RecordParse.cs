using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.ParseBase
{
    public class RecordParse
    {
        private byte[] file;
        private string songname;
        private string artistsong;
        private double length;
        private string username;
        private string instrument;
        private string collaborator;
        private Uri urlFile;

        public Uri UrlFile
        {
            get { return urlFile; }
            set { urlFile = value; }
        }

        public string Collaborator
        {
            get { return collaborator; }
            set { collaborator = value; }
        }

        public string Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
       
        public string ArtistSong
        {
            get { return artistsong; }
            set { artistsong = value; }
        }
       
        

        public string Songname
        {
            get { return songname; }
            set { songname = value; }
        }
        


        public byte[] File
        {
            get { return file; }
            set { file = value; }
        }

        public double Length
        {
            get { return length; }
            set { length = value; }
        }


        public RecordParse()
        {
            collaborator = "";
        }
    }
}
