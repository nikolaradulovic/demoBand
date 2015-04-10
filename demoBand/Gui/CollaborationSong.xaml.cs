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

        //List<InstrumentCollaborators> stackPanels;

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

            songname = so.getExtra("songname") as string;
            author = so.getExtra("author") as string;
            length = (int)so.getExtra("length");

            populatePageWithList();



        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            if (Frame.CanGoBack)
                Frame.GoBack();
        }


        private void populatePageWithList()
        {

            //stackPanels = new List<InstrumentCollaborators>();


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


            //foreach (InstrumentCollaborators sp in stackPanels)
            //{
            //    if (sp.isMyChoice())
            //    {
            //        myInstrument = sp.InstrumentType;
            //    }

            //    else if (sp.getChoosenInstrument() != null)
            //    {
            //        Instrument ins = sp.getChoosenInstrument();
            //        instruments.Add(ins);
            //    }
            //}
            List<GridViewItemCollaborator> listColl = getChoosenCollaborators(); //potrebno da bih napunio listu isturmenata u Song objektu
            List<Instrument> instruments = new List<Instrument>();

            foreach (GridViewItemCollaborator coll in listColl)
            {
                int insNum = getMyChoosenInstrument(coll);
                if (insNum != 0) //izabrao je da peva/svira
                {
                    myInstrument = convertToTypeInstrument(insNum);
                }
                else //nije izabrao da pva/svira, vec nekog
                {
                    Instrument i = coll.Instrument;
                    instruments.Add(i);
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


        private List<GridViewItemCollaborator> getChoosenCollaborators()
        {
            List<GridViewItemCollaborator> list = new List<GridViewItemCollaborator>();

            GridViewItemCollaborator voice = gridViewVoice.SelectedItem as GridViewItemCollaborator;
            GridViewItemCollaborator guitar = gridViewGuitar.SelectedItem as GridViewItemCollaborator;
            GridViewItemCollaborator drums = gridViewDrums.SelectedItem as GridViewItemCollaborator;
            GridViewItemCollaborator piano = gridViewPiano.SelectedItem as GridViewItemCollaborator;

            if (voice != null)
                list.Add(voice);
            if (guitar != null)
                list.Add(guitar);
            if (drums != null)
                list.Add(drums);
            if (piano != null)
                list.Add(piano);


            return list;



        }

        private int getMyChoosenInstrument(GridViewItemCollaborator coll)
        {
            if (coll.Text == ChoiceText.Voice)
                return 1;
            if (coll.Text == ChoiceText.Guitar)
                return 2;
            if (coll.Text == ChoiceText.Drums)
                return 3;
            if (coll.Text == ChoiceText.Piano)
                return 4;
            return 0;
        }

        private type convertToTypeInstrument(int number)
        {
            return (type)number;
        }



        public class ChoiceText
        {
            public static string Voice = "I sing";
            public static string Guitar = "I play guitar";
            public static string Drums = "I play drumms";
            public static string Piano = "I play piano";
        }


    }
}
