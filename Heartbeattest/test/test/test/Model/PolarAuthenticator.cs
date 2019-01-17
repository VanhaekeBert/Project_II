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
        const string ClientId = "ea578976-f76f-48a4-b2c1-fce5d8b5446b";
        const string ClientSecret = "9763ab20-59ce-4bf7-a32d-dd7229c77f20";

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
                Debug.WriteLine("polarcode " + PolarCode.Code);
                var request = $"grant_type={PolarCode.Grant_type}&code={PolarCode.Code}&redirect_uri={PolarCode.Redirect_uri}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/x-www-form-urlencoded");
                string url = "https://polarremote.com/v2/oauth2/token";
                var message = await client.PostAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine("response token: " + responseString);
                var token = JsonConvert.DeserializeObject<PolarToken>(responseString);
                Debug.WriteLine("acces token: " + token.Acces_token);
                await PostUserAuthorize(token);
                await GetUserData(token);
                //DeleteUserAuthorize(token);
                //PostUserAuthorize(token);
                //await PostTransactionTD(token);
                //await PostTransactionDA(token);
                //await PostTransactionPI(token);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

//USER!!!
        public static async Task PostUserAuthorize(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var request = "{\"member-id\": \"" + token.X_user_id + "\"}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://www.polaraccesslink.com/v3/users";
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

        public static async Task GetUserData(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}";
                Debug.WriteLine(url);
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //public static async Task DeleteUserAuthorize(PolarToken token)
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
        //        string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}";
        //        var message = await client.DeleteAsync(url);
        //        var responseString = await message.Content.ReadAsStringAsync();
        //        Debug.WriteLine(message.StatusCode.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Error: " + ex.Message);
        //        throw ex;
        //    }
        //}

 //NOTIFICATIONS!!!       
        public static async Task GetListUserExercises(PolarToken token)
        {
            try
            {
                var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64}");
                string url = $"https://www.polaraccesslink.com/v3/notifications";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(message.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

 //TRAINIG DATA!!!
        public static async Task PostTransactionTD(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var request = "{}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/exercise-transactions";
                var message = await client.PostAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
                var tempToken = JsonConvert.DeserializeObject<TrainingData>(responseString);
                token.TransactionTD = tempToken.TransactionTD;
                Debug.WriteLine(token.TransactionTD);
                Debug.WriteLine(responseString);
                await GetListExercisesTD(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetListExercisesTD(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/exercise-transactions/{token.TransactionTD}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                var tempToken = JsonConvert.DeserializeObject<TrainingData>(responseString);
                token.Exercise = tempToken.Exercise;
                Debug.WriteLine(token.Exercise);
                Debug.WriteLine(responseString);
                //await PutTransactionTD(token);
                await GetExerciseTD(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task PutTransactionTD(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var request = "{}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/exercise-transactions/{token.TransactionTD}";
                var message = await client.PutAsync(url, httpContent) ;
                var responseString = await message.Content.ReadAsStringAsync();
                //krijg niets terug???
                Debug.WriteLine(message.StatusCode.ToString());
                //await GetExerciseTD(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetExerciseTD(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var message = await client.GetAsync(token.Exercise[0].ToString());
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
                await GetHartRateZonesTD(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetHartRateZonesTD(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var message = await client.GetAsync(token.Exercise[0].ToString()+ "/heart-rate-zones");
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
                await GetAvailableSamplesTD(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetAvailableSamplesTD(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var message = await client.GetAsync(token.Exercise[0].ToString() + "/samples");
                var responseString = await message.Content.ReadAsStringAsync();
                var tempToken = JsonConvert.DeserializeObject<TrainingData>(responseString);
                token.Samples = tempToken.Samples;
                Debug.WriteLine(token.Samples);
                Debug.WriteLine(responseString);
                await GetSampleTD(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetSampleTD(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var message = await client.GetAsync(token.Samples[0].ToString());
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

//DAILY ACTIVITY !!!
        public static async Task PostTransactionDA(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var request = "{}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/activity-transactions";
                var message = await client.PostAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
                var tempToken = JsonConvert.DeserializeObject<DailyActivity>(responseString);
                token.TransactionDA = tempToken.TransactionDA;
                Debug.WriteLine(token.TransactionDA);
                Debug.WriteLine(responseString);
                await GetListExercisesDA(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetListExercisesDA(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/activity-transactions/{token.TransactionDA}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                var tempToken = JsonConvert.DeserializeObject<DailyActivity>(responseString);
                token.Activity = tempToken.Activity;
                Debug.WriteLine(token.Activity);
                Debug.WriteLine(responseString);
                //await PutTransactionDA(token);
                await GetActivityDA(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task PutTransactionDA(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var request = "{}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/activity-transactions/{token.TransactionDA}";
                var message = await client.PutAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
                //krijg niets terug???
                Debug.WriteLine(message.StatusCode.ToString());
                await GetActivityDA(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetActivityDA(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var message = await client.GetAsync(token.Activity[0].ToString());
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
                await GetStepSampleDA(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }
        
        public static async Task GetStepSampleDA(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var message = await client.GetAsync(token.Activity[0].ToString() + "/step-samples");
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
                await GetZoneSampleDA(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetZoneSampleDA(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var message = await client.GetAsync(token.Activity[0].ToString() + "/zone-samples");
                var responseString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

//PHYSICAL INFO!!!
        public static async Task PostTransactionPI(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var request = "{}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/physical-information-transactions";
                var message = await client.PostAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
                var tempToken = JsonConvert.DeserializeObject<PhysicalInfo>(responseString);
                token.TransactionPI = tempToken.TransactionPI;
                Debug.WriteLine(token.TransactionPI);
                Debug.WriteLine(responseString);
                await GetListPhysicalInfoPI(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetListPhysicalInfoPI(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/physical-information-transactions/{token.TransactionPI}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                var tempToken = JsonConvert.DeserializeObject<PhysicalInfo>(responseString);
                token.Physicalinfo = tempToken.Physicalinfo;
                Debug.WriteLine(token.Physicalinfo);
                Debug.WriteLine(responseString);
                //await PutTransactionPI(token);
                await GetPhysicalInfoPI(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task PutTransactionPI(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var request = "{}";
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = $"https://www.polaraccesslink.com/v3/users/{token.X_user_id}/physical-information-transactions/{token.TransactionPI}";
                var message = await client.PutAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
                //krijg niets terug???
                Debug.WriteLine(message.StatusCode.ToString());
                await GetPhysicalInfoPI(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task GetPhysicalInfoPI(PolarToken token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Acces_token);
                var message = await client.GetAsync(token.Physicalinfo[0].ToString());
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
