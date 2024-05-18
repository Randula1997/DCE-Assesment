using Assignment.Models;

namespace Assignment.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier> CreateSupplierAsync(Supplier supplier);
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier> GetSupplierByIdAsync(Guid supplierId);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Guid supplierId);

    }
}
