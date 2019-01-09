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

            Task.Run(async () =>
            {
                await PolarAsync();
            });
        }
        public async Task FitBitAsync()
        {

            var scopes = new[]
            {
                    "activity nutrition heartrate location"
             };
            var api = new FitBitApi("google", "22D9J5", "8889b872288980d53e2cad3a2043955b", true)

            {
                Scopes = scopes
            };


            var account = (SimpleAuth.OAuthAccount)await api.Authenticate();           
            Debug.WriteLine(account.Token);

            var song = await api.Get<HeartRateSeries>("https://api.fitbit.com/1/user/-/activities/heart/date/today/7d.json", new Dictionary<string, string> { ["Authorization"] = "Bearer "+ account.Token });
            Debug.WriteLine(song.FullJson);
                       
        }

                          
        public async Task PolarAsync()
        {

            //var auth = new OAuth2Authenticator(
            //   clientId: "b8f68549-94d1-49ed-a502-47c773bf3cca",
            //   clientSecret: "c9d759e9-8acf-4145-bda9-cdb9fcff6ee4",
            //   scope: "accesslink.read_all",
            //   authorizeUrl: new Uri("https://flow.polar.com/oauth2/authorization"),
            //   redirectUrl: new Uri("com.companyname.test:/oauth2redirect"),
            //   accessTokenUrl: new Uri("https://polarremote.com/v2/oauth2/token"),
            //   isUsingNativeUI: true);
            var auth = PolarAuthenticator.GetPolarAuth();
            auth.AllowCancel = true;
           
            // If authorization succeeds or is canceled, .Completed will be fired.


            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(auth);
            presenter.Completed += (s, ee) =>
            {
                Debug.WriteLine("COMPLETED");
                if (ee.IsAuthenticated)
                {
                    Debug.WriteLine("Authenticated");

                }


            };
        }
    }
}
