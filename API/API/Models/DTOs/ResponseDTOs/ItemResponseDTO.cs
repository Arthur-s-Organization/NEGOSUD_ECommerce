namespace API.Models.DTOs.ResponseDTOs
{
	public class ItemResponseDTO
	{
		public Guid ItemId { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int Stock { get; set; }
		public string Description { get; set; }
		public float Price
		{ get; set; }
		public string OriginCountry { get; set; }
		public DateTime CreationDate { get; set; }
		public int QuantitySold { get; set; }
		public virtual SupplierResponseDTO Supplier { get; set; }
		public virtual AlcoholFamilyResponseDTO AlcoholFamily { get; set; }
		public string AlcoholVolume { get; set; }
		public string Year { get; set; }
		public float Capacity { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string ItemImage { get; set; }

	}
}
