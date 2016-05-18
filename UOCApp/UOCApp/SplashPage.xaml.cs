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
        bool inPage;

        public SplashPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);

            //execute timer
            inPage = true;
            Device.StartTimer(new TimeSpan(0, 0, 5), () => {
                
                if(inPage)
                {
                    Navigation.PushModalAsync(new NavigationPage(new UOCApp.StopwatchPage()));
                    inPage = false;
                }                   

                return false; //stop the timer after one shot
            });
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            inPage = false;

            Navigation.PushModalAsync(new NavigationPage(new UOCApp.StopwatchPage()));
        }
    }
}
