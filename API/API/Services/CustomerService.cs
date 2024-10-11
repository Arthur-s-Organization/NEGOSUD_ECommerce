﻿using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;

namespace API.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public CustomerService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<CustomerResponseDTO> AddCustomerAsync(CustomerRequestDTO customerRequestDTO)
		{
			var adress = await _context.Addresses
			.Include(a => a.Supplier)
			.Include(a => a.Customer)
			.SingleOrDefaultAsync(a => a.AddressId == customerRequestDTO.AddressId);

			if (adress == null)
			{
				throw new InvalidOperationException($"Unable to add adress : adress '{customerRequestDTO.AddressId}' doesn't exists");
			}


			if (adress.Supplier != null)
			{
				throw new InvalidOperationException($"Unable to add adress :  adress '{customerRequestDTO.AddressId}' already belongs to a supplier");
			}

			if (adress.Customer != null)
			{
				throw new InvalidOperationException($"Unable to add adress :  adress '{customerRequestDTO.AddressId}' already belongs to an other customer");
			}


			var customer = _mapper.Map<Customer>(customerRequestDTO);
			await _context.Customers.AddAsync(customer);
			await _context.SaveChangesAsync();

			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}

		public async Task<CustomerResponseDTO> DeleteCustomerAsync(string id)
		{
			var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
			if (customer is null)
			{
				throw new InvalidOperationException($"Unable to delete : customer '{id}' doesn't exists");
			}
			_context.Customers.Remove(customer);
			await _context.SaveChangesAsync();

			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}

		public async Task<IEnumerable<CustomerResponseDTO>> GetAllCustomersAsync()
		{
			var customers = await _context.Customers.Include(c => c.Address).ToListAsync();

			var custmersResponseDTO = _mapper.Map<IEnumerable<CustomerResponseDTO>>(customers);

			return custmersResponseDTO;
		}

		public async Task<CustomerResponseDTO> GetCustomerByIdAsync(string id)
		{
			var customer = await _context.Customers.Include(c => c.Address).SingleOrDefaultAsync(c => c.Id == id);
			if (customer is null)

			{
				throw new InvalidOperationException($"Unable to get : customer '{id}' doesn't exists");
			}
			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}

		public async Task<CustomerResponseDTO> UpdateCustomerAsync(string id, CustomerRequestDTO customerRequestDTO)
		{

			var customer = await _context.Customers
				.Include(c => c.Address)
				.SingleOrDefaultAsync(c => c.Id == id);
			if (customer == null)
			{
				throw new InvalidOperationException($"Unable to modify : customer '{id}' doesn't exists");
			}

			_mapper.Map(customerRequestDTO, customer);

			await _context.SaveChangesAsync();
			var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(customer);
			return customerResponseDTO;
		}

	}

}