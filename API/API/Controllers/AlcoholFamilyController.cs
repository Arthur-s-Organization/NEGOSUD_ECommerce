using API.Models.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlcoholFamilyController : ControllerBase
	{
		private readonly IAlcoholFamilyService _AlcoholFamilyService;

		public AlcoholFamilyController(IAlcoholFamilyService AlcoholFamilyService)
		{
			_AlcoholFamilyService = AlcoholFamilyService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AlcoholFamily>>> GetAllAlcoholFamilys()
		{
			var AlcoholFamilys = await _AlcoholFamilyService.GetAllAlcoholFamilysAsync();

			return Ok(AlcoholFamilys);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<AlcoholFamily>> GetAlcoholFamilyById(Guid id)
		{
			var AlcoholFamily = await _AlcoholFamilyService.GetAlcoholFamilyByIdAsync(id);
			if (AlcoholFamily == null)
			{
				return BadRequest("AlcoholFamily not found");
			}
			return Ok(AlcoholFamily);
		}

		[HttpPost]
		public async Task<ActionResult<AlcoholFamily>> AddAlcoholFamily(AlcoholFamilyDTO AlcoholFamilyDTO)
		{
			var createdAlcoholFamily = await _AlcoholFamilyService.AddAlcoholFamilyAsync(AlcoholFamilyDTO);

			return createdAlcoholFamily;
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<AlcoholFamily>> UpdateAlcoholFamily(Guid id, AlcoholFamilyDTO AlcoholFamilyDTO)
		{
			var updatedAlcoholFamily = await _AlcoholFamilyService.UpdateAlcoholFamilyAsync(id, AlcoholFamilyDTO);

			if (updatedAlcoholFamily == null)
			{
				return BadRequest("AlcoholFamily doesn't exists");
			}
			return Ok(updatedAlcoholFamily);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<AlcoholFamily>> DeleteAlcoholFamily(Guid id)
		{
			var deletedAlcoholFamily = await _AlcoholFamilyService.DeleteAlcoholFamilyAsync(id);
			if (deletedAlcoholFamily == null)
			{
				return BadRequest($"Unable to delete AlcoholFamily {id}");
			}
			return Ok(deletedAlcoholFamily);
		}
	}
}
