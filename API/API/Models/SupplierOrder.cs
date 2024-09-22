namespace API.Models
{
	public class SupplierOrder
	{
		public Guid SupplierId { get; set; }
		public virtual Supplier Supplier { get; set; }
	}
}
