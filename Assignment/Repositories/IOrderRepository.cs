using Assignment.Models;

namespace Assignment.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid orderId);
        Task<IEnumerable<Order>> GetActiveOrdersByCustomerIdAsync(Guid customerId);
    }
}
