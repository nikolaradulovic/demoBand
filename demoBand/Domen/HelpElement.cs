using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Domen
{
    public class HelpElement
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public HelpElement(string text, string imagePath) 
        {
            this.text = text;
            this.imagePath = imagePath;
        }


    }
}
