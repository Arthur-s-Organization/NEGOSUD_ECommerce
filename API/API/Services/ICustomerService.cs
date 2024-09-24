using API.Models.DTOs;
using API.Models;

namespace API.Services
{
	public interface ICustomerService
	{
		public Task<Customer> AddCustomerAsync(CustomerDTO customerDTO);
		public Task<IEnumerable<Customer>> GetAllCustomersAsync();
		public Task<Customer> GetCustomerByIdAsync(Guid id);
		public Task<Customer> UpdateCustomerAsync(Guid id, CustomerDTO customerDTO);
		public Task<Customer> DeleteCustomerAsync(Guid id);
		public Task<Customer> AddAdressToCustomerAsync(Guid CustomerId, Guid AdressId);
	}
}
