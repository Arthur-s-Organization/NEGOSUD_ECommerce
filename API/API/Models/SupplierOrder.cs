namespace API.Models
{
	public class SupplierOrder : Order
	{
		public Guid SupplierId { get; set; }
		public virtual Supplier Supplier { get; set; }
	}
}
