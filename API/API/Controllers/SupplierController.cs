using API.Models.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliers()
		{
			var Suppliers = await _SupplierService.GetAllSuppliersAsync();

			return Ok(Suppliers);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Supplier>> GetSupplierById(Guid id)
		{
			var Supplier = await _SupplierService.GetSupplierByIdAsync(id);
			if (Supplier == null)
			{
				return BadRequest("Supplier not found");
			}
			return Ok(Supplier);
		}

		[HttpPost]
		public async Task<ActionResult<Supplier>> AddSupplier(SupplierDTO SupplierDTO)
		{
			var createdSupplier = await _SupplierService.AddSupplierAsync(SupplierDTO);

			return createdSupplier;
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Supplier>> UpdateSupplier(Guid id, SupplierDTO SupplierDTO)
		{
			var updatedSupplier = await _SupplierService.UpdateSupplierAsync(id, SupplierDTO);

			if (updatedSupplier == null)
			{
				return BadRequest("Supplier doesn't exists");
			}
			return Ok(updatedSupplier);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Supplier>> DeleteSupplier(Guid id)
		{
			var deletedSupplier = await _SupplierService.DeleteSupplierAsync(id);
			if (deletedSupplier == null)
			{
				return BadRequest($"Unable to delete Supplier {id}");
			}
			return Ok(deletedSupplier);
		}


		[HttpPost("{SupplierId}/Adresses/{AdressId}")]
		public async Task<ActionResult> AddAdressToSupplier(Guid SupplierId, Guid AdressId)
		{
			var Supplier = await _SupplierService.AddAdressToSupplierAsync(SupplierId, AdressId);
			if (Supplier == null)
			{
				return BadRequest($"Impossible d'ajouter l'adresse {AdressId} au Supplier {SupplierId}");
			}
			return Ok(Supplier);
		}
	}
}
