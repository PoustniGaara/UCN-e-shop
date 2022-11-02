﻿using DataAccessLayer;
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
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _productDataAccess.GetProductByIdAsync(id);
            if (product == null) { return NotFound(); }
            else { return Ok(product.ToDto()); }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ProductDto newProductDto)
        {
            int id = await _productDataAccess.CreateProductAsync(newProductDto.FromDto());
            if(id == -1) { return 500; }
            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDto updatedProductDto)
        {
            return Ok(await _productDataAccess.UpdateProductAsync(updatedProductDto.FromDto()));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           return Ok( await _productDataAccess.DeleteProductAsync(id));    
        }
        #endregion 
    }
}