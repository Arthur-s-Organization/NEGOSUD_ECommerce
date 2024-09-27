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
				.Include(co => co.Customer)
				.ToListAsync();
			return CustomerOrders;
		}

		public async Task<CustomerOrder> GetCustomerOrderByIdAsync(Guid id)
		{
			var CustomerOrder = await _context.CustomerOrders
				.Include(co => co.OrderDetails)
				.Include(co => co.Customer)
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

		public async Task<OrderDetail> AddItemToCustomerOrderAsync(Guid supplierOrderId, Guid itemId)
		{
			var supplierOrder = await _context.SupplierOrders.SingleOrDefaultAsync(so => so.OrderID == supplierOrderId);

			if (supplierOrderId == null)
			{
				return null;
			}

			var item = await _context.Items.SingleOrDefaultAsync(i => i.ItemId == itemId);

			if (itemId == null)
			{
				return null;
			}

			var existingOrderDetail = await _context.OrderDetails
				.SingleOrDefaultAsync(od => od.ItemId == itemId && od.OrderId == supplierOrderId);

			if (existingOrderDetail != null)
			{
				return null;
			}

			var orderDetail = new OrderDetail
			{
				OrderId = supplierOrderId,
				ItemId = itemId,
				Quantity = 3
			};

			await _context.OrderDetails.AddAsync(orderDetail);
			await _context.SaveChangesAsync();

			return orderDetail;


		}
	}
}
