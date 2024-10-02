using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface ICommonItemService
    {
        public Task<CommonItem> AddCommonItemAsync(CommonItemRequestDTO CommonItemDTO);
        public Task<IEnumerable<CommonItem>> GetAllCommonItemsAsync();
        public Task<CommonItem> GetCommonItemByIdAsync(Guid id);
        public Task<CommonItem> UpdateCommonItemAsync(Guid id, CommonItemRequestDTO CommonItemDTO);
        public Task<CommonItem> DeleteCommonItemAsync(Guid id);
        public Task<IEnumerable<CommonItemResponseDTO>> GetAllCommonItemsBySupplierAsync(Guid supplierId);

	}
}
