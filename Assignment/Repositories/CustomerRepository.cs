using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Assignment.Models;

namespace Assignment.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO Customer (UserId, Username, Email, FirstName, LastName, CreatedOn, IsActive) VALUES (@UserId, @Username, @Email, @FirstName, @LastName, @CreatedOn, @IsActive);";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Guid.NewGuid());
                    command.Parameters.AddWithValue("@Username", customer.Username);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@IsActive", customer.IsActive);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
            return customer; // Assuming UserId is set by the application before saving.
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = new List<Customer>();

            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Customer";
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                UserId = (Guid)reader["UserId"],
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                CreatedOn = (DateTime)reader["CreatedOn"],
                                IsActive = (bool)reader["IsActive"]
                            });
                        }
                    }
                }
            }

            return customers;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE Customer SET Username = @Username, Email = @Email, FirstName = @FirstName, LastName = @LastName, IsActive = @IsActive WHERE UserId = @UserId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", customer.UserId);
                    command.Parameters.AddWithValue("@Username", customer.Username);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@IsActive", customer.IsActive);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCustomerAsync(Guid customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var deleteOrdersSql = "DELETE FROM [Order] WHERE OrderBy = @UserId";
                using (var orderCommand = new SqlCommand(deleteOrdersSql, connection))
                {
                    orderCommand.Parameters.AddWithValue("@UserId", customerId);
                    await orderCommand.ExecuteNonQueryAsync();
                }

                const string sql = "DELETE FROM Customer WHERE UserId = @UserId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", customerId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> CustomerExists(string email, string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT COUNT(1) FROM Customer WHERE Email = @Email OR Username = @Username";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    var count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }
    }
}
