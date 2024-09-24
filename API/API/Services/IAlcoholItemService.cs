using API.Models.DTOs;
using API.Models;

namespace API.Services
{
	public interface IAlcoholItemService
	{
		public Task<AlcoholItem> AddAlcoholItemAsync(AlcoholItemDTO AlcoholItemDTO);
		public Task<IEnumerable<AlcoholItem>> GetAllAlcoholItemsAsync();
		public Task<AlcoholItem> GetAlcoholItemByIdAsync(Guid id);
		public Task<AlcoholItem> UpdateAlcoholItemAsync(Guid id, AlcoholItemDTO AlcoholItemDTO);
		public Task<AlcoholItem> DeleteAlcoholItemAsync(Guid id);
	}
}
