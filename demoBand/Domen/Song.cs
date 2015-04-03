using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace demoBand.Domen
{
    public class Song
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private string length;

        public string Length
        {
            get { return length; }
            set { length = value; }
        }

        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private List<Instrument> instruments;

        public List<Instrument> Instruments
        {
            get { return instruments; }
            set { instruments = value; }
        }

        public Song() { }
    }
}
