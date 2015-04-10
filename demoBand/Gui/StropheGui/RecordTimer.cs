using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using demoBand.Gui;

namespace demoBand.Gui.StropheGui
{
    class RecordTimer : Grid
    {
        private int time;
        private DispatcherTimer timer;

        private TextBlock text;

        public RecordTimer()
        {
            time = 3;

            text = new TextBlock();
            arrangeText();
            
            Children.Add(text);


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += changeRemainTime;
            timer.Start();
            

        }

        private void changeRemainTime(object sender, object e)
        {

            text.Text = time.ToString();
            if (time == 0)
            {
                stopTimer();
                startRecording();

            }
            time--;

        }

        public void stopTimer()
        {
            timer.Stop();
            time = 3;
            text.Text = "";
        }

        public static event SongPage.recordDelegate recordEvent;

        

        public void startRecording()
        {
            
            if (recordEvent != null)
            {
                recordEvent();
            }
        }

        public void arrangeText()
        {
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.FontSize = 200;
            text.TextWrapping = TextWrapping.Wrap;
        }


    }
}
