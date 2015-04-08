using demoBand.Domen;
using demoBand.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace demoBand.Component
{

    public class SliderStackPanel : StackPanel
    {
        private Instrument instrument;
        private Player player;

        public Instrument Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }

        

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        

        public async  static Task<SliderStackPanel> createSliderStackPanel(Instrument instrument)
        {
            SliderStackPanel stack = new SliderStackPanel();
            stack.Instrument = instrument;
            //if (instrument.AudioByteArray == null)
            //{
                stack.Player = new Player(new Uri(instrument.Path));
            //}
            //else
            //{
            //    stack.Player = await Player.createPlayer(instrument.AudioByteArray);
            //}
            return stack;
        }



        private StackPanel CreateStackPanelForSlider()
        {
            StackPanel stackSlider = new StackPanel();
            stackSlider.Children.Add(player.Slider);

            //stackSlider.Background = new SolidColorBrush(Color.FromArgb(0xFF, 112, 138, 144));
            //stackSlider.Width = 42;
            return stackSlider;
        }

        private Image createImageBox()
        {
            Image img = new Image();
            img.Height = 80;
            img.Width = 80;
            VerticalAlignment = VerticalAlignment.Center;
            BitmapImage bp = new BitmapImage();
            Uri uri = InstrumentPicture.getImageUri(instrument.TypeOfInstrument);
            bp.UriSource = uri;
            img.Source = bp;
            return img;
        }

        //private TextBlock createTextBlock()
        //{

        //    TextBlock tb = new TextBlock();
        //    tb.Margin = new Thickness(0, 10, 0, 0);
        //    tb.Text = instrument.TypeOfInstrument.ToString();
        //    tb.FontSize = 20;
        //    tb.HorizontalAlignment = HorizontalAlignment.Center;
        //    return tb;
        //}

        public void Initilize()
        {
            Height = 160;
            Width = 140;
            // stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(10);
            Orientation = Orientation.Horizontal;
            addComponents();
        }

        private void addComponents()
        {
            Children.Add(player.MediaElement);
            Children.Add(CreateStackPanelForSlider());
            Children.Add(createImageBox());

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
