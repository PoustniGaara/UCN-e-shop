using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class OrderIndexVM
    {
        public IEnumerable<OrderDetailsVM> Orders { get; set; }

        public string Title
        {
            get
            {
                return "Orders";
            }
        }
    }
}
