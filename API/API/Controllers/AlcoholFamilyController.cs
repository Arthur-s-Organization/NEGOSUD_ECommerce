using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.ResponseDTOs;
using API.Models.DTOs.RequestDTOs;

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
		public async Task<ActionResult<IEnumerable<AlcoholFamilyResponseDTO>>> GetAllAlcoholFamilys()
		{
			var alcoholFamiliesResponseDTO = await _AlcoholFamilyService.GetAllAlcoholFamiliesAsync();

			return Ok(alcoholFamiliesResponseDTO);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<AlcoholFamilyResponseDTO>> GetAlcoholFamilyById(Guid id)
		{
			try
			{
				var alcoholFamiliesResponseDTO = await _AlcoholFamilyService.GetAlcoholFamilyByIdAsync(id);
				return Ok(alcoholFamiliesResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPost]
		public async Task<ActionResult<AlcoholFamilyResponseDTO>> AddAlcoholFamily(AlcoholFamilyRequestDTO AlcoholFamilyDTO)
		{
			try
			{
				var alcoholFamilyResponseDTO = await _AlcoholFamilyService.AddAlcoholFamilyAsync(AlcoholFamilyDTO);
				return Ok(alcoholFamilyResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPut("{id}")]
		public async Task<ActionResult<AlcoholFamilyResponseDTO>> UpdateAlcoholFamily(Guid id, AlcoholFamilyRequestDTO AlcoholFamilyDTO)
		{
			try
			{
				var alcoholFamilyResponseDTO = await _AlcoholFamilyService.UpdateAlcoholFamilyAsync(id, AlcoholFamilyDTO);
				return Ok(alcoholFamilyResponseDTO);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<AlcoholFamilyResponseDTO>> DeleteAlcoholFamily(Guid id)
		{
			try
			{
				var alcoholFamilyResponseDTO = await _AlcoholFamilyService.DeleteAlcoholFamilyAsync(id);
				return Ok(alcoholFamilyResponseDTO);
			}

			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
