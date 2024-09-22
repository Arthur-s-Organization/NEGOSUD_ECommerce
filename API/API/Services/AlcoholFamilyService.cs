using API.Data;
using API.Models.DTOs;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
		public async Task<AlcoholFamily> AddAlcoholFamilyAsync(AlcoholFamilyDTO AlcoholFamilyDTO)
		{
			var AlcoholFamily = _mapper.Map<AlcoholFamily>(AlcoholFamilyDTO);
			await _context.AlcoholFamilies.AddAsync(AlcoholFamily);
			await _context.SaveChangesAsync();

			return AlcoholFamily;
		}

		public async Task<AlcoholFamily> DeleteAlcoholFamilyAsync(Guid id)
		{
			var AlcoholFamily = await _context.AlcoholFamilies.SingleOrDefaultAsync(c => c.AlcoholFamilyId == id);
			if (AlcoholFamily is null)
			{
				return null;
			}
			_context.AlcoholFamilies.Remove(AlcoholFamily);
			await _context.SaveChangesAsync();
			return AlcoholFamily;
		}

		public async Task<AlcoholFamily> GetAlcoholFamilyByIdAsync(Guid id)
		{
			var AlcoholFamily = await _context.AlcoholFamilies.SingleOrDefaultAsync(c => c.AlcoholFamilyId == id);
			if (AlcoholFamily is null)
			{
				return null;
			}
			return AlcoholFamily;
		}

		public async Task<IEnumerable<AlcoholFamily>> GetAllAlcoholFamilysAsync()
		{
			var AlcoholFamilys = await _context.AlcoholFamilies.ToListAsync();
			return AlcoholFamilys;
		}

		public async Task<AlcoholFamily> UpdateAlcoholFamilyAsync(Guid id, AlcoholFamilyDTO AlcoholFamilyDTO)
		{
			var existingAlcoholFamily = await _context.AlcoholFamilies.FindAsync(id);
			if (existingAlcoholFamily == null)
			{
				return null;
			}

			_mapper.Map(AlcoholFamilyDTO, existingAlcoholFamily);

			await _context.SaveChangesAsync();
			return existingAlcoholFamily;
		}
	}
}
