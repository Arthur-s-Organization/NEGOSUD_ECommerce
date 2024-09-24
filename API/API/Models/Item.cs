namespace API.Models
{
	public class Item
	{
		public Guid ItemId { get; set; }
		public string Name { get; set; }
		public int Stock { get; set; }
		public string Description { get; set; }
		public float Price
		{ get; set; }
		public string OriginCountry { get; set; }
		public Guid SupplierId { get; set; }
		public virtual Supplier Supplier { get; set; }

		public virtual IEnumerable<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
	}
}
