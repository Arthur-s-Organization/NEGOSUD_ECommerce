using API.Data;
using API.Models;
using API.Models.DTOs;
using API.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
	public class CommonItemService : ICommonItemService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public CommonItemService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<CommonItem> AddCommonItemAsync(CommonItemDTO CommonItemDTO)
		{
			var Supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == CommonItemDTO.SupplierId);

			if (Supplier == null)
			{
				return null;
			}

			var commonItem = _mapper.Map<CommonItem>(CommonItemDTO);

			commonItem.Slug = SlugHelper.GenerateSlug(commonItem.Name);

			await _context.CommonItems.AddAsync(commonItem);
			await _context.SaveChangesAsync();

			return commonItem;


		}

		public async Task<CommonItem> DeleteCommonItemAsync(Guid id)
		{
			var CommonItem = await _context.CommonItems.SingleOrDefaultAsync(ci => ci.ItemId == id);
			if (CommonItem is null)
			{
				return null;
			}
			_context.CommonItems.Remove(CommonItem);
			await _context.SaveChangesAsync();
			return CommonItem;
		}

		public async Task<CommonItem> GetCommonItemByIdAsync(Guid id)
		{
			var CommonItem = await _context.CommonItems
				.Include(ai => ai.Supplier)
				.SingleOrDefaultAsync(ci => ci.ItemId == id);
			if (CommonItem is null)
			{
				return null;
			}
			return CommonItem;
		}

		public async Task<IEnumerable<CommonItem>> GetAllCommonItemsAsync()
		{
			var CommonItems = await _context.CommonItems
				.Include(ai => ai.Supplier)
				.ToListAsync();
			return CommonItems;
		}

		public async Task<CommonItem> UpdateCommonItemAsync(Guid id, CommonItemDTO CommonItemDTO)
		{
			var existingCommonItem = await _context.CommonItems.FindAsync(id);

			if (existingCommonItem == null)
			{
				return null;
			}

			_mapper.Map(CommonItemDTO, existingCommonItem);

			await _context.SaveChangesAsync();
			return existingCommonItem;

		}
	}
}
