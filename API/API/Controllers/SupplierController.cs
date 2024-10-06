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
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierService _SupplierService;

		public SupplierController(ISupplierService SupplierService)
		{
			_SupplierService = SupplierService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<SupplierResponseDTO>>> GetAllSuppliers()
		{
			var supplierResponseDTOs = await _SupplierService.GetAllSuppliersAsync();

			return Ok(supplierResponseDTOs);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<SupplierResponseDTO>> GetSupplierById(Guid id)
		{
			try
			{
				var supplierResponseDTO = await _SupplierService.GetSupplierByIdAsync(id);
				return Ok(supplierResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<SupplierResponseDTO>> AddSupplier(SupplierRequestDTO SupplierDTO)
		{
			try
			{
				var supplierResponseDTO = await _SupplierService.AddSupplierAsync(SupplierDTO);

				return Ok(supplierResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPut("{id}")]
		public async Task<ActionResult<SupplierResponseDTO>> UpdateSupplier(Guid id, SupplierRequestDTO supplierRequestDTO)
		{
			try
			{
				var supplierResponseDTO = await _SupplierService.UpdateSupplierAsync(id, supplierRequestDTO);
				return Ok(supplierResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<SupplierResponseDTO>> DeleteSupplier(Guid id)
		{
			try
			{
				var supplierResponseDTO = await _SupplierService.DeleteSupplierAsync(id);
				return Ok(supplierResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPost("{supplierId}/Adresses/{adressId}")]
		public async Task<ActionResult<SupplierResponseDTO>> AddAdressToSupplier(Guid supplierId, Guid adressId)
		{
			try
			{
				var supplierResponseDTO = await _SupplierService.AddAdressToSupplierAsync(supplierId, adressId);
				return Ok(supplierResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
