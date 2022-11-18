using AutoMapper;
using DataAccessLayer.Model;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using WebApi.DTOs;
using WebApi.ActionFilters;
using DataAccessLayer.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Properties and Constructor
        IProductDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public ProductsController(IProductDataAccess productDataAccess, IMapper mapper)
        {
            _dataAccess = productDataAccess;
            _mapper = mapper;
        }

        #endregion

        #region Default Crud Actions
        // GET: api/products/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] string? category)
        {
            IEnumerable<Product> products;
            if (!string.IsNullOrEmpty(category)) 
            {
                products = await _dataAccess.GetAllByCategoryAsync(category);
            }
            else
            {
                products = await _dataAccess.GetAllAsync();
            }
            IEnumerable<ProductDto> productDtos = products.Select(s => _mapper.Map<ProductDto>(s));

            return Ok(productDtos);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _dataAccess.GetByIdAsync(id);
            if (product == null) { return NotFound(); }

            ProductDto productDto = _mapper.Map<ProductDto>(product);
            
           return Ok(productDto); 
        }

        //POST api/<ProductController>
        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ProductDto newProductDto)
        {
            Product product = _mapper.Map<Product>(newProductDto);

            int id = await _dataAccess.CreateAsync(product);

            return Ok(id);
        }

        //PUT api/<ProductController>/5
        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDto updatedProductDto)
        {
            Product product = await _dataAccess.GetByIdAsync(id);
            if (product == null) { return NotFound(); }

            product = _mapper.Map<Product>(updatedProductDto);
            await _dataAccess.UpdateAsync(product);

            return Ok();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           Product product = await _dataAccess.GetByIdAsync(id);
           if(product == null) { return NotFound(); }

           await _dataAccess.DeleteAsync(id);

           return Ok();    
        }
        #endregion 
    }
}
