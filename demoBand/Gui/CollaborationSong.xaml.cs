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
        public CollaborationSong()
        {
            this.InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
                       
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton1.IsChecked == true)
            {
                txtStatus.Text = "First button is check";
            }

            else if (radioButton2.IsChecked == true)
            {
                txtStatus.Text = "Seconds button is check";
            }
            else if (radioButton3.IsChecked == true)
            {
                txtStatus.Text = "Third button is check";
            }
            else if (radioButton4.IsChecked == true)
            {
                txtStatus.Text = "Fourth button is check";
            }
            else
            {
                txtStatus.Text = "Select an instrument";
            }
        }
    }
}
