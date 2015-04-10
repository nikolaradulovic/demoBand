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


            populatePageWithList();


        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            if (Frame.CanGoBack)
                Frame.GoBack();
        }


        private void populatePageWithList()
        {

            collaboration_Grid.Children.Add(new InstrumentCollaborators(voiceList, type.Voice));
            collaboration_Grid.Children.Add(new InstrumentCollaborators(guitarList, type.Guitar));
            collaboration_Grid.Children.Add(new InstrumentCollaborators(drumList, type.Drums));
            collaboration_Grid.Children.Add(new InstrumentCollaborators(pianoList, type.Piano));
        }


    }
}
