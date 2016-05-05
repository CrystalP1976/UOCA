using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace UOCApp
{
	public partial class StopwatchPage : ContentPage
	{
        //for the stopwatch/timer
        bool timerActive;
        long startTime;
        long stopTime;

		public StopwatchPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            WatchText.Text = "00:00.000";
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //stop the timer on leaving (good idea?)
            timerActive = false;
            startTime = 0;
            stopTime = 0;
        }

        private void ButtonTimesClick(object sender, EventArgs args)
        {
            //TODO on click
            Console.WriteLine("Clicked top times button");
        }

        private void ButtonStartClick(object sender, EventArgs args)
        {
            startTimer();
            Console.WriteLine("Clicked start button");
        }

        private void ButtonStopClick(object sender, EventArgs args)
        {
            stopTimer();
            Console.WriteLine("Clicked stop button");
        }

        private void ButtonClearClick(object sender, EventArgs args)
        {
            clearTimer();
            Console.WriteLine("Clicked clear button");
        }

        private void ButtonSaveClick(object sender, EventArgs args)
        {
            //TODO on click
            Console.WriteLine("Clicked save button");
            //TODO use PushModal and await
            Navigation.PushAsync(new EntryPage()); //should this be modal?
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

        private void startTimer()
        {
            //get current time and note
            startTime = System.DateTime.Now.Ticks;
            timerActive = true;

            //actually start the update ticker
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 25), () => {

                if (timerActive)
                {
                    updateTimer(System.DateTime.Now.Ticks - startTime);
                    return true; //continue
                }
                else return false;                
            });
        }

        private void stopTimer()
        {
            if (timerActive)
            {
                //note final time and stop
                stopTime = System.DateTime.Now.Ticks;
                timerActive = false;
                updateTimer(stopTime - startTime);
            }
        }

        private void clearTimer()
        {
            if(!timerActive)
            {
                startTime = 0;
                stopTime = 0;
                //WatchText.Text = "00:00.000";
                updateTimer(0);
            }
        }

        //TODO: update stopwatch text
        private void updateTimer(long time)
        {
            //display time
            DateTime dt = new DateTime(time);
            string displayTime = String.Format("{0:00}:{1:00}.{2:000}", dt.Minute, dt.Second, dt.Millisecond);
            //string displayTime = dt.Minute + ":" + dt.Second + "." + dt.Millisecond;
            WatchText.Text = displayTime;
        }


    }
}
