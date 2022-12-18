using AutoMapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.ActionFilters;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
    #region Properties and Constructor
    IOrderDataAccess _orderDataAccess;
    private readonly IMapper _mapper;

    public OrdersController(IOrderDataAccess orderDataAccess, IMapper mapper)
    {
        _orderDataAccess = orderDataAccess;
        _mapper = mapper;
    }
        #endregion
        #region Default Crud Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            IEnumerable<Order> orders = await _orderDataAccess.GetAllAsync();
            if(orders == null) { return NotFound(); }
            IEnumerable<OrderDto> ordersDTO = orders.Select(order => _mapper.Map<OrderDto>(order));
            return Ok(ordersDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            Order order = await _orderDataAccess.GetByIdAsync(id);
            if (order == null) { return NotFound(); }
            OrderDto orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _orderDataAccess.DeleteAsync(id)) { return Ok(); }
            return NotFound();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<ActionResult<int>> Post([FromBody] OrderDto newOrderDto)
        {
            int id = await _orderDataAccess.CreateAsync(_mapper.Map<Order>(newOrderDto));
            if (id <= 0) { return BadRequest(); }
            return Ok(id);
        }


        #endregion
    }
}

