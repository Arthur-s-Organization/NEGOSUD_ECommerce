using API.Models.DTOs;
using API.Models;

namespace API.Services
{
	public interface ISupplierService
	{
		public Task<Supplier> AddSupplierAsync(SupplierDTO SupplierDTO);
		public Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
		public Task<Supplier> GetSupplierByIdAsync(Guid id);
		public Task<Supplier> UpdateSupplierAsync(Guid id, SupplierDTO SupplierDTO);
		public Task<Supplier> DeleteSupplierAsync(Guid id);
		public Task<Supplier> AddAdressToSupplierAsync(Guid SupplierId, Guid AdressId);
	}
}

