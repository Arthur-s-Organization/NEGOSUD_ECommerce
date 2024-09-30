using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface IAddressService
    {

        public Task<AddressResponseDTO> AddAddressAsync(AddressRequestDTO AddressDTO);
        public Task<IEnumerable<AddressResponseDTO>> GetAllAddresssAsync();
        public Task<AddressResponseDTO> GetAddressByIdAsync(Guid id);
        public Task<AddressResponseDTO> UpdateAddressAsync(Guid id, AddressRequestDTO AddressDTO);
        public Task<AddressResponseDTO> DeleteAddressAsync(Guid id);

    }
}

