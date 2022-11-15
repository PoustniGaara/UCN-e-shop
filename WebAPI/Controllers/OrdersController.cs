using System.Collections.ObjectModel;
using AutoMapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
    #region Properties and Constructor
    IOrderDataAccess _dataAccess;
    private readonly IMapper _mapper;

    public OrdersController(IOrderDataAccess orderDataAccess, IMapper mapper)
    {
        _dataAccess = orderDataAccess;
        _mapper = mapper;
    }
        #endregion
        #region Default Crud Actions
        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            IEnumerable<Order>? orders = null;
            ICollection<OrderDto>? ordersDTO = new Collection<OrderDto>();

            if (!string.IsNullOrEmpty("")) 
            {
                orders = null;
            }
            else
            {
                orders = await _dataAccess.GetAllAsync();
            }

            foreach (Order order in orders)
            {
                OrderDto orderDto = _mapper.Map<OrderDto>(order);
                ordersDTO.Add(orderDto);
            }
            return Ok(ordersDTO);
        }

        // GET api/<OrderController>/1
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var order = await _dataAccess.GetOrderByIdAsync(id);
            OrderDto orderDto = _mapper.Map<OrderDto>(order);
            if (order == null) { return NotFound(); }
            else { return Ok(orderDto); }
        }

        // DELETE api/<OrderController>/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var isdeleted = await _dataAccess.DeleteOrderAsync(id);
            if (isdeleted == false)
            {
                return NotFound();
            }
            else return Ok(isdeleted);
        }

         //POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] OrderDto newOrderDto)
        {
            int id = await _dataAccess.CreateOrderAsync(_mapper.Map<Order>(newOrderDto));
            return Created("api/orders/" + id, id);
        }

        // PUT api/<OrderController>/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrderDto updatedOrderDto)
        {
            return Ok(await _dataAccess.UpdateOrderAsync(_mapper.Map<Order>(updatedOrderDto)));
        }
        #endregion

    }
}

