using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;

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
		public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
		{
			var Customers = await _CustomerService.GetAllCustomersAsync();

			return Ok(Customers);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
		{
			var Customer = await _CustomerService.GetCustomerByIdAsync(id);
			if (Customer == null)
			{
				return BadRequest("Customer not found");
			}
			return Ok(Customer);
		}

		[HttpPost]
		public async Task<ActionResult<Customer>> AddCustomer(CustomerRequestDTO CustomerDTO)
		{
			var createdCustomer = await _CustomerService.AddCustomerAsync(CustomerDTO);

			return createdCustomer;
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Customer>> UpdateCustomer(Guid id, CustomerRequestDTO CustomerDTO)
		{
			var updatedCustomer = await _CustomerService.UpdateCustomerAsync(id, CustomerDTO);

			if (updatedCustomer == null)
			{
				return BadRequest("Customer doesn't exists");
			}
			return Ok(updatedCustomer);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Customer>> DeleteCustomer(Guid id)
		{
			var deletedCustomer = await _CustomerService.DeleteCustomerAsync(id);
			if (deletedCustomer == null)
			{
				return BadRequest($"Unable to delete Customer {id}");
			}
			return Ok(deletedCustomer);
		}



		[HttpPost("{CustomerId}/Adresses/{AdressId}")]
		public async Task<ActionResult> AddAdressToCustomer(Guid CustomerId, Guid AdressId)
		{
			var customer = await _CustomerService.AddAdressToCustomerAsync(CustomerId, AdressId);
			if (customer == null)
			{
				return BadRequest($"Impossible d'ajouter l'adresse {AdressId} au Customer {CustomerId}");
			}
			return Ok(customer);
		}
	}
}
