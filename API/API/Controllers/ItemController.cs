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
		private readonly IItemService _itemService;


		public ItemController(IItemService ItemService)
		{
			_itemService = ItemService;
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ItemResponseDTO>> DeleteItemAsync(Guid id)
		{
			try
			{
				var itemResponseDTO = await _itemService.DeleteItemAsync(id);
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
			var ItemResponseDTO = await _itemService.GetAllItemsAsync();

			return Ok(ItemResponseDTO);
		}


		[HttpGet("Supplier/{supplierId}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetAllItemsBySuppliers(Guid supplierId)
		{
			try
			{
				var itemResponseDTO = await _itemService.GetAllItemsBySupplierAsync(supplierId);
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
			var itemResponseDTO = await _itemService.GetTopSellingItemsAsync(topCount);
			return Ok(itemResponseDTO);
		}

		[HttpGet("recent/{topCount}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetRecentlyAddedItems(int topCount)
		{
			var itemResponseDTO = await _itemService.GetRecentlyAddedItemsAsync(topCount);
			return Ok(itemResponseDTO);
		}

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilteredItems([FromQuery] ItemFilterRequestDTO filters)
		{
			try
			{
				var result = await _itemService.GetFilteredItemsAsync(filters);
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
				var itemResponseDTO = await _itemService.UpdateItemAsync(id, itemRequestDTO);
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
				var itemResponseDTO = await _itemService.AddItemAsync(itemRequestDTO);
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
			var ItemResponseDTO = await _itemService.GetItemBySlugAsync(slug);

			return Ok(ItemResponseDTO);
		}

		[HttpGet("{id}/image")]
		public async Task<IActionResult> GetItemImage(Guid id)
		{
			try
			{
				var imageBytes = await _itemService.GetItemImageAsync(id);

				// Retourner l'image avec le bon content-type (par exemple, image/jpeg ou image/png)
				return File(imageBytes, "image/jpeg");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

