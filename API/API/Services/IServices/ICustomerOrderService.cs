using API.Models;
using API.Models.DTOs.RequestDTOs;

namespace API.Services.IServices
{
    public interface ICustomerOrderService
    {
        public Task<CustomerOrder> AddCustomerOrderAsync(CustomerOrderRequestDTO CustomerOrderDTO);
        public Task<IEnumerable<CustomerOrder>> GetAllCustomerOrdersAsync();
        public Task<CustomerOrder> GetCustomerOrderByIdAsync(Guid id);
        public Task<CustomerOrder> UpdateCustomerOrderAsync(Guid id, CustomerOrderRequestDTO CustomerOrderDTO);
        public Task<CustomerOrder> DeleteCustomerOrderAsync(Guid id);
        public Task<OrderDetail> AddItemToCustomerOrderAsync(Guid customerOrderId, Guid itemId, int itemQuantity);
    }
}
