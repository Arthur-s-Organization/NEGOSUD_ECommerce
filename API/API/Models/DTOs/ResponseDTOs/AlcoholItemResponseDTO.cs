namespace API.Models.DTOs.ResponseDTOs
{
	public class AlcoholItemResponseDTO : ItemResponseDTO
	{
		
		public string AlcoholVolume { get; set; }
		public string Year { get; set; }
		public float Capacity { get; set; }
		public DateTime ExpirationDate { get; set; }
		public AlcoholFamilyResponseDTO AlcoholFamily { get; set; }
	}
}
