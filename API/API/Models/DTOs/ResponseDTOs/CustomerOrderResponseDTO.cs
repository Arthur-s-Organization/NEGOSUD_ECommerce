namespace API.Models.DTOs.ResponseDTOs
{
	public class CustomerOrderResponseDTO
	{
		public Guid OrderID { get; set; }
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }
		//public Guid CustomerId { get; set; }

		public virtual CustomerResponseDTO Customer { get; set; }
		public virtual IEnumerable<OrderDetailResponseDTO> OrderDetails { get; set; }
	}
}
