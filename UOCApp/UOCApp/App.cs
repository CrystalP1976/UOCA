using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace UOCApp
{
	public class App : Application
	{

        //safe to put constants here?
        public const string password = "12345";

		public App ()
		{
            // The root page of your application
            MainPage = new NavigationPage(new UOCApp.StopwatchPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
