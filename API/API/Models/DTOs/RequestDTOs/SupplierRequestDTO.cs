namespace API.Models.DTOs.RequestDTOs
{
    public class SupplierRequestDTO
    {
        public string Name { get; set; }
		public string Description { get; set; }
		public string PhoneNumber { get; set; }
		public bool IsActive { get; set; }
		public Guid?  AddressId { get; set; }
	}
}
