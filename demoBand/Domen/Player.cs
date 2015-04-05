using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace demoBand.Domen
{
   public class Player
    {
        private Slider slider;

        public Slider Slider
        {
            get { return slider; }

        }
        private MediaElement mediaElement;

        public MediaElement MediaElement
        {
            get { return mediaElement; }

        }

        public Player(Uri uri)
        {
            mediaElement = new MediaElement();
            mediaElement.Source = uri;
            mediaElement.AutoPlay = false;
            slider = new Slider();
            slider.Height = 150;
            slider.Value = 50;
            slider.Orientation = Orientation.Vertical;
            slider.HorizontalAlignment = HorizontalAlignment.Center;
            


            ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(slider)).ValueChanged += volumeChanged;

        }


        public void volumeChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            slider = sender as Slider;
            double volume = slider.Value * 0.01;
            mediaElement.Volume = volume;
        }


        public void start()
        {
            mediaElement.Play();

        }

        public void SetSliderValue(int value)
        {
            slider.Value = value;
        }



        internal void pause()
        {
            mediaElement.Pause();
        }

        internal void stop()
        {
            mediaElement.Stop();
        }
    }
}
