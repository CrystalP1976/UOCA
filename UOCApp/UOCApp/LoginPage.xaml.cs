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
            //TODO write login functionality

            //TODO display message

            //if login is good, log in
            Application.Current.Properties["loggedin"] = true;

            await Application.Current.SavePropertiesAsync();            

            await Navigation.PopModalAsync();
            MessagingCenter.Send<LoginPage, Boolean>(this, "LoginComplete", true);
        }

        private async void ButtonCancelClick(object sender, EventArgs args)
        {
            //exit this screen
            

            await Navigation.PopModalAsync();
            MessagingCenter.Send<LoginPage, Boolean>(this, "LoginComplete", false);
        }
	}
}
