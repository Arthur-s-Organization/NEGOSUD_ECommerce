using API.Models;
using API.Models.DTOs;

namespace API.Services
{
	public interface IAddressService
	{

		public Task<Address> AddaddressAsync(AddressDTO AddressDTO);
		public Task<IEnumerable<Address>> GetAllAddresssAsync();
		public Task<Address> GetAddressByIdAsync(Guid id);
		public Task<Address> UpdateAddressAsync(Guid id, AddressDTO AddressDTO);
		public Task<Address> DeleteAddressAsync(Guid id);

	}
}

