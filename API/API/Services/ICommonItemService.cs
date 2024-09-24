using API.Models.DTOs;
using API.Models;

namespace API.Services
{
	public interface ICommonItemService
	{
		public Task<CommonItem> AddCommonItemAsync(CommonItemDTO CommonItemDTO);
		public Task<IEnumerable<CommonItem>> GetAllCommonItemsAsync();
		public Task<CommonItem> GetCommonItemByIdAsync(Guid id);
		public Task<CommonItem> UpdateCommonItemAsync(Guid id, CommonItemDTO CommonItemDTO);
		public Task<CommonItem> DeleteCommonItemAsync(Guid id);
	}
}
