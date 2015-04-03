using demoBand.Component;
using demoBand.Domen;
using demoBand.Model;
using demoBand.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace demoBand.Gui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongPage : Page
    {
        private type instrument;
        DispatcherTimer mediaTimer;
        private Dictionary<type, Player> players;
        private Recorder recorder;

        public SongPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Song s = (Song) Session.GetInstance().getValueAt("song");
            instrument = (type)Enum.Parse(typeof(type), (string)Session.GetInstance().getValueAt("instrument"));
            loadInstruments(s);
        }

        private void timer_Tick(object sender, object e)
        {

            progressBar.Value += 0.1;
        }

        private void loadInstruments(Song s)
        {



            players = new Dictionary<type, Player>();
            foreach (Instrument i in s.Instruments)
            {
                SliderStackPanel sliderPanel = new SliderStackPanel(i);
                if (i.TypeOfInstrument == instrument)
                    sliderPanel.SetVolumeSlider(0);
                sliderPanel.Initilize();
                slideContainer.Children.Add(sliderPanel);

                players.Add(i.TypeOfInstrument, sliderPanel.GetPlayer());

            }
            progressBar.Maximum = Convert.ToDouble(s.Length);
            int min = Convert.ToInt32(s.Length) / 60;
            int sec = Convert.ToInt32(s.Length) % 60;
            txtDuration.Text = min.ToString() + ":" + sec.ToString();
        }

      

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.pause();

            }
            mediaTimer.Stop();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.stop();

            }

            progressBar.Value = 0;
            mediaTimer.Stop();
            if (recorder.Active)
            {
                recorder.stopRecording();
                mediaRecording.SetSource(recorder.AudioStream,"audio/mpeg");
            }
                
            
        }

        private void progressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            txtProgres.Text = progressBar.Value.ToString();
            int secT = Convert.ToInt32(progressBar.Value) % 60;
            int minT = Convert.ToInt32(progressBar.Value) / 60;
            txtProgres.Text = minT.ToString() + ":" + secT.ToString("00");
        }

        private void progressBar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            double MousePosition = e.GetPosition(progressBar).X;
            this.progressBar.Value = SetProgressBarValue(MousePosition);


        }

        private double SetProgressBarValue(double MousePosition)
        {
            double ratio = MousePosition / progressBar.ActualWidth;
            double progressBarValue = ratio * progressBar.Maximum;


            foreach (KeyValuePair<type, Player> ply in players)
            {
                int min = Convert.ToInt32(progressBarValue) / 60;
                int sec = Convert.ToInt32(progressBarValue) % 60;
                TimeSpan time = new TimeSpan(0, min, sec);
                ply.Value.MediaElement.Position = time;



            }

            return progressBarValue;
        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.start();

            }

            mediaTimer = new DispatcherTimer();
            mediaTimer.Interval = TimeSpan.FromSeconds(0.1);
          
            mediaTimer.Tick += timer_Tick;
            mediaTimer.Start();
            recorder = new Recorder();
            recorder.startRecording();


        }

        private void btnListen_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.start();

            }
               
          

            mediaTimer = new DispatcherTimer();
            mediaTimer.Interval = TimeSpan.FromSeconds(0.1);

            mediaTimer.Tick += timer_Tick;
            mediaTimer.Start();

            mediaRecording.Play();
            
            
        }


    


    }
}
