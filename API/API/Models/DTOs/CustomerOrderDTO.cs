namespace API.Models.DTOs
{
	public class CustomerOrderDTO
	{
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }

		public Guid CustomerId { get; set; }
	}
}
