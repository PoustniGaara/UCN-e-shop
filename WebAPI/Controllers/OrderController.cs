using AutoMapper;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

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
    }
}
