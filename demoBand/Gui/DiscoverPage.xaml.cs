using demoBand.Domen;
using demoBand.Model;
using demoBand.ParseBase;
using demoBand.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class DiscoverPage : Page
    {
        public DiscoverPage()
        {
            this.InitializeComponent();
        }

     //   private Stack stack;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
         //   stack = new Stack(DiscoverView.shownSongs);
            //discoverView = new DiscoverView();

        }


        private async void gridList_ItemClick(object sender, ItemClickEventArgs e)
        {
            SongListItem songItem = e.ClickedItem as SongListItem;

            List<string> allInstruments = Instrument.allStringInstruments();

            string songname = songItem.SongName;
            string author = songItem.ArtistName;
            int length = await DataBaseParse.getLengthOfSong(songname, author);

            List<Collaborator> voiceList = await DataBaseParse.getCollaborator(songname, author, "Voice");
            List<Collaborator> guitarList = await DataBaseParse.getCollaborator(songname, author, "Guitar");
            List<Collaborator> drumList = await DataBaseParse.getCollaborator(songname, author, "Drums");
            List<Collaborator> pianoList = await DataBaseParse.getCollaborator(songname, author, "Piano");

            SenderObject so = new SenderObject();
            so.putExtra("voicelist", voiceList);
            so.putExtra("guitarlist", guitarList);
            so.putExtra("drumlist", drumList);
            so.putExtra("pianolist", pianoList);
            so.putExtra("songname", songname);
            so.putExtra("author", author);
            so.putExtra("length", length);


            Session.GetInstance().insertValue("choice", Choice.collaborator.ToString());


            //hardkodovan instrument
            Session.GetInstance().insertValue("instrument", type.Voice.ToString());
            //
            Frame.Navigate(typeof(CollaborationSong), so);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void searchSong_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            //SongListItem sl = new SongListItem();
            //sl.ArtistName = "Proba";
            //sl.SongName = "Proba";
            //DiscoverView.shownSongs.Add(sl);

            //ovde nesto ne radi
            //if (e.Key == VirtualKey.Back)
            //{
            //    DiscoverView.shownSongs = stack.pop();
            //}
            string text = txtSearchSong.QueryText;
            ObservableCollection<SongListItem> result = searchByText(text);
          //  stack.push(result);

            DiscoverView.shownSongs.Clear();
            foreach (SongListItem sl in result)
            {
                DiscoverView.shownSongs.Add(sl);
            }
            //ovako ne moze da se puni ObservableCollection a da se to vidi u gridu u real time
         //   DiscoverView.shownSongs = result;
     

        }


        private ObservableCollection<SongListItem> searchByText(string text)
        {
            ObservableCollection<SongListItem> result = new ObservableCollection<SongListItem>();
            ObservableCollection<SongListItem> allSongs = DiscoverView.AllDiscoverSongs;
            foreach(SongListItem sli in allSongs) {
                if (sli.ArtistName.Contains(text) || sli.SongName.Contains(text)) {
                    result.Add(sli);
                }
                if (result.Count > 20)
                    return result;
            }
            return result;
        }

      

        
    }


    //public class Stack
    //{

    //    private ObservableCollection<ObservableCollection<SongListItem>> stack;


    //    public Stack(ObservableCollection<SongListItem> shownSongs)
    //    {

    //        stack = new ObservableCollection<ObservableCollection<SongListItem>>();
    //        stack.Add(shownSongs);
    //    }

    //    public void push(ObservableCollection<SongListItem> list)
    //    {
    //        stack.Add(list);
    //    }

    //    public ObservableCollection<SongListItem> pop()
    //    {
    //        if (stack.Count == 1)
    //        {
    //            return stack.ElementAt(0);
    //        }
    //        ObservableCollection<SongListItem> last = stack.ElementAt(stack.Count - 1);
    //        stack.RemoveAt(stack.Count - 1);
    //        return last;
    //    }


    //}


}
