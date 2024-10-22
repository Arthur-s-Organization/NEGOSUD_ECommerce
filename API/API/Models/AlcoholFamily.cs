namespace API.Models
{
	public class AlcoholFamily
	{
		public Guid AlcoholFamilyId { get; set; }
		public string Name { get; set; }
		public virtual IEnumerable<Item> Items { get; set; }
	}
}
