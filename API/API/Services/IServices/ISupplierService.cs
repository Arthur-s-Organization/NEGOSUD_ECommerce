using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface ISupplierService
    {
        public Task<SupplierResponseDTO> AddSupplierAsync(SupplierRequestDTO supplierRequestDTO);
        public Task<IEnumerable<SupplierResponseDTO>> GetAllSuppliersAsync();
        public Task<SupplierResponseDTO> GetSupplierByIdAsync(Guid id);
        public Task<SupplierResponseDTO> UpdateSupplierAsync(Guid id, SupplierRequestDTO supplierRequestDTO);
        public Task<SupplierResponseDTO> DeleteSupplierAsync(Guid id);
        public Task<SupplierResponseDTO> AddAdressToSupplierAsync(Guid supplierId, Guid adressId);
    }
}

