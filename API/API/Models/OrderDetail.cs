namespace API.Models
{
	public class OrderDetail
	{
		public Guid OrderId { get; set; }
		public virtual Order Order { get; set; }
		public Guid ItemId { get; set; }
		public virtual Item Item { get; set; }
		public int Quantity { get; set; }
	}
}
