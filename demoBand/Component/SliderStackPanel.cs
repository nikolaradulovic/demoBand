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

namespace demoBand.Component
{
    
    public class SliderStackPanel : StackPanel
    {
        private Instrument instrument;

        private Player player;

        public SliderStackPanel(Instrument instrument)
        {
            this.instrument = instrument;
            player = new Player(new Uri(instrument.Path));

            
        }

        private StackPanel CreateStackPanelForSlider() {
            StackPanel stackSlider = new StackPanel();
            stackSlider.Children.Add(player.Slider);
            stackSlider.Background = new SolidColorBrush(Color.FromArgb(0xFF, 112, 138, 144));
            stackSlider.Width = 42;
            return stackSlider;
        }

        private TextBlock createTextBlock()
        {
            TextBlock tb = new TextBlock();
            tb.Margin = new Thickness(0, 10, 0, 0);
            tb.Text = instrument.TypeOfInstrument.ToString();
            tb.FontSize = 20;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            return tb;
        }

        public void Initilize()
        {
            Height = 160;
            Width = 80;
            // stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(10);
            addComponents();
        }

        private void addComponents()
        {
            Children.Add(player.MediaElement);
            Children.Add(CreateStackPanelForSlider());
            Children.Add(createTextBlock());
            
        }
        
        public void SetVolumeSlider(int value)
        {
            player.Slider.Value = value;
        }

        public Player GetPlayer()
        {
            return player;
        }

    }
}
