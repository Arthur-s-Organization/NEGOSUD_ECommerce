using API.Models.DTOs;
using API.Models;
using AutoMapper;
using API.Services;

namespace API.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Customer, CustomerDTO>();
			CreateMap<CustomerDTO, Customer>();

			CreateMap<Supplier, SupplierDTO>();
			CreateMap<SupplierDTO, Supplier>();

			CreateMap<Address, AddressDTO>();
			CreateMap<AddressDTO, Address>();

			CreateMap<AlcoholFamily, AlcoholFamilyDTO>();
			CreateMap<AlcoholFamilyDTO, AlcoholFamily>();

			CreateMap<AlcoholItem, AlcoholItemDTO>();
			CreateMap<AlcoholItemDTO, AlcoholItem>();
			
			CreateMap<CommonItem, CommonItemDTO>();
			CreateMap<CommonItemDTO, CommonItem>();
			
			CreateMap<CustomerOrder, CustomerOrderDTO>();
			CreateMap<CustomerOrderDTO, CustomerOrder>();
			
			CreateMap<SupplierOrder, SupplierOrderDTO>();
			CreateMap<SupplierOrderDTO, SupplierOrder>();
		}

	}
}
