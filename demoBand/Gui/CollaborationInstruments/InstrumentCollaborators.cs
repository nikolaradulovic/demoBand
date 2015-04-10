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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace demoBand.Gui.CollaborationInstruments
{
    public class InstrumentCollaborators
    {

        private List<Collaborator> collaborators;
        private List<GridViewItemCollaborator> gridItemColl;
        private type instrumentType;
        private GridViewItemCollaborator choosenCollaborator;

        public type InstrumentType
        {
            get { return instrumentType; }
            set { instrumentType = value; }
        }

        public List<GridViewItemCollaborator> GridItemColl
        {
            get { return gridItemColl; }
            set { gridItemColl = value; }
        }


        public InstrumentCollaborators(List<Collaborator> collaborators, type instrumentType) 
        {
            this.collaborators = collaborators;
            this.instrumentType = instrumentType;
            Observer.getInstance().register(this);

            choosenCollaborator = null;

            populateList();

           


            //((global::Windows.UI.Xaml.Controls.ListViewBase)(listViewCollaborators)).ItemClick += clickItem;
            //((global::Windows.UI.Xaml.UIElement)(listViewCollaborators)).RightTapped += rightClickItem;
            //((global::Windows.UI.Xaml.Controls.Primitives.Selector)(listViewCollaborators)).SelectionChanged+=selectionChanged;
        }

        private void populateList()
        {
            //listViewCollaborators = new ListView();

            //listViewCollaborators.IsItemClickEnabled = true;
            //listViewCollaborators.IsRightTapEnabled = true;
            //listViewCollaborators.IsEnabled = true;
            
            
            //listViewCollaborators.Items.Add(createMeAsCollaborator());
            //foreach (Collaborator coll in collaborators)
            //{
            //    listViewCollaborators.Items.Add(new GridViewItemCollaborator(coll));
            //}

            gridItemColl = new List<GridViewItemCollaborator>();
            gridItemColl.Add(createMeAsCollaborator());

            foreach (Collaborator coll in collaborators)
            {
                gridItemColl.Add(new GridViewItemCollaborator(coll));
            }


            
            
        }

        private GridViewItemCollaborator createMeAsCollaborator()
        {
            Instrument ins = new Instrument();
            ins.TypeOfInstrument = instrumentType;
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
            coll.Instrument = ins;

            GridViewItemCollaborator item = new GridViewItemCollaborator(coll);
           
            return item;
        }


        



       

     
       

        //public void clickItem(object sender, ItemClickEventArgs e)
        //{
        //    ListViewItem lv = e.ClickedItem as ListViewItem;
        //    lv.IsSelected = true;
        //    if (listViewCollaborators.SelectedItem == listViewCollaborators.Items[0])
        //    {
        //        Observer.getInstance().notifyAll(this);
        //    }
        //}

        //public void rightClickItem(object sender, RightTappedRoutedEventArgs e)
        //{
        //    if (listViewCollaborators.SelectedItem == listViewCollaborators.Items[0])
        //    {
        //        Observer.getInstance().notifyAll(this);
        //    }
        //}


        //public void selectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    choosenCollaborator = listViewCollaborators.SelectedItem as GridViewItemCollaborator;
        //    if (listViewCollaborators.SelectedItem == listViewCollaborators.Items[0])
        //    {
        //        Observer.getInstance().notifyAll(this);
        //    }
        //}



        //public class GridViewItemCollaborator : GridViewItem
        //{
        //    private Instrument instrument;
        //    private string text;

        //    public Instrument Instrument
        //    {
        //        get { return instrument; }
        //        set { instrument = value; }
        //    } 

        //    public GridViewItemCollaborator(Collaborator collaborator)
        //    {
                
        //        text = collaborator.CollaboratorName;
        //        instrument = collaborator.Instrument;
        //        arrange();
                
        //    }

        //    private void arrange()
        //    {
        //        Height = 50;
        //        Width = 200;
        //        BorderThickness = new Thickness(4);
        //        BorderBrush = new SolidColorBrush(Colors.AliceBlue);
        //        FontSize = 20; 
        //     //   BorderBrush = BorderBrush.
        //        Background = new SolidColorBrush(Colors.DodgerBlue);
        //        Background.Opacity = 0.2;
        //        Foreground = new SolidColorBrush(Colors.White);
        //     //   Foreground = new SolidColorBrush(Colors.White);
        //        //TextBox tb = new TextBox();
        //        //tb.Text = text;
        //       // Name = text;
        //        Content = text;
        //    }

        //    public override string ToString()
        //    {
        //        return text;
        //    }
            


        //}


        internal void notify()
        {

            //if (listViewCollaborators.SelectedItem == listViewCollaborators.Items[0])
            //{
            //    listViewCollaborators.SelectedItem = -1;
            //}
        }

        public Instrument getChoosenInstrument()
        {
            if (choosenCollaborator == null)
                return null;

            Instrument i = new Instrument();
            i.TypeOfInstrument = instrumentType;
            i.Path = choosenCollaborator.Instrument.Path;
            return i;
        }

        public bool isMyChoice()
        {
            //if (choosenCollaborator == listViewCollaborators.Items[0])
            //{
            //    return true;
            //}
            return false;
        }


    }

    

}
