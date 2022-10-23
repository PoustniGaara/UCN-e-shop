using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using WebApiClient;
using WebApiClient.DTOs;

namespace WebAppMVC.Controllers
{
    public class ProductController : Controller
    {
        private IApiClient _client;

        public ProductController(IApiClient client)
        {
            _client = client;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View();
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
