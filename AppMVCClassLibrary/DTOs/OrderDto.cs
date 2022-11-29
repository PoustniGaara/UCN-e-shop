using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiClient.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public string Address { get; set; } = "";
        public IEnumerable<LineItemDto> Items { get; set; } /*= new List<LineItem>()*/

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

        public int? AptNumber { get; set; }
        public string? Note { get; set; }

    }
}

