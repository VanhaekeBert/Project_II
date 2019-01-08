using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;

namespace test.Droid
{
    public class androidtestje
    {
        public async void rwerw()
        {
            var auth = new OAuth2Authenticator(
               clientId: "b8f68549-94d1-49ed-a502-47c773bf3cca",
               clientSecret: "c9d759e9-8acf-4145-bda9-cdb9fcff6ee4",
               scope: "accesslink.read_all",
               authorizeUrl: new Uri("https://flow.polar.com/oauth2/authorization"),
               redirectUrl: new Uri("http://localhost"),
               accessTokenUrl: new Uri("https://polarremote.com/v2/oauth2/token"),
               isUsingNativeUI: true);

            auth.AllowCancel = true;

            // If authorization succeeds or is canceled, .Completed will be fired.


            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(auth);
            auth.Completed += (s, ee) => {


                if (ee.IsAuthenticated)
                {
                    App.SuccesfullLoginAction.Invoke();
                }
                App.SuccesfullLoginAction.Invoke();


            };
        }
    }
}