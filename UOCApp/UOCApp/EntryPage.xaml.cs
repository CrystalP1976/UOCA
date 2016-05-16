using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using UOCApp.Helpers;

using Xamarin.Forms;
using UOCApp.Models;

namespace UOCApp
{
	public partial class EntryPage : ContentPage
	{
		public EntryPage ()
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

		private async void SaveResult(object sender, EventArgs args) //for debug
		{
			Console.WriteLine("Clicked Save Result");

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
				// error behavior? no gender set
					Gender = null;
					break;
			}

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

			try 
			{
				SharedResult result = new SharedResult (picker_Date.Date, entry_Time.Text, false, false, entry_Name.Text, Gender, Grade, entry_School.Text);
				Result localresult = new Result { date = picker_Date.Date.ToString(), ranked = Convert.ToInt32(switch_Official.IsToggled), time = Convert.ToDecimal(entry_Time.Text), student_gender = Gender, student_name = entry_Name.Text, student_grade = Grade };
				//Result localresult = new Result (picker_Date.Date, entry_Time.Text, switch_Official.IsToggled, entry_Name.Text, Gender, Grade);

				var sure = await DisplayAlert ("Confirm Save", "Winners Don't Cheat, \n Champions Don't Lie! \n Please record accurate race times!", "Save", "Back");
				if (sure == true) {
					// save to client database 
					App.databaseHelper.insertTime(localresult);

					if(true) // await joti(result)
					{
						if (switch_Public.IsToggled) { // did the user specify that they wish to post to the leaderboard?
							if (await result.share ()) {
								await DisplayAlert ("Thank you!", "Your result has been saved and shared with the leaderboard", "OK");
								Navigation.PopToRootAsync (); // return to home once save complete

							} else {
								Console.WriteLine ("Failed to share with leaderboard");
								//feedback to user in the event of leaderboard server failure
							}
						}
						await DisplayAlert ("Thank you!", "Your result has been saved", "OK");
						Navigation.PopToRootAsync (); // return to home once save complete


					}
					else 
					{
						// fail to save locally
					}

				} else {
					//abort save
				}

			}
			catch (ArgumentException e) 
			{
				await DisplayAlert ("Error", "Please fill in all your information", "OK");
				Console.WriteLine (e);
			}

			
		}



	}
}
    

