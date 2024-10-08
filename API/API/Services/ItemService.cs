using API.Data;
using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;
using API.Services.IServices;
using API.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
			var item = await _context.Items.FindAsync(id);
			if (item is null)
			{
				throw new InvalidOperationException($"Unable to delete : Item '{id}' doesn't exists");
			}
			_context.Items.Remove(item);
			await _context.SaveChangesAsync();

			var itemResponseDTO = _mapper.Map<ItemResponseDTO>(item);
			return itemResponseDTO;
		}

		public async Task<IEnumerable<ItemResponseDTO>> GetAllItemsAsync()
		{

			var items = await _context.Items
				.Include(i => i.AlcoholFamily)
				.Include(i => i.Supplier)
				.ToListAsync();

			var ItemsResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);
			return ItemsResponseDTO;
			//var items = await _context.Items
			//  .Include(i => i.Supplier) 
			//  .ToListAsync();

			//// Filtrer les AlcoholItem pour inclure AlcoholFamily uniquement pour eux
			//var alcoholItems = items.OfType<AlcoholItem>()
			//	.ToList();

			//foreach (var alcoholItem in alcoholItems)
			//{
			//	_context.Entry(alcoholItem).Reference(i => i.AlcoholFamily).Load();
			//}

			//var itemResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);

			//return itemResponseDTO;
		}

		public async Task<IEnumerable<ItemResponseDTO>> GetTopSellingItemsAsync(int topCount)
		{
			var items = await _context.Items
				.Include(i => i.Supplier)
				.OrderByDescending(i => i.QuantitySold)
			.Take(topCount)
			.ToListAsync();

			var itemResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);
			return itemResponseDTO;
		}

		public async Task<IEnumerable<ItemResponseDTO>> GetRecentlyAddedItemsAsync(int topCount)
		{
			var items = await _context.Items
				.Include(i => i.Supplier)
				.OrderByDescending(i => i.CreationDate)
			.Take(topCount)
			.ToListAsync();

			var itemResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);
			return itemResponseDTO;

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
				query = query.Where(i => i.AlcoholFamilyId == filters.AlcoholFamilyId.Value);
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
			var item = await _context.Items
				.Include(i => i.AlcoholFamily)
				.Include(i => i.Supplier)
				.SingleOrDefaultAsync(ai => ai.ItemId == id);
			if (item is null)
			{
				throw new InvalidOperationException($"Unable to get : Item '{id}' doesn't exists");
			}

			var itemResponseDTO = _mapper.Map<ItemResponseDTO>(item);
			return itemResponseDTO;
		}

		public async Task<IEnumerable<ItemResponseDTO>> GetAllItemsBySupplierAsync(Guid supplierId)
		{
			var supplier = await _context.Suppliers.FindAsync(supplierId);
			if (supplier == null)
			{
				throw new InvalidOperationException($"Unable to get : supplier '{supplierId}' doesn't exists");
			}

			var items = await _context.Items
				.Include(i => i.Supplier)
				.Where(i => i.SupplierId == supplierId)
				.ToListAsync();

			var itemsResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);

			return itemsResponseDTO;
		}


		public async Task<ItemResponseDTO> UpdateItemAsync(Guid id, ItemRequestDTO itemRequestDTO)
		{
			var item = await _context.Items.FindAsync(id);
			if (item == null)
			{
				throw new InvalidOperationException($"Unable to modify : Item '{id}' doesn't exists");
			}

			var alcoholFamily = await _context.AlcoholFamilies.FindAsync(itemRequestDTO.AlcoholFamilyId);
			if (alcoholFamily == null)
			{
				throw new InvalidOperationException($"Unable to modify : alcoholFamily named '{itemRequestDTO.AlcoholFamilyId}' already exsists");
			}

			var supplier = _context.Suppliers.SingleOrDefault(i => i.SupplierId == itemRequestDTO.SupplierId);
			if (supplier == null)
			{
				throw new InvalidOperationException($"Unable to modify : supplier '{itemRequestDTO.SupplierId}' doesn't exists");
			}

			var itemNameExist = await _context.Items.SingleOrDefaultAsync(i => i.Name == itemRequestDTO.Name && i.ItemId != id);
			if (itemNameExist != null)
			{
				throw new InvalidOperationException($"Unable to modify : Item named '{item.Name}' already exsists");
			}

			_mapper.Map(itemRequestDTO, item);
			await _context.SaveChangesAsync();

			var itemResponseDTO = _mapper.Map<ItemResponseDTO>(item);

			return itemResponseDTO;
		}



		public async Task<ItemResponseDTO> AddItemAsync(ItemRequestDTO itemRequestDTO, IFormFile imageFile)
		{
			var alcoholFamily = _context.AlcoholFamilies.SingleOrDefault(ai => ai.AlcoholFamilyId == itemRequestDTO.AlcoholFamilyId);

			if (alcoholFamily == null && itemRequestDTO.AlcoholFamilyId != null)
			{
				throw new InvalidOperationException($"Unable to add : alcoholfamily '{itemRequestDTO.AlcoholFamilyId}' doesn't exists");
			}

			var supplier = _context.Suppliers.SingleOrDefault(ai => ai.SupplierId == itemRequestDTO.SupplierId);

			if (supplier == null)
			{
				throw new InvalidOperationException($"Unable to add : supplier '{itemRequestDTO.SupplierId}' doesn't exists");
			}

			var itemNameExist = await _context.Items.SingleOrDefaultAsync(ai => ai.Name == itemRequestDTO.Name);
			if (itemNameExist != null)
			{
				throw new InvalidOperationException($"Unable to add : a alcoholItem named '{itemRequestDTO.Name}' already exsists");
			}


			var item = _mapper.Map<Item>(itemRequestDTO);
			item.Slug = SlugHelper.GenerateSlug(item.Name);
			item.CreationDate = DateTime.Now;


			if (imageFile != null && imageFile.Length > 0)
			{
				using var memoryStream = new MemoryStream();
				await imageFile.CopyToAsync(memoryStream);
				item.ItemImage = memoryStream.ToArray(); 
			}


			await _context.Items.AddAsync(item);
			await _context.SaveChangesAsync();

			var alcoholItemResponseDTO = _mapper.Map<ItemResponseDTO>(item);

			return alcoholItemResponseDTO;
		}

		public async Task<IEnumerable<ItemResponseDTO>> GetItemsByNameAsync(string name)
		{
			var items = await _context.Items.Where(i => i.Name.Contains(name)).ToListAsync();

			var ItemsResponseDTO = _mapper.Map<IEnumerable<ItemResponseDTO>>(items);
			return ItemsResponseDTO;
		}

	}
}
