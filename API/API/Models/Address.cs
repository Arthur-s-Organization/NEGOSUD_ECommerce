namespace API.Models
{
	public class Address
	{
		public Guid AddressId { get; set; }
		public string StreetAddress { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
		public Customer? Customer { get; set; }
		public Supplier? Supplier { get; set; }
	}
}
