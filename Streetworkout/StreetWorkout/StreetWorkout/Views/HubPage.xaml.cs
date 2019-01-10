using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkout.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HubPage : ContentPage
	{
        public HubPage ()
		{
            InitializeComponent();
            BackgroundColor = Color.FromHex("#303C41");
            Title = "Mainpage";
        }

        private void btnToestel_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSpier_Clicked(object sender, EventArgs e)
        {

        }

        private void btnQr_Clicked(object sender, EventArgs e)
        {

        }
    }
}