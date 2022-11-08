using System;
using System.Collections.Generic;
using DataAccessLayer.Model;

namespace WebApi.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public string UserEmail { get; set; }
        public List<LineItem> Items { get; set; }

    }
}

