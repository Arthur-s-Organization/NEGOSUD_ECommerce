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
	public class AdressController : ControllerBase
	{
		private readonly IAddressService _AddressService;

		public AdressController(IAddressService AddressService)
		{
			_AddressService = AddressService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AddressResponseDTO>>> GetAllAddresss()
		{
			var addressResponseDTO = await _AddressService.GetAllAddresssAsync();

			return Ok(addressResponseDTO);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<AddressResponseDTO>> GetAddressById(Guid id)
		{
			try
			{
				var addressResponseDTO = await _AddressService.GetAddressByIdAsync(id);
				return Ok(addressResponseDTO);

			}

			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<AddressResponseDTO>> AddAddress(AddressRequestDTO addressRequestDTO)
		{
			var addressResponseDTO = await _AddressService.AddAddressAsync(addressRequestDTO);

			return addressResponseDTO;
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<AddressResponseDTO>> UpdateAddress(Guid id, AddressRequestDTO addressRequestDTO)
		{
			try
			{
				var addressResponseDTO = await _AddressService.UpdateAddressAsync(id, addressRequestDTO);
				return Ok(addressResponseDTO);
			}

			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<AddressResponseDTO>> DeleteAddress(Guid id)
		{
			try
			{
				var addressResponseDTO = await _AddressService.DeleteAddressAsync(id);
				return Ok(addressResponseDTO);
			}

			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}

