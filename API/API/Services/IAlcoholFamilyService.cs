using API.Models.DTOs;
using API.Models;

namespace API.Services
{
	public interface IAlcoholFamilyService
	{
		public Task<AlcoholFamily> AddAlcoholFamilyAsync(AlcoholFamilyDTO AlcoholFamilyDTO);
		public Task<IEnumerable<AlcoholFamily>> GetAllAlcoholFamilysAsync();
		public Task<AlcoholFamily> GetAlcoholFamilyByIdAsync(Guid id);
		public Task<AlcoholFamily> UpdateAlcoholFamilyAsync(Guid id, AlcoholFamilyDTO AlcoholFamilyDTO);
		public Task<AlcoholFamily> DeleteAlcoholFamilyAsync(Guid id);
	}
}
