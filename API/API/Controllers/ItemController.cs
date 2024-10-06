using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;
using API.Services;
using API.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		private readonly IItemService _ItemService;

		public ItemController(IItemService ItemService)
		{
			_ItemService = ItemService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetAllItems()
		{
			var ItemResponseDTO = await _ItemService.GetAllItemsAsync();

			return Ok(ItemResponseDTO);
		}


		[HttpGet("Supplier{supplierId}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetAllItemsBySuppliers(Guid supplierId)
		{
			try
			{
				var itemResponseDTO = await _ItemService.GetAllItemsBySupplierAsync(supplierId);
				return Ok(itemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("topselling/{topCount}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetTopSellingItems(int topCount)
		{
			var itemResponseDTO = await _ItemService.GetTopSellingItemsAsync(topCount);
			return Ok(itemResponseDTO);
		}

		[HttpGet("recent/{topCount}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetRecentlyAddedItems(int topCount)
		{
			var itemResponseDTO = await _ItemService.GetRecentlyAddedItemsAsync(topCount);
			return Ok(itemResponseDTO);
		}

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilteredItems([FromQuery] ItemFilterRequestDTO filters)
		{
			try
			{
				var result = await _ItemService.GetFilteredItemsAsync(filters);
				return Ok(result);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}

