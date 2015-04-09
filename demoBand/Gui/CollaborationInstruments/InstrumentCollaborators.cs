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

        private List<string> instrumentList;
        public InstrumentCollaborators() 
        {
            loadInstrumentList();
            arrangeStack();            
            createStackList();

        }

        private void arrangeStack()
        {
            Orientation = Orientation.Horizontal;

        }



        private void createStackList()
        {
            
            //foreach (Instrument i in instrumentList) 
            //{
            //    //uzmi sve izvodjace za radio group...
            //    RadioButton rb = new RadioButton();
            //    StackPanel sp = new StackPanel();
            //    sp.Margin = new Thickness(0,0,30,0);
            //    sp.Orientation = Orientation.Vertical;
            //    rb.FontSize = 20;
            //    rb.Foreground = new SolidColorBrush(Colors.DodgerBlue);
                
            //    //staviti iz izvodjaca ime samo
            //    rb.Content = null;
            //    Children.Add(rb);
            //}
        }



        private void loadInstrumentList()
        {
            throw new NotImplementedException();
        }
    }
}
