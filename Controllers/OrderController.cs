using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create_order")]
        public async Task<IActionResult>? CreateOrder([FromForm] CreateOrderDto newOrder)
        {
            return Ok();
        }
    }
}
