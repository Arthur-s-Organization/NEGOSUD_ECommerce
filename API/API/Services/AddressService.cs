using API.Data;
using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;
using API.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Services
{
    public class AddressService : IAddressService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public AddressService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<AddressResponseDTO> AddAddressAsync(AddressRequestDTO addressRequestDTO)
		{
			var address = _mapper.Map<Address>(addressRequestDTO);
			await _context.Addresses.AddAsync(address);
			await _context.SaveChangesAsync();

			var addressResponseDTO = _mapper.Map<AddressResponseDTO>(address);

			return addressResponseDTO;
		}

		public async Task<AddressResponseDTO> DeleteAddressAsync(Guid id)
		{
			var address = await _context.Addresses.SingleOrDefaultAsync(a => a.AddressId == id);
			if (address is null)
			{
				throw new InvalidOperationException($"Unable to delete : Address '{id}' not found");
			}
			_context.Addresses.Remove(address);
			await _context.SaveChangesAsync();

			var addressResponseDTO = _mapper.Map<AddressResponseDTO>(address);

			return addressResponseDTO;
		}

		public async Task<AddressResponseDTO> GetAddressByIdAsync(Guid id)
		{
			var address = await _context.Addresses.SingleOrDefaultAsync(a => a.AddressId == id);
			if (address is null)
			{
				throw new InvalidOperationException($"Unable to get : Address '{id}' not found");
			}

			var addressResponseDTO = _mapper.Map<AddressResponseDTO>(address);

			return addressResponseDTO;
		}

		public async Task<IEnumerable<AddressResponseDTO>> GetAllAddresssAsync()
		{
			var addresss = await _context.Addresses.ToListAsync();

			var addressesResponseDTO = _mapper.Map<IEnumerable<AddressResponseDTO>>(addresss);

			return addressesResponseDTO;
		}

		public async Task<AddressResponseDTO> UpdateAddressAsync(Guid id, AddressRequestDTO addressRequestDTO)
		{
			var address = await _context.Addresses.FindAsync(id);
			if (address == null)
			{
				throw new InvalidOperationException($"Unable tu Update : Address '{id}' not found");
			}

			_mapper.Map(addressRequestDTO, address);
			await _context.SaveChangesAsync();

			var addressResponseDTO = _mapper.Map<AddressResponseDTO>(address);

			return addressResponseDTO;
		}
	}
}

