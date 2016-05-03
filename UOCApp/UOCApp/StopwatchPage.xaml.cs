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
		public StopwatchPage ()
		{
			InitializeComponent ();
		}

        private void ButtonTimesClick(object sender, EventArgs args)
        {
            //TODO on click
            Console.WriteLine("Clicked top times button");
        }

        private void ButtonStartClick(object sender, EventArgs args)
        {
            //TODO on click
            Console.WriteLine("Clicked start button");
        }

        private void ButtonStopClick(object sender, EventArgs args)
        {
            //TODO on click
            Console.WriteLine("Clicked stop button");
        }

        private void ButtonClearClick(object sender, EventArgs args)
        {
            //TODO on click
            Console.WriteLine("Clicked clear button");
        }

        private void ButtonSaveClick(object sender, EventArgs args)
        {
            //TODO on click
            Console.WriteLine("Clicked save button");
        }
    }
}
