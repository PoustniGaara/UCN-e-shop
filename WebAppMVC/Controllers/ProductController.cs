using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Dynamic;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;
using WebAppMVC.ActionFilters;
using WebApiClient.Interfaces;
using WebAppMVC.Tools;

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
            HttpContext.GetCart();
            //Get the IEnumerable from API client
            IEnumerable<ProductDto> productDtoList = await _client.GetAllAsync();

            //Create new view model
            ProductIndexVM productIndexVM = _mapper.Map<ProductIndexVM>(productDtoList);

            return View(productIndexVM);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //NOT FINISHED
            var product = await _client.GetByIdAsync(id);
            //var author = await _client.GetProductByIdAsync(blogPost.AuthorId);
            //model.Author = author;
            return View();
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
