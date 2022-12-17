using AutoMapper;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IProductDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public ProductsController(IProductDataAccess productDataAccess, IMapper mapper)
        {
            _dataAccess = productDataAccess;
            _mapper = mapper;
        }

        #endregion

        #region Default Crud Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] string? category)
        {
            IEnumerable<Product> products;
            //Check wheter some certain category is requested, if no return all...
            if (!string.IsNullOrEmpty(category)) { products = await _dataAccess.GetAllByCategoryAsync(category); }
            else { products = await _dataAccess.GetAllAsync(); }

            if(products == null) { return NotFound(); }
            IEnumerable<ProductDto> productDtos = products.Select(product => _mapper.Map<ProductDto>(product));
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _dataAccess.GetByIdAsync(id);
            if (product == null) { return NotFound(); }
            ProductDto productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ProductDto newProductDto)
        {
            int id = await _dataAccess.CreateAsync(_mapper.Map<Product>(newProductDto));
            if (id <= 0) { return BadRequest(); }
            return Ok(id);
        }

        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDto updatedProductDto)
        {
            var productDB = await _dataAccess.GetByIdAsync(id);
            if (productDB == null) { return NotFound(); }
            Product product = _mapper.Map<Product>(updatedProductDto);
            await _dataAccess.UpdateAsync(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Product product = await _dataAccess.GetByIdAsync(id);
            if (product == null) { return NotFound(); }
            await _dataAccess.DeleteAsync(id);
            return Ok();
        }
        #endregion 
    }
}
