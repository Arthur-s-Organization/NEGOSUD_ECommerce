﻿namespace API.Models.DTOs
{
	public class AlcoholItemDTO
	{
		public string Name { get; set; }
		public int Stock { get; set; }
		public string Description { get; set; }
		public float Price
		{ get; set; }
		public string OriginCountry { get; set; }
		public Guid SupplierId { get; set; }



		public string AlcoholVolume { get; set; }
		public string Year { get; set; }
		public Guid AlcoholFamilyId { get; set; }
	}
}
