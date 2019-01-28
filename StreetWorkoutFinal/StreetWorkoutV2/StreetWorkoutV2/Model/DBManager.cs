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
    //---------------------------------------------------------------------------------------//
    //-----------------------Alles wat temaken heeft met onze database-----------------------//
    //---------------------------------------------------------------------------------------//

    public static class DBManager
    {
        //---Encryptreren van wachtwoorden---//
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

        //---mailservice aanroepen---//
        public static async Task<string> MailService(string email, string name)
        {
            try
            {
                JObject reg = new JObject();
                reg["Email"] = email;
                reg["Name"] = name;
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

        //---Registreren van personen---//
        public static async Task<bool> Register(string email, string name, string password)
        {
            try
            {
                JObject reg = new JObject();
                reg["Email"] = email;
                reg["Name"] = name;
                reg["ApiName"] = name;
                reg["Password"] = password;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                var request = JsonConvert.SerializeObject(reg);
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://streetworkout.azurewebsites.net/api/Register";
                var message = await client.PostAsync(url, httpContent);
                return message.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Login van persoon---//
        public static async Task<bool> Login(string name, string password)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/Login/{name}/{password}";
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

        //---Checken ofdat de userdata overeenkomt---//
        public static async Task<bool> CheckUserData(string value, string reference)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/CheckUserData/{value}/{reference}";
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

        internal static Task<bool> Login(object p, string v)
        {
            throw new NotImplementedException();
        }

        //---ophalen van user data---//
        public static async Task<JObject> GetUserData(string value, string reference)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/GetUserData/{value}/{reference}";
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

        //---updaten van user data---//
        public static async Task PutUserData(string value, string reference, JObject data)
        {
            try
            {
                data[reference] = value;
                data["Reference"] = reference;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                var request = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://streetworkout.azurewebsites.net/api/PutUserData";
                var message = await client.PutAsync(url, httpContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---verwijderen van user data---//
        public static async Task DeleteUserData(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/DeleteUserData/{name}";
                var message = await client.DeleteAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Aanmaken van exercise---//
        public static async Task<bool> PostExerciseData(ExerciseDB data)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                var request = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://streetworkout.azurewebsites.net/api/PostExerciseData";
                var message = await client.PostAsync(url, httpContent);
                return message.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Ophalen van exercises---//
        public static async Task<JArray> GetExerciseData(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/GetExerciseData/{name}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                JArray data = JsonConvert.DeserializeObject<JArray>(responseString.ToString());
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Verwijderen van Exercises---//
        public static async Task DeleteExerciseData(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/DeleteExerciseData/{name}";
                var message = await client.DeleteAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Aanmaken water data---//
        public static async Task<bool> PostWaterData(string name, int watergoal, int waterdrunk)
        {
            try
            {
                TimeZone localZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOffset = localZone.GetUtcOffset(DateTime.Now);
                JObject water = new JObject();
                water["WaterGoal"] = watergoal;
                water["Name"] = name;
                water["WaterDrunk"] = waterdrunk;
                water["Date"] = DateTime.Now.ToString("MM-dd-yyyy");
                if (currentOffset.ToString().Substring(0, 1) == "-")
                {
                    water["DateTime"] = DateTime.Now.AddHours(-int.Parse(currentOffset.ToString().Substring(1, 2)));
                }
                else
                {
                    water["DateTime"] = DateTime.Now.AddHours(int.Parse(currentOffset.ToString().Substring(0, 2)));
                }
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                var request = JsonConvert.SerializeObject(water);
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://streetworkout.azurewebsites.net/api/PostWaterData";
                var message = await client.PostAsync(url, httpContent);
                return message.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Ophalen water data---//
        public static async Task<JArray> GetWaterData(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/GetWaterData/{name}";
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

        //---Ophalen laatste water data---//
        public static async Task<JObject> GetLatestWaterData(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/GetLatestWaterData/{name}";
                var message = await client.GetAsync(url);
                var responseString = await message.Content.ReadAsStringAsync();
                JObject data = JsonConvert.DeserializeObject<JObject>(responseString.ToString());
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Verwijderen water data---//
        public static async Task DeleteWaterData(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/DeleteWaterData/{name}?code=bk6UkwIfGGQRaWzcO9KEW45OxtkLbzkgEAoJbXhAZLnNjeINMcFNhQ==";
                var message = await client.DeleteAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Updaten water data---//
        public static async Task PutWaterData(JObject data)
        {
            try
            {
                data["Date"] = DateTime.Now.ToString("MM-dd-yyyy");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                var request = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://streetworkout.azurewebsites.net/api/PutWaterData";
                var message = await client.PutAsync(url, httpContent);
                var responseString = await message.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Opslaan/updaten van de profielfoto---//
        public static async Task<byte[]> PostProfilePicture(string name, Stream content)
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
                ProfilePicture picture = new ProfilePicture() { Name = name, stream = bytes };
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                var request = JsonConvert.SerializeObject(picture);
                var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
                string url = "https://streetworkout.azurewebsites.net/api/PostProfilePicture";
                var message = await client.PostAsync(url, httpContent);
                return bytes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        //---Ophalen van de profiel foto---//
        public static async Task<Uri> GetProfilePicture(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                string url = $"https://streetworkout.azurewebsites.net/api/GetProfilePicture/{name}";
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

        //---verwijderen van de profielfoto---//
        public static async Task DeleteProfilePicture(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/string");
                string url = $"https://streetworkout.azurewebsites.net/api/DeleteProfilePicture/{name}";
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