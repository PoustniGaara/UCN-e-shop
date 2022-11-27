using WebApiClient.DTOs;

namespace WebAppMVC.Tools
{
    public static class CartTool
    {
        private const string shoppingCartKey = "shopping_cart";

        public static OrderDto GetCart(this HttpContext context)
        {
            var cart = context.Session.Get<OrderDto>(shoppingCartKey);
            
            if (cart == null)
                cart = new OrderDto() { Items = new List<LineItemDto>() };
            
            if(cart.Items == null)
                cart.Items = new List<LineItemDto>();

            cart.TotalPrice = cart.Items.Sum(item => item.Price * item.Quantity);

            return cart;
        }

        public static void SaveCart(this HttpContext context, OrderDto cart)
        {
            context.Session.Set<OrderDto>(shoppingCartKey, cart);
        }

        public static int GetCartCount(this HttpContext context)
        {
            var cart = context.Session.Get<OrderDto>(shoppingCartKey);
            if (cart == null || cart.Items == null)
                return 0;
            else
                return cart.Items.Count();
        }
    }
}