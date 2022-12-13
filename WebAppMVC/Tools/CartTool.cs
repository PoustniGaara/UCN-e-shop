using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Tools
{
    public static class CartTool
    {
        private const string shoppingCartKey = "shopping_cart";

        public static OrderCreateVM GetCart(this HttpContext context)
        {
            var cart = context.Session.Get<OrderCreateVM>(shoppingCartKey);
            
            if (cart == null)
                cart = new OrderCreateVM() { Items = new List<LineItemVM>() };
            
            if(cart.Items == null)
                cart.Items = new List<LineItemVM>();

            cart.TotalPrice = cart.Items.Sum(item => item.Price * item.Quantity);

            return cart;
        }

        public static void SaveCart(this HttpContext context, OrderCreateVM cart)
        {
            context.Session.Set<OrderCreateVM>(shoppingCartKey, cart);
        }

        public static int GetCartCount(this HttpContext context)
        {
            var cart = context.Session.Get<OrderCreateVM>(shoppingCartKey);
            if (cart == null || cart.Items == null)
                return 0;
            else
                return cart.Items.Count();
        }
    }
}