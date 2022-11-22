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
            ProductIndexVM productIndexVM = _mapper.Map<ProductIndexVM>(productDtoList);

            return View(productIndexVM);
        }

        public async Task<ActionResult> Add(int id,[FromQuery] string size)
        {
            var cart = HttpContext.GetCart();
           // cart.Items.Add(new LineItemDto { ProductId = id, SizeId = 0});
            HttpContext.SaveCart(cart);
            return Redirect("/product/details/" + id);
            
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var producsdsd = await _client.GetByIdAsync(id);
            //var author = await _client.GetProductByIdAsync(blogPost.AuthorId);
            //model.Author = author;
            
            return View();
        }

        public async Task<ActionResult> QuickDetails(ProductDto productDto)
        {

            return View(productDto);
        }


    }
}
