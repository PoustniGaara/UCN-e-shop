using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            }
            throw new ArgumentException($"Unknown type {typeof(T).FullName}");
        }
    }
}
