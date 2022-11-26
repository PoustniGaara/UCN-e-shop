using DataAccessLayer.SqlDbDataAccess;

namespace DataAccessLayer
{
    public static class DataAccessFactory
    {
        public static T CreateRepository<T>(string connectionstring) where T : class
        {
            switch (typeof(T).Name)
            {
                case "IProductDataAccess": return new ProductDAO(connectionstring) as T;
                case "IOrderDataAccess": return new OrderDAO(connectionstring) as T;
                case "IUserDataAccess": return new UserDAO(connectionstring) as T;
            }
            throw new ArgumentException($"Unknown type {typeof(T).FullName}");
        }


    }
}
