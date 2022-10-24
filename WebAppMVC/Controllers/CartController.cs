using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using WebApiClient;
using WebApiClient.DTOs;

namespace WebAppMVC.Controllers
{

    // WARNING THIS IS JUST A COPY OF PRODUCT CONTROLLER, CHANGES ARE NECCESSARY!

    public class CartController : Controller
    {
        private IApiClient _client;

        public CartController(IApiClient client) => _client = client;


        // GET: CartController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CartController/Details/5
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

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
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

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
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

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
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
