namespace API.Models.DTOs
{
	public class CustomerDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
	}
}
