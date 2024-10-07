using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;
using API.Services;

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
		public async Task<ActionResult<IEnumerable<SupplierOrderResponseDTO>>> GetAllSupplierOrders()
		{
			var supplierOrdersReponseDTO = await _SupplierOrderService.GetAllSupplierOrdersAsync();
			return Ok(supplierOrdersReponseDTO);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<SupplierOrderResponseDTO>> GetSupplierOrderById(Guid id)
		{
			try
			{
				var supplierOrderReponseDTO = await _SupplierOrderService.GetSupplierOrderByIdAsync(id);
				return Ok(supplierOrderReponseDTO);

			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("Supplier/{supplierId}")]
		public async Task<ActionResult<IEnumerable<SupplierOrderResponseDTO>>> GetSupplierOrdersBySupplierId(Guid supplierId)
		{
			try
			{
				var supplierOrdersResponseDTOs = await _SupplierOrderService.GetSupplierOrdersBySupplierIdAsync(supplierId);
				return Ok(supplierOrdersResponseDTOs);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<SupplierOrderResponseDTO>> AddSupplierOrder(SupplierOrderRequestDTO supplierOrderRequestDTO)
		{
			try
			{
				var supplierOrderReponseDTO = await _SupplierOrderService.AddSupplierOrderAsync(supplierOrderRequestDTO);
				return Ok(supplierOrderReponseDTO);

			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<SupplierOrderResponseDTO>> UpdateSupplierOrder(Guid id, SupplierOrderRequestDTO supplierOrderRequestDTO)
		{
			try
			{
				var supplierOrderResponseDTO = await _SupplierOrderService.UpdateSupplierOrderAsync(id, supplierOrderRequestDTO);
				return Ok(supplierOrderResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<SupplierOrderResponseDTO>> DeleteSupplierOrder(Guid id)
		{
			try
			{
				var supplierOrderResponseDTO = await _SupplierOrderService.DeleteSupplierOrderAsync(id);
				return Ok(supplierOrderResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
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
