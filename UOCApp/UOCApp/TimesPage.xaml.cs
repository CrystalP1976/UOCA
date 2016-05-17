using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOCApp.Helpers;
using UOCApp.Models;
using Xamarin.Forms;

namespace UOCApp
{
	public partial class TimesPage : ContentPage
	{
        List<PrivateResult> baseResults;
        ObservableCollection<PrivateResult> results; 

        public TimesPage ()
		{
			InitializeComponent ();

            baseResults = new List<PrivateResult>();
            results = new ObservableCollection<PrivateResult>();

            ListViewTimes.ItemsSource = results;

            //results.Add(new PrivateResult { result_id = 5, student_name = "John Doe", date = "April 28 2016", time = "11:11.111" });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //TODO initial get results
            baseResults = App.databaseHelper.GetPrivateResults();
            CopyResults();

        }

        private void CopyResults()
        {
            this.results.Clear();

            foreach (PrivateResult result in this.baseResults)
            {
                this.results.Add(result);
            }
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

        private void ButtonDeleteClick(object sender, EventArgs args)
        {
            //TODO

        }

        private void ButtonShareClick(object sender, EventArgs args)
        {
            //TODO

        }

        private void ButtonQueryClick(object sender, EventArgs args)
        {
            //TODO
            int result_id = (int)((Button)sender).CommandParameter;

            Console.WriteLine("Clicked id: " + result_id);
            //this checks baseResults for a matching result_id, gets the first match, gets the missedObstacles list
            List<string> obstacles = baseResults.Where(v => v.result_id == result_id).First().missedObstacles;
            foreach(string obstacle in obstacles)
            {
                Console.WriteLine(obstacle);
            } 

        }

        //Fired when any filter is changed, refilters the list
        private void FilterChange(object sender, EventArgs args)
        {
            //TODO
        }

        //Fired when the sort is changed, resorts the list
        private void SortChange(object sender, EventArgs args)
        {
            //TODO
        }
    }
}
