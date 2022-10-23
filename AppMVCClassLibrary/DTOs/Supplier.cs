using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.DTOs
{
    public class Supplier
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }

        public Supplier(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
