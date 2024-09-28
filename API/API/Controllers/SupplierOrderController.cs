using API.Models.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierOrderController : ControllerBase
	{
		private readonly ISupplierOrderService _SupplierOrderService;

		public SupplierOrderController(ISupplierOrderService SupplierOrderService)
		{
			_SupplierOrderService = SupplierOrderService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<SupplierOrder>>> GetAllSupplierOrders()
		{
			var SupplierOrders = await _SupplierOrderService.GetAllSupplierOrdersAsync();

			return Ok(SupplierOrders);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<SupplierOrder>> GetSupplierOrderById(Guid id)
		{
			var SupplierOrder = await _SupplierOrderService.GetSupplierOrderByIdAsync(id);
			if (SupplierOrder == null)
			{
				return BadRequest("SupplierOrder not found");
			}
			return Ok(SupplierOrder);
		}

		[HttpPost]
		public async Task<ActionResult<SupplierOrder>> AddSupplierOrder(SupplierOrderDTO SupplierOrderDTO)
		{
			var createdSupplierOrder = await _SupplierOrderService.AddSupplierOrderAsync(SupplierOrderDTO);

			if (createdSupplierOrder == null)
			{
				return BadRequest("Supplier Id not found");
			}
			return Ok(createdSupplierOrder);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<SupplierOrder>> UpdateSupplierOrder(Guid id, SupplierOrderDTO SupplierOrderDTO)
		{
			var updatedSupplierOrder = await _SupplierOrderService.UpdateSupplierOrderAsync(id, SupplierOrderDTO);

			if (updatedSupplierOrder == null)
			{
				return BadRequest("SupplierOrder doesn't exists");
			}
			return Ok(updatedSupplierOrder);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<SupplierOrder>> DeleteSupplierOrder(Guid id)
		{
			var deletedSupplierOrder = await _SupplierOrderService.DeleteSupplierOrderAsync(id);
			if (deletedSupplierOrder == null)
			{
				return BadRequest($"Unable to delete SupplierOrder {id}");
			}
			return Ok(deletedSupplierOrder);
		}

		[HttpPost("{supplierOrderId}/Items/{itemId}/ItemQuantity{itemQuantity}")]
		public async Task<ActionResult<OrderDetail>> AddItemToSuppliererOrder(Guid supplierOrderId, Guid itemId, int itemQuantity)
		{
			var orderDetail = await _SupplierOrderService.AddItemToSupplierOrderAsync(supplierOrderId, itemId, itemQuantity);
			if (orderDetail == null)
			{
				return BadRequest($"Order {supplierOrderId} already contains item {itemId}");
			}
			return Ok(orderDetail);
		}
	}
}
