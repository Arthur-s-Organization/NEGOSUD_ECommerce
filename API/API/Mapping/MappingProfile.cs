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
			CreateMap<Customer, CustomerResponseDTO>();
			CreateMap<CustomerResponseDTO, Customer>();

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

			CreateMap<Item, ItemRequestDTO>();
			CreateMap<ItemRequestDTO, Item>();
			CreateMap<Item, ItemResponseDTO>();
			CreateMap<ItemResponseDTO, Item>();


			CreateMap<CustomerOrder, CustomerOrderRequestDTO>();
			CreateMap<CustomerOrderRequestDTO, CustomerOrder>();
			CreateMap<CustomerOrder, CustomerOrderResponseDTO>();
			CreateMap<CustomerOrderResponseDTO, CustomerOrder>();

			CreateMap<SupplierOrder, SupplierOrderRequestDTO>();
			CreateMap<SupplierOrderRequestDTO, SupplierOrder>();
			CreateMap<SupplierOrder, SupplierOrderResponseDTO>();
			CreateMap<SupplierOrderResponseDTO, SupplierOrder>();

			CreateMap<OrderDetail, OrderDetailResponseDTO>();
			CreateMap<OrderDetailResponseDTO, OrderDetail>();
		}

	}
}
