using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Models.DTOs.ResponseDTOs;
using API.Models.DTOs.RequestDTOs;

namespace API.Services
{
    public class AlcoholFamilyService : IAlcoholFamilyService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public AlcoholFamilyService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<AlcoholFamilyResponseDTO> AddAlcoholFamilyAsync(AlcoholFamilyRequestDTO alcoholFamilyDTO)
		{
			var alcoholFamilyNameExist = await _context.AlcoholFamilies.SingleOrDefaultAsync(af => af.Name == alcoholFamilyDTO.Name);
			if (alcoholFamilyNameExist != null) 
			{
				throw new InvalidOperationException($"Unable to add : the alcohol Family named '{alcoholFamilyDTO.Name}' already exsists");
			}

			var alcoholFamily = _mapper.Map<AlcoholFamily>(alcoholFamilyDTO);
			await _context.AlcoholFamilies.AddAsync(alcoholFamily);
			await _context.SaveChangesAsync();

			var alcoholFamilyResponseDTO = _mapper.Map<AlcoholFamilyResponseDTO>(alcoholFamily);

			return alcoholFamilyResponseDTO;
		}

		public async Task<AlcoholFamilyResponseDTO> DeleteAlcoholFamilyAsync(Guid id)
		{
			var alcoholFamily = await _context.AlcoholFamilies.FindAsync(id);
			if (alcoholFamily is null)
			{
				throw new InvalidOperationException($"Unable to delete : the alcohol Family '{id}' doesn't exists");
			}

			_context.AlcoholFamilies.Remove(alcoholFamily);
			await _context.SaveChangesAsync();

			var alcoholFamilyResponseDTO = _mapper.Map<AlcoholFamilyResponseDTO>(alcoholFamily);

			return alcoholFamilyResponseDTO;
		}

		public async Task<AlcoholFamilyResponseDTO> GetAlcoholFamilyByIdAsync(Guid id)
		{
			var alcoholFamily = await _context.AlcoholFamilies.FindAsync(id);
			if (alcoholFamily is null)
			{
				throw new InvalidOperationException($"Unable to get : the alcohol Family '{id}' doesn't exists");
			}

			var alcoholFamilyResponseDTO = _mapper.Map<AlcoholFamilyResponseDTO>(alcoholFamily);

	
			return alcoholFamilyResponseDTO;
		}

		public async Task<IEnumerable<AlcoholFamilyResponseDTO>> GetAllAlcoholFamiliesAsync()
		{
			var alcoholFamilies = await _context.AlcoholFamilies.ToListAsync();

			var alcoholFamiliesResponseDTO = _mapper.Map<IEnumerable<AlcoholFamilyResponseDTO>>(alcoholFamilies);

			return alcoholFamiliesResponseDTO;
		}

		public async Task<AlcoholFamilyResponseDTO> UpdateAlcoholFamilyAsync(Guid id, AlcoholFamilyRequestDTO alcoholFamilyDTO)
		{
			var alcoholFamily = await _context.AlcoholFamilies.FindAsync(id);
			if (alcoholFamily == null)
			{
				throw new InvalidOperationException($"Unable to modify : the alcohol Family '{id}' doesn't exists");
			}

			var alcoholFamilyNameExist = await _context.AlcoholFamilies.SingleOrDefaultAsync(af => af.Name == alcoholFamilyDTO.Name && af.AlcoholFamilyId != id);
			if (alcoholFamilyNameExist != null)
			{
				throw new InvalidOperationException($"Unable to modify : the alcohol Family named '{alcoholFamilyDTO.Name}' already exsists");
			}

			_mapper.Map(alcoholFamilyDTO, alcoholFamily);
			await _context.SaveChangesAsync();

			var alcoholFamilyResponseDTO = _mapper.Map<AlcoholFamilyResponseDTO>(alcoholFamily);

			return alcoholFamilyResponseDTO;
		}
	}
}
