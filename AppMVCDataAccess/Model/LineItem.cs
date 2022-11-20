using System;
namespace DataAccessLayer.Model
{
    public class LineItem
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }


        public LineItem(int productId, int sizeId, int quantity)
        {
            ProductId = productId;
            SizeId = sizeId;
            Quantity = quantity;    
        }
    }
}

