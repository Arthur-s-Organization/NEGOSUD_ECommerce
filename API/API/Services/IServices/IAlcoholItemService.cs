using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface IAlcoholItemService
    {
        public Task<AlcoholItemResponseDTO> AddAlcoholItemAsync(AlcoholItemRequestDTO alcoholItemRequestDTO);
        public Task<IEnumerable<AlcoholItemResponseDTO>> GetAllAlcoholItemsAsync();
        public Task<AlcoholItemResponseDTO> GetAlcoholItemByIdAsync(Guid id);
        public Task<AlcoholItemResponseDTO> UpdateAlcoholItemAsync(Guid id, AlcoholItemRequestDTO alcoholItemRequestDTO);
        public Task<AlcoholItemResponseDTO> DeleteAlcoholItemAsync(Guid id);
        public Task<IEnumerable<AlcoholItemResponseDTO>> GetAllAlcoholItemsBySupplierAsync(Guid supplierId);

	}
}
