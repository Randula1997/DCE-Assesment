using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment.Models;

namespace Assignment.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);

        Task<bool> CustomerExists(string email, string username);
    }
}
