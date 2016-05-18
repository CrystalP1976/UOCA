using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOCApp.Models;
using Xamarin.Forms;

namespace UOCApp
{
	public partial class TimesPage : ContentPage
	{
		List<Result> MyResults = new List<Result>();

		public TimesPage ()
		{
			InitializeComponent ();
		}

        private void NavHome(object sender, EventArgs args)
        {
            Console.WriteLine("Clicked Nav Home");
            Navigation.PopToRootAsync();
        }

        private void NavLeaderboard(object sender, EventArgs args)
        {
            Console.WriteLine("Clicked Nav Leaderboard");
            Navigation.PushAsync(new LeaderboardPage());
        }

        private void NavAdmin(object sender, EventArgs args)
        {
            Console.WriteLine("Clicked Nav Admin");
            Navigation.PushAsync(new AdminPage());
        }

    }
}
