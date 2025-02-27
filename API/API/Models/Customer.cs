﻿using Microsoft.AspNetCore.Identity;

namespace API.Models
{
	public class Customer : IdentityUser
	{
		//public Guid CustomerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		//public string EmailAddress { get; set; }

		//public string PhoneNumber { get; set; }
		public virtual IEnumerable<CustomerOrder>? CustomerOrders { get; set; } = new HashSet<CustomerOrder>();
		public virtual Address? Address { get; set; }
		public Guid? AddressId { get; set; }
	}
}
