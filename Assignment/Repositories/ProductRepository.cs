using Assignment.Models;
using System.Data.SqlClient;

namespace Assignment.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO Product (ProductId, ProductName, UnitPrice, SupplierId, CreatedOn, IsActive) VALUES (@ProductId, @ProductName, @UnitPrice, @SupplierId, @CreatedOn, @IsActive);";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", product.ProductId);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                    command.Parameters.AddWithValue("@SupplierId", product.SupplierId);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@IsActive", product.IsActive);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Product";
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = (Guid)reader["ProductId"],
                                ProductName = reader["ProductName"].ToString(),
                                UnitPrice = (decimal)reader["UnitPrice"],
                                SupplierId = (Guid)reader["SupplierId"],
                                CreatedOn = (DateTime)reader["CreatedOn"],
                                IsActive = (bool)reader["IsActive"]
                            });
                        }
                    }
                }
            }
            return products;
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword)
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Product WHERE ProductName LIKE @Keyword ";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = (Guid)reader["ProductId"],
                                ProductName = reader["ProductName"].ToString(),
                                UnitPrice = (decimal)reader["UnitPrice"],
                                SupplierId = (Guid)reader["SupplierId"],
                                CreatedOn = (DateTime)reader["CreatedOn"],
                                IsActive = (bool)reader["IsActive"],
                            });
                        }
                    }
                }
                }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Product WHERE ProductId = @ProductId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                ProductId = (Guid)reader["ProductId"],
                                ProductName = reader["ProductName"].ToString(),
                                UnitPrice = (decimal)reader["UnitPrice"],
                                SupplierId = (Guid)reader["SupplierId"],
                                CreatedOn = (DateTime)reader["CreatedOn"],
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task UpdateProductAsync(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE Product SET ProductName = @ProductName, UnitPrice = @UnitPrice, IsActive = @IsActive WHERE ProductId = @ProductId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", product.ProductId);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                    command.Parameters.AddWithValue("@IsActive", product.IsActive);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "DELETE FROM Product WHERE ProductId = @ProductId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
