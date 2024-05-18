using Assignment.Models;
using Assignment.Repositories;

namespace Assignment.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> CreateProductAsync(Product product)
        {
            return _productRepository.CreateProductAsync(product);
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _productRepository.GetAllProductsAsync();
        }

        public Task<Product> GetProductByIdAsync(Guid productId)
        {
            return _productRepository.GetProductByIdAsync(productId);
        }

        public Task UpdateProductAsync(Product product)
        {
            return _productRepository.UpdateProductAsync(product);
        }

        public Task DeleteProductAsync(Guid productId)
        {
            return _productRepository.DeleteProductAsync(productId);
        }
    }
}
