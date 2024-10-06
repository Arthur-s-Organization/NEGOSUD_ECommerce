using API.Data;
using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;
using API.Services.IServices;
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
		public async Task<SupplierOrderResponseDTO> AddSupplierOrderAsync(SupplierOrderRequestDTO supplierOrderRequestDTO)
		{
			var supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == supplierOrderRequestDTO.SupplierId);

			if (supplier == null)
			{
				throw new InvalidOperationException($"Unable to add : supplier '{supplierOrderRequestDTO.SupplierId}' doesn't exists");
			}

			var supplierOrder = _mapper.Map<SupplierOrder>(supplierOrderRequestDTO);
			await _context.SupplierOrders.AddAsync(supplierOrder);
			await _context.SaveChangesAsync();

			var supplierOrderResponseDTO = _mapper.Map<SupplierOrderResponseDTO>(supplierOrder);
			return supplierOrderResponseDTO;
		}

		public async Task<SupplierOrderResponseDTO> DeleteSupplierOrderAsync(Guid id)
		{
			var supplierOrder = await _context.SupplierOrders.SingleOrDefaultAsync(so => so.OrderID == id);
			if (supplierOrder is null)
			{
				throw new InvalidOperationException($"Unable to delete : supplier '{id}' doesn't exists");
			}
			_context.SupplierOrders.Remove(supplierOrder);
			await _context.SaveChangesAsync();

			var supplierOrderResponseDTO = _mapper.Map<SupplierOrderResponseDTO>(supplierOrder);
			return supplierOrderResponseDTO;
		}

		public async Task<IEnumerable<SupplierOrderResponseDTO>> GetAllSupplierOrdersAsync()
		{
			var supplierOrders = await _context.SupplierOrders
				.Include(so => so.OrderDetails)
				.ThenInclude(od => od.Item) // Inclut les items liés à chaque OrderDetail
				.Include(so => so.Supplier)
				.ToListAsync();

			var supplierOrderResponseDTOs = _mapper.Map<IEnumerable<SupplierOrderResponseDTO>>(supplierOrders);
			return supplierOrderResponseDTOs;
		}

		public async Task<SupplierOrderResponseDTO> GetSupplierOrderByIdAsync(Guid id)
		{
			var supplierOrder = await _context.SupplierOrders
				.Include(so => so.OrderDetails)
				.ThenInclude(od => od.Item) // Inclut les items liés à chaque OrderDetail
				.Include(so => so.Supplier)
				.SingleOrDefaultAsync(so => so.OrderID == id);
			if (supplierOrder is null)
			{
				throw new InvalidOperationException($"Unable to get : supplierOrder '{id}' doesn't exists");
			}

			var supplierOrderResponseDTO = _mapper.Map<SupplierOrderResponseDTO>(supplierOrder);
			return supplierOrderResponseDTO;
		}

		public async Task<SupplierOrderResponseDTO> UpdateSupplierOrderAsync(Guid id, SupplierOrderRequestDTO SupplierOrderRequestDTO)
		{
			var supplierOrder = await _context.SupplierOrders.FindAsync(id);

			if (supplierOrder == null)
			{
				throw new InvalidOperationException($"Unable to update : supplierOrder '{id}' doesn't exists");
			}

			_mapper.Map(SupplierOrderRequestDTO, supplierOrder);
			await _context.SaveChangesAsync();

			var supplierOrderResponseDTO = _mapper.Map<SupplierOrderResponseDTO>(supplierOrder);
			return supplierOrderResponseDTO;
		}

		public async Task<OrderDetail> AddItemToSupplierOrderAsync(Guid supplierOrderId, Guid itemId , int itemQuantity)
		{
			var supplierOrder = await _context.SupplierOrders.SingleOrDefaultAsync(so => so.OrderID == supplierOrderId);

			if (supplierOrder == null)
			{
				throw new InvalidOperationException($"Unable to add : supplierOrder '{supplierOrderId}' doesn't exists");
			}

			var item = await _context.Items.SingleOrDefaultAsync(i => i.ItemId == itemId);

			if (item == null)
			{
				throw new InvalidOperationException($"Unable to add : item '{itemId}' doesn't exists");
			}

			var existingOrderDetail = await _context.OrderDetails
				.SingleOrDefaultAsync(od => od.ItemId == itemId && od.OrderId == supplierOrderId);

			if (existingOrderDetail != null)
			{
				throw new InvalidOperationException($"Unable to add : this orderDetail already exists");
			}

			var orderDetail = new OrderDetail
			{
				OrderId = supplierOrderId,
				ItemId = itemId,
				Quantity = itemQuantity
			};

			await _context.OrderDetails.AddAsync(orderDetail);
			await _context.SaveChangesAsync();

			return orderDetail;
		}

	}
}
