using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.DTOs
{
    public class SupplierDto
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }

        public SupplierDto(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
