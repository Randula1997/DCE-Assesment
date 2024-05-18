using Assignment.Controllers.DTOs;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierDTO createSupplierDTO)
        {
            var supplier = new Supplier
            {
                SupplierId = Guid.NewGuid(),
                SupplierName = createSupplierDTO.SupplierName,
                CreatedOn = DateTime.UtcNow,
                IsActive = createSupplierDTO.IsActive
            };
            var createdSupplier = await _supplierService.CreateSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSupplier), new { id = createdSupplier.SupplierId }, createdSupplier);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSupplier(Guid supplierId)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(supplierId);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpPut("{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(Guid supplierId, [FromBody] CreateSupplierDTO updateSupplierDTO)
        {
            var existingSupplier = await _supplierService.GetSupplierByIdAsync(supplierId);
            if (existingSupplier == null)
            {
                return NotFound($"Supplier with ID {supplierId} not found.");
            }

            existingSupplier.SupplierName = updateSupplierDTO.SupplierName;
            existingSupplier.IsActive = updateSupplierDTO.IsActive;

            // Call service to update the customer
            await _supplierService.UpdateSupplierAsync(existingSupplier);

            return NoContent();
        }

        [HttpDelete("{supplierId}")]
        public async Task<IActionResult> DeleteSupplier(Guid supplierId)
        {
            await _supplierService.DeleteSupplierAsync(supplierId);
            return NoContent();
        }
    }
}
