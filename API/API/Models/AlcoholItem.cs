namespace API.Models
{
	public class AlcoholItem : Item
	{
		public string AlcoholVolume { get; set; }
		public string Year { get; set; }
		public float Capacity { get; set; }
		public DateTime ExpirationDate { get; set; }
		public Guid AlcoholFamilyId { get; set; }
		public AlcoholFamily AlcoholFamily { get; set; }
	}
}
