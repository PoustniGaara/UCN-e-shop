using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.DTOs.Converters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Properties and Constructor
        IProductDataAccess _productDataAccess;

        public ProductController(IProductDataAccess productDataAccess) => _productDataAccess = productDataAccess;

        #endregion

        #region Default Crud Actions
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            IEnumerable<Product> products = null;


            if (!string.IsNullOrEmpty("")) // for future catefory search
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
        #endregion 
    }
}
