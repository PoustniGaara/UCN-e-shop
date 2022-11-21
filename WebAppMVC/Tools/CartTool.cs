using WebApiClient.DTOs;

namespace WebAppMVC.Tools
{
    public static class CartTool
    {
        private const string shoppingCartKey = "shopping_cart";

        public static OrderDto GetCart(this HttpContext context)
        {
            if(context.Session.Get<OrderDto>(shoppingCartKey) == null)
            {
                context.Session.Set<OrderDto>(shoppingCartKey, new OrderDto());
            }
            return context.Session.Get<OrderDto>(shoppingCartKey);
        }

        public static void SaveCart(this HttpContext context, OrderDto cart)
        {
            context.Session.Set<OrderDto>(shoppingCartKey, cart);
        }
    }
}