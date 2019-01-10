using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        const string ClientId = "3bef4750-06d5-471f-884c-961db3df1607";
        const string ClientSecret = "db15f74c-cf12-4c7c-97cd-5ce1cb79adc7";

        public static OAuth2Authenticator GetPolarAuth()
        {
        var auth = new OAuth2Authenticator(
               clientId: ClientId,
               clientSecret: ClientSecret,
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
                var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64}");
                var jsonString = $"grant_type={PolarCode.Grant_type}&code={PolarCode.Code}&redirect_uri={PolarCode.Redirect_uri}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/x-www-form-urlencoded");
                string url = "https://polarremote.com/v2/oauth2/token";
                var message = await client.PostAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
                var token = JsonConvert.DeserializeObject<PolarToken>(responseString);
                Debug.WriteLine(token.X_user_id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static void GetPolarData(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                //var jsonString = $"grant_type={PolarCode.Grant_type}&code={PolarCode.Code}&redirect_uri={PolarCode.Redirect_uri}";
                //var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/x-www-form-urlencoded");
                //string url = "https://polarremote.com/v2/oauth2/token";
                //var message = await client.PostAsync(url, httpContent);
                //var responseString = await message.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }
    }
}
