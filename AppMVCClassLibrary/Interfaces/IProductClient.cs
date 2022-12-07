﻿using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient.Interfaces
{
    public interface IProductClient
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>?> GetAllAsync(string? category);
        Task<int> CreateAsync(ProductDto product);
        Task<bool> UpdateAsync(ProductDto product);
        Task<bool> DeleteAsync(int id);
    }

}
