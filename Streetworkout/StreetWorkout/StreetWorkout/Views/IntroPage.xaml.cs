using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkout
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IntroPage : ContentPage
	{
        public IntroPage()
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

        private void Button_Login_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}