using System.ComponentModel;

namespace DataAccessLayer.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Supplier Supplier { get; set; }
        public Category Category { get; set; }


    }
}