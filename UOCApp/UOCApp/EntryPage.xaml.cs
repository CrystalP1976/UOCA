using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using UOCApp.Helpers;
using UOCApp;
using UOCApp.Models;

using Xamarin.Forms;

namespace UOCApp
{


	public partial class EntryPage : ContentPage
	{

		public ObstaclesPage obstaclesPage;




		public EntryPage(string displayTime)
		{
			Console.WriteLine("Display Time:" + displayTime);

			obstaclesPage = new ObstaclesPage();
			InitializeComponent();
			entry_Time.Text = displayTime;


		}

		private void NavHome(object sender, EventArgs args)
		{
			// Console.WriteLine("Clicked Nav Home");
			Navigation.PopToRootAsync();
		}

		private void NavLeaderboard(object sender, EventArgs args)
		{
			// Console.WriteLine("Clicked Nav Leaderboard");
			Navigation.PushAsync(new LeaderboardPage());
		}

		private void NavTimes(object sender, EventArgs args)
		{
			// Console.WriteLine("Clicked Nav Times");
			Navigation.PushAsync(new TimesPage());
		}

		private void NavAdmin(object sender, EventArgs args)
		{
			//Console.WriteLine("Clicked Nav Admin");
			Navigation.PushAsync(new AdminPage());
		}


		async void NavObstacle(object sender, EventArgs args)
		{
			//Console.WriteLine("Clicked Obstacles");
			await Navigation.PushModalAsync(obstaclesPage);
		}

		private async void SaveResult(object sender, EventArgs args) //for debug
		{
			//Console.WriteLine("Clicked Save Result");

			try
			{
				SharedResult sharedResult = new SharedResult(picker_Date.Date, ConvertTime(entry_Time.Text), false, false, 
					entry_Name.Text, Gender(), Grade(), entry_School.Text);


				Result result = new Result();
				result.result_id = null;


				result.date =  String.Format("{0:yyyy-MM-dd}", picker_Date.Date);
				result.time = ConvertTime(entry_Time.Text);
				result.shared = Convert.ToInt32(switch_Public.IsToggled);
				result.student_name = entry_Name.Text;
				result.student_gender = Gender();
				result.student_grade =  Grade();

				var sure = await DisplayAlert("Confirm Save", "Please record accurate race times! \n \"Winners Don't Cheat, \n Champions Don't Lie!\"\n", "Save", "Back");


				if (sure == true)
				{


					//selected obstacles can be obtained from obstaclesPage.obstacleList
					// save to client database, unless in admin mode - TODO


					var LocalID = App.databaseHelper.InsertResult(result, obstaclesPage.obstacleList);


					if (LocalID >= 1)
					{ // true to fake success of adding to local database
						if (switch_Public.IsToggled)
						{ // did the user specify that they wish to post to the leaderboard?
							if (await sharedResult.share((bool)switch_Official.IsToggled))
							{ // post to server and return true if successful
								await DisplayAlert("Thank you!", "Your result has been saved and shared with the leaderboard", "OK");
								Navigation.PopToRootAsync(); // return to home once save complete

							}
							else
							{
								//Console.WriteLine ("Failed to share with leaderboard");
								await DisplayAlert("Sorry", "Unable to connect to leaderboard. \n Check your internet connection.", "OK");
							}
						}
						await DisplayAlert("Thank you!", "Your result has been saved", "OK");
						Navigation.PopToRootAsync(); // return to home once save complete
					}
					else
					{
						// fail to save locally
						await DisplayAlert("Sorry", "Unable to save. \n Please try again", "OK");
						//Console.WriteLine ("Failed to save result");
					}
				}
				else
				{
					//abort save, do nothing
				}
			}
			catch (ArgumentException e)
			{ // fail to create a result instance, bad parameters
				await DisplayAlert("Error", e.Message, "OK");
				//Console.WriteLine (e);
			}	
		}

		private String Gender()
		{
			// read the gender picker and assign values
			var Gender = "";
			var GenderIndex = picker_Gender.SelectedIndex;
			switch (GenderIndex)
			{
			case 0:
				Gender = "M";
				break;
			case 1:
				Gender = "F";
				break;
			default:
				// error behavior? no gender set, shouldn't be possible
				Gender = null;
				break;
			}
			return Gender;
		}

		private int Grade()
		{
			// read the grade picker and assign values
			var Grade = 0;
			var GradeIndex = picker_Grade.SelectedIndex;
			switch (GradeIndex)
			{
			case 0:
				Grade = 4;
				break;
			case 1:
				Grade = 5;
				break;
			case 2:
				Grade = 6;
				break;
			case 3:
				Grade = 7;
				break;
			case 4:  //				GRADE_TEENAGER = -1
				Grade = -1;
				break;
			case 5:  //				GRADE_ADULT = -2
				Grade = -2;
				break;
			case 6:  //				GRADE_OLDADULT = -3
				Grade = -3;
				break;
			default:
				// error behavior? no grade set
				break;
			}
			return Grade;
		}

		static decimal ConvertTime(string time)
		{
			decimal result;

			if (String.IsNullOrEmpty(time))
			{
				return 0;
			}

			if (time.Contains(':'))
			{
				//it's in mm:ss.iii format

				int sepPos = time.IndexOf(':');

				string minPart = time.Substring(0, sepPos);
				string secPart = time.Substring(sepPos + 1);

				result = (Convert.ToInt32(minPart) * 60) + Convert.ToDecimal(secPart);

			}
			else
			{
				//it's in ss.iii format so we can just convert it
				result = Convert.ToDecimal(time);
			}

			return Decimal.Round(result, 3);
		}


		public void ShareButtonStatus()
		{

			if (obstaclesPage.obstacleList.allComplete())
			{
				switch_Public.IsEnabled = true;
				label_obstacle.Opacity = 0;
			}
			else
			{
				switch_Public.IsToggled = false;
				switch_Public.IsEnabled = false;
				label_obstacle.Opacity = 1;
			}

		}

		public void OfficialButtonStatus()
		{
			bool loggedIn = false;
			if (Application.Current.Properties.ContainsKey("loggedin"))
			{
				loggedIn = Convert.ToBoolean(Application.Current.Properties["loggedin"]);
			}
			if (loggedIn)
			{
				switch_Official.IsEnabled = true;
				switch_Official.Opacity = 1;
				label_Official.Opacity = 1;
			}
			else
			{
				switch_Official.IsToggled = false;
				switch_Official.IsEnabled = false;
				switch_Official.Opacity = 0;
				label_Official.Opacity = 0;
			}

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			OfficialButtonStatus(); // update the offical button status dependant on if the user is logged in or not
			ShareButtonStatus(); // update the share buttons status dependant on if all obstacles are complete
		}











	}
}