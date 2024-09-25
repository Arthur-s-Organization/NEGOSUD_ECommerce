namespace API.Models.DTOs
{
	public class SupplierOrderDTO
	{
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }
		public Guid SupplierId { get; set; }
	}
}
