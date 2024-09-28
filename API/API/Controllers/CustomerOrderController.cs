using API.Models.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
			var CustomerOrders = await _CustomerOrderService.GetAllCustomerOrdersAsync();

			return Ok(CustomerOrders);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerOrder>> GetCustomerOrderById(Guid id)
		{
			var CustomerOrder = await _CustomerOrderService.GetCustomerOrderByIdAsync(id);
			if (CustomerOrder == null)
			{
				return BadRequest("CustomerOrder not found");
			}
			return Ok(CustomerOrder);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerOrder>> AddCustomerOrder(CustomerOrderDTO CustomerOrderDTO)
		{
			var createdCustomerOrder = await _CustomerOrderService.AddCustomerOrderAsync(CustomerOrderDTO);

			if (createdCustomerOrder == null)
			{
				return BadRequest("Customer Id not found");
			}
			return Ok(createdCustomerOrder);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CustomerOrder>> UpdateCustomerOrder(Guid id, CustomerOrderDTO CustomerOrderDTO)
		{
			var updatedCustomerOrder = await _CustomerOrderService.UpdateCustomerOrderAsync(id, CustomerOrderDTO);

			if (updatedCustomerOrder == null)
			{
				return BadRequest("CustomerOrder doesn't exists");
			}
			return Ok(updatedCustomerOrder);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomerOrder>> DeleteCustomerOrder(Guid id)
		{
			var deletedCustomerOrder = await _CustomerOrderService.DeleteCustomerOrderAsync(id);
			if (deletedCustomerOrder == null)
			{
				return BadRequest($"Unable to delete CustomerOrder {id}");
			}
			return Ok(deletedCustomerOrder);
		}

		[HttpPost("{customerOrderId}/Items/{itemId}")]
		public async Task<ActionResult<OrderDetail>> AddItemToCustomerOrde(Guid customerOrderId, Guid itemId)
		{
			var orderDetail = await _CustomerOrderService.AddItemToCustomerOrderAsync(customerOrderId, itemId);
			if (orderDetail == null)
			{
				return BadRequest($"Order {customerOrderId}already contians item {itemId}");
			}
			return Ok(orderDetail);
		}

	}
}
