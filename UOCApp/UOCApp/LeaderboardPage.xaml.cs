using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UOCApp.Helpers;
using UOCApp.Models;
using Xamarin.Forms;

namespace UOCApp
{
	public partial class LeaderboardPage : ContentPage
	{
        HttpClient client;
        GetResultsHelper resultsHelper;

        List<LeaderboardResult> baseResults = new List<LeaderboardResult>();
        ObservableCollection<LeaderboardResult> results = new ObservableCollection<LeaderboardResult>();

        public LeaderboardPage ()
		{
			InitializeComponent ();

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            resultsHelper = new GetResultsHelper(client, App.API_URL);

            //TEST DATA
            //results.Add(new LeaderboardResult { result_id = 1, ranked = false, student_name = "Jenny Craig", school_name = "Charles Drive", time = "11:11.111" });
            //results.Add(new LeaderboardResult { result_id = 41, ranked = true, student_name = "John Smith", school_name = "Cliff Drive", time = "12:11.111" });

            ListViewLeaderboard.ItemsSource = results;
        }

        //TODO on filter change refresh resultss
        private void FilterChange(object sender, EventArgs args)
        {

        }

        private void NavHome(object sender, EventArgs args)
        {
            Console.WriteLine("Clicked Nav Home");
            Navigation.PopToRootAsync();
        }

        private void NavTimes(object sender, EventArgs args)
        {
            Console.WriteLine("Clicked Nav Times");
            Navigation.PushAsync(new TimesPage());
        }

        private void NavAdmin(object sender, EventArgs args)
        {
            Console.WriteLine("Clicked Nav Admin");
            Navigation.PushAsync(new AdminPage());
        }

			
    }
}
