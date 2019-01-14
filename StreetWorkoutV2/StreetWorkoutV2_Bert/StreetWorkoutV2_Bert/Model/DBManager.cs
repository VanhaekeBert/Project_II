﻿using Newtonsoft.Json;
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

        public static async Task<bool> CheckUserNameAsync(string naam)
        {
            JObject checkNaam = new JObject();
            checkNaam["Naam"] = naam;
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
            JObject checkEmail = new JObject();
            checkEmail["Email"] = email;
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

        public static async Task<string> UserName(string email)
        {
            JObject ver = new JObject();
            ver["Email"] = email;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(ver);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/GetUserName";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            return responseString.ToString();
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

        public static async Task<string> WachtwoordReset(string email, string wachtwoord)
        {
            JObject wwr = new JObject();
            wwr["Email"] = email;
            wwr["Wachtwoord"] = wachtwoord;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(wwr);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/WachtwoordReset";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            return responseString.ToString();
        }

        public static async Task<string> CheckForAPI(string naam)
        {
            JObject CFA = new JObject();
            CFA["Naam"] = naam;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/string");
            var request = JsonConvert.SerializeObject(CFA);
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            string url = "https://streetworkout.azurewebsites.net/api/WachtwoordReset";
            var message = await client.PostAsync(url, httpContent);
            var responseString = await message.Content.ReadAsStringAsync();
            return responseString.ToString();
        }
    }
}
