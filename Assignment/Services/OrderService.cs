using Assignment.Models;
using Assignment.Repositories;

namespace Assignment.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            return _orderRepository.CreateOrderAsync(order);
        }

        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return _orderRepository.GetAllOrdersAsync();
        }

        public Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return _orderRepository.GetOrderByIdAsync(orderId);
        }

        public Task UpdateOrderAsync(Order order)
        {
            return _orderRepository.UpdateOrderAsync(order);
        }

        public Task DeleteOrderAsync(Guid orderId)
        {
            return _orderRepository.DeleteOrderAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersByCustomerIdAsync(Guid customerId)
        {
            return await _orderRepository.GetActiveOrdersByCustomerIdAsync(customerId);
        }
    }
}
