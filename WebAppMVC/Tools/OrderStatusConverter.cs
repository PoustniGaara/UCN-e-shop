namespace WebAppMVC.Tools
{
    public static class OrderStatusConverter
    {
        private static readonly Dictionary<int, string> Sizes = new Dictionary<int, string> {
            { 0, "Placed"  },
        };

        public static string GetStatusAsString(int status)
        {
            return Sizes[status];
        }
    }
}
