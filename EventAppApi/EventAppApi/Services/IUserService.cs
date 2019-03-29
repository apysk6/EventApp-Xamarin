using System.Collections.Generic;
using EventAppApi.Models;

namespace EventAppApi.Services
{
    public interface IUserService
    {
        string Authenticate(string username, string password);
        IEnumerable<Account> GetAll();
    }
}
