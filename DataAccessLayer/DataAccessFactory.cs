﻿using System;
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
                case "IProductDataAccess": return new ProductDataAccessDAO(connectionstring) as T;
                case "IOrderDataAccess": return new OrderDataAccessDAO(connectionstring) as T;
            }
            throw new ArgumentException($"Unknown type {typeof(T).FullName}");
        }
    }
}
