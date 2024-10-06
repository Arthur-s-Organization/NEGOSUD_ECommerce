using API.Data;
using API.Models.DTOs.ResponseDTOs;
using API.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
	public class ItemService : IItemService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public ItemService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<ItemResponseDTO> DeleteItemAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ItemResponseDTO>> GetAllItemsAsync()
		{
			var alcoholItems = await _context.AlcoholItems
				.Include(i => i.Supplier)
				.ToListAsync();

			var commonItems = await _context.CommonItems
				.Include(i => i.Supplier)
				.ToListAsync();

			var alcoholItemsResponseDTO = _mapper.Map<IEnumerable<AlcoholItemResponseDTO>>(alcoholItems);
			var commonItemsResponseDTO = _mapper.Map<IEnumerable<CommonItemResponseDTO>>(commonItems);

			var allItemsResponseDTO = alcoholItemsResponseDTO.Cast<ItemResponseDTO>()
				.Concat(commonItemsResponseDTO.Cast<ItemResponseDTO>());

			return allItemsResponseDTO;
		}

		public async Task<ItemResponseDTO> GetItemByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ItemResponseDTO>> GetAllItemsBySupplierAsync(Guid supplierId)
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

			var commonItems = await _context.CommonItems
				.Include(i => i.Supplier)
				.Where(i => i.SupplierId == supplierId)
				.ToListAsync();

			var alcoholItemsResponseDTO = _mapper.Map<IEnumerable<AlcoholItemResponseDTO>>(alcoholItems);
			var commonItemsResponseDTO = _mapper.Map<IEnumerable<CommonItemResponseDTO>>(commonItems);

			var allItemsResponseDTO = alcoholItemsResponseDTO.Cast<ItemResponseDTO>()
				.Concat(commonItemsResponseDTO.Cast<ItemResponseDTO>());

			return allItemsResponseDTO;
		}
	}
}
