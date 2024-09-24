using API.Models.DTOs;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdressController : ControllerBase
	{
		private readonly IAddressService _AddressService;

		public AdressController(IAddressService AddressService)
		{
			_AddressService = AddressService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresss()
		{
			var Addresss = await _AddressService.GetAllAddresssAsync();

			return Ok(Addresss);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Address>> GetAddressById(Guid id)
		{
			var Address = await _AddressService.GetAddressByIdAsync(id);
			if (Address == null)
			{
				return BadRequest("Address not found");
			}
			return Ok(Address);
		}

		[HttpPost]
		public async Task<ActionResult<Address>> AddAddress(AddressDTO AddressDTO)
		{
			var createdAddress = await _AddressService.AddAddressAsync(AddressDTO);

			return createdAddress;
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Address>> UpdateAddress(Guid id, AddressDTO AddressDTO)
		{
			var updatedAddress = await _AddressService.UpdateAddressAsync(id, AddressDTO);

			if (updatedAddress == null)
			{
				return BadRequest("Address doesn't exists");
			}
			return Ok(updatedAddress);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Address>> DeleteAddress(Guid id)
		{
			var deletedAddress = await _AddressService.DeleteAddressAsync(id);
			if (deletedAddress == null)
			{
				return BadRequest($"Unable to delete Address {id}");
			}
			return Ok(deletedAddress);
		}

	}
}

