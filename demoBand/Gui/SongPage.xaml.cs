﻿using demoBand.Component;
using demoBand.Domen;
using demoBand.Model;
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
        private IRandomAccessStream audioStream; 
        private MediaCapture captureMedia;


        DispatcherTimer mediaTimer;
        private Dictionary<type, Player> players;
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
        }


        public async void startRecording()
        {
            MediaEncodingProfile encodingProfile = null;
            encodingProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);

            audioStream = new InMemoryRandomAccessStream();
            captureMedia.AudioDeviceController.VolumePercent = 90;
            await captureMedia.StartRecordToStreamAsync(encodingProfile, audioStream);

        }


        private async Task InitMediaCapture()
        {
            captureMedia = new MediaCapture();
            var captureInitSettings = new MediaCaptureInitializationSettings();

            //captureInitSettings.AudioDeviceId = "";
            // CaptureMedia.AudioDeviceController.Muted = true;
            captureInitSettings.StreamingCaptureMode = StreamingCaptureMode.Audio;
            await captureMedia.InitializeAsync(captureInitSettings);

            //CaptureMedia.MediaCaptureSettings.AudioDeviceId
            captureMedia.Failed += MediaCaptureOnFailed;
            captureMedia.RecordLimitationExceeded += MediaCaptureOnRecordLimitationExceeded;
        }


        private async void MediaCaptureOnRecordLimitationExceeded(MediaCapture sender)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                await sender.StopRecordAsync();
                var warningMessage = new MessageDialog("The media recording has been stopped because you exceeded the maximum recording length.", "Recording Stoppped");
                await warningMessage.ShowAsync();
            });
        }

        private async void MediaCaptureOnFailed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                var warningMessage = new MessageDialog(String.Format("The media capture failed: {0}", errorEventArgs.Message), "Capture Failed");
                await warningMessage.ShowAsync();
            });
        }


    }
}
