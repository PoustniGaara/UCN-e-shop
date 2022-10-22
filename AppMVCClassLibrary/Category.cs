using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMVCClassLibrary
{
    public class Category
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        public Category(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
