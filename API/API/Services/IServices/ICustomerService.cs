using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface ICustomerService
    {
        public Task<CustomerResponseDTO> AddCustomerAsync(CustomerRequestDTO customerRequestDTO);
        public Task<IEnumerable<CustomerResponseDTO>> GetAllCustomersAsync();
        public Task<CustomerResponseDTO> GetCustomerByIdAsync(Guid id);
        public Task<CustomerResponseDTO> UpdateCustomerAsync(Guid id, CustomerRequestDTO customerRequestDTO);
        public Task<CustomerResponseDTO> DeleteCustomerAsync(Guid id);
    }
}
