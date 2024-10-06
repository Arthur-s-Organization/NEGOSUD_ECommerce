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
		public async Task<ActionResult<IEnumerable<CommonItemResponseDTO>>> GetAllCommonItems()
		{
			var commonItemResponseDTOs = await _CommonItemService.GetAllCommonItemsAsync();

			return Ok(commonItemResponseDTOs);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<CommonItemResponseDTO>> GetCommonItemById(Guid id)
		{
			try
			{
				var commonItemResponseDTO = await _CommonItemService.GetCommonItemByIdAsync(id);
				return Ok(commonItemResponseDTO);

			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<CommonItemResponseDTO>> AddCommonItem(CommonItemRequestDTO CommonItemDTO)
		{
			try
			{
				var commonItemResponseDTO = await _CommonItemService.AddCommonItemAsync(CommonItemDTO);
				return Ok(commonItemResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CommonItemResponseDTO>> UpdateCommonItem(Guid id, CommonItemRequestDTO CommonItemDTO)
		{
			try
			{
				var commonItemResponseDTO = await _CommonItemService.UpdateCommonItemAsync(id, CommonItemDTO);
				return Ok(commonItemResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<CommonItemResponseDTO>> DeleteCommonItem(Guid id)
		{
			try
			{
				var commonItemResponseDTO = await _CommonItemService.DeleteCommonItemAsync(id);
				return Ok(commonItemResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpGet("Supplier{supplierId}")]
		public async Task<ActionResult<IEnumerable<CommonItemResponseDTO>>> GetAllCommonItemsBySuppliers(Guid supplierId)
		{
			try
			{
				var commonItemResponseDTOs = await _CommonItemService.GetAllCommonItemsBySupplierAsync(supplierId);
				return Ok(commonItemResponseDTOs);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
