using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using System.Dynamic;
using WebApiClient;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class ProductController : Controller
    {
        private IApiClient _client;
        private readonly IMapper _mapper;

        public ProductController(IApiClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        } 


        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            //Get the IEnumerable from API client
            IEnumerable<ProductDto> productDtoList = await _client.GetAllProductsAsync();

            //Create new view model
            ProductIndexVM productIndexVM = _mapper.Map<ProductIndexVM>(productDtoList);
            //ProductIndexVM productIndexVM = new(productDtoList);
            //productIndexVM.PageTitle = "Products";

            foreach(ProductDto productDto in productIndexVM.Products)
            {
                Console.WriteLine(productDto.Name);
            }

            return View(productIndexVM);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //NOT FINISHED
            var blogPost = await _client.GetProductByIdAsync(id);
            //var author = await _client.GetProductByIdAsync(blogPost.AuthorId);
            dynamic model = new ExpandoObject();
            model.BlogPost = blogPost;
            //model.Author = author;
            return View(model);
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
