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

		[HttpDelete("{id}")]
		public async Task<ActionResult<ItemResponseDTO>> DeleteItemAsync(Guid id)
		{
			try
			{
				var itemResponseDTO = await _ItemService.DeleteItemAsync(id);
				return Ok(itemResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetAllItems()
		{
			var ItemResponseDTO = await _ItemService.GetAllItemsAsync();

			return Ok(ItemResponseDTO);
		}


		[HttpGet("Supplier/{supplierId}")]
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

		[HttpPut("{id}")]
		public async Task<ActionResult<ItemResponseDTO>> UpdateItem(Guid id, ItemRequestDTO itemRequestDTO)
		{
			try
			{
				var itemResponseDTO = await _ItemService.UpdateItemAsync(id, itemRequestDTO);
				return Ok(itemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost()]
		public async Task<ActionResult<ItemResponseDTO>> AddItem(ItemRequestDTO itemRequestDTO)
		{
			try
			{
				var itemResponseDTO = await _ItemService.AddItemAsync(itemRequestDTO);
				return Ok(itemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpGet("{slug}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetItemsBySlug(string slug)
		{
			var ItemResponseDTO = await _ItemService.GetItemBySlugAsync(slug);

			return Ok(ItemResponseDTO);
		}

	}
}

