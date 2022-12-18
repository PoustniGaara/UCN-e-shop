using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebAppMVC.ActionFilters;
using WebAppMVC.Tools;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class ProductController : Controller
    {
        private const string categoryListCacheKey = "categoryList";
        private readonly IMemoryCache _cache;
        private readonly IProductClient _productclient;
        private readonly ICategoryClient _categoryclient;
        private readonly IMapper _mapper;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public ProductController(IProductClient prodClient, ICategoryClient catClient, IMapper mapper, IMemoryCache cache)
        {
            _cache = cache;
            _productclient = prodClient;
            _categoryclient = catClient;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index([FromQuery] string? category)
        {
            //Get the IEnumerable from API client
            IEnumerable<ProductDto> productDtoList = await _productclient.GetAllAsync(category);

            //Create new view model
            ProductIndexVM productIndexVM = _mapper.Map<ProductIndexVM>(productDtoList.Select(productDto => _mapper.Map<ProductDetailsVM>(productDto)));

            //Get categories
            //...
            //If possbile get from cache
            if (_cache.TryGetValue(categoryListCacheKey, out IEnumerable<CategoryVM> categories))
            {
                productIndexVM.Categories = categories;
            }
            //Else get from DB
            else
            {
                try
                {
                    //NOTE: Semaphore is here because we don't want a case when two user get data from DB and then write it in cache at the same time.
                    await semaphore.WaitAsync();
                    
                    //Get categories from DB
                    IEnumerable<CategoryDto> categoriesDB = await _categoryclient.GetAllAsync();
                    IEnumerable<CategoryVM> categoriesVM = categoriesDB.Select(categoryDto => _mapper.Map<CategoryVM>(categoryDto));

                    productIndexVM.Categories = categoriesVM;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(12))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(2))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                    _cache.Set(categoryListCacheKey, categoriesVM, cacheEntryOptions);
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return View(productIndexVM);
        }

        public async Task<ActionResult> Add(int id, [FromQuery] string size)
        {
            var cart = HttpContext.GetCart();
            var items = cart.Items.ToList();
            int sizeId = SizeToIdConverter.ConvertSizeToId(size);
            var productDto = await _productclient.GetByIdAsync(id);

            if (items.Where(i => i.ProductId == id && i.SizeId == sizeId).Any())
            {
                int index = items.FindIndex(i => i.ProductId == id && i.SizeId == sizeId);
                items[index].Quantity = items[index].Quantity + 1;
            }
            else
            {
                items.Add(new LineItemVM { ProductId = id, SizeName = size, SizeId = sizeId, Price = productDto.Price, ProductName = productDto.Name, Quantity = 1 });
            }

            cart.Items = items;
            HttpContext.SaveCart(cart);

            return View("Views/Product/AddedToCart.cshtml");
        }

        public async Task<ActionResult> Details(int id)
        {
            var productDto = await _productclient.GetByIdAsync(id);
            ProductDetailsVM productDetailsVM = _mapper.Map<ProductDetailsVM>(productDto);
            return View(productDetailsVM);
        }

    }
}
