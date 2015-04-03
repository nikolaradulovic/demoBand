using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Domen
{
    public class Instrument
    {
        private string path;
        private type typeOfInstrument;

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


    }
     public enum type
    {
        Voice,
        Guitar,
        Drums,
        Piano
       
    };
    
}
