using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace UOCApp
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            //TODO: go to next screen
            Console.WriteLine("Tapped image");

            Navigation.PushModalAsync(new NavigationPage(new UOCApp.StopwatchPage()));
        }

        //TODO: timer to go to next screen
    }
}
