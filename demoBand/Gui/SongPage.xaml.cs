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
      
        

        public SongPage()
        {
            this.InitializeComponent();
          
      
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Song s = (Song) Session.GetInstance().getValueAt("song");
            instrument = (type)Enum.Parse(typeof(type), (string)Session.GetInstance().getValueAt("instrument"));
            string songViewPath = s.SongViewPath;
            songView = await SongView.createSongView(songViewPath);
            
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
            if (mediaRecording != null)
            {
                mediaRecording.Pause();
            }
            if (mediaTimer != null)
            {
                mediaTimer.Stop();
            }
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.stop();
            }

            progressBar.Value = 0;
                     
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

                
            
        }

        private void progressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            txtProgres.Text = progressBar.Value.ToString();
            int seconds = Convert.ToInt32(progressBar.Value);
            changeTextGrid(seconds);

            int secT = Convert.ToInt32(progressBar.Value) % 60;
            int minT = Convert.ToInt32(progressBar.Value) / 60;
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
                mediaRecording.Position = time;
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

        }

        private void btnListen_Click(object sender, RoutedEventArgs e)
        {          
            playAll();         
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

        private void playAll()
        {
            foreach (KeyValuePair<type, Player> ply in players)
            {
                ply.Value.start();

            }

            mediaTimer = new DispatcherTimer();
            mediaTimer.Interval = TimeSpan.FromSeconds(0.1);

            mediaTimer.Tick += timer_Tick;
            mediaTimer.Start();

            if (mediaRecording != null)
            {
                mediaRecording.Play();
            }

        }

        public void createTextGrid() 
        {
            stropheGrid = new StropheText(songView);
            gridMain.Children.Add(stropheGrid);
        }


        private void startSong()
        {
            //i record i novi grid
            playAll();
        }

        private void startRecord() 
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
            createTextGrid();
        }


        
    }

   
}
