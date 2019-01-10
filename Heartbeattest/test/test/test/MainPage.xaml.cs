using SimpleAuth.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Model;
using Xamarin.Auth;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //Task.Run(async () =>
            //{
            //    await FitBitAsync();
            //});
            PolarAsync();
        }

        public async Task FitBitAsync()
        {
            var scopes = new[] { "activity nutrition heartrate location" };
            var api = new FitBitApi("google", "22D9J5", "8889b872288980d53e2cad3a2043955b", true)
            {
                Scopes = scopes
            };
            var account = (SimpleAuth.OAuthAccount)await api.Authenticate();
            var response = await api.Get<HeartRateSeries>("https://api.fitbit.com/1/user/-/activities/heart/date/today/7d.json", new Dictionary<string, string> { ["Authorization"] = $"Bearer {account.Token}"});
            Debug.WriteLine(response.FullJson);
        }
         
        public void PolarAsync()
        {
            var auth = PolarAuthenticator.GetPolarAuth();
            auth.AllowCancel = true;           
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(auth);
            presenter.Completed += (s, ee) =>
            {
                PolarAuthenticator.GetPolarToken();
            };  
        }
    }
}
