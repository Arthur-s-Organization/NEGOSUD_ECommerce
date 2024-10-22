namespace API.Models
{
	public class Order
	{
		public Guid OrderID { get; set; }
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }
		public virtual IEnumerable<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
	}
}
