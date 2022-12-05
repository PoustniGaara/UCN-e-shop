using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Dynamic;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;
using WebAppMVC.ActionFilters;
using WebApiClient.Interfaces;
using WebAppMVC.Tools;
using System.Drawing;
using NLog.Fluent;
using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace WebAppMVC.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class ProductController : Controller
    {
        private const string categoryListCacheKey = "categoryList";
        private IMemoryCache _cache;
        private IProductClient _productclient;
        private ICategoryClient _categoryclient;
        private readonly IMapper _mapper;


        public ProductController(IProductClient prodClient, ICategoryClient catClient, IMapper mapper, IMemoryCache cache)
        {
            _cache = cache;
            _productclient = prodClient;
            _categoryclient = catClient;
            _mapper = mapper;
        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            //Get the IEnumerable from API client
            IEnumerable<ProductDto> productDtoList = await _productclient.GetAllAsync();

            //Create new view model
            ProductIndexVM productIndexVM = _mapper.Map<ProductIndexVM>(productDtoList);

            //Try to get categories from cache
            if(_cache.TryGetValue(categoryListCacheKey, out IEnumerable<string> categories))
            {
                productIndexVM.Categories = categories;
            }
            else
            {
                IEnumerable<string> categories = 
            }

            return View(productIndexVM);
        }

        public async Task<ActionResult> Add(int id, [FromQuery] string size)
        {
            var cart = HttpContext.GetCart();
            var items = cart.Items.ToList();
            int sizeId = SizeToIdConverter.ConvertSizeToId(size);
            var productDto = await _productclient.GetByIdAsync(id);
           
            if(items.Where(i => i.ProductId == id && i.SizeId == sizeId).Any()) {
                int index = items.FindIndex(i => i.ProductId == id && i.SizeId == sizeId);
                items[index].Quantity = items[index].Quantity + 1;
            } else { 
                items.Add(new LineItemDto { ProductId = id, SizeName = size, SizeId = sizeId, Price = productDto.Price, ProductName = productDto.Name, Quantity = 1 });
            }

            cart.Items = items;
            HttpContext.SaveCart(cart);

            return Redirect("/product/details/" + id);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var productDto = await _productclient.GetByIdAsync(id);
            ProductDetailsVM productDetailsVM = _mapper.Map<ProductDetailsVM>(productDto);

            return View(productDetailsVM);
        }

    }
}
