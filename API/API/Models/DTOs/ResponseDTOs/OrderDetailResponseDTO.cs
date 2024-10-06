namespace API.Models.DTOs.ResponseDTOs
{
	public class OrderDetailResponseDTO
	{
		public Guid ItemId { get; set; }
		public virtual ItemResponseDTO Item { get; set; }
		public int Quantity { get; set; }
	}
}
