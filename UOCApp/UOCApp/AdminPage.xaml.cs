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
            //unsubscribe
            MessagingCenter.Unsubscribe<LoginPage, Boolean>(this, "LoginComplete");

            //on return from login page do something
            Console.WriteLine(arg);

            //if it returned true, the login was successful and we can do nothing

            //if it returned false, the login was unsuccessful and we need to leave immediately
            if(!arg && Navigation.NavigationStack.Count > 0) //for safety
            {
                Navigation.PopAsync();
            }

            if(arg)
            {
                //show the page if we're logged in
                //AdminLayout.IsVisible = true;
                //IsVisible is broken, use Opacity instead
                AdminLayout.Opacity = 1.0;
            }
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
            else
            {
                //show the page if we're logged in
                //AdminLayout.IsVisible = true;
                AdminLayout.Opacity = 1.0;
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

            await DisplayAlert("Alert", "You have been logged out successfully", "OK");

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
