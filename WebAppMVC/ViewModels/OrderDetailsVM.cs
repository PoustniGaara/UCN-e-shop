using System.Reflection;
using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class OrderDetailsVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<LineItemDto> Items { get; set; }
        public string Title { get
            {
                return "Order #" + Id;
            }
        }
    }
}
