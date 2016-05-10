using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UOCApp.Models;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace UOCApp
{
	public partial class AdminPage : ContentPage
	{
        //temporary, for testing
        //bool loggedIn = true;

        //should this be at the app level?
        HttpClient client;

        List<AdminResult> baseResults = new List<AdminResult>();
        ObservableCollection<AdminResult> results = new ObservableCollection<AdminResult>();

        public AdminPage ()
		{
			InitializeComponent ();

            MessagingCenter.Subscribe<LoginPage, Boolean>(this, "LoginComplete", (sender, arg) => OnLoginComplete(arg));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            //test data
            //results.Add(new AdminResult {result_id = 5, student_name = "John Doe", date = "April 28 2016", time="11:11.111"});

            ListViewAdmin.ItemsSource = results;
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

                //load result list
                GetResults();
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

                GetResults();
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

        //TODO: get the list of results from the server
        private async void GetResults()
        {
            
            //filters are dealt with in another method
            string url = App.API_URL + "results" + CreateQueryString();

            Console.WriteLine(url);

            var uri = new Uri(url);
            //UriBuilder builder = new UriBuilder(new Uri(url));
            
            try
            { 
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    //clear the current list
                    this.baseResults.Clear();

                    var content = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(content);
                    List<RawResult> rawresults = JsonConvert.DeserializeObject<List<RawResult>>(content);

                    //List<AdminResult> pResults = new List<AdminResult>();

                    //convert all RawResult into AdminResult and add to backing list 
                    foreach(RawResult result in rawresults)
                    {
                        //Console.WriteLine(result.ToString());
                        this.baseResults.Add(new AdminResult(result));
                    }

                    //copy results
                    CopyResults();

                }
                else
                {
                    //TODO handle a failure that does not result in an exception being thrown
                }
            }
            catch(Exception e) //pokemon exception handling
            {
                Console.WriteLine("Caught exception " + e.Message);
            }

        }

        private string CreateQueryString()
        {
            string output = "?";

            //get grade and map and append
            //TODO do this in a better spot
            string selectedGrade = !(PickerGrade == null) ? PickerGrade.Items[PickerGrade.SelectedIndex] : "Grade 4";
            int grade = 4;
            switch(selectedGrade)
            {
                case "Grade 4":
                    grade = 4;
                    break;
                case "Grade 5":
                    grade = 5;
                    break;
                case "Grade 6":
                    grade = 6;
                    break;
                case "Grade 7":
                    grade = 7;
                    break;
                case "Teenager":
                    grade = -1;
                    break;
                case "Adult Under 35":
                    grade = -2;
                    break;
                case "Adult Over 35":
                    grade = -3;
                    break;
            }
            output += "student_grade=" + grade;

            //get gender and map and append (null check and default)
            string selectedGender = !(PickerGender == null) ? PickerGender.Items[PickerGender.SelectedIndex] : "Male";
            string gender = Convert.ToString(selectedGender[0]);
            output += "&student_gender=" + gender;

            //get school and append if it's not null
            string school = !(EntrySchool == null) ? EntrySchool.Text : null;
            if(!String.IsNullOrEmpty(school))
            {
                output += "&school_name=" + school;
            }

            return output;
        }

        //copy backing list to display list
        private void CopyResults()
        {
            this.results.Clear();

            foreach (AdminResult result in this.baseResults)
            {
                this.results.Add(result);
            }
        }

        

        private async void ButtonLogoutClick(object sender, EventArgs args)
        {
            Application.Current.Properties["loggedin"] = false;

            await Application.Current.SavePropertiesAsync();

            await DisplayAlert("Alert", "You have been logged out successfully", "OK");

            Navigation.PopAsync();
        }

        private void ButtonDeleteClick(object sender, EventArgs args)
        {
            int result_id = (int)((Button)sender).CommandParameter;

            Console.WriteLine("Clicked delete result " + result_id);
        }

        private void FilterChange(object sender, EventArgs args)
        {
            //TODO: on change filters, refresh the list
            Console.WriteLine("Changed filter");
        }

        private void SortChange(object sender, EventArgs args)
        {
            //abort if the picker isn't actually loaded yet
            if (PickerSort == null)
                return;

            //when the sort method is changed, resort the backing list
            //Console.WriteLine("Changed sort");
            string selectedItem = PickerSort.Items[PickerSort.SelectedIndex];
            //Console.WriteLine(PickerSort.SelectedIndex);
            //string selectedItem = "";
            switch(selectedItem)
            {
                case "Name":
                    baseResults.Sort((o1, o2) => o1.student_name.CompareTo(o2.student_name));
                    break;
                case "Date":
                    baseResults.Sort((o1, o2) => o2.sortableDate.CompareTo(o1.sortableDate)); //we want most recent
                    break;
                default:
                    //default is to sort by time
                    baseResults.Sort((o1, o2) => o1.sortableTime.CompareTo(o2.sortableTime));
                    break;
            }

            //copy the results to the observable list
            CopyResults();
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
