using System;
namespace DataAccessLayer.Model
{
    public class LineItem
    {
        public Product? Product { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }

        public LineItem(Product product, int sizeId, int quantity)
        {
            Product = product;
            SizeId = sizeId;
            Quantity = quantity;    
        }

        public LineItem()
        {
        }
    }
}

