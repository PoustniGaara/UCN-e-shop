//using System;
//using System.ComponentModel.DataAnnotations;

//namespace WebApiClient.DTOs;

//public class LineItem
//{
//    public Product Product { get; set; }
//    public int SizeId { get; set; }
//    public int Quantity { get; set; }


//    public LineItem(Product product, int sizeId, int quantity)
//    {
//        Product = product;
//        SizeId = sizeId;
//        Quantity = quantity;    
//    }
//}

//public class Product
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string Description { get; set; }
//    public decimal Price { get; set; }
//    public IEnumerable<ProductSizeStock> ProductSizeStocks { get; set; }
//    public string Category { get; set; }

//    public Product(int id, string name, string description, decimal price, IEnumerable<ProductSizeStock> productSizeStocks, string category)
//    {
//        Id = id;
//        Name = name;
//        Description = description;
//        Price = price;
//        ProductSizeStocks = productSizeStocks;
//        Category = category;
//    }

//    public Product() { }
//}

