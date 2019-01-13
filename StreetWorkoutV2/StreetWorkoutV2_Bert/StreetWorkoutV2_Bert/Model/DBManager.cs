using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
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
        const string SendGrid_API = "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";
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

        public static async Task<string> WachtwoordVergetenAsync(string email)
        {
            EmailVer ver = new EmailVer();
            ver.Email = email;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(ver);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/GetUserName";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            return responseString.ToString();
        }

        public static async Task MailService(string email, string naam)
        {
            Random rnd = new Random();
            int length = rnd.Next(10, 15);
            string wachtwoord = "";
            for (int i = 0; i < length; i++)
            {
                Random rnd2 = new Random();
                int number = rnd.Next(3, 6);
                if (number % 3 == 0)
                {
                    Random rnd3 = new Random();
                    string element = rnd.Next(0, 10).ToString();
                    wachtwoord += element;
                }
                else if (number % 4 == 0)
                {
                    var chars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
                    Random rnd3 = new Random();
                    int index = rnd.Next(0, chars.Length+1);
                    wachtwoord += chars[index];
                }
                else if (number % 5 == 0)
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                    Random rnd3 = new Random();
                    int index = rnd.Next(0, chars.Length + 1);
                    wachtwoord += chars[index];
                }
            }
            var client = new SendGridClient(SendGrid_API);
            var from = new EmailAddress("nmctstreetworkout@outlook.com", "StreetWorkout");
            var subject = "Aanvraag voorlopig wachtwoord";
            var to = new EmailAddress(email, naam);
            var plainTextContent = $"Beste {naam}\nU heeft een nieuw wachtwoord aangevraagd in de app StreetBeat.\nVolgend wachtwoord is uw nieuw voorlopig wachtwoord: {wachtwoord}\nWe raden u tensterkste aan om uw wachtwoord te veranderen na het gebruiken van dit wachtwoord.\n\nGroeten support StreetBeat.";
            var htmlContent = $"Beste {naam}\nU heeft een nieuw wachtwoord aangevraagd in de app StreetBeat.\nVolgend wachtwoord is uw nieuw voorlopig wachtwoord: {wachtwoord}\nWe raden u tensterkste aan om uw wachtwoord te veranderen na het gebruiken van dit wachtwoord.\n\nGroeten support StreetBeat.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
