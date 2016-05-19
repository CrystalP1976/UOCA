using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace UOCApp
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private async void ButtonLoginClick(object sender, EventArgs args)
        {
            string password = App.password;
            string entered = TextBoxPassword.Text;

            Console.WriteLine(entered);

            if(!String.IsNullOrEmpty(entered) && String.Equals(password, entered))
            {
                //password matches, so log in!
                await DisplayAlert("Alert", "You have been logged in successfully", "OK");
            }
            else
            {
                //display message and exit method
                await DisplayAlert("Alert", "Your password is incorrect", "OK");

                return;
            }

            //if login is good, log in
            Application.Current.Properties["loggedin"] = true;

            await Application.Current.SavePropertiesAsync();            

            await Navigation.PopModalAsync();
        }

        private async void ButtonCancelClick(object sender, EventArgs args)
        {
            //exit this screen
            

            await Navigation.PopModalAsync();
        }
	}
}
