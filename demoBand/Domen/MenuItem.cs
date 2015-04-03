using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Domen
{
    public class MenuItem
    {
       private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public MenuItem(string name, string imagePath)
        {
            this.name = name;
            this.ImagePath = imagePath;
        }
        
    }
}
