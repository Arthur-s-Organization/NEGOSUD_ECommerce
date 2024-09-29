using API.Models;
using API.Models.DTOs.RequestDTOs;

namespace API.Services.IServices
{
    public interface ISupplierOrderService
    {
        public Task<SupplierOrder> AddSupplierOrderAsync(SupplierOrderRequestDTO SupplierOrderDTO);
        public Task<IEnumerable<SupplierOrder>> GetAllSupplierOrdersAsync();
        public Task<SupplierOrder> GetSupplierOrderByIdAsync(Guid id);
        public Task<SupplierOrder> UpdateSupplierOrderAsync(Guid id, SupplierOrderRequestDTO SupplierOrderDTO);
        public Task<SupplierOrder> DeleteSupplierOrderAsync(Guid id);
        public Task<OrderDetail> AddItemToSupplierOrderAsync(Guid supplierOrderId, Guid itemId, int itemQuantity);
    }
}
