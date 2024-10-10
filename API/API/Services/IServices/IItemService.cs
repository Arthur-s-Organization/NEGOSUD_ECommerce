using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
	public interface IItemService
	{
		public Task<IEnumerable<ItemResponseDTO>> GetAllItemsAsync();
		public Task<ItemResponseDTO> GetItemBySlugAsync(string slug);
		public Task<IEnumerable<ItemResponseDTO>> GetAllItemsBySupplierAsync(Guid supplierId);
		public Task<ItemResponseDTO> DeleteItemAsync(Guid id);
		public Task<IEnumerable<ItemResponseDTO>> GetTopSellingItemsAsync(int topCount);
		public Task<IEnumerable<ItemResponseDTO>> GetRecentlyAddedItemsAsync(int topCount);
		public Task<IEnumerable<ItemResponseDTO>> GetFilteredItemsAsync(ItemFilterRequestDTO filters);
		public Task<ItemResponseDTO> UpdateItemAsync(Guid id, ItemRequestDTO itemRequestDTO);
		public Task<ItemResponseDTO> AddItemAsync(ItemRequestDTO itemRequestDTO);
		public Task<IEnumerable<ItemResponseDTO>> GetItemsByNameAsync(string name);

	}
}
