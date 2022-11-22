using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
