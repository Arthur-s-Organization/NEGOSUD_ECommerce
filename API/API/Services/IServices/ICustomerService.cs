using API.Models;
using API.Models.DTOs.RequestDTOs;

namespace API.Services.IServices
{
    public interface ICustomerService
    {
        public Task<Customer> AddCustomerAsync(CustomerRequestDTO customerDTO);
        public Task<IEnumerable<Customer>> GetAllCustomersAsync();
        public Task<Customer> GetCustomerByIdAsync(Guid id);
        public Task<Customer> UpdateCustomerAsync(Guid id, CustomerRequestDTO customerDTO);
        public Task<Customer> DeleteCustomerAsync(Guid id);
        public Task<Customer> AddAdressToCustomerAsync(Guid CustomerId, Guid AdressId);
    }
}
