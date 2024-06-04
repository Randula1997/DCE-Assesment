using Assignment.Controllers.DTOs;
using Assignment.Repositories;

public interface IGlobalSearchService
{
    Task<IEnumerable<SearchResult>> SearchAsync(string keyword);
}

public class GlobalSearchService : IGlobalSearchService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public GlobalSearchService(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<SearchResult>> SearchAsync(string keyword)
    {
        var results = new List<SearchResult>();

        var customers = await _customerRepository.SearchCustomerAsync(keyword);
        var products = await _productRepository.SearchProductsAsync(keyword);

        results.AddRange(customers.Select(c => new SearchResult
        {
            EntityType = "Customer",
            Id = c.UserId.ToString(),
            Name = c.FirstName,
        }));

        results.AddRange(products.Select(p => new SearchResult
        {
            EntityType = "Product",
            Id = p.ProductId.ToString(),
            Name = p.ProductName,
            Description = p.UnitPrice.ToString("C")
        }));

        //results.AddRange(orders.Select(o => new SearchResult
        //{
        //    EntityType = "Order",
        //    Id = o.OrderId.ToString(),
        //    Description = $"Order Date: {o.OrderedOn.ToString("d")}"
        //}));

        return results;
    }
}
