namespace API.Models.DTOs.ResponseDTOs
{
	public class SupplierOrderResponseDTO
	{
		public Guid OrderID { get; set; }
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }
		public Guid SupplierId { get; set; }
		public virtual SupplierResponseDTO Supplier { get; set; }
		public virtual IEnumerable<OrderDetailResponseDTO> OrderDetails { get; set; }
	}
}
