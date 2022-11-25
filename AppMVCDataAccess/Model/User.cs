using System;
namespace DataAccessLayer.Model
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}

