using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using API.Models.DTOs.ResponseDTOs;
using API.Utils;

namespace API.Services
{
	public class SupplierService : ISupplierService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public SupplierService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<SupplierResponseDTO> AddSupplierAsync(SupplierRequestDTO supplierRequestDTO)
		{
			var adress = await _context.Addresses
				.Include(a => a.Supplier)
				.Include(a => a.Customer)
				.SingleOrDefaultAsync(a => a.AddressId == supplierRequestDTO.AddressId);

			

			if (adress == null)
			{
				throw new ValidationException($"Unable to add adress : adress '{supplierRequestDTO.AddressId}' doesn't exists");
			}

			if (adress.Supplier != null)
			{
				throw new ValidationException($"Unable to add adress :  adress '{supplierRequestDTO.AddressId}' already own to an other supplier");
			}

			if (adress.Customer != null)
			{
				throw new ValidationException($"Unable to add adress :  adress '{supplierRequestDTO.AddressId}' already own to a customer");
			}

			var supplierNameExist = await _context.Suppliers.SingleOrDefaultAsync(s => s.Name == supplierRequestDTO.Name);
			if (supplierNameExist != null)
			{
				throw new ValidationException($"Unable to add : a supplier named '{supplierRequestDTO.Name}' already exsists");
			}

			var supplier = _mapper.Map<Supplier>(supplierRequestDTO);
			await _context.Suppliers.AddAsync(supplier);
			await _context.SaveChangesAsync();

			var supplierResponseDTO = _mapper.Map<SupplierResponseDTO>(supplier);

			return supplierResponseDTO;
		}

		public async Task<SupplierResponseDTO> DeleteSupplierAsync(Guid id)
		{
			var supplier = await _context.Suppliers.FindAsync(id);
			if (supplier is null)
			{
				throw new ValidationException($"Unable to delete : supplier '{id}' doesn't exists");
			}

			_context.Suppliers.Remove(supplier);
			await _context.SaveChangesAsync();

			var supplierResponseDTO = _mapper.Map<SupplierResponseDTO>(supplier);

			return supplierResponseDTO;
		}

		public async Task<IEnumerable<SupplierResponseDTO>> GetAllSuppliersAsync()
		{
			var suppliers = await _context.Suppliers.Include(s => s.Address).ToListAsync();

			var suppliersResponseDTO = _mapper.Map<IEnumerable<SupplierResponseDTO>>(suppliers);

			return suppliersResponseDTO;
		}

		public async Task<SupplierResponseDTO> GetSupplierByIdAsync(Guid id)
		{
			var supplier = await _context.Suppliers.Include(s => s.Address).Include(s => s.Items).Include(s => s.SupplierOrders).SingleOrDefaultAsync(s => s.SupplierId == id);
			if (supplier is null)
			{
				throw new ValidationException($"Unable to get : supplier '{id}' doesn't exists");
			}

			var supplierResponseDTO = _mapper.Map<SupplierResponseDTO>(supplier);

			return supplierResponseDTO;
		}

		public async Task<SupplierResponseDTO> UpdateSupplierAsync(Guid id, SupplierRequestDTO supplierRequestDTO)
		{
			var supplier = await _context.Suppliers.FindAsync(id);
			if (supplier == null)
			{
				throw new ValidationException($"Unable to modify : supplier '{id}' doesn't exists");
			}

			var supplierNameExist = await _context.Suppliers.SingleOrDefaultAsync(s => s.Name == supplierRequestDTO.Name && s.SupplierId != id);
			if (supplierNameExist != null)
			{
				throw new ValidationException($"Unable to modify : supplier named '{supplierRequestDTO.Name}' already exsists");
			}

			_mapper.Map(supplierRequestDTO, supplier);
			await _context.SaveChangesAsync();

			var supplierResponseDTO = _mapper.Map<SupplierResponseDTO>(supplier);

			return supplierResponseDTO;
		}
	}
}
