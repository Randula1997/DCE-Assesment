using Assignment.Controllers.DTOs;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Guid supplierId, [FromBody] CreateProductDTO createProductDTO)
        {
            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = createProductDTO.ProductName,
                UnitPrice = createProductDTO.UnitPrice,
                SupplierId = supplierId,
                CreatedOn = DateTime.UtcNow,
                IsActive = createProductDTO.IsActive,
            };

            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] CreateProductDTO updateeProductDTO)
        {
            var existingProduct = await _productService.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {productId} not found.");
            }
            existingProduct.ProductName = updateeProductDTO.ProductName;
            existingProduct.IsActive = updateeProductDTO.IsActive;
            existingProduct.UnitPrice = updateeProductDTO.UnitPrice;
            await _productService.UpdateProductAsync(existingProduct);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await _productService.DeleteProductAsync(productId);
            return NoContent();
        }
    }
}
