namespace API.Models.DTOs.RequestDTOs
{
	public class UpdateCustomerRequestDTO
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		public Guid AddressId { get; set; }
		public string Email { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
