using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace StreetWorkoutV2_Bert.Model
{
    public class PolarManager
    {
        const string ClientId = "3bef4750-06d5-471f-884c-961db3df1607";
        const string ClientSecret = "db15f74c-cf12-4c7c-97cd-5ce1cb79adc7";

        public static void PolarAsync()
        {
            try
            {
                var auth = GetPolarAuth();
                auth.AllowCancel = true;
                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(auth);
                presenter.Completed += (s, ee) =>
                {
                    Task.Run(async () =>
                    {
                        await GetPolarToken();
                    });
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static OAuth2Authenticator GetPolarAuth()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetPolarToken()
        {
            try
            {
                var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64}");
                var request = $"grant_type={PolarAuth.Grant_type}&code={PolarAuth.Code}&redirect_uri={PolarAuth.Redirect_uri}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/x-www-form-urlencoded");
                string url = "https://polarremote.com/v2/oauth2/token";
                var message = await client.PostAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
                var acces = JsonConvert.DeserializeObject<PolarAcces>(responseString);
                await GetUserData(acces);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetUserData(PolarAcces acces)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + acces.Acces_token);
                string url = $"https://www.polaraccesslink.com/v3/users/{acces.X_user_id}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<PolarUser>(responseString);
                Debug.WriteLine(user.Leeftijd);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }
    }
}
