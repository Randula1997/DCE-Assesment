using Assignment.Models;

namespace Assignment.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid orderId);

        Task<IEnumerable<Order>> GetActiveOrdersByCustomerIdAsync(Guid customerId);
    }
}
