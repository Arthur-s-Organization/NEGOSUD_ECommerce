using API.Data;
using API.Models;
using API.Models.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
	public class SupplierOrderService : ISupplierOrderService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public SupplierOrderService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<SupplierOrder> AddSupplierOrderAsync(SupplierOrderDTO SupplierOrderDTO)
		{
			var existingSupplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == SupplierOrderDTO.SupplierId);

			if (existingSupplier == null)
			{
				return null;
			}

			var SupplierOrder = _mapper.Map<SupplierOrder>(SupplierOrderDTO);
			await _context.SupplierOrders.AddAsync(SupplierOrder);
			await _context.SaveChangesAsync();

			return SupplierOrder;
		}

		public async Task<SupplierOrder> DeleteSupplierOrderAsync(Guid id)
		{
			var SupplierOrder = await _context.SupplierOrders.SingleOrDefaultAsync(so => so.OrderID == id);
			if (SupplierOrder is null)
			{
				return null;
			}
			_context.SupplierOrders.Remove(SupplierOrder);
			await _context.SaveChangesAsync();
			return SupplierOrder;
		}

		public async Task<IEnumerable<SupplierOrder>> GetAllSupplierOrdersAsync()
		{
			var SupplierOrders = await _context.SupplierOrders
				.Include(so => so.OrderDetails)
				.ThenInclude(od => od.Item) // Inclut les items liés à chaque OrderDetail
				.Include(so => so.Supplier)
				.ToListAsync();
			return SupplierOrders;
		}

		public async Task<SupplierOrder> GetSupplierOrderByIdAsync(Guid id)
		{
			var SupplierOrder = await _context.SupplierOrders
				.Include(so => so.OrderDetails)
				.ThenInclude(od => od.Item) // Inclut les items liés à chaque OrderDetail
				.Include(so => so.Supplier)
				.SingleOrDefaultAsync(so => so.OrderID == id);
			if (SupplierOrder is null)
			{
				return null;
			}
			return SupplierOrder;
		}

		public async Task<SupplierOrder> UpdateSupplierOrderAsync(Guid id, SupplierOrderDTO SupplierOrderDTO)
		{
			var existingSupplierOrder = await _context.SupplierOrders.FindAsync(id);

			if (existingSupplierOrder == null)
			{
				return null;
			}

			_mapper.Map(SupplierOrderDTO, existingSupplierOrder);

			await _context.SaveChangesAsync();
			return existingSupplierOrder;
		}

		public async Task<OrderDetail> AddItemToSupplierOrderAsync(Guid supplierOrderId, Guid itemId)
		{
			var supplierOrder = await _context.SupplierOrders.SingleOrDefaultAsync(so => so.OrderID == supplierOrderId);

			if (supplierOrder == null)
			{
				return null;
			}

			var item = await _context.Items.SingleOrDefaultAsync(i => i.ItemId == itemId);

			if (item == null)
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
