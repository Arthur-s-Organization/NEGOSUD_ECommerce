using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;

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

		public async Task<CustomerResponseDTO> AddCustomerAsync(CustomerRequestDTO customerRequestDTO)
		{
			var customer = _mapper.Map<Customer>(customerRequestDTO);
			await _context.Customers.AddAsync(customer);
			await _context.SaveChangesAsync();

			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}

		public async Task<CustomerResponseDTO> DeleteCustomerAsync(Guid id)
		{
			var customer = await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);
			if (customer is null)
			{
				throw new InvalidOperationException($"Unable to delete : customer '{id}' doesn't exists");
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

		public async Task<CustomerResponseDTO> GetCustomerByIdAsync(Guid id)
		{
			var customer = await _context.Customers.Include(c => c.Address).SingleOrDefaultAsync(c => c.CustomerId == id);
			if (customer is null)

			{
				throw new InvalidOperationException($"Unable to get : customer '{id}' doesn't exists");
			}
			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}

		public async Task<CustomerResponseDTO> UpdateCustomerAsync(Guid id, CustomerRequestDTO customerRequestDTO)
		{

			var customer = await _context.Customers.FindAsync(id);
			if (customer == null)
			{
				throw new InvalidOperationException($"Unable to modify : customer '{id}' doesn't exists");
			}

			_mapper.Map(customerRequestDTO, customer);

			await _context.SaveChangesAsync();
			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}

		public async Task<CustomerResponseDTO> AddAdressToCustomerAsync(Guid customerId, Guid adressId)
		{
			var customer = await _context.Customers
				.Include(c => c.Address)
				.SingleOrDefaultAsync(c => c.CustomerId == customerId);

			var adress = await _context.Addresses
				.Include(a => a.Supplier)
				.Include(a => a.Customer)
				.SingleOrDefaultAsync(a => a.AddressId == adressId);


			if (customer == null)
			{
				throw new InvalidOperationException($"Unable to add adress : customer '{customerId}' doesn't exists");
			}

			if (adress == null)
			{
				throw new InvalidOperationException($"Unable to add adress : adress '{adressId}' doesn't exists");
			}

			if (customer.Address != null)
			{
				throw new InvalidOperationException($"Unable to add adress : customer '{customerId}' already has an adress");
			}

			if (adress.Supplier != null)
			{
				throw new InvalidOperationException($"Unable to add adress :  adress '{adressId}' already own to a supplier");
			}

			if (adress.Customer != null)
			{
				throw new InvalidOperationException($"Unable to add adress :  adress '{adressId}' already own to an other customer");
			}

			customer.Address = adress;
			await _context.SaveChangesAsync();

			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}
	}

}