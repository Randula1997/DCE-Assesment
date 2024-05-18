using Assignment.Controllers.DTOs;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Guid productId, Guid customerId, [FromBody] CreateOrderDTO createOrderDTO)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                ProductId = productId,
                OrderStatus = createOrderDTO.OrderStatus,
                OrderType = createOrderDTO.OrderType,
                OrderBy = customerId,
                OrderedOn = DateTime.UtcNow,
                ShippedOn = DateTime.UtcNow.AddDays(10),
                IsActive = createOrderDTO.IsActive,
            };

            var createdOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { orderId = createdOrder.OrderId }, createdOrder);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] CreateOrderDTO updateOrderDTO)
        {
            var existingOrder = await _orderService.GetOrderByIdAsync(orderId);
            if (existingOrder == null)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }
            existingOrder.OrderStatus = updateOrderDTO.OrderStatus;
            existingOrder.OrderType = updateOrderDTO.OrderType;
            existingOrder.IsActive = updateOrderDTO.IsActive;
            await _orderService.UpdateOrderAsync(existingOrder);
            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            await _orderService.DeleteOrderAsync(orderId);
            return NoContent();
        }

        [HttpGet("active/{customerId}")]
        public async Task<IActionResult> GetActiveOrdersByCustomer(Guid customerId)
        {
            var orders = await _orderService.GetActiveOrdersByCustomerIdAsync(customerId);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No active orders found for customer with ID {customerId}.");
            }
            return Ok(orders);
        }
    }
}
