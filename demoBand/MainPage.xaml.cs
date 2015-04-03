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
using Parse;
using demoBand.Register;
using demoBand.Gui;
using demoBand.Model;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace demoBand
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            
            this.InitializeComponent();
            //var curretntUser = ParseUser.CurrentUser;
            //if (ParseUser.CurrentUser != null)
            //{
            //    Frame.Navigate(typeof(Registration));
            //}
            //else
            //{
            //    // show the signup or login screen
            //}

           
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUser.Text;
            string password = txtPassword.Password;
            try
            {
                await ParseUser.LogInAsync(username, password);
                lblStatus.Text = "login succeeded";
                Frame.Navigate(typeof(HomePage));
              
            }
            catch (Exception ex)
            {
                lblStatus.Text = "The login failed.";
            }
        }

        private void lblAccount_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registration));            
        }
    }
}
