using API.Models;
using AutoMapper;
using API.Services;
using API.Models.DTOs.ResponseDTOs;
using API.Models.DTOs.RequestDTOs;

namespace API.Mapping
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Customer, CustomerRequestDTO>();
			CreateMap<CustomerRequestDTO, Customer>();

			CreateMap<Supplier, SupplierRequestDTO>();
			CreateMap<SupplierRequestDTO, Supplier>();
			CreateMap<Supplier, SupplierResponseDTO>();
			CreateMap<SupplierResponseDTO, Supplier>();

			CreateMap<Address, AddressRequestDTO>();
			CreateMap<AddressRequestDTO, Address>();
			CreateMap<Address, AddressResponseDTO>();
			CreateMap<AddressResponseDTO, Address>();


			CreateMap<AlcoholFamily, AlcoholFamilyRequestDTO>();
			CreateMap<AlcoholFamilyRequestDTO, AlcoholFamily>();
			CreateMap<AlcoholFamily, AlcoholFamilyResponseDTO>();
			CreateMap<AlcoholFamilyResponseDTO, AlcoholFamily>();


			CreateMap<AlcoholItem, AlcoholItemRequestDTO>();
			CreateMap<AlcoholItemRequestDTO, AlcoholItem>();
			CreateMap<AlcoholItem, AlcoholItemResponseDTO>();
			CreateMap<AlcoholItemResponseDTO, AlcoholItem>();
			
			CreateMap<CommonItem, CommonItemRequestDTO>();
			CreateMap<CommonItemRequestDTO, CommonItem>();
			CreateMap<CommonItem, CommonItemResponseDTO>();
			CreateMap<CommonItemResponseDTO, CommonItem>();

			//CreateMap<AlcoholItem, ItemResponseDTO>()
			//.Include<AlcoholItem, AlcoholItemResponseDTO>();

			//CreateMap<CommonItem, ItemResponseDTO>()
			//	.Include<CommonItem, CommonItemResponseDTO>();

			CreateMap<CustomerOrder, CustomerOrderRequestDTO>();
			CreateMap<CustomerOrderRequestDTO, CustomerOrder>();
			
			CreateMap<SupplierOrder, SupplierOrderRequestDTO>();
			CreateMap<SupplierOrderRequestDTO, SupplierOrder>();
		}

	}
}
