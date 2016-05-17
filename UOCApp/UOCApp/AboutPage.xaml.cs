using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace UOCApp
{
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();
		}

        private void ButtonContactClick(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));
        }
	}
}
