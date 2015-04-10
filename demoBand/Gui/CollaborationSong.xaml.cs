using demoBand.Domen;
using demoBand.Gui.CollaborationInstruments;
using demoBand.Model;
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
    public sealed partial class CollaborationSong : Page
    {
        List<Collaborator> voiceList;
        List<Collaborator> guitarList;
        List<Collaborator> drumList;
        List<Collaborator> pianoList;

        List<InstrumentCollaborators> stackPanels;

        string songname;
        string author;
        int length;


        public CollaborationSong()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SenderObject so = e.Parameter as SenderObject;

            voiceList = so.getExtra("voicelist") as List<Collaborator>;
            guitarList = so.getExtra("guitarlist") as List<Collaborator>;
            drumList = so.getExtra("drumlist") as List<Collaborator>;
            pianoList = so.getExtra("pianolist") as List<Collaborator>;

            songname =  so.getExtra("songname") as string;
            author =  so.getExtra("author") as string;
            length = (int) so.getExtra("length") ;

            populatePageWithList();
           


        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            if (Frame.CanGoBack)
                Frame.GoBack();
        }


        private void populatePageWithList()
        {

            stackPanels = new List<InstrumentCollaborators>();


            InstrumentCollaborators vc = new InstrumentCollaborators(voiceList, type.Voice);
            gridViewVoice.ItemsSource = vc.GridItemColl;
            //voiceGrid.Children.Add(vc);
            //stackPanels.Add(vc);

            InstrumentCollaborators gc = new InstrumentCollaborators(guitarList, type.Guitar);
            gridViewGuitar.ItemsSource = gc.GridItemColl;
            //guitarGrid.Children.Add(gc);
            //stackPanels.Add(gc);

            InstrumentCollaborators dc = new InstrumentCollaborators(drumList, type.Drums);
            gridViewDrums.ItemsSource = dc.GridItemColl;
            //stackPanels.Add(dc);

            InstrumentCollaborators pc = new InstrumentCollaborators(pianoList, type.Piano);
            gridViewPiano.ItemsSource = pc.GridItemColl;
            //pianoGrid.Children.Add(pc);
            //stackPanels.Add(pc);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            // mora se dodati provera da li je sebe selektovao
            Song song = new Song();
            type myInstrument = 0;
            List<Instrument> instruments = new List<Instrument>();

            foreach (InstrumentCollaborators sp in stackPanels)
            {
                if (sp.isMyChoice())
                {
                    myInstrument = sp.InstrumentType;
                }

                else if (sp.getChoosenInstrument() != null)
                {
                    Instrument ins = sp.getChoosenInstrument();
                    instruments.Add(ins);
                }
            }

            song.Author = author;
            song.Name = songname;
            song.Length = length.ToString();
            song.Instruments = instruments;

            Session.GetInstance().insertValue("song", song);
            Session.GetInstance().insertValue("instrument", myInstrument.ToString());
            Session.GetInstance().insertValue("choice", Choice.collaborator.ToString());


            Frame.Navigate(typeof(SongPage));

        }


    }
}
