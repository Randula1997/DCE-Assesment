using Assignment.Models;

namespace Assignment.Services
{
    public interface IGlobalSearchService
    {
            Task<GlobalSearchService> SearchResultsAsync(string query);
    }

    public class GlobalSearchResult
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Product> Products { get; set; }

    }
}
