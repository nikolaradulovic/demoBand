using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Domen
{
    public class Comment
    {
        string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        int min;

        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        int sec;

        public int Sec
        {
            get { return sec; }
            set { sec = value; }
        }

        public override string ToString()
        {
            return min + ":" + sec.ToString("00") + " - " + text;
        }
    }
}
