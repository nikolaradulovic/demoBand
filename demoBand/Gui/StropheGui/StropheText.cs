using demoBand.Domen;
using demoBand.Model;
using demoBand.SongDescription;
using demoBand.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace demoBand.Gui.StropheGui
{
    class StropheText : Grid
    {
        private Choice choice;
        private TextBlock text;
        private Image image;
        private SongView song;
        private Strophe currentStrophe;


        public StropheText(SongView song)
        {
            text = new TextBlock();
            formatText();
            Children.Add(text);
            this.song = song;
            currentStrophe = song.Strophes.ElementAt(0);
            paintGrid(currentStrophe.Text);
            choice = Choice.collaborator;
        }

        public StropheText(type instrument)
        {
            image = new Image();
            formatImage(instrument);
            Children.Add(image);
            //choice = Choice.solo;
        }

        


        private void formatText()
        {
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Stretch;
            text.TextAlignment = TextAlignment.Center;
            text.FontSize = 40;
            text.Margin = new Thickness(0, 20, 0, 0);
            text.TextWrapping = TextWrapping.Wrap;

        }

        private void formatImage(type instrument)
        {
            VerticalAlignment = VerticalAlignment.Center;
            BitmapImage bp = new BitmapImage();
            Uri uri = InstrumentPicture.getLargeImages(instrument);
            bp.UriSource = uri;
            image.Source = bp;
        }





        public void TryChange(int currentSecond)
        {
            if (choice == Choice.collaborator)
            {
                if (currentSecond >= currentStrophe.Start && currentSecond < currentStrophe.End)
                    return;
                foreach (Strophe strophe in song.Strophes)
                {
                    if (currentSecond >= strophe.Start && currentSecond < strophe.End)
                    {
                        currentStrophe = strophe;
                        fireChanges();
                        return;
                    }
                }
            }
        }


        private void fireChanges()
        {
            //ispisi novi text
            paintGrid(currentStrophe.Text);
        }

        private void paintGrid(string text1)
        {
            text.Text = text1;
        }



    }
}
