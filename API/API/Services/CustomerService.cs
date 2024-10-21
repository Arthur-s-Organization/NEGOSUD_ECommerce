using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;
using API.Utils;

namespace API.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public CustomerService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		

		public async Task<CustomerResponseDTO> DeleteCustomerAsync(string id)
		{
			var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
			if (customer is null)
			{
				throw new ValidationException($"Unable to delete : customer '{id}' doesn't exists");
			}
			_context.Customers.Remove(customer);
			await _context.SaveChangesAsync();

			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}


		public async Task<IEnumerable<CustomerResponseDTO>> GetAllCustomersAsync()
		{
			var customers = await _context.Customers.Include(c => c.Address).ToListAsync();

			var custmersResponseDTO = _mapper.Map<IEnumerable<CustomerResponseDTO>>(customers);

			return custmersResponseDTO;
		}


		public async Task<CustomerResponseDTO> GetCustomerByIdAsync(string id)
		{
			var customer = await _context.Customers.Include(c => c.Address).SingleOrDefaultAsync(c => c.Id == id);
			if (customer is null)

			{
				throw new ValidationException($"Unable to get : customer '{id}' doesn't exists");
			}
			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}

	}

}