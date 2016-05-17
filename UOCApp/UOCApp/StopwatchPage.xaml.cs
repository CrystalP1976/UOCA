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
        long time;
        public string endTime;
        public string displayTime;


        public StopwatchPage()
        {
            InitializeComponent();
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

        private void ButtonPauseClick(object sender, EventArgs args)
        {
            pauseTimer();
            Console.WriteLine("Clicked pause button");
        }

        private void ButtonStopClick(object sender, EventArgs args)
        {
            stopTimer();
            Console.WriteLine("Clicked stop button");
            Console.WriteLine(displayTime);
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
            Navigation.PushAsync(new EntryPage(displayTime));

           // endTime = displayTime.ToString();

        }

        private void ButtonAboutClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AboutPage());
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
            Console.WriteLine("Result in startTime initial " + startTime);
            startTime = startTime - result;
            Console.WriteLine("Result in startTime&time " + startTime);

            Console.WriteLine("Result in startTimer: " + result + " StartTime " + startTime);
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
            Console.WriteLine("2)Result in startTimer: " + result + " StartTime " + startTime);
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
                Console.WriteLine("Result in pauseTimer" + result);
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
                Console.WriteLine("Result in stopTimer" + result);
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
            Console.WriteLine(dt);
            string displayTime = String.Format("{0:00}:{1:00}.{2:000}", dt.Minute, dt.Second, dt.Millisecond);
            //string displayTime = dt.Minute + ":" + dt.Second + "." + dt.Millisecond;
            WatchText.Text = displayTime;
        }
        
    }
}
