using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Domen
{
    public class Collaborator 
    {

        private string collaboratorName;
        private Instrument instrument;

        public Instrument Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }

        public string CollaboratorName
        {
            get { return collaboratorName; }
            set { collaboratorName = value; }
        }
        
        public Collaborator() { }

        public override string ToString()
        {
            return collaboratorName;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Collaborator)) {
                Collaborator col = obj as Collaborator;
                if (col.CollaboratorName == collaboratorName)
                    return true;

            }
            return false;
                
        }


    }
}
