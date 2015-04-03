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

namespace demoBand.Register
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Registration : Page
    {
        public Registration()
        {
            this.InitializeComponent();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string userName = txtUser.Text;
            string password = txtPassword.Password;
            string password2 = txtPassword2.Password;

            string error = "";


            if (!email.Contains("@"))
            {
                error += "Mail is not in valid format\n";
            }
            if (password != password2)
            {
                error += "Password missmatch";
            }
            if (error.Length > 0)
            {
                lblStatus.Text = error;
                return;
            }


            var user = new ParseUser()
            {
                Username = userName,
                Password = password,
                Email = email
            };

           
            try
            {
                await user.SignUpAsync();
            }
            catch (Exception)
            {

                lblStatus.Text = "User already exist";
            }
        }

        private void lblLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
