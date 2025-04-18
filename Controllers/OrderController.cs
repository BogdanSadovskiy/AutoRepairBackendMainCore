using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private ITokenValidationService _tokenValidationService;

        public OrderController(IOrderService orderService, ITokenValidationService tokenValidationService)
        {
            _orderService = orderService;
            _tokenValidationService = tokenValidationService;
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPost("create_order")]
        public async Task<IActionResult>? CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            string token = Request.Headers["Authorization"].ToString();
            int userId = _tokenValidationService.GetAutoServiceIdFromToken(token);
            Order newOrder = _orderService.CreateOrder(userId, orderDto);
            return Ok(newOrder);
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetTenOrders([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            string token = Request.Headers["Authorization"].ToString();
            int userId = _tokenValidationService.GetAutoServiceIdFromToken(token);
            List<OrderDto> orders = await _orderService.GetTenOrdersAsync(userId, skip, take);
            return Ok(orders);
        }
    }
}
