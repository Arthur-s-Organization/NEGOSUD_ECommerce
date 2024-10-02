using API.Data;
using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;
using API.Services.IServices;
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
		public async Task<CommonItemResponseDTO> AddCommonItemAsync(CommonItemRequestDTO commonItemRequestDTO)
		{
			var Supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == commonItemRequestDTO.SupplierId);

			if (Supplier == null)
			{
				throw new InvalidOperationException($"Unable to add : supplier '{commonItemRequestDTO.SupplierId}' doesn't exists");
			}

			var commonItemNameExist = await _context.AlcoholItems.SingleOrDefaultAsync(ai => ai.Name == commonItemRequestDTO.Name && ai.SupplierId == commonItemRequestDTO.SupplierId);
			if (commonItemNameExist != null)
			{
				throw new InvalidOperationException($"Unable to add : a commonItem named '{commonItemRequestDTO.Name}' already exsists for supplier '{commonItemRequestDTO.SupplierId}'");
			}

			var commonItem = _mapper.Map<CommonItem>(commonItemRequestDTO);
			commonItem.Slug = SlugHelper.GenerateSlug(commonItem.Name);
			commonItem.CreationDate = DateTime.Now;
			await _context.CommonItems.AddAsync(commonItem);
			await _context.SaveChangesAsync();

			var commonItemResponseDTO = _mapper.Map<CommonItemResponseDTO>(commonItem);
			return commonItemResponseDTO;


		}

		public async Task<CommonItemResponseDTO> DeleteCommonItemAsync(Guid id)
		{
			var commonItem = await _context.CommonItems.SingleOrDefaultAsync(ci => ci.ItemId == id);
			if (commonItem is null)
			{
				throw new InvalidOperationException($"Unable to delete : commonItem '{id}' doesn't exists");
			}
			_context.CommonItems.Remove(commonItem);
			await _context.SaveChangesAsync();

			var commonItemResponseDTO = _mapper.Map<CommonItemResponseDTO>(commonItem);
			return commonItemResponseDTO;
		}

		public async Task<CommonItemResponseDTO> GetCommonItemByIdAsync(Guid id)
		{
			var commonItem = await _context.CommonItems
				.Include(ai => ai.Supplier)
				.SingleOrDefaultAsync(ci => ci.ItemId == id);
			if (commonItem is null)
			{
				throw new InvalidOperationException($"Unable to delete : clcoholItem '{id}' doesn't exists");
			}

			var commonItemResponseDTO = _mapper.Map<CommonItemResponseDTO>(commonItem);
			return commonItemResponseDTO;
		}

		public async Task<IEnumerable<CommonItemResponseDTO>> GetAllCommonItemsAsync()
		{
			var commonItems = await _context.CommonItems
				.Include(ai => ai.Supplier)
				.ToListAsync();

			var commonItemsResponseDTO = _mapper.Map<IEnumerable<CommonItemResponseDTO>>(commonItems);
			return commonItemsResponseDTO;
		}

		public async Task<CommonItemResponseDTO> UpdateCommonItemAsync(Guid id, CommonItemRequestDTO CommonItemRequestDTO)
		{
			var commonItem = await _context.CommonItems.FindAsync(id);

			if (commonItem == null)
			{
				throw new InvalidOperationException($"Unable to modify : commonItem '{id}' doesn't exists");
			}

			_mapper.Map(CommonItemRequestDTO, commonItem);
			await _context.SaveChangesAsync();

			var commonItemResponseDTO = _mapper.Map<CommonItemResponseDTO>(commonItem);
			return commonItemResponseDTO;

		}

		public async Task<IEnumerable<CommonItemResponseDTO>> GetAllCommonItemsBySupplierAsync(Guid supplierId)
		{
			var supplier = await _context.Suppliers.FindAsync(supplierId);
			if (supplier == null)
			{
				throw new InvalidOperationException($"Unable to get : supplier '{supplierId}' doesn't exists");
			}

			var commonItems = await _context.CommonItems
				.Include(i => i.Supplier)
				.Where(i => i.SupplierId == supplierId)
				.ToListAsync();

			var commonItemsResponseDTO = _mapper.Map<IEnumerable<CommonItemResponseDTO>>(commonItems);

			return commonItemsResponseDTO;
		}
	}
}
