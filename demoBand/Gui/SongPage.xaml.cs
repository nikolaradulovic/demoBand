using demoBand.Component;
using demoBand.Domen;
using demoBand.Gui.StropheGui;
using demoBand.Model;
using demoBand.SongDescription;
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
using Windows.System;
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

        private SongView songView;
        private StropheText stropheGrid;
        private Choice choice;

        //private bool progressEnabled;



        public SongPage()
        {
            this.InitializeComponent();
          
      
            
        }

        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            players = new Dictionary<type, Player>();


            choice = (Choice)Enum.Parse(typeof(Choice), (string)Session.GetInstance().getValueAt("choice"));

            if (choice == Choice.collaborator)
                arrangeForCollaborator();
            if (choice == Choice.solo)
                arrangeForsolo();

          
            
        }

        private void arrangeForsolo()
        {
            instrument = (type)Enum.Parse(typeof(type), (string)Session.GetInstance().getValueAt("instrument"));
            stropheGrid = new StropheText(instrument);
            progressBar.IsEnabled = false;
            setStartPropertiesSolo();
        }

        private async void arrangeForCollaborator()
        {
            Song s = (Song)Session.GetInstance().getValueAt("song");
            instrument = (type)Enum.Parse(typeof(type), (string)Session.GetInstance().getValueAt("instrument"));
            string songViewPath = s.SongViewPath;
            songView = await SongView.createSongView(songViewPath);
            loadInstruments(s);
            createTextGridForCollaborator();
            progressBar.IsEnabled = true;
            setStartPropertiesCollaborator();
        }

        private void setStartPropertiesSolo()
        {
            btnRecord.IsEnabled = true;
            btnStop.IsEnabled = false;
            btnPause.IsEnabled = false;
            btnListen.IsEnabled = false;
        }



        private void setStartPropertiesCollaborator()
        {
            btnRecord.IsEnabled = true;
            btnStop.IsEnabled = false;
            btnPause.IsEnabled = false;
            btnListen.IsEnabled = true;
        }

        private void timer_Tick(object sender, object e)
        {
            progressBar.Value += 0.1;
            // ovo treba promeniti da radi,
            // timer tick treba d apovecava vrednost
            // ali ne da se prikazuje ako je recording
            if (progressBar.IsEnabled) {
                double seconds = progressBar.Value;
                changeProgressText((int)seconds);
            }
        }

        private void loadInstruments(Song s)
        {
            
            foreach (Instrument i in s.Instruments)
            {
                SliderStackPanel sliderPanel = new SliderStackPanel(i);
                if (i.TypeOfInstrument == instrument)
                    sliderPanel.SetVolumeSlider(0);
                sliderPanel.Initilize();
                slideContainer.Children.Add(sliderPanel);

                players.Add(i.TypeOfInstrument, sliderPanel.GetPlayer());

            }
            double length = Convert.ToDouble(s.Length);
            setMaximumProgressBar(length);
            //progressBar.Maximum = Convert.ToDouble(s.Length);
            //int min = Convert.ToInt32(s.Length) / 60;
            //int sec = Convert.ToInt32(s.Length) % 60;
            //txtDuration.Text ="/"+ min.ToString() + ":" + sec.ToString();
        }

        private void setMaximumProgressBar(double length)
        {
            progressBar.Maximum = length;
            int min = Convert.ToInt32(length) / 60;
            int sec = Convert.ToInt32(length) % 60;
            txtDuration.Text = "/" + min.ToString() + ":" + sec.ToString("00");
        }

        


        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.pause();

            }
            if (mediaRecording != null)
            {
                mediaRecording.Pause();
            }
            if (mediaTimer != null)
            {
                mediaTimer.Stop();
            }

            btnStop.IsEnabled = true;
            btnRecord.IsEnabled = false;
            btnListen.IsEnabled = true;
            btnPause.IsEnabled = false;
            
        }

        private void disableStopButton()
        {
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.stop();
            }

            if (progressBar.IsEnabled)
            {
                
                double seconds = progressBar.Value;
                setMaximumProgressBar(seconds);
                progressBar.Value = 0;

            }
                      
            if (recorder != null)
            {
                if (recorder.Active)
                    recorder.stopRecording();
                mediaRecording.SetSource(recorder.AudioStream,"audio/mpeg");
            }
            if (mediaTimer != null)
            {
                mediaTimer.Stop();
            }
           

            btnRecord.IsEnabled = true;
            btnListen.IsEnabled = true;
            btnPause.IsEnabled = false;
            btnStop.IsEnabled = false;
            clearGridMain();
           
        }

        private void clearGridMain()
        {
            if (choice == Choice.collaborator)
            {
                gridMain.Children.Clear();
            }
        }


        private void UpdateProgressBarValues()
        {
            progressBar.IsEnabled = true;
            TimeSpan time = mediaRecording.NaturalDuration.TimeSpan;
            setMaximumProgressBar(time.Seconds);
        }

        



        private void progressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
            txtProgres.Text = progressBar.Value.ToString();
            int seconds = Convert.ToInt32(progressBar.Value);
            changeTextGrid(seconds);
            changeProgressText(seconds);

            //int secT = seconds % 60;
            //int minT = seconds / 60;
            //txtProgres.Text =minT.ToString() + ":" + secT.ToString("00");
        }

        private void changeProgressText(int seconds)
        {
            int secT = seconds % 60;
            int minT = seconds / 60;
            txtProgres.Text = minT.ToString() + ":" + secT.ToString("00");
        }



        private void changeTextGrid(int seconds)
        {
            stropheGrid.TryChange(seconds);
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
            int min = Convert.ToInt32(progressBarValue) / 60;
            int sec = Convert.ToInt32(progressBarValue) % 60;
            TimeSpan time = new TimeSpan(0, min, sec);

            foreach (KeyValuePair<type, Player> ply in players)
            {
                
                ply.Value.MediaElement.Position = time;
            }

            if (mediaRecording != null)
            {
               
                TimeSpan length = mediaRecording.NaturalDuration.TimeSpan;

                if (length > time)
                {
                    mediaRecording.Play();
                    mediaRecording.Position = time;
                }
                else
                {
                    mediaRecording.Stop();
                }
            }
            return progressBarValue;
        }

        public delegate void recordDelegate();

        private  void btnRecord_Click(object sender, RoutedEventArgs e)
        {

            gridMain.Children.Add(new RecordTimer());
            RecordTimer.recordEvent += startRecord;
            btnPause.IsEnabled = false;
            btnListen.IsEnabled = false;
            btnRecord.IsEnabled = false;
            //btnStop.IsEnabled = true;

        }

        private void btnListen_Click(object sender, RoutedEventArgs e)
        {
            //UpdateProgressBarValues();
            startSong();
            
            btnRecord.IsEnabled = false;
            btnPause.IsEnabled = true;
            btnStop.IsEnabled = true;
            btnListen.IsEnabled = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();

        }


        private void txtComment_KeyUp(object sender, KeyRoutedEventArgs e)
        {

            if (e.Key == VirtualKey.Enter) {
                int min = Convert.ToInt32(progressBar.Value) / 60;
                int sec = Convert.ToInt32(progressBar.Value) % 60;
                Comment comment = new Comment();
                comment.Min = min;
                comment.Sec = sec;
                comment.Text = txtComment.Text;
                lstComments.Items.Add(comment);
                txtComment.Text = "";
            }
        }

        private void lstComments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Comment comment = e.AddedItems[0] as Comment;
            int pbValue = comment.Min * 60 + comment.Sec;
            progressBar.Value = pbValue;
            TimeSpan time = new TimeSpan(0, comment.Min, comment.Sec);

            foreach (KeyValuePair<type, Player> ply in players)
            {

                ply.Value.MediaElement.Position = time;
            }

            if (mediaRecording != null)
            {
                mediaRecording.Position = time;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {


        }

        private void startSounds()
        {
            createMediaTimer();
            mediaTimer.Start();           
            playAllInstruments();
        }

        public void createTextGridForCollaborator() 
        {
            stropheGrid = new StropheText(songView);
        }
        private void addTextGrid(StropheText stropheText)
        {
            gridMain.Children.Clear();
            gridMain.Children.Add(stropheText);
        }
        private void startSong()
        {
            
            startSounds();
            playRecordedIfExist();      
            addTextGrid(stropheGrid);
        }

        private void startRecord() 
        {
            startSounds();
            startRecording();;
            addTextGrid(stropheGrid);
            disableStopButton();
            
        }
        private void createMediaTimer()
        {
            if (mediaTimer == null)
            {
                mediaTimer = new DispatcherTimer();
                mediaTimer.Interval = TimeSpan.FromSeconds(0.1);
                mediaTimer.Tick += timer_Tick;

            }
        }

        private void playAllInstruments()
        {
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.start();

            }
        }

        private void playRecordedIfExist()
        {
            if (mediaRecording != null)
            {
                mediaRecording.Play();
            }
        }

        private void startRecording()
        {
            recorder = new Recorder();
            recorder.startRecording();
        }

    }

    


   
}
