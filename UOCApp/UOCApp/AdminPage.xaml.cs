using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace UOCApp
{
	public partial class AdminPage : ContentPage
	{
        //temporary, for testing
        //bool loggedIn = true;

		public AdminPage ()
		{
			InitializeComponent ();

            MessagingCenter.Subscribe<LoginPage, Boolean>(this, "LoginComplete", (sender, arg) => OnLoginComplete(arg));
        }

        private void OnLoginComplete(bool arg)
        {
            //TODO on return from login page do something
        }

        protected override async void OnAppearing() //is this safe?
        {
            base.OnAppearing();

            await Task.Yield();

            bool loggedIn = false;
            if (Application.Current.Properties.ContainsKey("loggedin"))
            {
                loggedIn = Convert.ToBoolean(Application.Current.Properties["loggedin"]);
            }


            //TODO: check if we're actually logged in
            if (!loggedIn)
            {
                //try to login
                await Navigation.PushModalAsync(new LoginPage());
            }

            //the below code doesn't work
            //let's try MessagingCenter

            //Console.WriteLine("Check again?");

            //we just finished logging in or failing to, so check again
            //if (Application.Current.Properties.ContainsKey("loggedin"))
            //{
            //    loggedIn = Convert.ToBoolean(Application.Current.Properties["loggedin"]);
            //}

            //are we logged in now? no?
            //if (!loggedIn)
            //{
                //leave the page
            //    Navigation.PopAsync();
            //}


        }

        private async void ButtonLogoutClick(object sender, EventArgs args)
        {
            Application.Current.Properties["loggedin"] = false;

            await Application.Current.SavePropertiesAsync();

            Navigation.PopAsync();
        }

        private void NavHome(object sender, EventArgs args)
        {
            Navigation.PopToRootAsync();
        }

        private void NavLeaderboard(object sender, EventArgs args)
        {
            Navigation.PushAsync(new LeaderboardPage());
        }

        private void NavTimes(object sender, EventArgs args)
        {
            Navigation.PushAsync(new TimesPage());
        }

    }
}
