using System;
namespace DataAccessLayer.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public User User { get; set; }
        public IEnumerable<LineItem> Items { get; set; }

        public Order(int id, DateTime date, decimal totalPrice, Status status, string address, string note, User user, List<LineItem> items)
        {
            Id = id;
            Date = date;
            TotalPrice = totalPrice;
            Address = address;
            Status = status;
            Note = note;
            User = user;
            Items = items;
        }

        public Order(DateTime date, decimal totalPrice, Status status, string address, string note, User user, List<LineItem> items)
        {
            Date = date;
            TotalPrice = totalPrice;
            Status = status;
            Address = address;
            Note = note;
            User = user;
            Items = items;
        }

        public Order(Status status, User user, List<LineItem> items)
        {
            Status = status;
            User = user;
            Items = items;
        }

        public Order()
        {

        }
    }

    public enum Status
    {
        CART, PLACED
    }
}

