using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.DTOs
{
    public class CategoryDto
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        public CategoryDto(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
