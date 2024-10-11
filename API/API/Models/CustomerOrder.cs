namespace API.Models
{
	public class CustomerOrder : Order
	{
		public string CustomerId { get; set; }

		public virtual Customer Customer { get; set; }
	}
}
