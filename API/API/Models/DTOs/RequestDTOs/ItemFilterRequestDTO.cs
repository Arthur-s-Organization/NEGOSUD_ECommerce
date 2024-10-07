namespace API.Models.DTOs.RequestDTOs
{
	public class ItemFilterRequestDTO
	{
		public Guid? SupplierId { get; set; }
		public float? MinPrice { get; set; }
		public float? MaxPrice { get; set; }
		public Guid? AlcoholFamilyId { get; set; }
	}
}
