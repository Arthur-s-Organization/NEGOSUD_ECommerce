using API.Models.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<ActionResult<IEnumerable<AlcoholItem>>> GetAllAlcoholItems()
		{
			var AlcoholItems = await _AlcoholItemService.GetAllAlcoholItemsAsync();

			return Ok(AlcoholItems);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<AlcoholItem>> GetAlcoholItemById(Guid id)
		{
			var AlcoholItem = await _AlcoholItemService.GetAlcoholItemByIdAsync(id);
			if (AlcoholItem == null)
			{
				return BadRequest("AlcoholItem not found");
			}
			return Ok(AlcoholItem);
		}

		[HttpPost]
		public async Task<ActionResult<AlcoholItem>> AddAlcoholItem(AlcoholItemDTO AlcoholItemDTO)
		{
			var createdAlcoholItem = await _AlcoholItemService.AddAlcoholItemAsync(AlcoholItemDTO);

			return createdAlcoholItem;
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<AlcoholItem>> UpdateAlcoholItem(Guid id, AlcoholItemDTO AlcoholItemDTO)
		{
			var updatedAlcoholItem = await _AlcoholItemService.UpdateAlcoholItemAsync(id, AlcoholItemDTO);

			if (updatedAlcoholItem == null)
			{
				return BadRequest("AlcoholItem doesn't exists");
			}
			return Ok(updatedAlcoholItem);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<AlcoholItem>> DeleteAlcoholItem(Guid id)
		{
			var deletedAlcoholItem = await _AlcoholItemService.DeleteAlcoholItemAsync(id);
			if (deletedAlcoholItem == null)
			{
				return BadRequest($"Unable to delete AlcoholItem {id}");
			}
			return Ok(deletedAlcoholItem);
		}

	}
}
