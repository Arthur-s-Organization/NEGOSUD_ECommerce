﻿using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services.IServices
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerResponseDTO>> GetAllCustomersAsync();
        public Task<CustomerResponseDTO> GetCustomerByIdAsync(string id);
        public Task<CustomerResponseDTO> DeleteCustomerAsync(string id);
    }
}
