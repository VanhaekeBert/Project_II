using SimpleAuth;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;

namespace test
{
    public class Polar : OAuthApi
    {

        public Polar(string identifier, string clientId, string clientSecret, string redirectUrl = "http://localhost", HttpMessageHandler handler = null) : base(identifier, clientId, clientSecret, handler)
        {
            var TokenUrl = "https://polarremote.com/v2/oauth2/token";
            RedirectUrl = new Uri(redirectUrl);
        }

        public Uri RedirectUrl { get; private set; }

        public class PolarAuthenticator : WebAuthenticator
        {
            public override string BaseUrl { get; set; } = "https://flow.polar.com/oauth2/authorization?";
            public override Uri RedirectUrl { get; set; }
            public override async Task<Dictionary<string, string>> GetTokenPostData(string clientSecret)
            {
                var data = await base.GetTokenPostData(clientSecret);
                data["redirect_uri"] = RedirectUrl.OriginalString;
                return data;
            }
        }

    }
}
