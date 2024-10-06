using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface ICustomerOrderService
    {
        public Task<CustomerOrderResponseDTO> AddCustomerOrderAsync(CustomerOrderRequestDTO CustomerOrderDTO);
        public Task<IEnumerable<CustomerOrderResponseDTO>> GetAllCustomerOrdersAsync();
        public Task<CustomerOrderResponseDTO> GetCustomerOrderByIdAsync(Guid id);
        public Task<CustomerOrderResponseDTO> UpdateCustomerOrderAsync(Guid id, CustomerOrderRequestDTO CustomerOrderDTO);
        public Task<CustomerOrderResponseDTO> DeleteCustomerOrderAsync(Guid id);
        public Task<OrderDetail> AddItemToCustomerOrderAsync(Guid customerOrderId, Guid itemId, int itemQuantity);
    }
}
