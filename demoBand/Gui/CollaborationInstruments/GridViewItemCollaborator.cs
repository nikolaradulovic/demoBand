using demoBand.Domen;
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

    public class GridViewItemCollaborator : GridViewItem
    {
        private Instrument instrument;
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Instrument Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }

        public GridViewItemCollaborator(Collaborator collaborator)
        {

            text = collaborator.CollaboratorName;
            instrument = collaborator.Instrument;
            arrange();

        }

        private void arrange()
        {
            Height = 50;
            Width = 200;
            BorderThickness = new Thickness(4);
            BorderBrush = new SolidColorBrush(Colors.AliceBlue);
            FontSize = 20;
            //   BorderBrush = BorderBrush.
            Background = new SolidColorBrush(Colors.DodgerBlue);
            Background.Opacity = 0.2;
            Foreground = new SolidColorBrush(Colors.White);
            //   Foreground = new SolidColorBrush(Colors.White);
            //TextBox tb = new TextBox();
            //tb.Text = text;
            // Name = text;
            Content = text;
        }

        public override string ToString()
        {
            return text;
        }



    }





}
