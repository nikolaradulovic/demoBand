using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Gui.CollaborationInstruments
{
    public class Observer
    {
        private static Observer instance;
        private List<InstrumentCollaborators> stackPanels;


        private Observer() { 
            stackPanels = new List<InstrumentCollaborators>();
        }


        public static Observer getInstance()
        {
            if (instance == null)
                instance = new Observer();
            return instance;
        }

        public void register(InstrumentCollaborators stackPanel)
        {
            stackPanels.Add(stackPanel);
        }

        public void notifyAll(InstrumentCollaborators stackPanelCol)
        {
            foreach (InstrumentCollaborators staPan in stackPanels)
            {
                if (staPan != stackPanelCol)
                {
                    staPan.notify();
                }
            }
        }







    }
}
