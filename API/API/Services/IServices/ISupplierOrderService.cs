using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface ISupplierOrderService
    {
        public Task<SupplierOrderResponseDTO> AddSupplierOrderAsync(SupplierOrderRequestDTO supplierOrderRequestDTO);
        public Task<IEnumerable<SupplierOrderResponseDTO>> GetAllSupplierOrdersAsync();
        public Task<SupplierOrderResponseDTO> GetSupplierOrderByIdAsync(Guid id);
        public Task<SupplierOrderResponseDTO> UpdateSupplierOrderAsync(Guid id, SupplierOrderRequestDTO supplierOrderRequestDTO);
        public Task<SupplierOrderResponseDTO> DeleteSupplierOrderAsync(Guid id);
        public Task<OrderDetail> AddItemToSupplierOrderAsync(Guid supplierOrderId, Guid itemId, int itemQuantity);
        public Task<IEnumerable<SupplierOrderResponseDTO>> GetSupplierOrdersBySupplierIdAsync(Guid supplierId);

	}
}
