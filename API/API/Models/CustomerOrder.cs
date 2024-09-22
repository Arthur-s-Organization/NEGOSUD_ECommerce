namespace API.Models
{
	public class CustomerOrder
	{
		public Guid CustomerId { get; set; }

		public virtual Customer Customer { get; set; }
	}
}
