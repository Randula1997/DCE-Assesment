using Assignment.Controllers.DTOs;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO createCustomerDTO)
        {

            if (await _customerService.CustomerExists(createCustomerDTO.Email, createCustomerDTO.Username))
            {
                return BadRequest("A customer with the same email or username already exists.");
            }

            var customer = new Customer
            {
                UserId = Guid.NewGuid(),
                Username = createCustomerDTO.Username,
                Email = createCustomerDTO.Email,
                FirstName = createCustomerDTO.FirstName,
                LastName = createCustomerDTO.LastName,
                CreatedOn = DateTime.UtcNow,
                IsActive = createCustomerDTO.IsActive
            };

            var createdCustomer = await _customerService.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { customerId = createdCustomer.UserId }, createdCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(Guid customerId, [FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            var existingCustomer = await _customerService.GetCustomerAsync(customerId);
            if (existingCustomer == null)
            {
                return NotFound($"Customer with ID {customerId} not found.");
            }

            existingCustomer.Username = updateCustomerDTO.Username;
            existingCustomer.Email = updateCustomerDTO.Email;
            existingCustomer.FirstName = updateCustomerDTO.FirstName;
            existingCustomer.LastName = updateCustomerDTO.LastName;
            existingCustomer.IsActive = updateCustomerDTO.IsActive;

            // Call service to update the customer
            await _customerService.UpdateCustomerAsync(existingCustomer);

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(Guid customerId)
        {
            await _customerService.DeleteCustomerAsync(customerId);
            return NoContent();
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer(Guid customerId)
        {
            var customer = await _customerService.GetCustomerAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
