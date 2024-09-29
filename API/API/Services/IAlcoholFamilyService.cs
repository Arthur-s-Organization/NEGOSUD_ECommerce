using API.Models;
using API.Models.DTOs.ResponseDTOs;
using API.Models.DTOs.RequestDTOs;

namespace API.Services
{
    public interface IAlcoholFamilyService
	{
		public Task<AlcoholFamilyResponseDTO> AddAlcoholFamilyAsync(AlcoholFamilyRequestDTO AlcoholFamilyDTO);
		public Task<IEnumerable<AlcoholFamilyResponseDTO>> GetAllAlcoholFamiliesAsync();
		public Task<AlcoholFamilyResponseDTO> GetAlcoholFamilyByIdAsync(Guid id);
		public Task<AlcoholFamilyResponseDTO> UpdateAlcoholFamilyAsync(Guid id, AlcoholFamilyRequestDTO AlcoholFamilyDTO);
		public Task<AlcoholFamilyResponseDTO> DeleteAlcoholFamilyAsync(Guid id);
	}
}
