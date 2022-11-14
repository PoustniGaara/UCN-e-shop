using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Model;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using WebApi.DTOs;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Properties and Constructor
        IProductDataAccess _productDataAccess;
        private readonly IMapper _mapper;

        public ProductsController(IProductDataAccess productDataAccess, IMapper mapper)
        {
            _productDataAccess = productDataAccess;
            _mapper = mapper;
        }

        #endregion

        #region Default Crud Actions
        // GET: api/products/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> Get([FromQuery] string? category)
        {
            IEnumerable<Product> products;
            if (!string.IsNullOrEmpty(category)) 
            {
                products = await _productDataAccess.GetByCategoryAsync(category);
            }
            else
            {
                products = await _productDataAccess.GetAllAsync();
            }
            IEnumerable<GetProductDto> productDtos = products.Select(s => _mapper.Map<GetProductDto>(s));

            return Ok(productDtos);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> Get(int id)
        {
            var product = await _productDataAccess.GetProductByIdAsync(id);
            GetProductDto productDto = _mapper.Map<GetProductDto>(product);
            if (product == null) { return NotFound(); }
            else { return Ok(productDto); }
        }

        //POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] PostProductDto newProductDto)
        {
            Product product = _mapper.Map<Product>(newProductDto);

            int id = await _productDataAccess.CreateProductAsync(product);

            return Ok(id);
        }

        //PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id, [FromBody] ProductDto updatedProductDto)
        //{
        //    return Ok(await _productDataAccess.UpdateProductAsync(updatedProductDto.FromDto()));
        //}

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           return Ok( await _productDataAccess.DeleteProductAsync(id));    
        }
        #endregion 
    }
}
