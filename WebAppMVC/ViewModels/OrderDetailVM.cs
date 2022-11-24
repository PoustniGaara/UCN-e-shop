using System.Reflection;
using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class OrderDetailVM
    {
        public OrderDto Order { get; set; }
        public string Title { get
            {
                return "Order #" + Order.Id;
            }
        }
    }
}
