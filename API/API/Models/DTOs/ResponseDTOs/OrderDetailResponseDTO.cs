namespace API.Models.DTOs.ResponseDTOs
{
	public class CustomerOrderDetailResponseDTO
	{
		public Guid ItemId { get; set; }
		public virtual ItemResponseDTO Item { get; set; }
		public int Quantity { get; set; }
	}
}
