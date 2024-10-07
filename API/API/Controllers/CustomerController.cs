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
		public async Task<ActionResult<CustomerResponseDTO>> GetCustomerById(Guid id)
		{
			try
			{
				var customerResponseDTO = await _CustomerService.GetCustomerByIdAsync(id);
				return Ok(customerResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<CustomerResponseDTO>> AddCustomer(CustomerRequestDTO customerRequestDTO)
		{
			try
			{
				var customerResponseDTO = await _CustomerService.AddCustomerAsync(customerRequestDTO);
				return customerResponseDTO;
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CustomerResponseDTO>> UpdateCustomer(Guid id, CustomerRequestDTO customerRequestDTO)
		{
			try
			{
				var customerResponseDTO = await _CustomerService.UpdateCustomerAsync(id, customerRequestDTO);
				return Ok(customerResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomerResponseDTO>> DeleteCustomer(Guid id)
		{
			try
			{
				var customerResponseDTO = await _CustomerService.DeleteCustomerAsync(id);
				return Ok(customerResponseDTO);

			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("{customerId}/Adresses/{adressId}")]
		public async Task<ActionResult<CustomerResponseDTO>> AddAdressToCustomer(Guid customerId, Guid adressId)
		{
			try
			{
				var customerResponseDTO = await _CustomerService.AddAdressToCustomerAsync(customerId, adressId);
				return Ok(customerResponseDTO);

			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
