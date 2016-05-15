using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UOCApp
{
	public partial class ObstaclesPage : ContentPage
	{
		public ObstaclesPage ()
		{
			InitializeComponent ();
		}

		async void OnSaveButtonClicked (object sender, EventArgs args)
		{
			await Navigation.PopModalAsync ();
		}
	}
}

