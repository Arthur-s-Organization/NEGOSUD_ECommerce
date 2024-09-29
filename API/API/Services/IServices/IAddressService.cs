using API.Models;
using API.Models.DTOs.RequestDTOs;

namespace API.Services.IServices
{
    public interface IAddressService
    {

        public Task<Address> AddAddressAsync(AddressRequestDTO AddressDTO);
        public Task<IEnumerable<Address>> GetAllAddresssAsync();
        public Task<Address> GetAddressByIdAsync(Guid id);
        public Task<Address> UpdateAddressAsync(Guid id, AddressRequestDTO AddressDTO);
        public Task<Address> DeleteAddressAsync(Guid id);

    }
}

