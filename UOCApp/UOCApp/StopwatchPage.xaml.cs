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
        long result;
        public string displayTime;

        public StopwatchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            startTime = 0;
            result = 0;

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

        }

        private void ButtonStartClick(object sender, EventArgs args)
        {
            startTimer();
        }

        private void ButtonPauseClick(object sender, EventArgs args)
        {
            pauseTimer();
        }

        private void ButtonStopClick(object sender, EventArgs args)
        {
            stopTimer();
        }

        private void ButtonClearClick(object sender, EventArgs args)
        {
            clearTimer();
        }

        private void ButtonSaveClick(object sender, EventArgs args)
        {

            Navigation.PushAsync(new EntryPage(displayTime)); //should this be modal?
        }

        private void ButtonAboutClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AboutPage());
        }


        private void NavLeaderboard(object sender, EventArgs args)
        {
            Navigation.PushAsync(new LeaderboardPage());
        }

        private void NavTimes(object sender, EventArgs args)
        {
            Navigation.PushAsync(new TimesPage());
        }

        private void NavAdmin(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AdminPage());
        }

        private void startTimer()
        {
            //get current time and note
            startTime = System.DateTime.Now.Ticks;
            startTime = startTime - result;

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

        private void pauseTimer()
        {
            if (timerActive)
            {
                //note final time and stop
                stopTime = System.DateTime.Now.Ticks;
                timerActive = false;
                result = stopTime - startTime;
                updateTimer(result);
            }
        }

        private void stopTimer()
        {
            if (timerActive)
            {
                //note final time and stop
                stopTime = System.DateTime.Now.Ticks;
                timerActive = false;
                result = stopTime - startTime;
                updateTimer(result);
                startTime = 0;
                result = 0;
            }
        }

        private void clearTimer()
        {
            if (!timerActive)
            {
                startTime = 0;
                stopTime = 0;
                result = 0;
                //WatchText.Text = "00:00.000";
                updateTimer(0);
            }
        }

        //TODO: update stopwatch text
        private void updateTimer(long time)
        {
            //display time
            DateTime dt = new DateTime(time);
            displayTime = String.Format("{0:00}:{1:00}.{2:000}", dt.Minute, dt.Second, dt.Millisecond);
            //string displayTime = dt.Minute + ":" + dt.Second + "." + dt.Millisecond;
            WatchText.Text = displayTime;
        }
        
    }
}
