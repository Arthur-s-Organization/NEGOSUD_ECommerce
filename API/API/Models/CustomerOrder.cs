namespace API.Models
{
	public class CustomerOrder : Order
	{
		public Guid CustomerId { get; set; }

		public virtual Customer Customer { get; set; }
	}
}
