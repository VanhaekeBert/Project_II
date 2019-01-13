using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StreetWorkoutV2_Bert.Model
{
    public static class DBManager
    {
        public static async Task<bool> RegistrerenAsync(string email, string naam, string wachtwoord)
        {
            Registreren reg = new Registreren();
            reg.Email = email;
            reg.Naam = naam;
            reg.Wachtwoord = wachtwoord;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(reg);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/Registreren";
            var message = await client.PostAsync(url, httpContent);
            return message.IsSuccessStatusCode;
        }

        public static async Task<bool> CheckUserNameAsync(string naam)
        {
            CheckNaam checkNaam = new CheckNaam();
            checkNaam.Naam = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(checkNaam);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/CheckUserName";
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

        public static async Task<bool> CheckEmailAsync(string email)
        {
            CheckEmail checkEmail = new CheckEmail();
            checkEmail.Email = email;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(checkEmail);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/CheckMail";
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

        public static async Task<bool> LoginAsync(string naam, string wachtwoord)
        {
            Login log = new Login();
            log.Naam = naam;
            log.Wachtwoord = wachtwoord;
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
    }
}
