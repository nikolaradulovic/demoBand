using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace demoBand.Util
{
    public class Recorder
    {
       

        private IRandomAccessStream audioStream;
        private MediaCapture captureMedia;
        private MediaEncodingProfile encodingProfile;
        private bool active;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public IRandomAccessStream AudioStream
        {
            get { return audioStream; }
            set { audioStream = value; }
        }
 

        public Recorder()
        {
            active = false;
            encodingProfile = new MediaEncodingProfile();
            audioStream = new InMemoryRandomAccessStream();
           
        }

        public async void startRecording() 
        {
            await InitMediaCapture();
            encodingProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
            captureMedia.AudioDeviceController.VolumePercent = 100;
            await captureMedia.StartRecordToStreamAsync(encodingProfile, audioStream);
            active = true;
           
        }

        public async void startRecording(int volumePercent)
        {
            await InitMediaCapture();
            encodingProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
            captureMedia.AudioDeviceController.VolumePercent = (float)volumePercent;
            await captureMedia.StartRecordToStreamAsync(encodingProfile, audioStream);
            active = true;

        }



        public async void stopRecording()
        {
            await captureMedia.StopRecordAsync();
            active = false;

        }
        //stavljeno na public bilo private
        public async Task InitMediaCapture()
        {
            captureMedia = new MediaCapture();
            var captureInitSettings = new MediaCaptureInitializationSettings();

            captureInitSettings.StreamingCaptureMode = StreamingCaptureMode.Audio;
            await captureMedia.InitializeAsync(captureInitSettings);

            captureMedia.Failed += MediaCaptureOnFailed;
            captureMedia.RecordLimitationExceeded += MediaCaptureOnRecordLimitationExceeded;
        }

        public async void MediaCaptureOnRecordLimitationExceeded(MediaCapture sender)
        {
            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            //{
                await sender.StopRecordAsync();
                var warningMessage = new MessageDialog("The media recording has been stopped because you exceeded the maximum recording length.", "Recording Stoppped");
                await warningMessage.ShowAsync();
            //});
        }

        private async void MediaCaptureOnFailed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            //{
                var warningMessage = new MessageDialog(String.Format("The media capture failed: {0}", errorEventArgs.Message), "Capture Failed");
                await warningMessage.ShowAsync();
            //});
        }
    }


}
