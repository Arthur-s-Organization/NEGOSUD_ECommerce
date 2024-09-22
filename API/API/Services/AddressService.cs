using API.Data;
using API.Models;
using API.Models.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
		public async Task<Address> AddAddressAsync(AddressDTO AddressDTO)
		{
			var Address = _mapper.Map<Address>(AddressDTO);
			await _context.Addresses.AddAsync(Address);
			await _context.SaveChangesAsync();
			return Address;
		}

		public async Task<Address> DeleteAddressAsync(Guid id)
		{
			var Address = await _context.Addresses.SingleOrDefaultAsync(a => a.AddressId == id);
			if (Address is null)
			{
				return null;
			}
			_context.Addresses.Remove(Address);
			await _context.SaveChangesAsync();
			return Address;
		}

		public async Task<Address> GetAddressByIdAsync(Guid id)
		{
			var Address = await _context.Addresses.Include(a => a.Customer).Include(a => a.Supplier).SingleOrDefaultAsync(a => a.AddressId == id);
			if (Address is null)
			{
				return null;
			}
			return Address;
		}

		public async Task<IEnumerable<Address>> GetAllAddresssAsync()
		{
			var Addresss = await _context.Addresses.Include(a => a.Customer).Include(a => a.Supplier).ToListAsync();
			return Addresss;
		}

		public async Task<Address> UpdateAddressAsync(Guid id, AddressDTO AddressDTO)
		{
			var existingAddress = await _context.Addresses.FindAsync(id);
			if (existingAddress == null)
			{
				return null;
			}

			_mapper.Map(AddressDTO, existingAddress);

			await _context.SaveChangesAsync();
			return existingAddress;
		}
	}
}

