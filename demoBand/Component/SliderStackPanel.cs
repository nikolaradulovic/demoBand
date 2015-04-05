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
using Windows.UI.Xaml.Media.Imaging;

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
            
            switch (instrument.TypeOfInstrument.ToString())
            {
                case "Voice": img.Source = setMicPicture();
                    break;
                case "Guitar": img.Source = setGuitarPicture();
                    break;
                case "Drums": img.Source = setDrumsPicture();
                    break;
                case "Piano": img.Source = setPianoPicture();
                    break;
            }
          
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

        public BitmapImage setMicPicture() 
        {
            BitmapImage bp = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/Images/Dashboard/dashboard_0000_Forma-1.png");
            bp.UriSource = uri;
            return bp;
        }

        public BitmapImage setGuitarPicture()
        {
            BitmapImage bp = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/Images/Dashboard/dashboard_0004_Layer-2.png");
            bp.UriSource = uri;
            return bp;
        }

        public BitmapImage setDrumsPicture()
        {
            BitmapImage bp = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/Images/Dashboard/dashboard_0002_Bass.png");
            bp.UriSource = uri;
            return bp;
        }

        public BitmapImage setPianoPicture()
        {
            BitmapImage bp = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/Images/Dashboard/dashboard_0003_Piano.png");
            bp.UriSource = uri;
            return bp;
        }
    }
}
