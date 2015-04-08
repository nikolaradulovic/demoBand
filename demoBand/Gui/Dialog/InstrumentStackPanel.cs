using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace demoBand.Gui.Dialog
{
    public class InstrumentStackPanel : StackPanel
    {

        public InstrumentStackPanel(List<Instrument> instruments)
        {
            arrangeStackPanel();
            foreach (Instrument instrument in instruments)
            {
                InstrumentButton btn = new InstrumentButton(instrument.TypeOfInstrument.ToString());
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.Margin = new Thickness(10, 0, 0, 0);
                Children.Add(btn);
            }
            
        }

        public InstrumentStackPanel()
        {
            arrangeStackPanel();
            List<string> instruments = new List<string>();
            instruments.Add("Voice");
            instruments.Add("Guitar");
            instruments.Add("Piano");
            instruments.Add("Drums");
            foreach (string instrument in instruments)
            {
                InstrumentButton btn = new InstrumentButton(instrument);
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.Margin = new Thickness(10, 0, 0, 0);
                Children.Add(btn);
            }

        }



        private void arrangeStackPanel()
        {
            Orientation = Orientation.Horizontal;
            TextBlock tb = new TextBlock();
            tb.Text = "Choose your instrument.";
            tb.FontSize = 30;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            tb.Margin = new Thickness(0, 30, 0, 20);
            Children.Add(tb);

        }

    }
}
