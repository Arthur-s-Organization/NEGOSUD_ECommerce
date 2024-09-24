using API.Data;
using API.Models.DTOs;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
		public async Task<Supplier> AddSupplierAsync(SupplierDTO SupplierDTO)
		{
			var Supplier = _mapper.Map<Supplier>(SupplierDTO);
			await _context.Suppliers.AddAsync(Supplier);
			await _context.SaveChangesAsync();

			return Supplier;
		}

		public async Task<Supplier> DeleteSupplierAsync(Guid id)
		{
			var Supplier = await _context.Suppliers.SingleOrDefaultAsync(c => c.SupplierId == id);
			if (Supplier is null)
			{
				return null;
			}
			_context.Suppliers.Remove(Supplier);
			await _context.SaveChangesAsync();
			return Supplier;
		}

		public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
		{
			var Suppliers = await _context.Suppliers.Include(s => s.Address).Include(s => s.Items).Include(s => s.SupplierOrders).ToListAsync();
			return Suppliers;
		}

		public async Task<Supplier> GetSupplierByIdAsync(Guid id)
		{
			var Supplier = await _context.Suppliers.Include(s => s.Address).Include(s => s.Items).Include(s => s.SupplierOrders).SingleOrDefaultAsync(c => c.SupplierId == id);
			if (Supplier is null)
			{
				return null;
			}
			return Supplier;
		}

		public async Task<Supplier> UpdateSupplierAsync(Guid id, SupplierDTO SupplierDTO)
		{
			var existingSupplier = await _context.Suppliers.FindAsync(id);
			if (existingSupplier == null)
			{
				return null;
			}

			_mapper.Map(SupplierDTO, existingSupplier);

			await _context.SaveChangesAsync();
			return existingSupplier;
		}

		public async Task<Supplier> AddAdressToSupplierAsync(Guid SupplierId, Guid AdressId)
		{
			var Supplier = await _context.Suppliers
				.Include(s => s.Address)
				.SingleOrDefaultAsync(s => s.SupplierId == SupplierId);

			var Adress = await _context.Addresses
				.Include(a => a.Supplier)
				.Include(a => a.Customer)
				.SingleOrDefaultAsync(a => a.AddressId == AdressId);

			if (Supplier == null || Adress == null || Supplier.Address != null || Adress.Supplier != null || Adress.Customer != null)
			{
				return null;  // Ou une autre gestion des erreurs, à voir
			}

			Supplier.Address = Adress;
			await _context.SaveChangesAsync();

			return Supplier;
		}

	}
}
