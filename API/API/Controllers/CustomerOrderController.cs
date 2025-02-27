﻿using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;
using API.Utils;

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
			var customerOrdersResponseDTO = await _CustomerOrderService.GetAllCustomerOrdersAsync();
			return Ok(customerOrdersResponseDTO);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerOrderResponseDTO>> GetCustomerOrderById(Guid id)
		{
			try
			{
				var customerOrdersResponseDTO = await _CustomerOrderService.GetCustomerOrderByIdAsync(id);
				return Ok(customerOrdersResponseDTO);
			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("Customer/{customerId}")]
		public async Task<ActionResult<IEnumerable<CustomerOrderResponseDTO>>> GetCustomerOrdersByCustomerId(string customerId)
		{
			try
			{
				var customerOrdersResponseDTOs = await _CustomerOrderService.GetCustomerOrdersByCustomerIdAsync(customerId);
				return Ok(customerOrdersResponseDTOs);
			}
			catch (ValidationException ex)
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
			catch (ValidationException ex)
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
			catch (ValidationException ex)
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
			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("{customerOrderId}/Items/{itemId}/ItemQuantity/{itemQuantity}")]
		public async Task<ActionResult<OrderDetail>> AddItemToCustomerOrder(Guid customerOrderId, Guid itemId, int itemQuantity)
		{
			try
			{
				var orderDetail = await _CustomerOrderService.AddItemToCustomerOrderAsync(customerOrderId, itemId, itemQuantity);
				return Ok(orderDetail);
			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
