using API.Models;
using API.Models.DTOs.RequestDTOs;

namespace API.Services.IServices
{
    public interface IAlcoholItemService
    {
        public Task<AlcoholItem> AddAlcoholItemAsync(AlcoholItemRequestDTO AlcoholItemDTO);
        public Task<IEnumerable<AlcoholItem>> GetAllAlcoholItemsAsync();
        public Task<AlcoholItem> GetAlcoholItemByIdAsync(Guid id);
        public Task<AlcoholItem> UpdateAlcoholItemAsync(Guid id, AlcoholItemRequestDTO AlcoholItemDTO);
        public Task<AlcoholItem> DeleteAlcoholItemAsync(Guid id);
    }
}
