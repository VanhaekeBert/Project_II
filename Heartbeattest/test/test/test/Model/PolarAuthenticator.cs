using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace test.Model
{
    public class PolarAuthenticator
    {

        public static Xamarin.Auth.OAuth2Authenticator GetPolarAuth()
        {

            var auth = new OAuth2Authenticator(
               clientId: "b8f68549-94d1-49ed-a502-47c773bf3cca",
               clientSecret: "c9d759e9-8acf-4145-bda9-cdb9fcff6ee4",
               scope: "accesslink.read_all",
               authorizeUrl: new Uri("https://flow.polar.com/oauth2/authorization"),
               redirectUrl: new Uri("com.companyname.test:/oauth2redirect"),
               accessTokenUrl: new Uri("https://polarremote.com/v2/oauth2/token"),
               isUsingNativeUI: true);

            return auth;
        }
    }
}
