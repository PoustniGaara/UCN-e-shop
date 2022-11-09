using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Model;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Properties and Constructor
        IProductDataAccess _productDataAccess;
        private readonly IMapper _mapper;
        private ILoggerManager _logger;


        public ProductController(IProductDataAccess productDataAccess, IMapper mapper, ILoggerManager logger)
        {
            _productDataAccess = productDataAccess;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Default Crud Actions
        // GET: api/products/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            IEnumerable<Product>? products = null;

            if (!string.IsNullOrEmpty("")) // for future category search
            {
                //Not implemented because of idea of the need of new DAO for category
            }
            else
            {
                _logger.LogInfo("Fetching all the Products from the DB");

                products = await _productDataAccess.GetAllAsync();

                _logger.LogInfo($"Returning {products.Count()} students.");

                
            }
            //products.ToList().ForEach(p => _mapper.Map<ProductDto>(p));
            IEnumerable<ProductDto> productDtos = products.Select(s => _mapper.Map<ProductDto>(s));

            return Ok(productDtos);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _productDataAccess.GetProductByIdAsync(id);
            ProductDto productDto = _mapper.Map<ProductDto>(product);
            if (product == null) { return NotFound(); }
            else { return Ok(productDto); }
        }

        // POST api/<ProductController>
        //[HttpPost]
        //public async Task<ActionResult<int>> Post([FromBody] ProductDto newProductDto)
        //{
        //    int id = await _productDataAccess.CreateProductAsync(newProductDto.FromDto());
        //    if(id == -1) { return 500; }
        //    return Ok();
        //}

        // PUT api/<ProductController>/5
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
