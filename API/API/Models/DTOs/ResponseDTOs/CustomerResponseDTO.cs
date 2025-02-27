﻿namespace API.Models.DTOs.ResponseDTOs
{
	public class CustomerResponseDTO
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		//public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public virtual AddressResponseDTO Address { get; set; }
	}
}
