using Assignment.Models;
using System.Data.SqlClient;

namespace Assignment.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly string _connectionString;

        public SupplierRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO Supplier (SupplierId, SupplierName, CreatedOn, IsActive) VALUES (@SupplierId, @SupplierName, @CreatedOn, @IsActive);";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
                    command.Parameters.AddWithValue("@SupplierName", supplier.SupplierName);
                    command.Parameters.AddWithValue("@CreatedOn", supplier.CreatedOn);
                    command.Parameters.AddWithValue("@IsActive", supplier.IsActive);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            var suppliers = new List<Supplier>();
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Supplier";
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new Supplier
                            {
                                SupplierId = (Guid)reader["SupplierId"],
                                SupplierName = reader["SupplierName"].ToString(),
                                CreatedOn = (DateTime)reader["CreatedOn"],
                                IsActive = (bool)reader["IsActive"]
                            });
                        }
                    }
                }
            }
            return suppliers;
        }

        public async Task<Supplier> GetSupplierByIdAsync(Guid supplierId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Supplier WHERE SupplierId = @SupplierId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SupplierId", supplierId);

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Supplier
                            {
                                SupplierId = (Guid)reader["SupplierId"],
                                SupplierName = reader["SupplierName"].ToString(),
                                CreatedOn = (DateTime)reader["CreatedOn"],
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE Supplier SET SupplierName = @SupplierName, IsActive = @IsActive WHERE SupplierId = @SupplierId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
                    command.Parameters.AddWithValue("@SupplierName", supplier.SupplierName);
                    command.Parameters.AddWithValue("@IsActive", supplier.IsActive);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteSupplierAsync(Guid supplierId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "DELETE FROM Supplier WHERE SupplierId = @SupplierId";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SupplierId", supplierId);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
