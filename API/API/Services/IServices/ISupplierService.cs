using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface ISupplierService
    {
        public Task<SupplierResponseDTO> AddSupplierAsync(SupplierRequestDTO SupplierDTO);
        public Task<IEnumerable<SupplierResponseDTO>> GetAllSuppliersAsync();
        public Task<SupplierResponseDTO> GetSupplierByIdAsync(Guid id);
        public Task<SupplierResponseDTO> UpdateSupplierAsync(Guid id, SupplierRequestDTO SupplierDTO);
        public Task<SupplierResponseDTO> DeleteSupplierAsync(Guid id);
        public Task<SupplierResponseDTO> AddAdressToSupplierAsync(Guid SupplierId, Guid AdressId);
    }
}

