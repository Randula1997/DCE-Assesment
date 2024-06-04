using Assignment.Models;

namespace Assignment.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid productId);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid productId);
        Task<IEnumerable<Product>> SearchProductsAsync(string keyword);
    }
}
