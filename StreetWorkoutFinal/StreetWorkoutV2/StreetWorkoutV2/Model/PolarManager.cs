using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace StreetWorkoutV2.Model
{
    //---------------------------------------------------------------------------------------//
    //--------------------PolarManager classe, alle acties op polar api----------------------//
    //---------------------------------------------------------------------------------------//

    public class PolarManager
    {
        const string ClientId = "ea578976-f76f-48a4-b2c1-fce5d8b5446b";
        const string ClientSecret = "9763ab20-59ce-4bf7-a32d-dd7229c77f20";

        //---polar authenticatie---//
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
                Debug.WriteLine("Error PolarAsync: " + ex.Message);
                throw ex;
            }
        }

        //---openenen van polar redirect scherm om authorizatie toetestaan---//
        public static OAuth2Authenticator GetPolarAuth()
        {
            try
            {
                var auth = new OAuth2Authenticator(
                       clientId: ClientId,
                       clientSecret: ClientSecret,
                       scope: "accesslink.read_all",
                       authorizeUrl: new Uri("https://flow.polar.com/oauth2/authorization"),
                       redirectUrl: new Uri("com.nmct.SICWorkout:/oauth2redirect"),
                       accessTokenUrl: new Uri("https://polarremote.com/v2/oauth2/token"),
                       isUsingNativeUI: true);
                return auth;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error GetPolarAuth: " + ex.Message);
                throw ex;
            }
        }

        //---ophalen van de polar token---//
        public static async Task<PolarUser> GetPolarToken()
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
                await PostUserAuthorize(acces);
                PolarUser user = await GetUserData(acces);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error GetPolarToken: " + ex.Message);
                throw ex;
            }
        }

        //---opslaan van polar user in authorized users voor ons---//
        public static async Task PostUserAuthorize(PolarAcces acces)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + acces.Acces_token);
                var request = "{\"member-id\": \"" + acces.X_user_id + "\"}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://www.polaraccesslink.com/v3/users";
                var message = await client.PostAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---ophalen van gebruikers gegevens van polar gebruiker---//
        public static async Task<PolarUser> GetUserData(PolarAcces acces)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + acces.Acces_token);
                string url = $"https://www.polaraccesslink.com/v3/users/{acces.X_user_id}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                PolarUser user = JsonConvert.DeserializeObject<PolarUser>(responseString);
                user.API = "Polar";
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error GetUserData: " + ex.Message);
                throw ex;
            }
        }
    }
}
