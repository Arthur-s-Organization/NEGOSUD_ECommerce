using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;

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

		public async Task<Customer> AddCustomerAsync(CustomerRequestDTO customerDTO)
		{
			var Customer = _mapper.Map<Customer>(customerDTO);
			await _context.Customers.AddAsync(Customer);
			await _context.SaveChangesAsync();

			return Customer;
		}

		public async Task<Customer> DeleteCustomerAsync(Guid id)
		{
			var Customer = await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);
			if (Customer is null)
			{
				return null;
			}
			_context.Customers.Remove(Customer);
			await _context.SaveChangesAsync();
			return Customer;
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			var Customers = await _context.Customers.Include(c => c.CustomerOrders).Include(c => c.Address).ToListAsync();
			return Customers;
		}

		public async Task<Customer> GetCustomerByIdAsync(Guid id)
		{
			var Customer = await _context.Customers.Include(c => c.CustomerOrders).Include(c => c.Address).SingleOrDefaultAsync(c => c.CustomerId == id);
			if (Customer is null)
			{
				return null;
			}
			return Customer;
		}

		public async Task<Customer> UpdateCustomerAsync(Guid id, CustomerRequestDTO customerDTO)
		{

			var existingCustomer = await _context.Customers.FindAsync(id);
			if (existingCustomer == null)
			{
				return null;
			}

			_mapper.Map(customerDTO, existingCustomer);

			await _context.SaveChangesAsync();
			return existingCustomer;
		}

		public async Task<Customer> AddAdressToCustomerAsync(Guid CustomerId, Guid AdressId)
		{
			var Customer = await _context.Customers
				.Include(c => c.Address)
				.SingleOrDefaultAsync(c => c.CustomerId == CustomerId);

			var Adress = await _context.Addresses
				.Include(a => a.Supplier)
				.Include(a => a.Customer)
				.SingleOrDefaultAsync(a => a.AddressId == AdressId);

			if (Customer == null || Adress == null || Customer.Address != null || Adress.Customer != null || Adress.Supplier != null)
			{
				return null;  // Ou une autre gestion des erreurs, à voir
			}

			Customer.Address = Adress;
			await _context.SaveChangesAsync();

			return Customer;
		}
	}
}
