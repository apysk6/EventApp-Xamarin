using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace EventApp.Models
{
    public sealed class Client
    {
        private static Client _client;

        public static Client Instance => _client ?? (_client = new Client());

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

            var response = await client.PostAsync("https://eventappapi.azurewebsites.net/api/Accounts", content);

            return response.IsSuccessStatusCode;
        }
    }
}
