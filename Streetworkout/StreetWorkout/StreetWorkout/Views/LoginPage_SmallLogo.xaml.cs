using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkout
{
	public partial class LoginPage_SmallLogo : ContentPage
	{
		public LoginPage_SmallLogo()
		{
            InitializeComponent();
            Task.Run(async () =>
            {
                await InitAsync();
            });
        }
        public async Task InitAsync()
        {
            Main_Logo.Source = ImageSource.FromResource("StreetWorkout.Assets.Logo_Main.png");
        }
    }
}