using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UOCApp
{
	public partial class ObstaclesPage : ContentPage
	{

		public ObstacleList obstacleList;
		public ObstaclesPage ()
		{
			obstacleList = new ObstacleList ();
			InitializeComponent ();
		}
			


		async void OnSaveButtonClicked (object sender, EventArgs args)
		{

            obstacleList.Switch_0 = switch_0.IsToggled;
			obstacleList.Switch_1 = switch_1.IsToggled;
			obstacleList.Switch_2 = switch_2.IsToggled;
			obstacleList.Switch_3 = switch_3.IsToggled;
			obstacleList.Switch_4 = switch_4.IsToggled;
			obstacleList.Switch_5 = switch_5.IsToggled;
			obstacleList.Switch_6 = switch_6.IsToggled;
			obstacleList.Switch_7 = switch_7.IsToggled;
			obstacleList.Switch_8 = switch_8.IsToggled;
			obstacleList.Switch_9 = switch_9.IsToggled;
			obstacleList.Switch_10 = switch_10.IsToggled;
			obstacleList.Switch_11 = switch_11.IsToggled;


			await Navigation.PopModalAsync ();
		}
	}
}

