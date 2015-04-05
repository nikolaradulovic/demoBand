using demoBand.Model;
using demoBand.SongDescription;
using Parse;
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
    public sealed partial class NewSong : Page
    {
        
        public NewSong()
        {
            this.InitializeComponent();
            Session.GetInstance().insertValue("songView", new SongView());

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            string text = txtStrophe.Text;
            int start = Convert.ToInt32(txtStart.Text);
            int end = Convert.ToInt32(txtEnd.Text);
            demoBand.SongDescription.Strophe s = new demoBand.SongDescription.Strophe(text, start, end);
            SongView songView = Session.GetInstance().getValueAt("songView") as SongView;
            songView.addStrophe(s);
            txtStrophe.Text = "";
            txtStart.Text = "";
            txtEnd.Text = "";
            
        }

        private async void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            SongView sw = Session.GetInstance().getValueAt("songView") as SongView;
            //ParseObject strophes = new ParseObject("strophes");
            //strophes["value"] = sw.Strophes;
            
            


            songView = new ParseObject("songView");
            songView["list"] = sw.Strophes;


            //load song
            //string id = songView.ObjectId;
            //ParseQuery<ParseObject> query = ParseObject.GetQuery("songView");
            //ParseObject objekat = await query.GetAsync(id);
            //txtStrophe.Text = objekat.ToString();


            try
            {
                await songView.SaveAsync();
            }
            catch (Exception ex)
            {

                txtStrophe.Text = ex.Message;
            }
           
        }
        private ParseObject songView;

        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //string id = songView.ObjectId;
            //ParseQuery<ParseObject> query = ParseObject.GetQuery("songView");
            //demoBand.SongDescription.Strophe objekat = await query.GetAsync(id) as demoBand.SongDescription.Strophe;
            //txtStrophe.Text = objekat.ToString();
        }
    }
}
