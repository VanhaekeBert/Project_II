using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleAuth.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StreetWorkoutV2.Model
{
    public class FitBitManager
    {
        static string _BearerToken = "";
        public static async Task<FitBitUser> FitBitAsync()
        {
            try
            {
                var scopes = new[] { "profile" };
                var api = new FitBitApi("google", "22D9J5", "8889b872288980d53e2cad3a2043955b", true)
                {
                    Scopes = scopes
                };
                var account = (SimpleAuth.OAuthAccount)await api.Authenticate();
                var response = await api.Get<FitBitUser>("https://api.fitbit.com/1/user/-/profile.json", new Dictionary<string, string> { ["Authorization"] = $"Bearer {account.Token}" });
                Preferences.Set("Token", $"Bearer {account.Token}");
                JObject token = new JObject();
                token["Token"] = $"Bearer {account.Token}";
                _BearerToken = $"Bearer {account.Token}";
                Preferences.Set("Token", $"Bearer {account.Token}");
                DBManager.PutUserData(Preferences.Get("Naam", ""), "Naam", token);
                response.API = "FitBit";
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Foutmelding: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<JObject> FitBitGetHeartRate(DateTime startTime, DateTime endTime)
        {
            try
            {
                var postStartTime = startTime.ToString("HH:mm:ss");
                var postStartDate = startTime.ToString("yyyy-MM-dd");
                var postEndTime = endTime.ToString("HH:mm:ss");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                string authToken = "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyMkQ5SjUiLCJzdWIiOiI3N1NRUUoiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJ3aHIgd3BybyB3bnV0IHdzbGUgd3dlaSB3c29jIHdhY3Qgd3NldCB3bG9jIiwiZXhwIjoxNTQ4NDIzNzY1LCJpYXQiOjE1NDgwNzk0Mjh9.m39NX6x91xwD6jT2oUVuZBs-kdGbQi6ll_i-veW0P9k";
                client.DefaultRequestHeaders.Add("Authorization", authToken);
                string url = $"https://api.fitbit.com/1/user/-/activities/heart/date/today/1d/1sec/time/{postStartTime}/{postEndTime}.json";

                string json = await client.GetStringAsync(url);
                if (json != null)
                {
                    
                    JObject hearRateObject = JsonConvert.DeserializeObject<JObject>(json);
                    return hearRateObject;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Foutmelding: " + ex.Message);
                throw ex;
            }
        }
        public static async Task<JObject> FitBitPostExercise(int activityId, DateTime startTime, int duration)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                string authToken = "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyMkQ5SjUiLCJzdWIiOiI3N1NRUUoiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJ3aHIgd3BybyB3bnV0IHdzbGUgd3dlaSB3c29jIHdhY3Qgd3NldCB3bG9jIiwiZXhwIjoxNTQ4NDIzNzY1LCJpYXQiOjE1NDgwODQ4MjJ9.39Wk-eblMXId2YodXEVIV6G_WHXAsuhabUfWfftGc4w";
                client.DefaultRequestHeaders.Add("Authorization", authToken);
                var postTime = startTime.ToString("HH:mm:ss");
                var postDate = startTime.ToString("yyyy-MM-dd");
                var durationInSeconds = duration * 1000;
                var jsonString = "";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                string url = $"https://api.fitbit.com/1/user/-/activities.json?activityId={activityId}&durationMillis={durationInSeconds}&date={postDate}&startTime={postTime}";
                var message = await client.PostAsync(url, httpContent);
                var json = await message.Content.ReadAsStringAsync();
                if (json != null)
                {
                    JObject returnObject = JsonConvert.DeserializeObject<JObject>(json);
                    return returnObject;
                }
                else
                {
                    return null;
                }
           

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }
    }
}
