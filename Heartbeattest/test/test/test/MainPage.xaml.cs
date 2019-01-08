using SimpleAuth.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PolarAsync();
        }
        public async void FitBitAsync()
        {

            var scopes = new[]
            {
                    "activity nutrition heartrate location"
                };
            var api = new FitBitApi("google", "22D9J5", "8889b872288980d53e2cad3a2043955b", true)

            {
                Scopes = scopes
            };


            var account = await api.Authenticate();
            Debug.WriteLine(account.ToString());

        }



        public async void PolarAsync()
        {
            var auth = new OAuth2Authenticator(
               clientId: "b8f68549-94d1-49ed-a502-47c773bf3cca",
               clientSecret: "c9d759e9-8acf-4145-bda9-cdb9fcff6ee4",
               scope: "accesslink.read_all",
               authorizeUrl: new Uri("https://flow.polar.com/oauth2/authorization"),
               redirectUrl: new Uri("http://localhost"),
               accessTokenUrl: new Uri("https://polarremote.com/v2/oauth2/token"),
               isUsingNativeUI : true);

            auth.AllowCancel = true;

            // If authorization succeeds or is canceled, .Completed will be fired.


            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(auth);
            presenter.Completed += (s, ee) => {
                Debug.WriteLine("COMPLETED");
                if (ee.IsAuthenticated)
                {
                    Debug.WriteLine("Authenticated");

                    App.SuccesfullLoginAction.Invoke();
                }  
                App.SuccesfullLoginAction.Invoke();
                

            };
        }
    }
}
