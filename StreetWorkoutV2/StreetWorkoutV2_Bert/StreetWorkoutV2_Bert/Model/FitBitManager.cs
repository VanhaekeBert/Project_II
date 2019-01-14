using SimpleAuth.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace StreetWorkoutV2_Bert.Model
{
    public class FitBitManager
    {
        public static async Task<FitBitUser> FitBitAsync()
        {
            var scopes = new[] { "profile" };
            var api = new FitBitApi("google", "22D9J5", "8889b872288980d53e2cad3a2043955b", true)
            {
                Scopes = scopes
            };
            var account = (SimpleAuth.OAuthAccount)await api.Authenticate();
            var response = await api.Get<FitBitUser>("https://api.fitbit.com/1/user/-/profile.json", new Dictionary<string, string> { ["Authorization"] = $"Bearer {account.Token}" });
            response.API = "FitBit";
            return response;
        }
    }
}
