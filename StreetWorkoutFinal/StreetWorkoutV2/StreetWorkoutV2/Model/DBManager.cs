using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StreetWorkoutV2.Model
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
            return (responseString.ToString());
        }

        public static async Task<bool> RegistrerenAsync(string email, string naam, string wachtwoord)
        {
            JObject reg = new JObject();
            reg["Email"] = email;
            reg["Naam"] = naam;
            reg["ApiNaam"] = naam;
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

        internal static Task<bool> LoginAsync(object p, string v)
        {
            throw new NotImplementedException();
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

        public static async Task DeleteUserData(string naam)
        {
            JObject data = new JObject();
            data["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/DeleteUserData";
            var message = await client.PostAsync(url, httpContent);
        }

        public static async Task<bool> PostOefening(JObject data)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/PostOefening";
            var message = await client.PostAsync(url, httpContent);
            return message.IsSuccessStatusCode;
        }

        public static async Task<JArray> GetOefeningenData(string naam)
        {
            JObject userData = new JObject();
            userData["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(userData);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/GetOefening";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            JArray gegevens = JsonConvert.DeserializeObject<JArray>(responseString.ToString());
            return gegevens;
        }

        public static async Task DeleteOefeningenData(string naam)
        {
            JObject data = new JObject();
            data["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/DeleteOefeningenData";
            var message = await client.PostAsync(url, httpContent);
        }

        public static async Task<bool> PostWater(string naam, int waterdoel, int watergedronken)
        {
            TimeZone localZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = localZone.GetUtcOffset(DateTime.Now);
            JObject reg = new JObject();
            reg["WaterDoel"] = waterdoel;
            reg["Naam"] = naam;
            reg["WaterGedronken"] = watergedronken;
            reg["Datum"] = DateTime.Now.ToString("MM-dd-yyyy");
            if (currentOffset.ToString().Substring(0, 1) == "-")
            {
                reg["DatumTijd"] = DateTime.Now.AddHours(-int.Parse(currentOffset.ToString().Substring(1, 2)));
            }
            else
            {
                reg["DatumTijd"] = DateTime.Now.AddHours(int.Parse(currentOffset.ToString().Substring(0, 2)));
            }
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(reg);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/PostWater";
            var message = await client.PostAsync(url, httpContent);
            return message.IsSuccessStatusCode;
        }

        public static async Task<JArray> GetWater(string naam)
        {
            JObject userData = new JObject();
            userData["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(userData);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/GetWater";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            JArray gegevens = JsonConvert.DeserializeObject<JArray>(responseString.ToString());
            return gegevens;
        }

        public static async Task<JObject> GetLatestWater(string naam)
        {
            JObject userData = new JObject();
            userData["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(userData);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/GetLatestWater";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            JObject gegevens = JsonConvert.DeserializeObject<JObject>(responseString.ToString());
            return gegevens;
        }

        public static async Task DeleteWater(string naam)
        {
            JObject data = new JObject();
            data["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/DeleteWater?code=cvCM8haZ00J5YYzWHglXIyJh9eANDVel5PSZsymy83Hv18rPnstrQA==";
            var message = await client.PostAsync(url, httpContent);
        }

        public static async Task<string> PutWater(JObject data)
        {
            data["Datum"] = DateTime.Now.ToString("MM-dd-yyyy");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/PutWater";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            return responseString.ToString();
        }

        public static async Task<bool> CheckWater(string naam, DateTime Datum)
        {
            JObject checkUserData = new JObject();
            checkUserData["Naam"] = naam;
            checkUserData["Datum"] = Datum.ToString("MM-dd-yyyy");
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

        public static async Task<bool> PostProfilePicture(string naam, Stream content)
        {
            byte[] bytes = new byte[16 * 1024];
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = content.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                bytes =  ms.ToArray();
            }
            ProfilePicture foto = new ProfilePicture() {Naam = naam, stream  = bytes};
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(foto);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/PostProfilePicture";
            var message = await client.PostAsync(url, httpContent);
            return message.IsSuccessStatusCode;
        }

        public static async Task<Uri> GetProfilePicture(string naam)
        {
            JObject gegevens = new JObject();
            gegevens["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var request = JsonConvert.SerializeObject(gegevens);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/GetProfilePicture";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            JObject uri = JsonConvert.DeserializeObject<JObject>(responseString.ToString());
            Debug.WriteLine(new Uri(uri["Uri"].ToString() + uri["SAS"].ToString()));
            return new Uri(uri["Uri"].ToString() + uri["SAS"].ToString());
        }

        public static async Task<bool> DeleteProfilePicture(string naam)
        {
            JObject gegevens = new JObject();
            gegevens["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(gegevens);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/DeleteProfilePicture";
            var message = await client.PostAsync(url, httpContent);
            return message.IsSuccessStatusCode;
        }
    }
}