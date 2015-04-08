using demoBand.Domen;
using demoBand.Model;
using demoBand.ParseBase;
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
    public sealed partial class MySongs : Page
    {
        
        public MySongs()
        {
            this.InitializeComponent();
            
        }

    

        private async void gridList_ItemClick(object sender, ItemClickEventArgs e)
        {
            SongListItem songItem = e.ClickedItem as SongListItem;
            List<RecordParse> list = await DataBaseParse.getAllFiles(songItem.SongName, songItem.ArtistName);

            Song s = new Song();
            s.Name = songItem.SongName;
            s.Author = songItem.ArtistName;
            List<Instrument> instruments = new List<Instrument>();
            foreach (RecordParse rp in list)
            {
                Instrument i = new Instrument();
                i.AudioByteArray = rp.File;
                i.TypeOfInstrument = (type)Enum.Parse(typeof(type), rp.Instrument);
                i.Path = rp.UrlFile.ToString();
                s.Length = rp.Length.ToString();
                instruments.Add(i);
            }
            s.Instruments = instruments;
            Session.GetInstance().insertValue("song", s);
            Session.GetInstance().insertValue("choice", Choice.collaborator.ToString());
            

            //hardkodovan instrument
            Session.GetInstance().insertValue("instrument", type.Voice.ToString());
            //
            Frame.Navigate(typeof(SongPage));


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
