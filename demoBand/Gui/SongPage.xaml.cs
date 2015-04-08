﻿using demoBand.Component;
using demoBand.Domen;
using demoBand.Gui.StropheGui;
using demoBand.Model;
using demoBand.ParseBase;
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
        private double progressValue;
        private Song song;
        private RecordParse recordParse;
        private Boolean textExist;

        //private bool progressEnabled;



        public SongPage()
        {
            this.InitializeComponent();
          
      
            
        }

        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            recordParse = new RecordParse();
            players = new Dictionary<type, Player>();
            progressValue = 0;

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
            setProgressBarForSolo();
            setStartPropertiesSolo();
            populateRecordParseSolo();
        }

        private async void arrangeForCollaborator()
        {
            song = (Song)Session.GetInstance().getValueAt("song");
            instrument = (type)Enum.Parse(typeof(type), (string)Session.GetInstance().getValueAt("instrument"));
            //string songViewPath = song.SongViewPath;
            string songViewP = "";
            foreach (Instrument i in song.Instruments)
	        {
                if (i.TypeOfInstrument == instrument)
                {
                    songViewP = i.TextPath;
                    break;
                }
	        }
            if (songViewP == null) {
                textExist = false;
            }
            else
            {
                textExist = true;
            }
            songView = await SongView.createSongView(songViewP);
            await loadInstruments(song);
            createTextGridForCollaborator();
            if (song.Length != null)
            {
                setProgressBarForCollaborator(Convert.ToDouble(song.Length));
            }
            else
            {
                setProgressBarForCollaborator(Convert.ToDouble(Session.GetInstance().getValueAt("length").ToString()));
            }
            setStartPropertiesCollaborator();
            populateRecordParseCollaborator(song);
        }

        private void setProgressBarForSolo()
        {
            progressBar.IsEnabled = false;
            progressBar.Maximum = 0;
        }

        private void setProgressBarForCollaborator(double seconds)
        {
            progressBar.IsEnabled = true;
            setMaximumProgressBar(seconds);
        }

        private void setStartPropertiesSolo()
        {
            btnRecord.IsEnabled = true;
            btnStop.IsEnabled = false;
            btnPause.IsEnabled = false;
            btnListen.IsEnabled = false;
        }

        private void populateRecordParseCollaborator(Song song) {
            recordParse.ArtistSong = song.Author;
            recordParse.Songname = song.Name;
            recordParse.Instrument = instrument.ToString();
            recordParse.Username = Session.GetInstance().getValueAt("username").ToString();
            recordParse.Length = Convert.ToDouble(song.Length);
        }

        private void populateRecordParseSolo()
        {
            recordParse.ArtistSong = Session.GetInstance().getValueAt("username").ToString();
            recordParse.Username = Session.GetInstance().getValueAt("username").ToString();
            recordParse.Instrument = instrument.ToString();
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
            progressValue += 0.1;
            changeProgressText((int)progressValue);
            //progressBar.Value += 0.1;
            // ovo treba promeniti da radi,
            // timer tick treba d apovecava vrednost
            // ali ne da se prikazuje ako je recording
            if (progressBar.IsEnabled) 
            {
                progressBar.Value = progressValue;
            }
            else
            {
                progressBar.Value = 0;
            }
        }

        private async Task loadInstruments(Song s)
        {
            
            foreach (Instrument i in s.Instruments)
            {
                SliderStackPanel sliderPanel = await SliderStackPanel.createSliderStackPanel(i);
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

           
            if (progressBar.IsEnabled == false)
            {
                setMaximumProgressBar(progressValue);
                progressBar.IsEnabled = true;
            }
            progressValue = 0;
            progressBar.Value = 0;
            changeProgressText(0);
                      

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



        private void progressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
            txtProgres.Text = progressBar.Value.ToString();
            
            int seconds = Convert.ToInt32(progressBar.Value);
            if (textExist)
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
                progressValue = progressBar.Value;
            


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


        //private void txtComment_KeyUp(object sender, KeyRoutedEventArgs e)
        //{

        //    if (e.Key == VirtualKey.Enter) {
        //        int min = Convert.ToInt32(progressBar.Value) / 60;
        //        int sec = Convert.ToInt32(progressBar.Value) % 60;
        //        Comment comment = new Comment();
        //        comment.Min = min;
        //        comment.Sec = sec;
        //        comment.Text = txtComment.Text;
        //        lstComments.Items.Add(comment);
        //        txtComment.Text = "";
        //    }
        //}

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


        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //dodati da ne moze da se uradi save ako nije snimio nista
            if (choice == Choice.solo)
                showPopupDialog();
            else
            {
                await saveSong();
                //da se doda ne znam ko ce al ja necu,
                //message dialog: Are you sure you wwant to save?
            }
                
            
        }

        public void showPopupDialog() 
        {
            mainGrid.Opacity = 0.1;
            cmbBar.IsOpen = false;
            popunQuestion.IsLightDismissEnabled = true;
            gridSave.Width = mainGrid.ActualWidth;
            if(mainGrid.ActualHeight/4 >250)
            gridSave.Height = mainGrid.ActualHeight / 4;

            popunQuestion.IsOpen = true;
        }

        private void startSounds()
        {
            createMediaTimer();
            mediaTimer.Start();           
            playAllInstruments();
        }

        public void createTextGridForCollaborator() 
        {
            if (textExist) 
            {
                stropheGrid = new StropheText(songView);
            }
            else
            {
                stropheGrid = new StropheText(instrument);
            }    
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

        private void popunQuestion_Closed(object sender, object e)
        {
            mainGrid.Opacity = 1;
        }

        private async void btnYes_Click(object sender, RoutedEventArgs e)
        {
            
            recordParse.Songname = txtSongName.Text;
            recordParse.ArtistSong = Session.GetInstance().getValueAt("username").ToString();
            recordParse.Collaborator = txtCollaborator.Text;
            recordParse.Length = mediaRecording.NaturalDuration.TimeSpan.Seconds;
            await saveSong();

            popunQuestion.IsOpen = false;

        }

        private async Task saveSong()
        {
            byte[] songFile = await Converter.AudioStreamToByteArray(recorder.AudioStream);
           

            DataBaseParse.saveSongToRecord(songFile,
                                           recordParse.Songname,
                                           recordParse.ArtistSong,
                                           recordParse.Username,
                                           recordParse.Instrument,
                                           recordParse.Collaborator,
                                           recordParse.Length
                                           );


        }

    }

    


   
}
