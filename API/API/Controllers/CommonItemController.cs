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
	public class CommonItemController : ControllerBase
	{
		private readonly ICommonItemService _CommonItemService;

		public CommonItemController(ICommonItemService CommonItemService)
		{
			_CommonItemService = CommonItemService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CommonItem>>> GetAllCommonItems()
		{
			var CommonItems = await _CommonItemService.GetAllCommonItemsAsync();

			return Ok(CommonItems);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<CommonItem>> GetCommonItemById(Guid id)
		{
			var CommonItem = await _CommonItemService.GetCommonItemByIdAsync(id);
			if (CommonItem == null)
			{
				return BadRequest("CommonItem not found");
			}
			return Ok(CommonItem);
		}

		[HttpPost]
		public async Task<ActionResult<CommonItem>> AddCommonItem(CommonItemRequestDTO CommonItemDTO)
		{
			var createdCommonItem = await _CommonItemService.AddCommonItemAsync(CommonItemDTO);

			if (createdCommonItem == null)
			{
				return BadRequest("SupplierId not found");
			}
			return Ok(createdCommonItem);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CommonItem>> UpdateCommonItem(Guid id, CommonItemRequestDTO CommonItemDTO)
		{
			var updatedCommonItem = await _CommonItemService.UpdateCommonItemAsync(id, CommonItemDTO);

			if (updatedCommonItem == null)
			{
				return BadRequest("CommonItem doesn't exists");
			}
			return Ok(updatedCommonItem);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<CommonItem>> DeleteCommonItem(Guid id)
		{
			var deletedCommonItem = await _CommonItemService.DeleteCommonItemAsync(id);
			if (deletedCommonItem == null)
			{
				return BadRequest($"Unable to delete CommonItem {id}");
			}
			return Ok(deletedCommonItem);
		}

		[HttpGet("Supplier{supplierId}")]
		public async Task<ActionResult<IEnumerable<ItemResponseDTO>>> GetAllCommonItemsBySuppliers(Guid supplierId)
		{
			try
			{
				var commonItemResponseDTO = await _CommonItemService.GetAllCommonItemsBySupplierAsync(supplierId);
				return Ok(commonItemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
