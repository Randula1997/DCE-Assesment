using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment.Models;

namespace Assignment.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerAsync(Guid customerId);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
        Task<bool> CustomerExists(string email, string username);
    }
}
