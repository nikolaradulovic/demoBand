using demoBand.Common;
using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using demoBand.Model;
using Parse;
using demoBand.ParseBase;
using demoBand.ViewModel;
using demoBand.Gui.Dialog;
using System.Threading.Tasks;

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=321224

namespace demoBand.Gui
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public HomePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
           

        }

        private async Task loadMyLoadMySong()
        {
            string username = Session.GetInstance().getValueAt("username").ToString();
            MySongsView.setMySongs(await DataBaseParse.getSongListItemAuthor(username));
            MySongsView.setCollaboratorSongs(await DataBaseParse.getSongListItemCollaborator(username));
            //popuniti listu...
            //DiscoverView.setDiscoverSongs();
            
            //MySongsView.setMySongs (await DataBaseParse.getSongListItemsForUser(username));
        }


        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Assign a collection of bindable groups to this.DefaultViewModel["Groups"]
           progressBar.Visibility = Visibility.Visible;
           await loadMyLoadMySong();
           progressBar.Visibility = Visibility.Collapsed;
           


        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion



        private void lstSongs_ItemClick(object sender, ItemClickEventArgs e)
        {
           

            Song song = e.ClickedItem as Song;
            Session.GetInstance().insertValue("song", song);
            //CreateMEssageDialog(song);
            createChooseInstrumentDialogForSong(song);
            Session.GetInstance().insertValue("choice", Choice.collaborator.ToString());
            //Frame.Navigate(typeof(SongPage));
        }

       

        private  void createChooseInstrumentDialogForSong(Song song)
        {

            List<Instrument> instruments = song.Instruments;
            InstrumentButton.NavigateEvent += goToSongPage;
            InstrumentStackPanel sp = new InstrumentStackPanel(instruments);
            sp.HorizontalAlignment = HorizontalAlignment.Center;
            stackDialogChoose.Children.Add(sp);
            arrangePopupMessageDialog();
            popupDialogChoose.IsOpen = true;
            Session.GetInstance().insertValue("choice", Choice.collaborator.ToString());
        }

        private void createChooseInsturmentDialogForCreate()
        {
            InstrumentButton.NavigateEvent += goToSongPage;
            InstrumentStackPanel sp = new InstrumentStackPanel();
            sp.HorizontalAlignment = HorizontalAlignment.Center;
            stackDialogChoose.Children.Add(sp);
            arrangePopupMessageDialog();
            popupDialogChoose.IsOpen = true;
            Session.GetInstance().insertValue("choice", Choice.solo.ToString());
        }



        private void arrangePopupMessageDialog()
        {
            mainGrid.Opacity = 0.1;
            popupDialogChoose.IsLightDismissEnabled = true;
            stackDialogChoose.Width = mainGrid.ActualWidth;
            stackDialogChoose.Height = mainGrid.ActualHeight /5;
        }


        private void probaInvokedHanler(IUICommand command)
        {
            Session.GetInstance().insertValue("instrument", command.Label);
            Session.GetInstance().insertValue("choice", Choice.solo.ToString());
            Frame.Navigate(typeof(SongPage));
        }


        private void CommandInvokedHanler(IUICommand command)
        {
            string label = command.Label;
            //Session.GetInstance().insertValue("instrument", command.Label);
            //Frame.Navigate(typeof(SongPage));
            type instrument = (type) Enum.Parse(typeof (type), label);
            navigateToSongPage(instrument);
        }

        private void navigateToSongPage(type instrument)
        {
            Session.GetInstance().insertValue("instrument", instrument.ToString());
            
            Frame.Navigate(typeof(SongPage));
        }



        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ParseUser.LogOut();
            Frame.Navigate(typeof(MainPage));
        }


        private void lstMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            MenuItem m = e.ClickedItem as MenuItem;
            switch (m.Name)
            {

                case "Create": createChooseInsturmentDialogForCreate();
                    //case "Create new song": Frame.Navigate(typeof(NewSong));
                    break;
                case "My songs": Frame.Navigate(typeof(MySongs));
                    break;
                case "Discover" : Frame.Navigate(typeof(DiscoverPage));
                    break;

                //case "Discover": Frame.Navigate(typeof(CollaborationSong));
                //    break;



            }

        }

        private void lstSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        

        public delegate void emptyDelegate();

        private void goToSongPage()
        {
            Frame.Navigate(typeof(SongPage));
        }

        private void popupDialogChoose_Closed(object sender, object e)
        {
            stackDialogChoose.Children.Clear();
            mainGrid.Opacity = 1;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }





    }
}
