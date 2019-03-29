using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
    }
}
