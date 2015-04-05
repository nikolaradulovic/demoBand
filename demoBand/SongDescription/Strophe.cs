using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.SongDescription
{
   // [ParseClassName("Strophe")]
    public class Strophe 
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private int start;

        public int Start
        {
            get { return start; }
            set { start = value; }
        }
        private int end;

        public int End
        {
            get { return end; }
            set { end = value; }
        }


        public Strophe(string text, int start, int end)
        {
            this.text = text;
            this.start = start;
            this.end = end;
        }

        public Strophe()
        {

        }

    }
}
