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

            RefreshResults();
        }

        private void RefreshResults()
        {
            LoadResults();
            FilterResults();
            SortResults();
            CopyResults();
        }

        private void LoadResults()
        {
            baseResults = App.databaseHelper.GetPrivateResults();       
        }

        private void SortResults()
        {

            //if the picker hasn't loaded, sort default and return
            if(PickerSort == null)
            {
                baseResults.Sort((o1, o2) => o1.sortableTime.CompareTo(o2.sortableTime));
                return;
            }

            switch (PickerSort.SelectedIndex)
            {
                case 0:
                    //sort by name
                    baseResults.Sort((o1, o2) => o1.student_name.CompareTo(o2.student_name));
                    break;
                case 1:
                    //sort by date
                    baseResults.Sort((o1, o2) => o2.sortableDate.CompareTo(o1.sortableDate));
                    break;
                default:
                    //sort by time
                    baseResults.Sort((o1, o2) => o1.sortableTime.CompareTo(o2.sortableTime));
                    break;
            }
        }

        private void FilterResults()
        {
            //abort if the Entry is not loaded or is empty
            if (EntryName == null || String.IsNullOrEmpty(EntryName.Text))
                return;

            //filter the list to only have results where the name starts with the name entered into the filter box (not case sensitive)
            baseResults = new List<PrivateResult>(baseResults.Where(x => x.student_name.ToLower().StartsWith(EntryName.Text.ToLower())));
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

        private async void ButtonDeleteClick(object sender, EventArgs args)
        {

            if (await DisplayAlert("Delete", "Are you sure?", "Ok", "Cancel"))
            {
                int rowcount = 0;
                int result_id = (int)((Button)sender).CommandParameter;
                rowcount = App.databaseHelper.DeleteResult(result_id);
                RefreshResults();
            }

        }

        private void ButtonShareClick(object sender, EventArgs args)
        {
            //TODO

        }

        private void ButtonQueryClick(object sender, EventArgs args)
        {
            //TODO
            int result_id = (int)((Button)sender).CommandParameter;

            //Console.WriteLine("Clicked id: " + result_id);
            //this checks baseResults for a matching result_id, gets the first match, gets the missedObstacles list
            List<string> obstacles = baseResults.Where(v => v.result_id == result_id).First().missedObstacles;

            string obstaclesString = "";

            foreach(string obstacle in obstacles)
            {
                obstaclesString += obstacle + "\n";
            }

            DisplayAlert("Missed obstacles", obstaclesString, "OK");

        }

        //Fired when any filter is changed, refilters the list
        private void FilterChange(object sender, EventArgs args)
        {
            RefreshResults();
        }

        //Fired when the sort is changed, resorts the list
        private void SortChange(object sender, EventArgs args)
        {
            //sanity check prevents this from running before the view is loaded
            if (PickerSort == null)
                return;

            RefreshResults();
        }
    }
}