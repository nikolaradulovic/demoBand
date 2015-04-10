using demoBand.Domen;
using demoBand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace demoBand.Gui.CollaborationInstruments
{
    public class InstrumentCollaborators: StackPanel
    {

        private List<Collaborator> collaborators;
        private ListView listViewCollaborators;
        private type instrumentType;

       

        public InstrumentCollaborators(List<Collaborator> collaborators, type instrumentType) 
        {
            this.collaborators = collaborators;
            this.instrumentType = instrumentType;
            Observer.getInstance().register(this);
            populateListView();
            arrangeStack();

            ((global::Windows.UI.Xaml.Controls.ListViewBase)(listViewCollaborators)).ItemClick += clickItem;
        }

        private void populateListView()
        {
            listViewCollaborators = new ListView();
            listViewCollaborators.Items.Add(createMeAsCollaborator());
            foreach (Collaborator coll in collaborators)
            {
                listViewCollaborators.Items.Add(new ListViewItemCollaborator(coll));
            }

            Children.Add(listViewCollaborators);
            
        }

        private ListViewItemCollaborator createMeAsCollaborator()
        {
            Instrument ins = new Instrument();
            string content = null;
            switch (instrumentType) {
                case type.Voice: content = "I sing";
                    break;
                case type.Guitar: content = "I play guitar";
                    break;
                case type.Drums: content = "I play drums";
                    break;
                case type.Piano: content = "I play piano";
                    break;
            }
            Collaborator coll = new Collaborator();
            coll.CollaboratorName = content;
            
            ListViewItemCollaborator item = new ListViewItemCollaborator(coll);
            return item;
        }


        



        private void arrangeStack()
        {
            Orientation = Orientation.Horizontal;


        }

        public type InstrumentType
        {
            get { return instrumentType; }
            set { instrumentType = value; }
        }
       

        public void clickItem(object sender, ItemClickEventArgs e)
        {
            if (listViewCollaborators.SelectedItem == listViewCollaborators.Items[0])
            {
                Observer.getInstance().notifyAll(this);
            }
        }



        private class ListViewItemCollaborator 
        {
            private Instrument instrument;
            private string text;

            public Instrument Instrument
            {
                get { return instrument; }
                set { instrument = value; }
            } 

            public ListViewItemCollaborator(Collaborator collaborator)
            {
                text = collaborator.CollaboratorName;
                instrument = collaborator.Instrument;
            }

            private void arrange()
            {
                
            }

            public override string ToString()
            {
                return text;
            }
            


        }


        internal void notify()
        {

            if (listViewCollaborators.SelectedItem == listViewCollaborators.Items[0])
            {
                listViewCollaborators.SelectedItem = -1;
            }
        }



    }

    

}
