using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace test.Model
{
    public class PolarAuthenticator
    {

        public static Xamarin.Auth.OAuth2Authenticator GetPolarAuth()
        {

            var auth = new OAuth2Authenticator(
               clientId: "3bef4750-06d5-471f-884c-961db3df1607",
               clientSecret: "db15f74c-cf12-4c7c-97cd-5ce1cb79adc7",
               scope: "accesslink.read_all",
               authorizeUrl: new Uri("https://flow.polar.com/oauth2/authorization"),
               redirectUrl: new Uri("com.companyname.test:/oauth2redirect"),
               accessTokenUrl: new Uri("https://polarremote.com/v2/oauth2/token"),
               isUsingNativeUI: true);

            return auth;
        }

        public static async Task GetPolarToken()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Basic M2JlZjQ3NTAtMDZkNS00NzFmLTg4NGMtOTYxZGIzZGYxNjA3OmRiMTVmNzRjLWNmMTItNGM3Yy05N2NkLTVjZTFjYjc5YWRjNw==");
                PolarToken token = new PolarToken();
                token.code = Token.Code;
                var jsonString = JsonConvert.SerializeObject(token);
                Debug.WriteLine(jsonString);
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                string url = "https://polarremote.com/v2/oauth2/token";
                var message = await client.PostAsync(url, httpContent);


                var responseString = await message.Content.ReadAsStringAsync();

                Debug.WriteLine(responseString);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }
    }
}
