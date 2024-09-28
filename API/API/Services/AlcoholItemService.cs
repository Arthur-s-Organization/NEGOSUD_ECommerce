using API.Data;
using API.Models.DTOs;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Utils;

namespace API.Services
{
	public class AlcoholItemService : IAlcoholItemService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public AlcoholItemService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<AlcoholItem> AddAlcoholItemAsync(AlcoholItemDTO AlcoholItemDTO)
		{
			var AlcoholFamily = _context.AlcoholFamilies.SingleOrDefault(af => af.AlcoholFamilyId == AlcoholItemDTO.AlcoholFamilyId);

			if (AlcoholFamily == null)
			{
				return null;
			}

			var Supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == AlcoholItemDTO.SupplierId);

			if (Supplier == null)
			{
				return null;
			}


			var alcoholItem = _mapper.Map<AlcoholItem>(AlcoholItemDTO);

			alcoholItem.Slug = SlugHelper.GenerateSlug(alcoholItem.Name);

			await _context.AlcoholItems.AddAsync(alcoholItem);
			await _context.SaveChangesAsync();

			return alcoholItem;
		}

		public async Task<AlcoholItem> DeleteAlcoholItemAsync(Guid id)
		{
			var AlcoholItem = await _context.AlcoholItems.SingleOrDefaultAsync(ai => ai.ItemId == id);
			if (AlcoholItem is null)
			{
				return null;
			}
			_context.AlcoholItems.Remove(AlcoholItem);
			await _context.SaveChangesAsync();
			return AlcoholItem;
		}

		public async Task<AlcoholItem> GetAlcoholItemByIdAsync(Guid id)
		{
			var AlcoholItem = await _context.AlcoholItems
				.Include(ai => ai.AlcoholFamily)
				.Include(ai => ai.Supplier)
				.SingleOrDefaultAsync(ai => ai.ItemId == id);
			if (AlcoholItem is null)
			{
				return null;
			}
			return AlcoholItem;
		}

		public async Task<IEnumerable<AlcoholItem>> GetAllAlcoholItemsAsync()
		{
			var AlcoholItems = await _context.AlcoholItems
				.Include(ai => ai.AlcoholFamily)
				.Include(ai => ai.Supplier)
				//.Include(ai => ai.OrderDetails)   => seulement si on veut s'avoir dans quelles commandes nos articles ont été ajoutés
				.ToListAsync();
			return AlcoholItems;
		}

		public async Task<AlcoholItem> UpdateAlcoholItemAsync(Guid id, AlcoholItemDTO AlcoholItemDTO)
		{
			var existingAlcoholItem = await _context.AlcoholItems.FindAsync(id);

			if (existingAlcoholItem == null)
			{
				return null;
			}

			var existingAlcoholFamily = await _context.AlcoholFamilies.FindAsync(AlcoholItemDTO.AlcoholFamilyId);

			if (existingAlcoholFamily == null)
			{
				return null;
			}

			_mapper.Map(AlcoholItemDTO, existingAlcoholItem);

			await _context.SaveChangesAsync();
			return existingAlcoholItem;
		}

	}
}
