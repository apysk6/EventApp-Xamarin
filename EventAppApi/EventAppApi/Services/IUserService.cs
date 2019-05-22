using System.Collections.Generic;
using EventAppApi.Models;

namespace EventAppApi.Services
{
    public interface IUserService
    {
        string Authenticate(string username, string password);
        IEnumerable<Event> GetEvents(string token);
        Account GetCurrentAccount(string token);
        IEnumerable<Event> GetEventsInYourCity(string token);
        IEnumerable<Account> GetAll();
    }
}
