using DataAccessLayer.SqlDbDataAccess;

namespace DataAccessLayer
{
    public static class DataAccessFactory
    {
        public static T CreateRepository<T>(string connectionstring) where T : class
        {
            switch (typeof(T).Name)
            {
                case "IProductDataAccess": return GetProductDAO(connectionstring) as T;
                case "IOrderDataAccess": return GetOrderDAO(connectionstring) as T;
                case "IUserDataAccess": return GetUserDAO(connectionstring) as T;
                case "ICategoryDataAccess": return new CategoryDAO(connectionstring) as T;
            }
            throw new ArgumentException($"Unknown type {typeof(T).FullName}");
        }

        private static ProductDAO GetProductDAO(string connectionString)
        {
            var sizeStockDAO = new ProductSizeStockDAO(connectionString);
            var categoryDAO = new CategoryDAO(connectionString);
            return new ProductDAO(connectionString, sizeStockDAO, categoryDAO);
        }

        private static OrderDAO GetOrderDAO(string connectionString)
        {
            var lineItemDAO = GetLineItemDAO(connectionString);
            var productSizeStockDAO = new ProductSizeStockDAO(connectionString);
            var userDAO = new UserDAO(connectionString);
            return new OrderDAO(connectionString, lineItemDAO, productSizeStockDAO, userDAO);
        }

        private static LineItemDAO GetLineItemDAO(string connectionString)
        {
            return new LineItemDAO(connectionString, GetProductDAO(connectionString));
        }

        private static UserDAO GetUserDAO(string connectionString)
        {
            var orderDAO = GetOrderDAO(connectionString);
            return new UserDAO(connectionString, orderDAO);
        }

    }
}
