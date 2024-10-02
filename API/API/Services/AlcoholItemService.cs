using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Utils;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;

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
		public async Task<AlcoholItemResponseDTO> AddAlcoholItemAsync(AlcoholItemRequestDTO alcoholItemRequestDTO)
		{
			var alcoholFamily = _context.AlcoholFamilies.SingleOrDefault(ai => ai.AlcoholFamilyId == alcoholItemRequestDTO.AlcoholFamilyId);

			if (alcoholFamily == null)
			{
				throw new InvalidOperationException($"Unable to add : alcoholfamily '{alcoholItemRequestDTO.AlcoholFamilyId}' doesn't exists");
			}

			var supplier = _context.Suppliers.SingleOrDefault(ai => ai.SupplierId == alcoholItemRequestDTO.SupplierId);

			if (supplier == null)
			{
				throw new InvalidOperationException($"Unable to add : supplier '{alcoholItemRequestDTO.SupplierId}' doesn't exists");
			}

			var alcoholItemNameExist = await _context.AlcoholItems.SingleOrDefaultAsync(ai => ai.Name == alcoholItemRequestDTO.Name && ai.SupplierId == alcoholItemRequestDTO.SupplierId);
			if (alcoholItemNameExist != null)
			{
				throw new InvalidOperationException($"Unable to add : a alcoholItem named '{alcoholItemRequestDTO.Name}' already exsists for supplier '{alcoholItemRequestDTO.SupplierId}'");
			}


			var alcoholItem = _mapper.Map<AlcoholItem>(alcoholItemRequestDTO);
			alcoholItem.Slug = SlugHelper.GenerateSlug(alcoholItem.Name);
			alcoholItem.CreationDate = DateTime.Now;
			await _context.AlcoholItems.AddAsync(alcoholItem);
			await _context.SaveChangesAsync();

			var alcoholItemResponseDTO = _mapper.Map<AlcoholItemResponseDTO>(alcoholItem);

			return alcoholItemResponseDTO;
		}

		public async Task<AlcoholItemResponseDTO> DeleteAlcoholItemAsync(Guid id)
		{
			var alcoholItem = await _context.AlcoholItems.FindAsync(id);
			if (alcoholItem is null)
			{
				throw new InvalidOperationException($"Unable to delete : alcoholItem '{id}' doesn't exists");
			}
			_context.AlcoholItems.Remove(alcoholItem);
			await _context.SaveChangesAsync();


			var alcoholItemResponseDTO = _mapper.Map<AlcoholItemResponseDTO>(alcoholItem);

			return alcoholItemResponseDTO;
		}

		public async Task<AlcoholItemResponseDTO> GetAlcoholItemByIdAsync(Guid id)
		{
			var alcoholItem = await _context.AlcoholItems
				.Include(ai => ai.AlcoholFamily)
				.Include(ai => ai.Supplier)
				.SingleOrDefaultAsync(ai => ai.ItemId == id);
			if (alcoholItem is null)
			{
				throw new InvalidOperationException($"Unable to get : alcoholItem '{id}' doesn't exists");
			}

			var alcoholItemResponseDTO = _mapper.Map<AlcoholItemResponseDTO>(alcoholItem);

			return alcoholItemResponseDTO;
		}

		public async Task<IEnumerable<AlcoholItemResponseDTO>> GetAllAlcoholItemsAsync()
		{
			var alcoholItems = await _context.AlcoholItems
				.Include(ai => ai.AlcoholFamily)
				.Include(ai => ai.Supplier)
				//.Include(ai => ai.OrderDetails)   => seulement si on veut s'avoir dans quelles commandes nos articles ont été ajoutés
				.ToListAsync();

			var alcoholItemsResponseDTO = _mapper.Map<IEnumerable<AlcoholItemResponseDTO>>(alcoholItems);

			return alcoholItemsResponseDTO;
		}

		public async Task<AlcoholItemResponseDTO> UpdateAlcoholItemAsync(Guid id, AlcoholItemRequestDTO alcoholItemRequestDTO)
		{
			var alcoholItem = await _context.AlcoholItems.FindAsync(id);
			if (alcoholItem == null)
			{
				throw new InvalidOperationException($"Unable to modify : alcoholItem '{id}' doesn't exists");
			}

			var alcoholFamily = await _context.AlcoholFamilies.FindAsync(alcoholItemRequestDTO.AlcoholFamilyId);
			if (alcoholFamily == null)
			{
				throw new InvalidOperationException($"Unable to modify : alcoholFamily named '{alcoholItemRequestDTO.AlcoholFamilyId}' already exsists");
			}

			var supplier = _context.Suppliers.SingleOrDefault(ai => ai.SupplierId == alcoholItemRequestDTO.SupplierId);
			if (supplier == null)
			{
				throw new InvalidOperationException($"Unable to modify : supplier '{alcoholItemRequestDTO.SupplierId}' doesn't exists");
			}

			var alcoholItemNameExist = await _context.AlcoholItems.SingleOrDefaultAsync(ai => ai.Name == alcoholItemRequestDTO.Name && ai.ItemId != id && ai.SupplierId == alcoholItemRequestDTO.SupplierId);
			if (alcoholItemNameExist != null)
			{
				throw new InvalidOperationException($"Unable to modify : alcoholItem named '{alcoholItem.Name}' already exsists");
			}

			_mapper.Map(alcoholItemRequestDTO, alcoholItem);
			await _context.SaveChangesAsync();

			var alcoholItemResponseDTO = _mapper.Map<AlcoholItemResponseDTO>(alcoholItem);

			return alcoholItemResponseDTO;
		}

		public async Task<IEnumerable<AlcoholItemResponseDTO>> GetAllAlcoholItemsBySupplierAsync(Guid supplierId)
		{
			var supplier = await _context.Suppliers.FindAsync(supplierId);
			if (supplier == null)
			{
				throw new InvalidOperationException($"Unable to get : supplier '{supplierId}' doesn't exists");
			}

			var alcoholItems = await _context.AlcoholItems
				.Include(i => i.Supplier)
				.Where(i => i.SupplierId == supplierId)
				.ToListAsync();

			var alcoholItemsResponseDTO = _mapper.Map<IEnumerable<AlcoholItemResponseDTO>>(alcoholItems);

			return alcoholItemsResponseDTO;
		}

	}
}
