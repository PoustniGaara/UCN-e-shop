using System;
namespace DataAccessLayer.Model
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }    
        public string PhoneNumber { get; set; }
        public string Address { get; set; } 
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public User()
        { }

        public User(string email, string name, string surename, string phoneNumber, string address, string password, bool isAdmin, IEnumerable<Order> orders)
        {
            Email = email;
            Name = name;
            Surname = surename;
            PhoneNumber = phoneNumber;
            Address = address;
            Password = password;
            IsAdmin = isAdmin;
            Orders = orders;
        }

        public User(string email, string name, string surename, string phoneNumber, string address, string password, bool isAdmin)
        {
            Email = email;
            Name = name;
            Surname = surename;
            PhoneNumber = phoneNumber;
            Address = address;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}