using Newtonsoft.Json.Linq;
using SimpleAuth.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StreetWorkoutV2.Model
{
    public class FitBitManager
    {
        static string _BearerToken = "";
        public static async Task<FitBitUser> FitBitAsync()
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
            DBManager.PutUserData(Preferences.Get("Naam", ""), "Naam", token);
            response.API = "FitBit";
            return response;
        }

        //public static async Task<FitBitUser> PostActivityAsync()
        //{
        //    var scopes = new[] { "profile" };
        //    var api = new FitBitApi("google", "22D9J5", "8889b872288980d53e2cad3a2043955b", true)
        //    {
        //        Scopes = scopes
        //    };

        //    var response = await api.Get<FitBitUser>("https://api.fitbit.com/1/user/-/profile.json", new Dictionary<string, string> { ["Authorization"] = $"Bearer {account.Token}" });
        //    response.API = "FitBit";
        //    return response;
        //}
    }
}
