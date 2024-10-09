using System.Numerics;

namespace API.Models
{
	public class Item
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
		public Guid SupplierId { get; set; }
		public virtual Supplier Supplier { get; set; }

		
		public virtual IEnumerable<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
		public string? ItemImagePath { get; set; }


		public string? AlcoholVolume { get; set; } = null;
		public string? Year { get; set; } = null;
		public float? Capacity { get; set; } = null;
		public DateTime? ExpirationDate { get; set; } = null;
		public Guid? AlcoholFamilyId { get; set; } = null;
		public virtual AlcoholFamily AlcoholFamily { get; set; }
	}
}
