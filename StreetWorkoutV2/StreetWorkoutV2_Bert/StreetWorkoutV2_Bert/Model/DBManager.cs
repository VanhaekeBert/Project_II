using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StreetWorkoutV2_Bert.Model
{
    public static class DBManager
    {
        public static string Encrypt(string raw)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(raw));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static async Task<string> MailService(string email, string naam)
        {
            JObject reg = new JObject();
            reg["Email"] = email;
            reg["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(reg);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/MailService";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString.ToString());
            return (responseString.ToString());
        }

        public static async Task<bool> RegistrerenAsync(string email, string naam, string wachtwoord)
        {
            JObject reg = new JObject();
            reg["Email"] = email;
            reg["Naam"] = naam;
            reg["Wachtwoord"] = wachtwoord;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(reg);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/Registreren";
            var message = await client.PostAsync(url, httpContent);
            return message.IsSuccessStatusCode;
        }

        public static async Task<bool> LoginAsync(string naam, string wachtwoord)
        {
            JObject log = new JObject();
            log["Naam"] = naam;
            log["Wachtwoord"] = wachtwoord;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(log);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/Login";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            if (responseString.ToString() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static async Task<bool> CheckUserData(string value, string referentie)
        {
            JObject checkUserData = new JObject();
            checkUserData[referentie] = value;
            checkUserData["Referentie"] = referentie;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            string request = JsonConvert.SerializeObject(checkUserData);
            StringContent httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/CheckUserData";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            if (responseString.ToString() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static async Task<JObject> GetUserData(string value, string referentie)
        {
            JObject userData = new JObject();
            userData[referentie] = value;
            userData["Referentie"] = referentie;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(userData);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/GetUserData";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            JObject gegevens = JsonConvert.DeserializeObject<JObject>(responseString.ToString());
            return gegevens;
        }

        public static async Task<string> PutUserData(string value, string referentie, JObject data)
        {
            data[referentie] = value;
            data["Referentie"] = referentie;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/PutUserData";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            return responseString.ToString();
        }
    }
}
