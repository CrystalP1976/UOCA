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

        bool official;

        public LeaderboardPage ()
		{
			InitializeComponent ();

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            resultsHelper = new GetResultsHelper(client, App.API_URL);

            ListViewLeaderboard.ItemsSource = results;

            official = false;

        }

        protected override async void OnAppearing() //is this safe?
        {
            //initial get


            //baseResults = resultsHelper.ConvertLeaderboardResults(await resultsHelper.GetRawResults(""));

            //CopyResults();

            GetResults();
        }

        private async void GetResults()
        {
            //build the querystring with the help of the helper
            string selectedPeriod = !(PickerPeriod == null) ? PickerPeriod.Items[PickerPeriod.SelectedIndex] : "Daily";
            string selectedGrade = !(PickerGrade == null) ? PickerGrade.Items[PickerGrade.SelectedIndex] : "Grade 4";
            string selectedGender = !(PickerGender == null) ? PickerGender.Items[PickerGender.SelectedIndex] : "Male";
            string selectedSchool = !(EntrySchool == null) ? EntrySchool.Text : null;
            string query = resultsHelper.CreateQueryString(selectedPeriod, selectedGrade, selectedGender, selectedSchool, official);

            //Console.WriteLine(query);

            try
            {
                //see how elegant using the helper makes this?

                List<RawResult> rawresults = await resultsHelper.GetRawResults(query);

                this.baseResults = resultsHelper.ConvertLeaderboardResults(rawresults);

                //copy results
                CopyResults();


            }
            catch (Exception e) //pokemon exception handling
            {
                Console.WriteLine("Caught exception " + e.Message);
                await DisplayAlert("Alert", "An unexpected error occurred while getting the list", "OK");
                //abort the Page?
            }

        }

        private void CopyResults()
        {
            this.results.Clear();

            foreach (LeaderboardResult result in this.baseResults)
            {
                this.results.Add(result);
            }
        }

        //on filter change refresh, results
        private void FilterChange(object sender, EventArgs args)
        {
            //sanity check
            if (PickerGrade == null)
                return;

            GetResults();
        }
        
        private void ButtonOfficialClick(object sender, EventArgs args)
        {
            Console.WriteLine("Clicked ButtonOfficial!");

            if(official)
            {
                ButtonOfficial.Text = "Show Official";
                LabelDescription.Text = "All results";
                official = false;
            }
            else
            {
                ButtonOfficial.Text = "Show All";
                LabelDescription.Text = "Official results";
                official = true;
            }

            GetResults();
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
