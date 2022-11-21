using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<LineItemDto> Items { get; set; } = new List<LineItemDto>();

    }
}

