using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment.Models;
using Assignment.Repositories;

namespace Assignment.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<Customer> CreateCustomerAsync(Customer customer)
        {
            return _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            var customers = await GetAllCustomersAsync();
            return customers.FirstOrDefault(c => c.UserId == customerId);
        }
        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return _customerRepository.GetAllCustomersAsync();
        }

        public Task UpdateCustomerAsync(Customer customer)
        {
            return _customerRepository.UpdateCustomerAsync(customer);
        }

        public Task DeleteCustomerAsync(Guid customerId)
        {
            return _customerRepository.DeleteCustomerAsync(customerId);
        }

        public async Task<bool> CustomerExists(string email, string username)
        {
            return await _customerRepository.CustomerExists(email, username);
        }
    }
}
