using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace UOCApp.Droid
{
	[Activity (Label = "Ultimate Obstacle Course", Icon = "@drawable/icon", Theme = "@style/Theme.Splash", MainLauncher = true,  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(global::Android.Resource.Style.ThemeDeviceDefaultLight); //not working, don't know  why
            base.OnCreate (bundle);            
            //base.ActionBar.Hide();

            global::Xamarin.Forms.Forms.Init (this, bundle);            
            LoadApplication (new UOCApp.App ());            
            base.ActionBar.Show();
            
        }
	}
}

