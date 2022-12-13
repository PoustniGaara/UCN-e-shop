using System.ComponentModel.DataAnnotations;
using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class OrderCreateVM
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "E-mail is required!")]
        public string UserEmail { get; set; }
        public string? Phone { get; set; }
        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Street and house number is required!")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Postal code is required!")]
        public string PostalCode { get; set; }
        public IEnumerable<LineItemVM> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; } = "";
        public int? AptNumber { get; set; }
        public string? Note { get; set; }

        public string PageTitle
        {
            get
            {
                return "New Order";
            }
            set { }
        }
    }
}
