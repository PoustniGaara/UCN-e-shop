using System;
namespace DataAccessLayer.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public enum Status { get; set; }
        public string Note { get; set; }

      
    }

    public enum Status
    {
        CART, PLACED
    }
}

