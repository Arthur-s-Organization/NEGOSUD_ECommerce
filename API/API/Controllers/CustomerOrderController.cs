using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerOrderController : ControllerBase
	{
		private readonly ICustomerOrderService _CustomerOrderService;

		public CustomerOrderController(ICustomerOrderService CustomerOrderService)
		{
			_CustomerOrderService = CustomerOrderService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CustomerOrder>>> GetAllCustomerOrders()
		{
			var CustomerOrdersResponseDTO = await _CustomerOrderService.GetAllCustomerOrdersAsync();
			return Ok(CustomerOrdersResponseDTO);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerOrder>> GetCustomerOrderById(Guid id)
		{
			try
			{
				var CustomerOrdersResponseDTO = await _CustomerOrderService.GetCustomerOrderByIdAsync(id);
				return Ok(CustomerOrdersResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPost]
		public async Task<ActionResult<CustomerOrderResponseDTO>> AddCustomerOrder(CustomerOrderRequestDTO customerOrderRequestDTO)
		{
			try
			{
				var CustomerOrdersResponseDTO = await _CustomerOrderService.AddCustomerOrderAsync(customerOrderRequestDTO);
				return Ok(CustomerOrdersResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CustomerOrderResponseDTO>> UpdateCustomerOrder(Guid id, CustomerOrderRequestDTO customerOrderRequestDTO)
		{
			try
			{
				var CustomerOrdersResponseDTO = await _CustomerOrderService.UpdateCustomerOrderAsync(id, customerOrderRequestDTO);
				return Ok(CustomerOrdersResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomerOrderResponseDTO>> DeleteCustomerOrder(Guid id)
		{
			try
			{
				var CustomerOrdersResponseDTO = await _CustomerOrderService.DeleteCustomerOrderAsync(id);
				return Ok(CustomerOrdersResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("{customerOrderId}/Items/{itemId}/ItemQuantity{itemQuantity}")]
		public async Task<ActionResult<OrderDetail>> AddItemToCustomerOrder(Guid customerOrderId, Guid itemId, int itemQuantity)
		{
			try
			{
				var orderDetail = await _CustomerOrderService.AddItemToCustomerOrderAsync(customerOrderId, itemId, itemQuantity);
				return Ok(orderDetail);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
