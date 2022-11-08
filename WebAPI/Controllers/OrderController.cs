using System.Collections.ObjectModel;
using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
    #region Properties and Constructor
    IOrderDataAccess _orderDataAccess;
    private readonly IMapper _mapper;

    public OrderController(IOrderDataAccess orderDataAccess, IMapper mapper)
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
            IEnumerable<Order>? orders = null;
            ICollection<OrderDto>? ordersDTO = new Collection<OrderDto>();

            if (!string.IsNullOrEmpty("")) 
            {
                orders = null;
            }
            else
            {
                orders = await _orderDataAccess.GetAllAsync();
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
            var order = await _orderDataAccess.GetOrderByIdAsync(id);
            OrderDto orderDto = _mapper.Map<OrderDto>(order);
            if (order == null) { return NotFound(); }
            else { return Ok(orderDto); }
        }

        // DELETE api/<OrderController>/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (_orderDataAccess.DeleteOrderAsync(id) == null)
            {
                return NotFound();
            }
            else return Ok(await _orderDataAccess.DeleteOrderAsync(id));
        }

        // POST api/<OrderController>
        //[HttpPost]
        //public async Task<ActionResult<int>> Post([FromBody] OrderDto newOrderDto)
        //{
            //int id = await _orderDataAccess.CreateOrderAsync(newOrderDto.);
           
        //}

        #endregion

    }
}

