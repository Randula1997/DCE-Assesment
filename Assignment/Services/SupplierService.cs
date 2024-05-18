using Assignment.Models;
using Assignment.Repositories;

namespace Assignment.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            return _supplierRepository.CreateSupplierAsync(supplier);
        }

        public Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return _supplierRepository.GetAllSuppliersAsync();
        }

        public Task<Supplier> GetSupplierByIdAsync(Guid supplierId)
        {
            return _supplierRepository.GetSupplierByIdAsync(supplierId);
        }

        public Task UpdateSupplierAsync(Supplier supplier)
        {
            return _supplierRepository.UpdateSupplierAsync(supplier);
        }

        public Task DeleteSupplierAsync(Guid supplierId)
        {
            return _supplierRepository.DeleteSupplierAsync(supplierId);
        }
    }
}
