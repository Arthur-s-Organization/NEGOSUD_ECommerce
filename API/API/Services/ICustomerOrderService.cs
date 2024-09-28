using API.Models;
using API.Models.DTOs;

namespace API.Services
{
	public interface ICustomerOrderService
	{
		public Task<CustomerOrder> AddCustomerOrderAsync(CustomerOrderDTO CustomerOrderDTO);
		public Task<IEnumerable<CustomerOrder>> GetAllCustomerOrdersAsync();
		public Task<CustomerOrder> GetCustomerOrderByIdAsync(Guid id);
		public Task<CustomerOrder> UpdateCustomerOrderAsync(Guid id, CustomerOrderDTO CustomerOrderDTO);
		public Task<CustomerOrder> DeleteCustomerOrderAsync(Guid id);
		public Task<OrderDetail> AddItemToCustomerOrderAsync(Guid customerOrderId, Guid itemId);
	}
}
