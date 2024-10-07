using API.Data;
using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;
using API.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

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
			var items = await _context.Items
			  .Include(i => i.Supplier) 
			  .ToListAsync();

			// Filtrer les AlcoholItem pour inclure AlcoholFamily uniquement pour eux
			var alcoholItems = items.OfType<AlcoholItem>()
				.ToList();

			foreach (var alcoholItem in alcoholItems)
			{
				_context.Entry(alcoholItem).Reference(i => i.AlcoholFamily).Load();
			}

			var itemResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);

			return itemResponseDTO;


			//var alcoholItems = await _context.AlcoholItems
			//	.Include(i => i.Supplier)
			//	.Include(i => i.AlcoholFamily)
			//	.ToListAsync();

			//var commonItems = await _context.CommonItems
			//	.Include(i => i.Supplier)
			//	.ToListAsync();

			//var alcoholItemsResponseDTO = _mapper.Map<IEnumerable<AlcoholItemResponseDTO>>(alcoholItems);
			//var commonItemsResponseDTO = _mapper.Map<IEnumerable<CommonItemResponseDTO>>(commonItems);

			//var allItemsResponseDTO = alcoholItemsResponseDTO.Cast<ItemResponseDTO>()
			//	.Concat(commonItemsResponseDTO.Cast<ItemResponseDTO>());

			//return allItemsResponseDTO;
		}

		public async Task<IEnumerable<ItemResponseDTO>> GetTopSellingItemsAsync(int topCount)
		{
			var items = await _context.Items
				.Include(i => i.Supplier)
				.OrderByDescending(i => i.QuantitySold)
			.Take(topCount)
			.ToListAsync();

			var alcoholItems = items.OfType<AlcoholItem>()
				.ToList();

			foreach (var alcoholItem in alcoholItems)
			{
				_context.Entry(alcoholItem).Reference(i => i.AlcoholFamily).Load();
			}

			var ItemResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);
			return ItemResponseDTO;

		}

		public async Task<IEnumerable<ItemResponseDTO>> GetRecentlyAddedItemsAsync(int topCount)
		{
			var items = await _context.Items
				.Include(i => i.Supplier)
				.OrderByDescending(i => i.CreationDate)
			.Take(topCount)
			.ToListAsync();

			var alcoholItems = items.OfType<AlcoholItem>()
				.ToList();

			foreach (var alcoholItem in alcoholItems)
			{
				_context.Entry(alcoholItem).Reference(i => i.AlcoholFamily).Load();
			}

			var ItemResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);
			return ItemResponseDTO;

		}

		public async Task<IEnumerable<ItemResponseDTO>> GetFilteredItemsAsync(ItemFilterRequestDTO filters)
		{
			if (filters.MaxPrice < filters.MinPrice)
			{
				throw new InvalidOperationException($"Unable to get : Max Price is inferior to Min Price");
			}
			var query = _context.Items
				.Include(ai => ai.Supplier)
				.AsQueryable();

			if (filters.AlcoholFamilyId.HasValue)
			{
				query = query.OfType<AlcoholItem>().Include(a => a.AlcoholFamily).Where(ai => ai.AlcoholFamilyId == filters.AlcoholFamilyId.Value);
			}

			if (filters.SupplierId.HasValue)
			{
				query = query.Where(ai => ai.SupplierId == filters.SupplierId.Value);
			}

			if (filters.MinPrice.HasValue)
			{
				query = query.Where(ai => ai.Price >= filters.MinPrice.Value);
			}

			if (filters.MaxPrice.HasValue)
			{
				query = query.Where(ai => ai.Price <= filters.MaxPrice.Value);
			}

			var filteredItems = await query.ToListAsync();

			var itemsResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(filteredItems);

			return itemsResponseDTO;

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
