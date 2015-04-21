using demoBand.Domen;
using demoBand.ParseBase;
using demoBand.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            instrument = type.Voice;
        }


        private Recorder recorder = null;
        private type instrument;
        private int micValue;

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            recorder = new Recorder();
            recorder.startRecording();
            btnRecord.IsEnabled = false;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (recorder != null && recorder.Active)
            {
                recorder.stopRecording();
                playerRec.SetSource(recorder.AudioStream, "audio/mpeg");
            }
            if (recorder != null && !recorder.Active)
            {
                playerRec.Stop();
                playerRec.Position = new TimeSpan(0);
            }
            btnRecord.IsEnabled = true;

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (recorder != null && !recorder.Active) 
            {
                playerRec.Play();
            }
        }

        private void btnPlaySample_Click(object sender, RoutedEventArgs e)
        {
            // inicijalizacija source za playerSample
            //playerSample.Source = new Uri("");
            playerSample.Volume = ((double)sliderVolume.Value)/100;
            playerSample.Play();
            
        }

        private void btnStopSample_Click(object sender, RoutedEventArgs e)
        {
            playerSample.Stop();
        }

        private void cmbInstrumentPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInstrumentPicker != null)
            {
                int index = cmbInstrumentPicker.SelectedIndex;
                switch (index)
                {
                    case 0: instrument = type.Voice;
                        break;
                    case 1: instrument = type.Guitar;
                        break;
                    case 2: instrument = type.Piano;
                        break;
                    case 3: instrument = type.Drums;
                        break;
                }
            }   
        }

        private void sliderVolume_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sliderVolume != null)
            {
                micValue = (int)sliderVolume.Value;
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataBaseParse.insertOrUpdateVolume(instrument.ToString(), micValue);
        }
    }
}
