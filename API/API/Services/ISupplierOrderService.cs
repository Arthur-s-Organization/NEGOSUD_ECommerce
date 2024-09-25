using API.Models.DTOs;
using API.Models;

namespace API.Services
{
	public interface ISupplierOrderService
	{
		public Task<SupplierOrder> AddSupplierOrderAsync(SupplierOrderDTO SupplierOrderDTO);
		public Task<IEnumerable<SupplierOrder>> GetAllSupplierOrdersAsync();
		public Task<SupplierOrder> GetSupplierOrderByIdAsync(Guid id);
		public Task<SupplierOrder> UpdateSupplierOrderAsync(Guid id, SupplierOrderDTO SupplierOrderDTO);
		public Task<SupplierOrder> DeleteSupplierOrderAsync(Guid id);
	}
}
