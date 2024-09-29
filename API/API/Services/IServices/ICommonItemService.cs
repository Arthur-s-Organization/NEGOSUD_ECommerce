using API.Models;
using API.Models.DTOs.RequestDTOs;

namespace API.Services.IServices
{
    public interface ICommonItemService
    {
        public Task<CommonItem> AddCommonItemAsync(CommonItemRequestDTO CommonItemDTO);
        public Task<IEnumerable<CommonItem>> GetAllCommonItemsAsync();
        public Task<CommonItem> GetCommonItemByIdAsync(Guid id);
        public Task<CommonItem> UpdateCommonItemAsync(Guid id, CommonItemRequestDTO CommonItemDTO);
        public Task<CommonItem> DeleteCommonItemAsync(Guid id);
    }
}
