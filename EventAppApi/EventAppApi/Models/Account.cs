using System.Collections.Generic;

namespace EventAppApi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Token { get; set; }
    }
}
