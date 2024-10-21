using API.Models;
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
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _CustomerService;

		public CustomerController(ICustomerService CustomerService)
		{
			_CustomerService = CustomerService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CustomerResponseDTO>>> GetAllCustomers()
		{
			var customerResponseDTOs = await _CustomerService.GetAllCustomersAsync();

			return Ok(customerResponseDTOs);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerResponseDTO>> GetCustomerById(string id)
		{
			try
			{
				var customerResponseDTO = await _CustomerService.GetCustomerByIdAsync(id);
				return Ok(customerResponseDTO);
			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomerResponseDTO>> DeleteCustomer(string id)
		{
			try
			{
				var customerResponseDTO = await _CustomerService.DeleteCustomerAsync(id);
				return Ok(customerResponseDTO);

			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
