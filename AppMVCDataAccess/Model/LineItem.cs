using System;
namespace DataAccessLayer.Model
{
    public class LineItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public LineItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;    
        }
    }
}

