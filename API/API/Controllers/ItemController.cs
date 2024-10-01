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


		[HttpGet("/Supplier{supplierId}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetAllItemsBySuppliers(Guid supplierId)
		{
			try
			{
				var ItemResponseDTO = await _ItemService.GetAllItemsBySupplierAsync(supplierId);
				return Ok(ItemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

