using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.DTOs.Converters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Properties
        const string baseURI = "api/v1/products";
        private IProductDataAccess _productDataAccess { get; set; }
        #endregion

        #region Constructor
        public ProductController(IProductDataAccess productDataAccess)
        {
            _productDataAccess = productDataAccess;
        }
        #endregion

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] string category)
        {
            IEnumerable<Product> products;


            if (!string.IsNullOrEmpty(category))
            {
                //Not implemented because of dilema of the need of new DAO for category
                products = null;
            }
            else
            {
                products = await _productDataAccess.GetAllAsync();
            }

            return Ok(products.ToDtos());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
