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
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<GetProductDto>>> Get([FromQuery] string? category)
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
            IEnumerable<GetProductDto> productDtos = products.Select(s => _mapper.Map<GetProductDto>(s));

            return Ok(productDtos);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> Get(int id)
        {
            var product = await _dataAccess.GetByIdAsync(id);
            if (product == null) { return NotFound(); }

            GetProductDto productDto = _mapper.Map<GetProductDto>(product);
            
           return Ok(productDto); 
        }

        //POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] PostProductDto newProductDto)
        {
            Product product = _mapper.Map<Product>(newProductDto);

            int id = await _dataAccess.CreateAsync(product);

            return Ok(id);
        }

        //PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PostProductDto updatedProductDto)
        {
            Product product = await _dataAccess.GetByIdAsync(id);
            if (product == null) { return NotFound(); }

            product = _mapper.Map<Product>(updatedProductDto);

            bool success = await _dataAccess.UpdateAsync(product);
            if (success) return Ok();
            else throw new Exception("Update was not successful");
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           Product product = await _dataAccess.GetByIdAsync(id);
           if(product == null) { return NotFound(); }
           return Ok();    
        }
        #endregion 
    }
}
