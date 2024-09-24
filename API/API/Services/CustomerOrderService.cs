using API.Data;
using API.Models;
using API.Models.DTOs;
using AutoMapper;

namespace API.Services
{
	public class CustomerOrderService : ICustomerOrderService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public CustomerOrderService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public Task<CustomerOrder> AddCustomerOrderAsync(CustomerOrderDTO CustomerOrderDTO)
		{
			throw new NotImplementedException();
		}

		public Task<CustomerOrder> DeleteCustomerOrderAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<CustomerOrder>> GetAllCustomerOrdersAsync()
		{
			throw new NotImplementedException();
		}

		public Task<CustomerOrder> GetCustomerOrderByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<CustomerOrder> UpdateCustomerOrderAsync(Guid id, CustomerOrderDTO CustomerOrderDTO)
		{
			throw new NotImplementedException();
		}
	}
}
