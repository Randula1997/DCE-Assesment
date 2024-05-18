using Assignment.Models;

namespace Assignment.Services
{
    public interface ISupplierService
    {
        Task<Supplier> CreateSupplierAsync(Supplier supplier);
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier> GetSupplierByIdAsync(Guid supplierId);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Guid supplierId);
    }
}
