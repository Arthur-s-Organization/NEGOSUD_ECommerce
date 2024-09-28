namespace API.Models
{
	public class Supplier
	{
		public Guid SupplierId { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public virtual Address Address { get; set; }
		public virtual IEnumerable<Item> Items { get; set; } = new HashSet<Item>();
		public virtual IEnumerable<SupplierOrder> SupplierOrders { get; set; } = new List<SupplierOrder>();
	}
}
