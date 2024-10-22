namespace API.Models.DTOs.RequestDTOs
{
    public class CustomerRequestDTO
    {
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
		public Guid? AddressId { get; set; }
	}
}
