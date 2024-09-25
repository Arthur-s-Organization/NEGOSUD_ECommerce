using API.Data;
using API.Models;
using API.Models.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
	public class CustomerOrderService : ICustomerOrderService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public CustomerOrderService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<CustomerOrder> AddCustomerOrderAsync(CustomerOrderDTO CustomerOrderDTO)
		{
			var existingCustomer = _context.Customers.SingleOrDefault(c => c.CustomerId == CustomerOrderDTO.CustomerId);

			if (existingCustomer == null)
			{
				return null;
			}

			var CustomerOrder = _mapper.Map<CustomerOrder>(CustomerOrderDTO);
			await _context.CustomerOrders.AddAsync(CustomerOrder);
			await _context.SaveChangesAsync();

			return CustomerOrder;
		}

		public async Task<CustomerOrder> DeleteCustomerOrderAsync(Guid id)
		{
			var CustomerOrder = await _context.CustomerOrders.SingleOrDefaultAsync(co => co.OrderID == id);
			if (CustomerOrder is null)
			{
				return null;
			}
			_context.CustomerOrders.Remove(CustomerOrder);
			await _context.SaveChangesAsync();
			return CustomerOrder;
		}

		public async Task<IEnumerable<CustomerOrder>> GetAllCustomerOrdersAsync()
		{
			var CustomerOrders = await _context.CustomerOrders
				.Include(co => co.OrderDetails)
				.ToListAsync();
			return CustomerOrders;
		}

		public async Task<CustomerOrder> GetCustomerOrderByIdAsync(Guid id)
		{
			var CustomerOrder = await _context.CustomerOrders
				.Include(co => co.OrderDetails)
				.SingleOrDefaultAsync(co => co.OrderID == id);
			if (CustomerOrder is null)
			{
				return null;
			}
			return CustomerOrder;
		}

		public async Task<CustomerOrder> UpdateCustomerOrderAsync(Guid id, CustomerOrderDTO CustomerOrderDTO)
		{
			var existingCustomerOrder = await _context.CustomerOrders.FindAsync(id);

			if (existingCustomerOrder == null)
			{
				return null;
			}

			_mapper.Map(CustomerOrderDTO, existingCustomerOrder);

			await _context.SaveChangesAsync();
			return existingCustomerOrder;
		}
	}
}
