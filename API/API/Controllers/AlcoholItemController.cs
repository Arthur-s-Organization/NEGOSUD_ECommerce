using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;
using API.Services;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlcoholItemController : ControllerBase
	{
		private readonly IAlcoholItemService _AlcoholItemService;

		public AlcoholItemController(IAlcoholItemService AlcoholItemService)
		{
			_AlcoholItemService = AlcoholItemService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AlcoholItemResponseDTO>>> GetAllAlcoholItems()
		{
			var alcoholItemResponseDTOs = await _AlcoholItemService.GetAllAlcoholItemsAsync();

			return Ok(alcoholItemResponseDTOs);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<AlcoholItemResponseDTO>> GetAlcoholItemById(Guid id)
		{
			try
			{
				var alcoholItemResponseDTO = await _AlcoholItemService.GetAlcoholItemByIdAsync(id);
				return Ok(alcoholItemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPost]
		public async Task<ActionResult<AlcoholItemResponseDTO>> AddAlcoholItem(AlcoholItemRequestDTO AlcoholItemDTO)
		{
			try
			{
				var alcoholItemResponseDTO = await _AlcoholItemService.AddAlcoholItemAsync(AlcoholItemDTO);
				return Ok(alcoholItemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPut("{id}")]
		public async Task<ActionResult<AlcoholItemResponseDTO>> UpdateAlcoholItem(Guid id, AlcoholItemRequestDTO AlcoholItemDTO)
		{
			try
			{
				var alcoholItemResponseDTO = await _AlcoholItemService.UpdateAlcoholItemAsync(id, AlcoholItemDTO);
				return Ok(alcoholItemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<AlcoholItemResponseDTO>> DeleteAlcoholItem(Guid id)
		{
			try
			{
				var alcoholItemResponseDTO = await _AlcoholItemService.DeleteAlcoholItemAsync(id);
				return Ok(alcoholItemResponseDTO);

			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("Supplier{supplierId}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetAllAlcoholBySuppliers(Guid supplierId)
		{
			try
			{
				var alcoholItemResponseDTOs = await _AlcoholItemService.GetAllAlcoholItemsBySupplierAsync(supplierId);
				return Ok(alcoholItemResponseDTOs);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
