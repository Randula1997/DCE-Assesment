using Assignment.Models;
using System.Data.SqlClient;

namespace Assignment.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO [Order] (OrderId, ProductId, OrderStatus, OrderType, OrderBy, OrderedOn, ShippedOn, IsActive) VALUES (@OrderId, @ProductId, @OrderStatus, @OrderType, @OrderBy, @OrderedOn, @ShippedOn, @IsActive);";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@ProductId", order.ProductId);
                    command.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                    command.Parameters.AddWithValue("@OrderType", order.OrderType);
                    command.Parameters.AddWithValue("@OrderBy", order.OrderBy);
                    command.Parameters.AddWithValue("@OrderedOn", order.OrderedOn);
                    command.Parameters.AddWithValue("@ShippedOn", (object)order.ShippedOn ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", order.IsActive);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = new List<Order>();
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM [Order]";
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = (Guid)reader["OrderId"],
                                ProductId = (Guid)reader["ProductId"],
                                OrderStatus = (int)reader["OrderStatus"],
                                OrderType = (int)reader["OrderType"],
                                OrderBy = (Guid)reader["OrderBy"],
                                OrderedOn = (DateTime)reader["OrderedOn"],
                                ShippedOn = reader.IsDBNull(reader.GetOrdinal("ShippedOn")) ? (DateTime?)null : (DateTime)reader["ShippedOn"],
                                IsActive = (bool)reader["IsActive"]
                            });
                        }
                    }
                }
            }
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM [Order] WHERE OrderId = @OrderId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Order
                            {
                                OrderId = (Guid)reader["OrderId"],
                                ProductId = (Guid)reader["ProductId"],
                                OrderStatus = (int)reader["OrderStatus"],
                                OrderType = (int)reader["OrderType"],
                                OrderBy = (Guid)reader["OrderBy"],
                                OrderedOn = (DateTime)reader["OrderedOn"],
                                ShippedOn = reader.IsDBNull(reader.GetOrdinal("ShippedOn")) ? (DateTime?)null : (DateTime)reader["ShippedOn"],
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE [Order] SET ProductId = @ProductId, OrderStatus = @OrderStatus, OrderType = @OrderType, ShippedOn = @ShippedOn, IsActive = @IsActive WHERE OrderId = @OrderId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@ProductId", order.ProductId);
                    command.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                    command.Parameters.AddWithValue("@OrderType", order.OrderType);
                    command.Parameters.AddWithValue("@ShippedOn", (object)order.ShippedOn ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", order.IsActive);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "DELETE FROM [Order] WHERE OrderId = @OrderId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersByCustomerIdAsync(Guid customerId)
        {
            var orders = new List<Order>();
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = @"
            SELECT * FROM [Order] 
            WHERE OrderBy = @CustomerId AND IsActive = 1";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = (Guid)reader["OrderId"],
                                ProductId = (Guid)reader["ProductId"],
                                OrderStatus = (int)reader["OrderStatus"],
                                OrderType = (int)reader["OrderType"],
                                OrderBy = (Guid)reader["OrderBy"],
                                OrderedOn = (DateTime)reader["OrderedOn"],
                                ShippedOn = reader.IsDBNull(reader.GetOrdinal("ShippedOn")) ? (DateTime?)null : (DateTime)reader["ShippedOn"],
                                IsActive = (bool)reader["IsActive"]
                            });
                        }
                    }
                }
            }
            return orders;
        }
    }

}
