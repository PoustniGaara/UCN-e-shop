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

namespace WebAppMVC.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class ProductController : Controller
    {
        private IProductClient _client;
        private readonly IMapper _mapper;

        private readonly static List<ProductDto> products = new List<ProductDto>()
        {
            new ProductDto()
            {
                Id = 1, Name = "Black T-Shirt", Category = "Clothing", Description= "High quality T-shirt with the logo of UCN", 
                Price = 100, ProductSizeStocks = new List<ProductSizeStockDto>()
                {
                    new ProductSizeStockDto()
                    {
                        Id = 1, Size = "S", Stock = 50
                    }, new ProductSizeStockDto()
                    {
                        Id = 2, Size = "M", Stock = 40
                    }, new ProductSizeStockDto()
                    {
                        Id = 3, Size = "L", Stock = 7
                    }
                }
            }
        };

        public ProductController(IProductClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            //Get the IEnumerable from API client
            IEnumerable<ProductDto> productDtoList = await _client.GetAllAsync();

            //Create new view model
            ProductIndexVM productIndexVM = _mapper.Map<ProductIndexVM>(products);

            return View(productIndexVM);
        }

        public async Task<ActionResult> Add(int id,[FromQuery] string size)
        {
            var cart = HttpContext.GetCart();
            cart.Items.Add(new LineItemDto { Id = id, SizeId = 0});
            HttpContext.SaveCart(cart);
            return Redirect("/product/details/" + id);
            
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //NOT FINISHED
            var producsdsd = await _client.GetByIdAsync(id);
            //var author = await _client.GetProductByIdAsync(blogPost.AuthorId);
            //model.Author = author;
            
            return View(products.First());
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductDto product)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductDto product)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductDto product)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
