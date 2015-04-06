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
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Assign a collection of bindable groups to this.DefaultViewModel["Groups"]
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
            CreateMEssageDialog(song);
            //Frame.Navigate(typeof(SongPage));
        }

        private async void CreateMEssageDialog(Song song)
        {
            var messageDialog = new MessageDialog("Choose your instrument");
            for (int i = 0; i < song.Instruments.Count; i++)
            {
                messageDialog.Commands.Add(new UICommand(song.Instruments.ElementAt(i).TypeOfInstrument.ToString(), new UICommandInvokedHandler(this.CommandInvokedHanler)));
            }

            await messageDialog.ShowAsync();

        }

        private async void createChooseInstrumentDialog()
        {
            //UICommandInvokedHandler a = new UICommandInvokedHandler(probaInvokedHanler);

            var messageDialog = new MessageDialog("Chooe your instrument");

            //messageDialog.Commands.Add(new UICommand("Voice"));
            //messageDialog.Commands.Add(new UICommand("Guitar"));
            //messageDialog.Commands.Add(new UICommand("Drums"));
            //messageDialog.Commands.Add(new UICommand("Piano"));
            //messageDialog.Commands.Add(new UICommand("Voice", new UICommandInvokedHandler(this.probaInvokedHanler)));
            messageDialog.Commands.Add(new UICommand("Guitar", new UICommandInvokedHandler(this.probaInvokedHanler)));
            messageDialog.Commands.Add(new UICommand("Drums", new UICommandInvokedHandler(this.probaInvokedHanler)));
            messageDialog.Commands.Add(new UICommand("Piano", new UICommandInvokedHandler(this.probaInvokedHanler)));
            //   messageDialog.Commands.Add(
            await messageDialog.ShowAsync();
        }

        private void probaInvokedHanler(IUICommand command)
        {
            Session.GetInstance().insertValue("instrument", command.Label);
            Frame.Navigate(typeof(SongPage));
        }


        private void CommandInvokedHanler(IUICommand command)
        {
            Session.GetInstance().insertValue("instrument", command.Label);
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

                case "Create new song": createChooseInstrumentDialog();
                    //case "Create new song": Frame.Navigate(typeof(NewSong));
                    break;


            }

        }

        private void lstSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



    }
}
