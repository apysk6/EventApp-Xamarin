﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using EventAppApi.Helpers;
using EventAppApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventAppApi.Services
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        private readonly AppSettings _appSettings;
        private readonly DataContext _context;
        private List<Account> _accounts;
        private List<Event> _events;

        public UserService(DataContext context, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _accounts = _context.Accounts.ToList();
            _events = _context.Event.ToList();
        }

        public string Authenticate(string email, string password)
        {
            var user = _accounts.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

             _context.SaveChangesAsync();

            return user.Token;
        }

        public IEnumerable<Event> GetEvents(string token)
        {
            var user = _accounts.FirstOrDefault(x => x.Token.Equals(token));
            int userId = user.Id;

            var events = _events.Where(x => x.AccountId == userId).ToList();

            if (user == null)
                return new List<Event>();

            return events;
        }

        public IEnumerable<Event> GetEventsInYourCity(string token)
        {
            var user = _accounts.FirstOrDefault(x => x.Token.Equals(token));

            List<Event> events = _events.Where(x => x.Place.Contains(user.City)).ToList();

            if (user == null)
                return new List<Event>();

            return events;
        }

        public Account GetCurrentAccount(string token)
        {
            return _accounts.FirstOrDefault(x => x.Token.Equals(token));
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts.Select(x => {
                x.Password = null;
                return x;
            });
        }
    }
}
