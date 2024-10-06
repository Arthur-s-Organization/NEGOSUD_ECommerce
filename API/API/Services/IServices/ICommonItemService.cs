﻿using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface ICommonItemService
    {
        public Task<CommonItemResponseDTO> AddCommonItemAsync(CommonItemRequestDTO CommonItemRequestDTO);
        public Task<IEnumerable<CommonItemResponseDTO>> GetAllCommonItemsAsync();
        public Task<CommonItemResponseDTO> GetCommonItemByIdAsync(Guid id);
        public Task<CommonItemResponseDTO> UpdateCommonItemAsync(Guid id, CommonItemRequestDTO CommonItemRequestDTO);
        public Task<CommonItemResponseDTO> DeleteCommonItemAsync(Guid id);
        public Task<IEnumerable<CommonItemResponseDTO>> GetAllCommonItemsBySupplierAsync(Guid supplierId);

	}
}
