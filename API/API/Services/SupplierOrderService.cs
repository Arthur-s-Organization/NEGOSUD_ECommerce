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
				.Include(so => so.Supplier)
				.ToListAsync();
			return SupplierOrders;
		}

		public async Task<SupplierOrder> GetSupplierOrderByIdAsync(Guid id)
		{
			var SupplierOrder = await _context.SupplierOrders
				.Include(so => so.OrderDetails)
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
	}
}
