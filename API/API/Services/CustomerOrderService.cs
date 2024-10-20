using API.Data;
using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;
using API.Services.IServices;
using API.Utils;
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
		public async Task<CustomerOrderResponseDTO> AddCustomerOrderAsync(CustomerOrderRequestDTO customerOrderRequestDTO)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == customerOrderRequestDTO.CustomerId);

			if (customer == null)
			{
				throw new ValidationException($"Unable to add : customer '{customerOrderRequestDTO.CustomerId}' doesn't exists");
			}

			var customerOrder = _mapper.Map<CustomerOrder>(customerOrderRequestDTO);
			customerOrder.OrderDate = DateTime.Now;
			await _context.CustomerOrders.AddAsync(customerOrder);
			await _context.SaveChangesAsync();

			var customerOrderResponseDTO = _mapper.Map<CustomerOrderResponseDTO>(customerOrder);
			return customerOrderResponseDTO;
		}

		public async Task<CustomerOrderResponseDTO> DeleteCustomerOrderAsync(Guid id)
		{
			var customerOrder = await _context.CustomerOrders.SingleOrDefaultAsync(co => co.OrderID == id);
			if (customerOrder is null)
			{
				throw new ValidationException($"Unable to delete : customer '{id}' doesn't exists");
			}
			_context.CustomerOrders.Remove(customerOrder);
			await _context.SaveChangesAsync();

			var customerOrderResponseDTO = _mapper.Map<CustomerOrderResponseDTO>(customerOrder);
			return customerOrderResponseDTO;
		}

		public async Task<IEnumerable<CustomerOrderResponseDTO>> GetAllCustomerOrdersAsync()
		{
			//var CustomerOrders = await _context.CustomerOrders
			//	.Include(co => co.OrderDetails)
			//	.Include(co => co.Customer)
			//	.ToListAsync();


			var customerOrders = await _context.CustomerOrders
				.Include(co => co.OrderDetails)
				.ThenInclude(od => od.Item) // Inclut les items liés à chaque OrderDetail
				.Include(co => co.Customer)
				.ToListAsync();

			var customerOrderResponseDTOs = _mapper.Map<IEnumerable<CustomerOrderResponseDTO>>(customerOrders);

			return customerOrderResponseDTOs;
		}

		public async Task<CustomerOrderResponseDTO> GetCustomerOrderByIdAsync(Guid id)
		{
			var customerOrder = await _context.CustomerOrders
				.Include(co => co.OrderDetails)
				.ThenInclude(od => od.Item) // Inclut les items liés à chaque OrderDetail
				.Include(co => co.Customer)
				.SingleOrDefaultAsync(co => co.OrderID == id);
			if (customerOrder is null)
			{
				throw new ValidationException($"Unable to get : customerOrder '{id}' doesn't exists");
			}

			var customerOrderResponseDTO = _mapper.Map<CustomerOrderResponseDTO>(customerOrder);
			return customerOrderResponseDTO;
		}


		public async Task<IEnumerable<CustomerOrderResponseDTO>> GetCustomerOrdersByCustomerIdAsync(string customerId)
		{
			var customer = await _context.Customers.FindAsync(customerId);
			if (customer == null)
			{
				throw new ValidationException($"Unable to get : customer '{customerId}' doesn't exists");
			}

			var customerOrders = await _context.CustomerOrders
				.Where(co => co.CustomerId == customerId)
				.Include(co => co.OrderDetails)
				.ThenInclude(od => od.Item) // Inclut les items liés à chaque OrderDetail
				.Include(co => co.Customer)
				.ToListAsync();

			var customerOrderResponseDTOs = _mapper.Map<IEnumerable<CustomerOrderResponseDTO>>(customerOrders);
			return customerOrderResponseDTOs;
		}

		public async Task<CustomerOrderResponseDTO> UpdateCustomerOrderAsync(Guid id, CustomerOrderRequestDTO customerOrderRequestDTO)
		{
			var customerOrder = await _context.CustomerOrders
				.Include(co => co.OrderDetails)
					.ThenInclude(od => od.Item)
				.FirstOrDefaultAsync(co => co.OrderID == id);


			if (customerOrder == null)
			{
				throw new ValidationException($"Unable to update : customerOrder '{id}' doesn't exists");
			}

			// Cas ou la commande client passe en statut "payée" (donc validée)
			if (customerOrderRequestDTO.Status == "1")
			{
				foreach (var orderDetail in customerOrder.OrderDetails)
				{
					var item = await _context.Items.FindAsync(orderDetail.ItemId);
					// On repasse une commande fournisseur si la commande client fait baisser nos stocks en dessous de 10
					if (item != null && (item.Stock - orderDetail.Quantity < 10))
					{
						var newSupplierOrder = new SupplierOrder
						{
							Status = "0",
							SupplierId = item.SupplierId,
							OrderDate = DateTime.Now
						};

						await _context.SupplierOrders.AddAsync(newSupplierOrder);
						await _context.SaveChangesAsync();

						var newOrderDetail = new OrderDetail
						{
							OrderId = newSupplierOrder.OrderID,
							ItemId = item.ItemId,
							Quantity = orderDetail.Quantity + 20
						};

						await _context.OrderDetails.AddAsync(newOrderDetail);

					}
				}
				await _context.SaveChangesAsync();
			}


			// Cas ou la commande client passe en statut  "expédiée"
			if (customerOrderRequestDTO.Status == "3")
			{
				foreach (var orderDetail in customerOrder.OrderDetails)
				{
					var item = await _context.Items.FindAsync(orderDetail.ItemId);
					// On vérifie que l'on a assez de stock pour envoyer la commande
					if (orderDetail.Quantity > item.Stock)
					{
						throw new ValidationException($"Unable to update : There is not enough stock to ship the order");
					}
					// on incrémente la quantitySold de l'item
					item.QuantitySold += orderDetail.Quantity;
				}

				foreach (var orderDetail in customerOrder.OrderDetails)
				{
					var item = await _context.Items.FindAsync(orderDetail.ItemId);
					if (item != null)
					{
						item.Stock -= orderDetail.Quantity;
					}
				}
				await _context.SaveChangesAsync();
			}

			_mapper.Map(customerOrderRequestDTO, customerOrder);
			await _context.SaveChangesAsync();

			var customerOrderResponseDTO = _mapper.Map<CustomerOrderResponseDTO>(customerOrder);
			return customerOrderResponseDTO;
		}

		public async Task<OrderDetail> AddItemToCustomerOrderAsync(Guid customerOrderId, Guid itemId, int itemQuantity)
		{
			var customerOrder = await _context.CustomerOrders.SingleOrDefaultAsync(co => co.OrderID == customerOrderId);
			if (customerOrder == null)
			{
				throw new ValidationException($"Unable to add : customerOrder '{customerOrderId}' doesn't exists");
			}

			var item = await _context.Items.SingleOrDefaultAsync(i => i.ItemId == itemId);
			if (item == null)
			{
				throw new ValidationException($"Unable to add : item '{itemId}' doesn't exists");
			}

			var existingOrderDetail = await _context.OrderDetails
				.SingleOrDefaultAsync(od => od.ItemId == itemId && od.OrderId == customerOrderId);

			if (existingOrderDetail != null)
			{
				throw new ValidationException($"Unable to add : this orderDetail already exists");
			}

			var orderDetail = new OrderDetail
			{
				OrderId = customerOrderId,
				ItemId = itemId,
				Quantity = itemQuantity
			};

			await _context.OrderDetails.AddAsync(orderDetail);
			await _context.SaveChangesAsync();

			return orderDetail;
		}
	}
}
