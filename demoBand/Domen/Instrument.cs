using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace demoBand.Domen
{
    public class Instrument
    {
        private string path;
        private type typeOfInstrument;
        private byte[] audioByteArray;
        private string textPath;

        public string TextPath
        {
            get { return textPath; }
            set { textPath = value; }
        }

        public byte[] AudioByteArray
        {
            get { return audioByteArray; }
            set { audioByteArray = value; }
        }

        public type TypeOfInstrument
        {
            get { return typeOfInstrument; }
            set { typeOfInstrument = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public Instrument(string path) 
        {
            this.path = path;
           // this.tipInstrumenta = tipInstrumenta;
        }

        //public Instrument(byte[] audioByteArray)
        //{
        //    this.audioByteArray = audioByteArray;
        //}

        public Instrument()
        {

        }

        public static List<string> allStringInstruments()
        {
            List<string> list = new List<string>();
            list.Add("Voice");
            list.Add("Guitar");
            list.Add("Drum");
            list.Add("Piano");

            return list;

        }


    }
     public enum type
    {
        Voice,
        Guitar,
        Drums,
        Piano
       
    };
    
}
