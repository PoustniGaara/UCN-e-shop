using System;
using System.Collections.Generic;

namespace WebApiClient.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public string UserEmail { get; set; }
        
        public IEnumerable<LineItemDto> Items { get; set; }

    }
}

