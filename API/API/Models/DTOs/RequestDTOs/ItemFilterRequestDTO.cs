namespace API.Models.DTOs.RequestDTOs
{
	public class ItemFilterRequestDTO
	{
		public string? Name { get; set; }
		public Guid? SupplierId { get; set; }
		public float? MinPrice { get; set; }
		public float? MaxPrice { get; set; }
		public Guid? AlcoholFamilyId { get; set; }
		public string? Category { get; set; }
		public string? Year { get; set; }
		public string? OriginCountry { get; set; }

	}
}
