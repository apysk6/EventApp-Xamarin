using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace EventApp.Models
{
    public sealed class Client
    {
        private static Client _client;

        public static Client Instance => _client ?? (_client = new Client());

        public static ISettings AppSettings => CrossSettings.Current;

        public async Task<bool> RegisterAsync(string email, string password, string firstName, string surname, string city)
        {
            var client = new HttpClient();

            var model = new Account
            {
                Email = email,
                Password = password,
                Surname = surname,
                FirstName = firstName,
                City = city
            };

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://eventappapi.azurewebsites.net/api/Accounts", content)
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var client = new HttpClient();

            var model = new Account
            {
                Email = email,
                Password = password,
            };

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://eventappapi.azurewebsites.net/api/Authenticate/authenticate", content)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                AppSettings.AddOrUpdateValue("Token", token);
            }

            return response.IsSuccessStatusCode;
        } 

        public async Task<bool> AddEventAsync(Event newEvent)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(newEvent);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.Add("token", AppSettings.GetValueOrDefault("Token", string.Empty));

            var response = await client.PostAsync("https://eventappapi.azurewebsites.net/api/Events", content).ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Event>> GetEvents()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://eventappapi.azurewebsites.net/api/Events").ConfigureAwait(false);
            var events = new List<Event>();

            if (response.IsSuccessStatusCode)
            {
                events = JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
            }

            return events;
        }
    }
}
