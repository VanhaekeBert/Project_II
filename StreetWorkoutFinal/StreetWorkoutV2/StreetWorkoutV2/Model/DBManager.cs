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
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<string> MailService(string email, string naam)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<bool> RegistrerenAsync(string email, string naam, string wachtwoord)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<bool> LoginAsync(string naam, string wachtwoord)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/Login/{naam}/{wachtwoord}";
                var message = await client.GetAsync(url);
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }


        public static async Task<bool> CheckUserData(string value, string referentie)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/CheckUserData/{value}/{referentie}";
                var message = await client.GetAsync(url);
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        internal static Task<bool> LoginAsync(object p, string v)
        {
            throw new NotImplementedException();
        }

        public static async Task<JObject> GetUserData(string value, string referentie)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/GetUserData/{value}/{referentie}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                JObject gegevens = JsonConvert.DeserializeObject<JObject>(responseString.ToString());
                return gegevens;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<string> PutUserData(string value, string referentie, JObject data)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task DeleteUserData(string naam)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/DeleteUserData/{naam}";
                var message = await client.DeleteAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<bool> PostOefening(JObject data)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                var request = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://streetworkout.azurewebsites.net/api/PostOefening";
                var message = await client.PostAsync(url, httpContent);
                return message.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<JArray> GetOefeningenData(string naam)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/GetOefening/{naam}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                JArray gegevens = JsonConvert.DeserializeObject<JArray>(responseString.ToString());
                return gegevens;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task DeleteOefeningenData(string naam)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/DeleteOefeningenData/{naam}";
                var message = await client.DeleteAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<bool> PostWater(string naam, int waterdoel, int watergedronken)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<JArray> GetWater(string naam)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/GetWater/{naam}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                JArray gegevens = JsonConvert.DeserializeObject<JArray>(responseString.ToString());
                return gegevens;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<JObject> GetLatestWater(string naam)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/GetLatestWater/{naam}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                JObject gegevens = JsonConvert.DeserializeObject<JObject>(responseString.ToString());
                return gegevens;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task DeleteWater(string naam)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/DeleteWater/{naam}?code=cvCM8haZ00J5YYzWHglXIyJh9eANDVel5PSZsymy83Hv18rPnstrQA==";
                var message = await client.DeleteAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<string> PutWater(JObject data)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<bool> PostProfilePicture(string naam, Stream content)
        {
            try
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
                    bytes = ms.ToArray();
                }
                ProfilePicture foto = new ProfilePicture() { Naam = naam, stream = bytes };
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                var request = JsonConvert.SerializeObject(foto);
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://streetworkout.azurewebsites.net/api/PostProfilePicture";
                var message = await client.PostAsync(url, httpContent);
                return message.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task<Uri> GetProfilePicture(string naam)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                string url = $"https://streetworkout.azurewebsites.net/api/GetProfilePicture/{naam}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                JObject uri = JsonConvert.DeserializeObject<JObject>(responseString.ToString());
                return new Uri(uri["Uri"].ToString() + uri["SAS"].ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public static async Task DeleteProfilePicture(string naam)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/DeleteProfilePicture/{naam}";
                var message = await client.DeleteAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }
    }
}