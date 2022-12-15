using System.Collections.ObjectModel;
using AutoMapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
    #region Properties and Constructor
    IOrderDataAccess _orderDataAccess;
    IProductDataAccess _productDataAccess;
    private readonly IMapper _mapper;

    public OrdersController(IOrderDataAccess orderDataAccess, IMapper mapper)
    {
        _orderDataAccess = orderDataAccess;
        _mapper = mapper;
    }
        #endregion
        #region Default Crud Actions
        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            IEnumerable<Order> orders = await _orderDataAccess.GetAllAsync();
            IEnumerable<OrderDto> ordersDTO = orders.Select(order => _mapper.Map<OrderDto>(order));
            return Ok(ordersDTO);
        }

        // GET api/<OrderController>/1
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            Order order = await _orderDataAccess.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            OrderDto orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        // DELETE api/<OrderController>/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _orderDataAccess.DeleteOrderAsync(id))
                return Ok(); 
            else 
                return NotFound();
        }

         //POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] OrderDto newOrderDto)
        {
            int id = await _orderDataAccess.CreateOrderAsync(_mapper.Map<Order>(newOrderDto));
            return Ok(id);
        }

        // PUT api/<OrderController>/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] OrderDto updatedOrderDto)
        {
            if (await _orderDataAccess.UpdateOrderAsync(_mapper.Map<Order>(updatedOrderDto)))
                return Ok();
            else
                return NotFound();
        }
        #endregion
    }
}

