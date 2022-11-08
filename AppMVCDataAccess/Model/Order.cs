using System;
namespace DataAccessLayer.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public string Note { get; set; }
        public User User { get; set; }
        public List<LineItem> Items { get; set; } = new List<LineItem>();
    }

    public enum Status
    {
        CART, PLACED
    }
}

